using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class BeeBreadCrafter : MonoBehaviour, IXRSelectFilter
{
    [SerializeField] private Ingredient[] _recepie;
    [SerializeField] private Ingredient _food;

    [SerializeField] private bool[] _recepieStatus;

    [SerializeField] private XRSocketInteractor _socketInteractor;
    // [SerializeField] private XRInteractionManager _interactionManager;
    
    [SerializeField] private bool _shouldSpawn;

    public bool canProcess => isActiveAndEnabled;

    void Awake()
    {
        _socketInteractor = GetComponent<XRSocketInteractor>();
        // _interactionManager = _socketInteractor.interactionManager;
    }

    void Start()
    {
        _shouldSpawn = false;
        
        if (_recepie.Length <= 0) return;
      
        _recepieStatus = new bool[_recepie.Length];
        for(int i = 0; i < _recepieStatus.Length; i++)
            _recepieStatus[i] = false;
    }

    void Update()
    {
        if(!_shouldSpawn) return;
        
        SpawnFood();
    }

    private void SpawnFood()
    {
        Instantiate(_food.prefab, transform.position, _food.prefab.transform.rotation);
        _shouldSpawn = false;
    }
    
    private void OnEnable()
    {
        _socketInteractor.selectEntered.AddListener(OnSelectEntered);
        // _socketInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        _socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
        // _socketInteractor.selectExited.RemoveListener(OnSelectExited);
    }
    
    // private void OnSelectExited(SelectExitEventArgs args)
    // {
    //     Debug.Log("[CRAFTER INTERACTION] Exited: " + args.interactableObject.transform.name);
    // }

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        if (interactor.hasSelection)
            return false;

        var t = interactable?.transform;
        if (!t) return false;

        WrapperIngredient wrapper = t.GetComponent<WrapperIngredient>();
        if (!wrapper) return false;

        bool isValidId = false;
        int index = -1;
        for (int i = 0; i < _recepie.Length; i++)
        {
            if(wrapper.GetId() != _recepie[i].id) continue;
            isValidId = true;
            index = i;
            break;
        }

        if (!isValidId) return false;

        if (_recepieStatus[index]) return false;

        
        
        return true;
    }
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject obj = args.interactableObject.transform.gameObject;
        
        WrapperIngredient wrapper = obj.GetComponent<WrapperIngredient>();
        
        int id = wrapper.GetId();
        for (int i = 0; i < _recepie.Length; i++)
        {
            if (_recepie[i].id == id)
            {
                _recepieStatus[i] = true;
                // XRGrabInteractable interactable = obj.GetComponent<XRGrabInteractable>();
                // interactable.throwOnDetach = false;
            }
        }
        
        bool isRecepieComplete = true;
        for (int i = 0; i < _recepieStatus.Length; i++)
        {
            if(!_recepieStatus[i])
                isRecepieComplete = false;
        }
        
        _shouldSpawn = isRecepieComplete;
        
        
        StartCoroutine(ConsumeSelectedNextFrame(args.interactorObject, args.interactableObject));
        
        // Debug.Log("[CRAFTER INTERACTION] Entered: " + args.interactableObject.transform.name);
        // GameObject obj = args.interactableObject.transform.gameObject;
        // if (obj == null)
        // {
        //     Debug.Log("obj is null");
        //     return;
        // }
        // Debug.Log("[SOCKET CRAFTER] found object: " + obj.name);
        // WrapperIngredient wrapper = obj.GetComponent<WrapperIngredient>();
        //
        // if(wrapper != null)
        //     _interactionManager.SelectExit(_socketInteractor, obj.GetComponent<IXRSelectInteractable>());
        //
        // int id = wrapper.GetId();
        // for (int i = 0; i < _recepie.Length; i++)
        // {
        //     if (_recepie[i].id == id && _recepieStatus[i])
        //     {
        //         _interactionManager.SelectExit(_socketInteractor, obj.GetComponent<IXRSelectInteractable>());
        //         Destroy(obj);
        //         return;
        //     }
        //     if (_recepie[i].id == id && !_recepieStatus[i])
        //     {
        //         _recepieStatus[i] = true;
        //         _interactionManager.SelectExit(_socketInteractor, obj.GetComponent<IXRSelectInteractable>());
        //         Destroy(obj);
        //         return;
        //     }
        // }
        // _interactionManager.SelectExit(_socketInteractor, obj.GetComponent<IXRSelectInteractable>());
    }

    private IEnumerator ConsumeSelectedNextFrame(IXRSelectInteractor interactor, IXRSelectInteractable interactable) 
    {
        var comp = interactable as Component;
        if(!comp) yield break;
        
        var go = comp.gameObject;
        // XRGrabInteractable grabInteractable = go.GetComponent<XRGrabInteractable>();
        
        yield return null;
        
        if (interactor != null && interactor.IsSelecting(interactable)) 
            _socketInteractor.interactionManager.SelectExit(interactor, interactable);

        yield return new WaitForEndOfFrame();

        if(go) Destroy(go);
    }
}