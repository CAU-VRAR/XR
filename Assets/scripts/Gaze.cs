using UnityEngine;

public class GazeDestroy : MonoBehaviour
{
    public Transform HMD; // VR HMD의 Transform
    public float gazeTime = 3.0f; // 큐브를 응시해야 하는 시간
    private float timer = 0.0f; // 응시 시간 누적
    private bool isGazing = false; // 응시 중인지 여부

    void Update()
    {
        Ray ray = new Ray(HMD.position, HMD.forward); // HMD에서 나가는 레이저
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 큐브를 응시 중인지 확인
            if (hit.transform == transform)
            {
                isGazing = true;
                timer += Time.deltaTime;

                // 응시 시간이 설정된 시간을 초과하면 큐브 삭제
                if (timer >= gazeTime)
                {
                    Destroy(gameObject); // 큐브 삭제
                }
            }
            else
            {
                ResetGaze(); // 다른 오브젝트를 응시할 경우 초기화
            }
        }
        else
        {
            ResetGaze(); // 아무 것도 응시하지 않을 경우 초기화
        }
    }

    void ResetGaze()
    {
        isGazing = false;
        timer = 0.0f; // 타이머 초기화
    }
}
