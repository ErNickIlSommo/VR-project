using UnityEngine;

public class SpawnerInteraction : MonoBehaviour, IInteractable
{
    private SphereCollider _sphereCollider;
    private bool _isEmpty;
    [SerializeField] private Ingredient _ingredient;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();    
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
