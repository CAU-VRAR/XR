using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    private bool _isShowingTitle;
    private bool _isShowingTutorial;
    private bool _isGameOver;

    [SerializeField] private InputActionReference anyButtonPressAction;

    private void Start()
    {
        _isShowingTitle = true;
        _isShowingTutorial = false;
        _isGameOver = false;
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
    public void isGameOver()
    {
        _isGameOver = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        anyButtonPressAction.action.Enable();
        anyButtonPressAction.action.performed += OnSubmit;
    }
    
    private void OnDisable()
    {
        anyButtonPressAction.action.Disable();
        anyButtonPressAction.action.performed -= OnSubmit;
    }
    
    
    
    public void OnSubmit(InputAction.CallbackContext context)
    {
        Debug.Log("Submit");
        if (_isShowingTitle)
        {
            transform.GetChild(2).gameObject.SetActive(false);
            _isShowingTitle = false;
            _isShowingTutorial = true;
            _isGameOver = false;
        }
        else if (_isShowingTutorial)
        {
            _isShowingTitle = false;
            _isShowingTutorial = false;
            _isGameOver = false;
            transform.GetChild(1).gameObject.SetActive(false);
            GameManager.instance.Game();
            gameObject.SetActive(false);
        }
        else if (_isGameOver)
        {
            GameManager.instance.Retry();
            Start();
        }
    }
}
