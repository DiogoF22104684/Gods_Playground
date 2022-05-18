using UnityEditor;
using UnityEngine;
using DialogueSystem;
using DialogueSystem.Editor;
using System;

//https://answers.unity.com/questions/634110/associate-my-custom-asset-with-a-custom-editorwind.html
public class OnOpenDialogueHandler : MonoBehaviour
{
    
    [UnityEditor.Callbacks.OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        string assetPath = AssetDatabase.GetAssetPath(instanceID);
        DialogueScript scriptableObject = AssetDatabase.LoadAssetAtPath<DialogueScript>(assetPath);
        if (scriptableObject != null)
        {
            DialogueGraph.OpenDialogueGraphWindow(scriptableObject, path: TrimPath(assetPath));
            return true;
        }
        return false; //let unity open it.
    }

    private static string TrimPath(string assetPath)
    {
        string returnString = assetPath;
        for (int i = returnString.Length -1; i >= 0; i--)
        {
            returnString = returnString.Substring(0, i);
            
            if (returnString[i -1] == '/')
            {  
                break;
            }
        }

        return returnString.Substring(0,returnString.Length - 1);
    }
}