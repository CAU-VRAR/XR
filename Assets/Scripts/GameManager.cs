using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GamaType
{
    Waiting,
    Projectile,
    Quiz
}

public class GameManager : MonoBehaviour
{
    const int STAGE_COUNT = 5;
    private const int START_HEALTH = 100;
    
    public static GameManager instance { get; private set; }

    public GameObject player;
    public QuizControl quizControl;
    
    public GamaType gameType = GamaType.Waiting;

    public UnityEvent onStartProjectile;
    public UnityEvent onEndProjectile;
    public UnityEvent onStartQuiz;
    public UnityEvent onEndQuiz;

    public float projectileTime = 20.0f;
    
    public int nowStage = 1;

    public int playerHealth = START_HEALTH;
    public int projectileDamage = 20;
    [SerializeField] private Slider playerHealthSlider;

    public GameObject MainMenuCanvas;
    public GameObject GameOverCanvas;
    
    [SerializeField] private MeshRenderer ground;
    [SerializeField] List<Material> groundMaterials;
    private List<Color> _groundColors = new List<Color>
    {
        new Color(145,150,156),
        new Color(112,126,142),
        new Color(55,78,104),
        new Color(29,55,85),
        new Color(7,28,51)
    };
    
    private void Awake()
    {
        // 싱글톤 인스턴스가 이미 존재하면 파괴
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 현재 인스턴스를 싱글톤으로 설정
        instance = this;

        // 씬 전환 시 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);
    }

    public void Game()
    {
        Debug.Log("Game started."); // 디버깅 로그 추가
    	ChangeStage(0);
        GameOverCanvas.SetActive(false);
        quizControl.onCorrect.AddListener(OnClearQuiz);
        quizControl.onWrong.AddListener(OnFailQuiz);
        
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        ChangeStage(0);
        quizControl.onCorrect.AddListener(OnClearQuiz);
        quizControl.onWrong.AddListener(OnFailQuiz);
        yield return new WaitForSeconds(1.5f);

        ChangeStage(++nowStage);
        StartProjectile();
        yield return new WaitForSeconds(projectileTime);
        EndProjectile();
        yield return new WaitForSeconds(3.0f);
        StartQuiz();
    }

    void StartProjectile()
    {
        gameType = GamaType.Projectile;
        onStartProjectile.Invoke();
    }

    void EndProjectile()
    {
        gameType = GamaType.Waiting;
        onEndProjectile.Invoke();
    }
    
    void StartQuiz()
    {
        gameType = GamaType.Quiz;
        Debug.Log("STARTQUIZ");
        onStartQuiz.Invoke();
    }

    void OnClearQuiz()
    {
        gameType = GamaType.Waiting;
        Debug.Log("CLEARQUIZ");
        onEndQuiz.Invoke();
        StartCoroutine(StartGame());
    }

    void OnFailQuiz()
    {
        gameType = GamaType.Waiting;
        Debug.Log("FAILQUIZ");
        onEndQuiz.Invoke();
        GameOver();
    }

    void GameOver()
    {
        //TODO: 게임오버 처리 (눈덩이 날아오는거 멈추기)
        SoundManager.Instance.PlaySoundOneShot("GameOver",0.7f);
        GameOverCanvas.SetActive(true);
    }

    public void Retry()
    {
        nowStage = 0;
        playerHealth = START_HEALTH;
    }
    
    void ChangeStage(int stage)
    {
        Debug.Log($"Stage {stage}");
        //혹시 몰라서 범위 체크
        if (stage < 0 || stage >= STAGE_COUNT)
        {
            Debug.LogError("Invalid stage");
            return;
        }

        SoundManager.Instance.ChangeStageBGM();
        SoundManager.Instance.PlaySoundOneShot("NextStage",0.7f);
        projectileDamage = 10 + 5 * stage;
        ProjectileControl.projectileSpeed = 10 + 2 * stage;
        
        nowStage = stage;
        ground.material = groundMaterials[stage];
    }
    
    public void PlayerProjectileHit()
    {
        playerHealth = Mathf.Max(playerHealth-projectileDamage,0);
        
        //바로 감소시키면 너무 빨라서 서서히 감소
        //playerHealthSlider.value = playerHealth;
        StartCoroutine(DecreaseHealthSlider());
        
        if (playerHealth <= 0)
        {
            GameOver();
        }
        else
        {
            SoundManager.Instance.PlaySoundOneShot("Hit",0.5f);
        }
    }
    
    private float  healthDecreaseDuration = 0.5f;
    IEnumerator DecreaseHealthSlider()
    {
        float currentTime = 0;
        float startValue = playerHealthSlider.value;
        float targetValue = playerHealth;

        while (currentTime < healthDecreaseDuration)
        {
            playerHealthSlider.value = Mathf.Lerp(startValue, targetValue, currentTime / healthDecreaseDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        playerHealthSlider.value = targetValue;
    }

    public void StartPanelInput()
    {
        Debug.Log($"MainMenuCanvas active state before: {MainMenuCanvas.activeSelf}");
        MainMenuCanvas.SetActive(false);
        Debug.Log($"MainMenuCanvas active state after: {MainMenuCanvas.activeSelf}");
        Game(); //게임 시작
    }

    public void GameOverPanelInput()
    {
        GameOverCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
        Retry();
    }
}
