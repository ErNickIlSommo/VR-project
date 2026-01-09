using System;
using TMPro;
using UnityEngine;

public class LinearDialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text bodyText;
    
    public void Show(bool show) => root.SetActive(show);

    public void SetLine(string speaker, string text)
    {
        speakerText.text = speaker;
        bodyText.text = text;
    }

    private void Awake()
    {
        root.SetActive(false);
    }
}
