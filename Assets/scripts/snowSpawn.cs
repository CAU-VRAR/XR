using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

public class snowSpawn : MonoBehaviour
{

    public GameObject spherePrefab;
     public Transform spawnPoint;    // 구체 생성 위치
    public Transform target;        // 구체가 날아갈 목표 (보통 플레이어)
    public float spawnInterval = 2f; // 구체 생성 간격 (초)
    public float sphereSpeed = 5f;   // 구체 속도

    public void SpawnSphere(){
        Instantiate(spherePrefab,new Vector3(Random.Range(-10, 10), 10, Random.Range(-10, 10)), Quaternion.identity);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnSpheres());
    }

    IEnumerator SpawnSpheres()
    {
        while (true)
        {
            // 구체 생성
            GameObject sphere = Instantiate(spherePrefab, spawnPoint.position, Quaternion.identity);

            // 구체가 목표를 향해 날아가도록 설정
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb != null && target != null)
            {
                Vector3 direction = (target.position - spawnPoint.position).normalized;
                rb.linearVelocity = direction * sphereSpeed;
            }

            // 다음 구체 생성까지 대기
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
