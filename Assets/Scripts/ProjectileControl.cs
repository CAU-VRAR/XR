using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public ProjectilePoolControl pool;
    public Transform targetPos;
    public static float projectileSpeed = 7.0f;

    private Rigidbody _rb;
    
    public bool grabbed = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        pool = GetComponentInParent<ProjectilePoolControl>();
        _rb = GetComponent<Rigidbody>();
        grabbed = false;
    }

    private void Start()
    {
        targetPos = GameManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.PlayerProjectileHit();
            DisableThis();
        }
        
        else if (other.gameObject.CompareTag("Ice") && gameObject.CompareTag("Snow"))
        {
            SoundManager.Instance.PlaySoundOneShot("Snow",0.6f);
            other.transform.GetComponent<ProjectileControl>().DisableThis();
            DisableThis();
        }
    }


    public void Launch()
    {
        if(grabbed){
            return;
        }
        if (targetPos != null)
    {
        // 목표 방향 계산
        Vector3 direction = (targetPos.position - transform.position).normalized;
        // Rigidbody를 사용해 목표 방향으로 발사
        _rb.linearVelocity = direction * projectileSpeed;
    }
    else
    {
        Debug.LogWarning("Target position is not set for this projectile!");
    }
}
        //_rb.linearVelocity = transform.forward * projectileSpeed;
        //투사체가 플레이어 위치를 추적하도록 설정
    

    public void Grabbed()
    {
        grabbed = true;
    }

    public void DisableThis()
    {
        gameObject.SetActive(false);
        pool.Projectiles.Enqueue(this);
    }
}
