using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float castDistance = 5f;
    [SerializeField] private InputActionProperty interactionButton;

    [SerializeField] private Transform cameraTransform;
    
    void OnEnable()
    {
        interactionButton.action.Enable();
        interactionButton.action.performed += TryInteract;

    }

    void OnDisable()
    {
        interactionButton.action.Disable();
    }
    
    private void TryInteract(InputAction.CallbackContext ctx)
    {
        // Debug.Log($"Trying interaction {ctx.action.name}");
        if (!CheckInteraction(out IInteractable interactable)) return;
        
        if (interactable.CanInteract())
            interactable.Interact(this);
    }

    private void Update()
    {
       // Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red, castDistance); 
    }

    private bool CheckInteraction(out IInteractable interactable)
    {
        interactable = null;
        
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        // Debug.DrawRay(ray.origin, ray.direction * castDistance, Color.red);
        Debug.Log("[INTERACTOR] Checking interaction");
        
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, castDistance)) return false;
        
        Debug.Log($"[INTERACTOR] Hit: {hitInfo.collider.gameObject.name}");
        
        interactable = hitInfo.collider.GetComponent<IInteractable>();
        return interactable != null;
    }
}

