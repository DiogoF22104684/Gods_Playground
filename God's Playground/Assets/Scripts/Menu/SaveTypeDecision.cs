using UnityEngine;

public class SaveTypeDecision : MonoBehaviour
{
    public Saves type;
    public void SelectSave()
    {
        PlayerPrefs.SetInt("saveType", (int)type);
    }
}