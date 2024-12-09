using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public ProjectilePoolControl pool;
    public Transform targetPos;
    public static float projectileSpeed = 10.0f;

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
        _rb.velocity = transform.forward * projectileSpeed;
    }

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
