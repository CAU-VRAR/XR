using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GamaType
{
    Waiting,
    Projectile,
    Quiz
}

public class GameManager : MonoBehaviour
{
    const int STAGE_COUNT = 5;
    public static GameManager instance { get; private set; }

    public GameObject player;
    public QuizControl quizControl;
    
    public GamaType gameType = GamaType.Waiting;

    public UnityEvent onStartProjectile;
    public UnityEvent onEndProjectile;
    public UnityEvent onStartQuiz;
    public UnityEvent onEndQuiz;

    public float projectileTime = 20.0f;
    
    public int nowStage = 0;
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

    void Start()
    {
        quizControl.onCorrect.AddListener(OnClearQuiz);
        quizControl.onWrong.AddListener(OnFailQuiz);
        
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
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
    }

    void Retry()
    {
        nowStage = 0;
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
        nowStage = stage;
        ground.material = groundMaterials[stage];
    }
}
