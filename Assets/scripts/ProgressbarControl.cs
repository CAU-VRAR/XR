using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressbarControl : MonoBehaviour
{
    private float _originScaleX;
    private float _originScaleY;
    private float _originScaleZ;
    
    // Start is called before the first frame update
    void Start()
    {
        _originScaleX = transform.localScale.x;
        _originScaleY = transform.localScale.y;
        _originScaleZ = transform.localScale.z;
        SetProgress(0.0f);
    }

    public void SetProgress(float progress)
    {
        float newScaleX = progress * _originScaleX;
        float newPositionX = 0.5f * (progress - 1);
        
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(newPositionX, transform.localPosition.y, transform.localPosition.z);
    }
}
