using UnityEngine;
using Valve.VR; // SteamVR 네임스페이스

public class RightTriggerInput : MonoBehaviour
{
    // SteamVR Input 액션
    public SteamVR_Action_Boolean triggerAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("grabpinch");
     // 트리거 액션
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.RightHand; // 오른손 컨트롤러

    void Update()
    {
        // 트리거 버튼이 눌렸는지 확인
        if (triggerAction.GetStateDown(handType))
        {
            Debug.Log("Right Trigger Pressed");
            OnTriggerPressed();
        }
    }

    void OnTriggerPressed()
    {
        // 트리거 버튼을 눌렀을 때 수행할 동작
        Debug.Log("Perform action for right trigger");
    }
}
