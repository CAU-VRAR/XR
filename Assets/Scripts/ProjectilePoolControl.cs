using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolControl : MonoBehaviour
{
    public Queue<ProjectileControl> Projectiles = new();
    public Transform target; // 플레이어(혹은 목표) Transform
    public float speed = 10f; // 눈덩이 속도
     private Rigidbody rb; // Rigidbody 컴포넌트
    
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();

        if (target != null)
        {
            // 플레이어 방향으로 속도 설정
            Vector3 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            Debug.LogWarning("Target is not set for Snowball!");
        }
    
        Projectiles = new Queue<ProjectileControl>(GetComponentsInChildren<ProjectileControl>(true));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public ProjectileControl GetOneProjectile()
    {
        var projectile = Projectiles.Dequeue();
        projectile.gameObject.SetActive(true);
        return projectile;
    }
}
