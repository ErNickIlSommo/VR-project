using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeleportationActivator : MonoBehaviour
{
    [SerializeField] private XRRayInteractor _teleportInteractor;
    [SerializeField] private InputActionProperty _teleportActivatorAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        _teleportInteractor.gameObject.SetActive(false);
        _teleportActivatorAction.action.performed += ToggleTeleport;
    }

    // Update is called once per frame
    void Update()
    {
        if(_teleportActivatorAction.action.WasReleasedThisFrame())
        {
            _teleportInteractor.gameObject.SetActive(false);
        }
        
    }

    private void ToggleTeleport(InputAction.CallbackContext obj)
    {
        _teleportInteractor.gameObject.SetActive(true);
    }
}
