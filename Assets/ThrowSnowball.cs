using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ThrowSnowball : MonoBehaviour
{
    /*private Rigidbody rigidbody;
    XRGrabInteractable grabInteractable;
    
    public float velocityThreshold = 1.0f;
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        /#1#/ Retrieve the interactor object (e.g., controller)
        if (args.interactorObject is XRBaseInputInteractor controllerInteractor)
        {
            var controller = controllerInteractor.xrController;

            // Get velocity and angular velocity from the controller
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity) &&
                controller.inputDevice.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out Vector3 angularVelocity))
            {
                // Check if the velocity meets the threshold
                if (velocity.magnitude >= velocityThreshold)
                {
                    // Apply the velocity and angular velocity to the object
                    rigidbody.isKinematic = false; // Enable physics
                    rigidbody.velocity = velocity;
                    rigidbody.angularVelocity = angularVelocity;

                    Debug.Log($"Object thrown with velocity: {velocity}, angular velocity: {angularVelocity}");
                }
                else
                {
                    Debug.Log("Throw skipped: Velocity below threshold.");
                }
            }
        }#1#
    }*/
}
