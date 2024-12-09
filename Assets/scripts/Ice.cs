using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    
    [SerializeField] private float _speed = 3f;
    //아이스가 생성되면 현재 position 기준 (0,0,0) position 방향으로 회전
    private void Start()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Vector3.zero);
    }

    //아이스가 생성되면 앞으로 이동
    private void Update()
    {
        transform.Translate( _speed * Time.deltaTime * Vector3.forward);
    }
    
    
    //아이스가 다른 오브젝트와 충돌하면 삭제
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
