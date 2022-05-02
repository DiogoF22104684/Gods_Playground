using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DialogueSystem;

public class DialogueNameDisplay : MonoBehaviour
{
    [SerializeField]
    private DialogueDisplayHandler ddHandler;
    [SerializeField]
    private GameObject container;

    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        ddHandler.onStartLine += (NodeData data) =>
        {
            if (data.PresetName != "" && data.PresetName != "Default")
            {
                container.SetActive(true);
                textComponent.text = data.PresetName;
            }
            else
            {
                container.SetActive(false);
                textComponent.text = "";
            }
        };
    }

}
