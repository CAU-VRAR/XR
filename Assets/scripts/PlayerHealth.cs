using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // 플레이어의 최대 체력
    private int currentHealth; // 현재 체력
    public Slider healthSlider; // 체력을 표시할 UI 슬라이더

    void Start()
    {
        currentHealth = maxHealth; // 게임 시작 시 최대 체력으로 초기화
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    // 플레이어가 피해를 받을 때 호출
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // 체력을 감소
        Debug.Log("Player took damage: " + damage + " | Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0; // 체력이 0 이하로 내려가지 않도록 제한
            Die(); // 체력이 0 이하가 되면 게임 오버
        }
        UpdateHealthUI(); // 체력 감소 후 UI 업데이트
    }

    // 플레이어가 회복할 때 호출 - 체력을 회복할 수 있는 요소도 있으면 좋을까요?
    public void Heal(int amount)
    {
        currentHealth += amount; // 체력을 회복
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // 체력을 최대 체력으로 제한
        }
        Debug.Log("Player healed: " + amount + " | Current Health: " + currentHealth);
        UpdateHealthUI();
    }

     void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    // 체력이 0 이하가 되었을 때
    void Die()
    {
        Debug.Log("Player has died!");
        // 여기에서 게임 오버 처리 (예: 게임 종료, 재시작, UI 표시 등)
        // 예: GameManager.Instance.GameOver();
    }
}
