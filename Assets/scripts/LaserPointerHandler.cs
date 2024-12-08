using UnityEngine;
using Valve.VR.Extras;

public class LaserPointerHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer; // 레이저 포인터 참조
    public GameObject canvas; // 닫을 Canvas

    void Awake()
    {
        // Laser Pointer 이벤트 등록
        laserPointer.PointerClick += OnPointerClick;
    }

    void OnDestroy()
    {
        // 이벤트 해제
        laserPointer.PointerClick -= OnPointerClick;
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        // X 버튼을 클릭했을 때만 Canvas 닫기
        if (e.target.name == "Xbutton")
        {
            Debug.Log("Xbutton clicked!");
            if (canvas != null)
            {
                canvas.SetActive(false); // Canvas 비활성화
            }
        }
    }
}
