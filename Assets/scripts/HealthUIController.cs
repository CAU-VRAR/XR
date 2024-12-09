using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위해 필요

public class HealthUIController : MonoBehaviour
{
    public TextMeshProUGUI healthText; // 체력 퍼센트를 표시할 텍스트
    public GameObject gameOverText; // 게임 오버 텍스트
    private Coroutine displayCoroutine;

    private void Start()
    {
        // 게임 시작 시 Game Over 텍스트를 숨김
        gameOverText.SetActive(false);
        healthText.text = ""; // 초기값은 빈 텍스트
    }

    // 체력 퍼센트를 업데이트하고 텍스트를 표시
    public void UpdateHealthPercentage(int currentHealth, int maxHealth)
    {
        float percentage = ((float)currentHealth / maxHealth) * 100.0f;
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine); // 기존 표시 코루틴을 중지
        }
        displayCoroutine = StartCoroutine(DisplayHealthText(Mathf.RoundToInt(percentage)));
    }

    // 체력을 화면 중앙에 표시 (1초 후 자동으로 사라짐)
    private IEnumerator DisplayHealthText(int percentage)
    {
        healthText.text = $"{percentage}%"; // 남은 체력을 표시
        yield return new WaitForSeconds(1.0f); // 1초 대기
        healthText.text = ""; // 텍스트 숨김
    }

    // 게임 오버 메시지 표시
    public void ShowGameOver()
    {
        gameOverText.SetActive(true);
    }
}
