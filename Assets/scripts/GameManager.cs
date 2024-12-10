using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore;
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
    private const int START_HEALTH = 150;
    
    public static GameManager instance { get; private set; }

    public GameObject player;
    public QuizControl quizControl;
    
    public GamaType gameType = GamaType.Waiting;

    public UnityEvent onStartProjectile;
    public UnityEvent onEndProjectile;
    public UnityEvent onStartQuiz;
    public UnityEvent onEndQuiz;

    public HealthUIController healthUIController; // HealthUIController 참조
    

    public float projectileTime = 30.0f;
    
    public int nowStage = 1;

    public int playerHealth = START_HEALTH;
    public int projectileDamage = 20;
    [SerializeField] private Slider playerHealthSlider;

    public GameObject MainMenuCanvas;
    public GameObject GameOverCanvas;
    public GameObject QuizCanvas; // 퀴즈 패널을 참조하기 위한 변수
    
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

    public void Start()
    {
        StartPanelInput(); //대기상태 // 메인 메뉴를 보여주며 대기
    }
    
    public void Game()
    {
    	ChangeStage(0);
        GameOverCanvas.SetActive(false);
        
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        Debug.Log("ASDFASDF");
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
        StartCoroutine(SpawnProjectiles()); //여기서 투사체 발사 시작됨
        gameType = GamaType.Projectile;
        onStartProjectile.Invoke();
    }

    IEnumerator SpawnProjectiles()
{
    float spawnInterval = 1.0f; // 투사체 발사 간격 (초)

    while (gameType == GamaType.Projectile)
    {
        // ProjectilePoolControl에서 하나의 투사체 가져오기
        var projectile = FindObjectOfType<ProjectilePoolControl>().GetOneProjectile();
        if (projectile != null)
        {
            projectile.Launch(); // 투사체 발사
        }

        // 다음 발사까지 대기
        yield return new WaitForSeconds(spawnInterval);
    }
    // 퀴즈 패널 활성화 
        if (QuizCanvas != null)
        {
            QuizCanvas.SetActive(true);
        }
}

    void EndProjectile()
    {
        StopCoroutine(SpawnProjectiles()); // 코루틴 종료
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

     public void GameOver()
    {
        
        SoundManager.Instance.PlaySoundOneShot("GameOver",0.7f);
        GameOverCanvas.SetActive(true);
        // 게임 오버 텍스트 표시
        healthUIController.ShowGameOver();
        GameOverPanelInput();
        
    }

    public void Retry()
    {
        nowStage = 0;
        playerHealth = START_HEALTH;
        MainMenuCanvas.SetActive(true);
    }
    
    public float spawnInterval = 1.0f; // 투사체 발사 간격
    public float initialProjectileSpeed = 5.0f; //속도
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
        spawnInterval = Mathf.Max(0.5f, 1.0f - 0.1f * stage); // 발사 간격 줄이기
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
        // 체력 UI 업데이트
        healthUIController.UpdateHealthPercentage(playerHealth, START_HEALTH);
        SoundManager.Instance.PlaySoundOneShot("Ouch",0.5f);
        
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
        MainMenuCanvas.SetActive(true); //대기상태 
        //유저가 클릭함 
    }

    public void OnStartButtonPressed()
{
    if (MainMenuCanvas != null)
    {
        Debug.Log("Disabling MainMenuCanvas.");
        MainMenuCanvas.SetActive(false);
    }
    else
    {
        Debug.LogWarning("MainMenuCanvas is null!");
    }
     // 메인 메뉴 비활성화
    Game(); // 게임 시작
}

    public void GameOverPanelInput()
    {
        GameOverCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
        Retry();
    }
}
