using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{

    [SerializeField] private LinearDialogueAsset dialogue;
    [SerializeField] private LinearDialogueManager manager;
    
    public bool CanInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("NPC Is starting interaction");

        if (manager.Index < 0 || !manager.isRunning)
            manager.StartDialogue(dialogue);
        else
            manager.Continue();
        
        return true;
    }
}
