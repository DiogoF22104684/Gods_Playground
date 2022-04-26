using UnityEngine;
using Cinemachine;

public class Camera_Control : MonoBehaviour
{

    [SerializeField]
    private GameObject _freelook;

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

        if  ((Cursor.lockState == CursorLockMode.None && Cursor.visible == true) 
            && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _freelook.SetActive(true);
        }

        else if ((Cursor.lockState != CursorLockMode.None && Cursor.visible == false) && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _freelook.SetActive(false);
        }

    }

    
}
