using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _icePrefab;
    
    //0.5초와 1초 사이로 랜덤하게 아이스 생성
    private void Start()
    {
        StartCoroutine(SpawnIce());
    }
    
    private IEnumerator SpawnIce()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1f));
            Instantiate(_icePrefab, transform.position, Quaternion.identity);
        }
    }
}
