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
    public static GameManager instance { get; private set; }

    public GameObject player;
    
    public GamaType gameType;

    public UnityEvent onStartProjectile;
    public UnityEvent onEndProjectile;
    public UnityEvent onStartQuiz;
    public UnityEvent onEndQuiz;
    
    public int nowStage = 1;
    
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
}
