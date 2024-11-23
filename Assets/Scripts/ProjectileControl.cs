using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public ProjectilePoolControl pool;
    public Transform targetPos;
    public float projectileSpeed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        pool = GetComponentInParent<ProjectilePoolControl>();
        targetPos = GameManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisableThis();
        }
    }


    public void Launch()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
    }

    public void DisableThis()
    {
        gameObject.SetActive(false);
        pool.Projectiles.Enqueue(this);
    }
}
