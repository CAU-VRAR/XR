using UnityEngine;
using Valve.VR; // SteamVR 네임스페이스
using Valve.VR.Extras; // SteamVR LaserPointer

public class LaserPointerHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer; // 레이저 포인터
    public SteamVR_Action_Boolean interactUIAction; // InteractUI 액션

    void Awake()
    {
        // LaserPointer의 이벤트에 함수 등록
        laserPointer.PointerClick += OnPointerClick;
    }

    void OnDestroy()
    {
        // 이벤트 등록 해제
        laserPointer.PointerClick -= OnPointerClick;
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        // 선택된 오브젝트가 캡슐인지 확인
        if (e.target.CompareTag("Capsule"))
        {
            // 캡슐의 색상 변경
            Renderer renderer = e.target.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red; // 색상을 빨간색으로 변경
            }
        }
    }
}
