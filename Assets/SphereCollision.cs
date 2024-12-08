using UnityEngine;

public class SphereCollision : MonoBehaviour
{
    public int damage = 10; // 구체가 줄 피해량
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by sphere!");
            Destroy(gameObject); // 2. 플레이어와충돌 시 구체 삭제
            // 플레이어와 충돌 시 1. 플레이어 체력 감소
            //collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(10);
             // PlayerHealth 컴포넌트를 가져와서 체력을 감소시킴
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // 바닥과 충돌 시
            Debug.Log("Hit the ground!");
            Destroy(gameObject); // 공을 제거
        }
        Destroy(gameObject); //구체 삭제
    }
}
