using UnityEngine;
using Cinemachine;

public class Camera_Control : MonoBehaviour
{

    [SerializeField]
    private GameObject _freelook;
    public GameObject cam => _freelook;

    [SerializeField]
    private ScriptableBool inMenus;

    private bool islocked;
    public bool IsLocked => islocked;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CheckForMouseControl();
    }

    private void CheckForMouseControl()
    {

        if (Input.GetMouseButton(1))
        {
            islocked = false;
        }
        else
        {
            islocked = true;
        }

        if (_freelook.activeSelf == false
            && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _freelook.SetActive(true);
            inMenus.Value = false;
        }
        else if (_freelook.activeSelf == true && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _freelook.SetActive(false);
            inMenus.Value = true;
        }

    }

    
}
