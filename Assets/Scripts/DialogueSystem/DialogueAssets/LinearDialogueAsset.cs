using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Linear Dialogue")]
public class LinearDialogueAsset : ScriptableObject
{
   public List<DialogueLine> lines = new(); 
}

[Serializable]
public class DialogueLine
{
    public string speaker;
    [TextArea(3, 10)] 
    public string text;
}
