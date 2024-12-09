using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // 플레이어의 최대 체력
    private int currentHealth; // 현재 체력
    public Slider healthSlider; // 체력 슬라이더 UI
    public Image redFlashImage; // 빨간색 깜빡임 이미지
    public TextMeshProUGUI healthPercentageText; // 체력 퍼센트를 표시할 텍스트
     public Image damageOverlay; // 빨간색 화면 오버레이 이미지
    public float damageOverlayDuration = 0.5f; // 빨간색 화면 지속 시간
    public Color damageColor = new Color(1, 0, 0, 0.5f); // 투명한 빨간색
    private Coroutine redFlashCoroutine; // 깜빡임 코루틴을 저장할 변수

    void Start()
    {
        currentHealth = maxHealth; // 게임 시작 시 최대 체력으로 초기화
        if (healthSlider != null)
        {
            healthPercentageText.text = "100%";
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        if (redFlashImage != null)
        {
            redFlashImage.gameObject.SetActive(false); // 깜빡임 이미지 비활성화
        }
        if (healthPercentageText != null)
        {
            healthPercentageText.text = ""; // 초기 텍스트 비우기
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
        if (redFlashImage != null)
        {
            if (redFlashCoroutine != null)
            {
                StopCoroutine(redFlashCoroutine); // 깜빡임 코루틴 중지
            }
            redFlashCoroutine = StartCoroutine(FlashRed()); // 깜빡임 코루틴 시작
        }
        // 체력 퍼센트 표시
        if (healthPercentageText != null)
        {
            StartCoroutine(ShowHealthPercentage());
        }

        if (currentHealth <= 0)
        {
            Die();
        }



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

        if (healthPercentageText != null)
        {
            float percentage = ((float)currentHealth / maxHealth) * 100;
            healthPercentageText.text = $"{Mathf.RoundToInt(percentage)}%";
        }
    }

     // 화면 빨간색 깜빡임 효과
    IEnumerator FlashRed()
    {
        if (redFlashImage != null)
        {
            redFlashImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f); // 0.5초 동안 표시
            redFlashImage.gameObject.SetActive(false);
        }
    }

    // 체력 퍼센트를 1초 동안 표시
    IEnumerator ShowHealthPercentage()
    {
        if (healthPercentageText != null)
        {
            float percentage = ((float)currentHealth / maxHealth) * 100f;
            healthPercentageText.text = Mathf.RoundToInt(percentage) + "%"; // 퍼센트 텍스트 표시
            yield return new WaitForSeconds(1.0f); // 1초 후 텍스트 숨기기
            healthPercentageText.text = "";
        }
    }

    // 체력이 0 이하가 되었을 때
    void Die()
    {
        Debug.Log("Player has died!");
        GameManager.instance.GameOver();
        // 여기에서 게임 오버 처리 (예: 게임 종료, 재시작, UI 표시 등)
        // 예: GameManager.Instance.GameOver();
    }
}
