using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SpawnerHandler : MonoBehaviour
{
    private bool _isEmpty;
    
    [SerializeField] float _timer;
    
    [SerializeField] private Ingredient _ingredient;
    
    [SerializeField] private XRSocketInteractor _socketInteractor;
    // private SphereCollider _sphereCollider;
    
    [SerializeField] private GameObject _gameObject = null;

    void Start()
    {
        // _sphereCollider = GetComponent<SphereCollider>();
        _socketInteractor = GetComponent<XRSocketInteractor>();
        Spawn();
    }

    void Update()
    {
        if (!_isEmpty) return;
        
        _timer -= Time.deltaTime;

        if (_timer <= 0.0f)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        _gameObject = Instantiate(_ingredient.prefab, transform.position, _ingredient.prefab.transform.rotation);
        _isEmpty = false;
        _timer = _ingredient.cooldown;
        _socketInteractor.enabled = true;
    }

    private void Detach()
    {
        // Enable gravity (XR Socket Interactor disables that property
        var rb = _gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        // change status
        _isEmpty = true;
        _gameObject = null;
        _socketInteractor.enabled = false;
    }

    private void OnEnable()
    {
        _socketInteractor.selectEntered.AddListener(OnSelectEntered);
        _socketInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        _socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
        _socketInteractor.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Debug.Log("[SPAWNER INTERACTION] Entered: " + args.interactableObject.transform.name);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Debug.Log("[SPAWNER INTERACTION] Exited: " + args.interactableObject.transform.name);
        Detach();
    }
}
