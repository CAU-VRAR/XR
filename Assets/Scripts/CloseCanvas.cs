using UnityEngine;
using UnityEngine.UI; // For Button
using Valve.VR; // For SteamVR input

namespace Valve.VR.InteractionSystem.Sample
{
public class CloseCanvas : MonoBehaviour
{
    public GameObject canvas; 
    public SteamVR_Action_Boolean triggerAction; // SteamVR Input Action for the trigger
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any; // Controller type (default: any hand)

    private Button closeButton;

    void Start()
    {
        // X 버튼 설정
        closeButton = GetComponent<Button>();
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HideCanvas);
        }
    }

    void Update()
    {
        // Trigger 버튼을 눌렀을 때 버튼이 활성화 상태인지 확인
        if (triggerAction.GetStateDown(handType))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == closeButton.gameObject)
                {
                    HideCanvas();
                }
            }
        }
    }

    void HideCanvas()
    {
        if (canvas != null)
        {
            canvas.SetActive(false); // Canvas를 비활성화
            Debug.Log("Canvas is now hidden.");
        }
    }
}
}
