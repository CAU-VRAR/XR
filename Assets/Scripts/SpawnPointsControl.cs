using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointsControl : MonoBehaviour
{
    public List<Transform> spawnPoints = new();
    public ProjectilePoolControl snowProjectilePool = new();
    public ProjectilePoolControl iceProjectilePool = new();

    public float iceProbability = 0.0f;
    public float shootCoolTime = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new List<Transform>(GetComponentsInChildren<Transform>())
            .Where(t => t != transform)
            .ToList();
        
        GameManager.instance.onStartProjectile.AddListener(() => { StartCoroutine(ShootProjectile()); });
        GameManager.instance.onEndProjectile.AddListener(StopAllCoroutines);
    }

    private IEnumerator ShootProjectile()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootCoolTime);
            
            int spawnPointIndex = Random.Range(0, spawnPoints.Count);

            var projectile = Random.value < iceProbability ? iceProjectilePool.GetOneProjectile() : snowProjectilePool.GetOneProjectile();
            projectile.transform.position = spawnPoints[spawnPointIndex].position;
            projectile.transform.LookAt(GameManager.instance.transform);
            projectile.Launch();
        }
    }
}
