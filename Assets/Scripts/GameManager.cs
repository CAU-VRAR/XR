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
    
    public GamaType gameType;

    public UnityEvent onStartProjectile;
    public UnityEvent onEndProjectile;
    public UnityEvent onStartQuiz;
    public UnityEvent onEndQuiz;
    
    public int nowStage = 1;
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
        ChangeStage(0);
        StartCoroutine(StartGame());
    }
    
    public void StartProjectile()
    {
        onStartProjectile.Invoke();
    }

    public void EndProjectile()
    {
        onEndProjectile.Invoke();
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.5f);

        Invoke(nameof(EndGame), 10.0f);
        StartProjectile();
        
        //스테이지 변경 테스트
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Stage 2");
        ChangeStage(1);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Stage 3");
        ChangeStage(2);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Stage 4");
        ChangeStage(3);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Stage 5");
        ChangeStage(4);
    }

    void EndGame()
    {
        EndProjectile();
        StartQuiz();
    }

    void StartQuiz()
    {
        Debug.Log("STARTQUIZ");
        onStartQuiz.Invoke();
    }

    
    void ChangeStage(int stage)
    {
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
