using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpawnerInteraction : MonoBehaviour, IInteractable
{
    private bool _isEmpty;
    private bool _isTimerStarted;
    
    [SerializeField] private Ingredient _ingredient;
    
    [SerializeField] private XRSocketInteractor _socketInteractor;
    // private SphereCollider _sphereCollider;
    
    private GameObject _gameObject = null;

    void Start()
    {
        // _sphereCollider = GetComponent<SphereCollider>();
        _socketInteractor = GetComponent<XRSocketInteractor>();
        Spawn();
    }

    void Update()
    {
        if (!_isEmpty) return;
        if (!_isTimerStarted) return;

    }

    private void Spawn()
    {
       _gameObject = Instantiate(_ingredient.prefab, transform.position, _ingredient.prefab.transform.rotation);
        _isEmpty = false;
        _isTimerStarted = false;
    }

    private void OnEnable()
    {
        _socketInteractor.selectEntered.AddListener(OnSelectEntered);
        _socketInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        _socketInteractor.selectEntered.AddListener(OnSelectEntered);
        _socketInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("[SPAWNER INTERACTION] Entered: " + args.interactableObject.transform.name);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("[SPAWNER INTERACTION] Exited: " + args.interactableObject.transform.name);
        
        // Enable gravity (XR Socket Interactor disables that property
        Rigidbody rb = _gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        
        // change status
        _isEmpty = true;
        _isTimerStarted = true;
    }

    public bool CanInteract()
    {
        return _isEmpty;
    }

    public bool Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }
}
