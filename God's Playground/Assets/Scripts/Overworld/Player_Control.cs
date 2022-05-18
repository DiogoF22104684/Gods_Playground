using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player_Control : Agent, ISavable
{

    private CharacterController _controller;
    private Camera_Control _camera;
    private Rigidbody rigi;


    [SerializeField]
    private List<ScriptableBool> stopMovementBool;

 
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        mover = new Mover(gameObject,5,5,1);
        _controller = GetComponent<CharacterController>();
        _camera = GetComponent<Camera_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopMovementBool.Any(x => x.Value == true))
        {
            //_controller.Move(Vector3.zero);
            return;
        }

        if (Input.GetAxis("Vertical") != 0 && _camera.IsLocked)
        {
            transform.LookAt(_camera.cam.transform.position.y(transform.position.y));
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }



    #region Save/Load

    private int iD;

    public int ID { get => iD; set => iD = value; }

    public string GetData()
    {
        return "";
    }

    public void LoadData(string data)
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
