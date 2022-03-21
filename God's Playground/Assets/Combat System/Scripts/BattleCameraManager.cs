using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class BattleCameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera geral, focused;
    [SerializeField]
    private GameObject canvas, focusedCanvas;

    private GameObject target;

    [SerializeField]
    private float focusDistance;

    public void SwitchCameras(GameObject target = null)
    {

        if (target)
        {
            this.target = target;
            canvas.SetActive(false);
            focusedCanvas.SetActive(true);
            geral.enabled = false;
            FocusCamera();
        }
        else
        {
            canvas.SetActive(true);
            focusedCanvas.SetActive(false);
            geral.enabled = true;
        }
    }

    public GameObject ActiveCanvas => canvas.activeSelf == true ? canvas : focusedCanvas;

    private void FocusCamera()
    {
        focused.LookAt = target.transform;
        Vector3 forwardVect = target.transform.forward.normalized;
        focused.transform.position = new Vector3
            (target.transform.position.x, focused.transform.position.y, target.transform.position.z) 
            + forwardVect * focusDistance;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
