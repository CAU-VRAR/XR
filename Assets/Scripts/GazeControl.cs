using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeControl : MonoBehaviour
{
    public Camera mainCamera;       // 사용 중인 카메라
    public LayerMask interactableLayer; // 상호작용 가능한 레이어
    public float maxDistance = 10f; // Gaze의 최대 거리
    public float gazeDuration = 2.0f; // 응시 지속 시간

    private float _gazeTime = 0.0f;  // 현재 응시 시간
    private GameObject _lastGazedObject = null; // 마지막으로 감지된 객체
    private ProgressbarControl _pgbar = null;

    void Update()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            GameObject gazedObject = hit.collider.gameObject;
            
            if (gazedObject == _lastGazedObject)
            {
                _gazeTime += Time.deltaTime;
                if (_gazeTime >= gazeDuration)
                {
                    int answer = _lastGazedObject.name switch
                    {
                        "Answer1" => 1,
                        "Answer2" => 2,
                        "Answer3" => 3,
                        _ => 0
                    };
        
                    GameManager.instance.quizControl.CheckAnswer(answer);
                    _gazeTime = 0.0f;
                }
            }
            else
            {
                _pgbar = gazedObject.GetComponentInChildren<ProgressbarControl>();
                _gazeTime = 0.0f;
                _lastGazedObject = gazedObject;
            }
        }
        else
        {
            _gazeTime = 0.0f;
            _lastGazedObject = null;
        }
        
        _pgbar?.SetProgress(_gazeTime / gazeDuration);
    }
}
