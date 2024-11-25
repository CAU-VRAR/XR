using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, ISubmitHandler
{
    private bool _isShowingTitle = true;


    public void OnSubmit(BaseEventData eventData)
    {
        if (_isShowingTitle)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            _isShowingTitle = false;
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    
    
}
