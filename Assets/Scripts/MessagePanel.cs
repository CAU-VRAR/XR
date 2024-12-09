using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem; // For InputSystem if you're using SteamVR or OpenXR

public class MessagePanel : MonoBehaviour
{
    public List<string> messages; // List of messages to display
    public TMP_Text messageText; // TextMeshPro component to display the messages
    public GameObject panel; // Panel GameObject (Canvas)
    public InputActionProperty rightTriggerAction; // Input Action for the right controller trigger

    private int currentMessageIndex = 0;

    void Start()
    {
        if (messages.Count > 0)
        {
            messageText.text = messages[currentMessageIndex]; // Display the first message
        }
    }

    void Update()
    {
        // Check if the right controller's trigger is pressed
        if (rightTriggerAction.action.WasPressedThisFrame())
        {
            ShowNextMessage();
        }
    }

    void ShowNextMessage()
    {
        if (currentMessageIndex < messages.Count - 1)
        {
            currentMessageIndex++;
            messageText.text = messages[currentMessageIndex];
        }
        else
        {
            // Hide the panel when all messages are displayed
            panel.SetActive(false);
        }
    }
}
