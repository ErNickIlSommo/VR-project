using System;
using UnityEditor;
using UnityEngine;

public class LinearDialogueManager : MonoBehaviour
{
    [SerializeField] private LinearDialogueUI ui;
    
    // [SerializeField] private GameObject locomotion;

    private LinearDialogueAsset _currentDialogue;
    private int _index = -1;
    
    public bool isRunning => _currentDialogue != null;

    public int Index { get => _index; set => _index = value; }

    public void StartDialogue(LinearDialogueAsset asset)
    {
        if (asset == null || asset.lines.Count == 0) return;

        _currentDialogue = asset;
        _index = -1;
        
        ui.Show(true);
        // locomotion.SetActive(false);
        Continue();
    }

    public void Continue()
    {
        if (!isRunning) return;

        _index++;

        if (_index >= _currentDialogue.lines.Count)
        {
            EndDialogue();
            return;
        }
        
        var line = _currentDialogue.lines[_index];
        ui.SetLine(line.speaker, line.text);
    }

    private void EndDialogue()
    {
        _currentDialogue = null;
        _index = -1;
        ui.Show(false);
        // locomotion.SetActive(true);
    }
}
