using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMouseControl();
    }

    private void CheckForMouseControl()
    {

        if ((Cursor.lockState == CursorLockMode.None && Cursor.visible == true) && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else if ((Cursor.lockState != CursorLockMode.None && Cursor.visible == false) && Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    
}
