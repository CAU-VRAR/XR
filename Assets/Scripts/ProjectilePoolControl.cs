using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolControl : MonoBehaviour
{
    public Queue<ProjectileControl> Projectiles = new();
    
    // Start is called before the first frame update
    void Start()
    {
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
