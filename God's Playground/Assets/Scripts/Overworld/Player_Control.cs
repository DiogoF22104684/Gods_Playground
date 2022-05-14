using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player_Control : Agent
{

    private CharacterController _controller;
    private Camera_Control _camera;
    private Rigidbody rigi;


    private float _speed = 5f;
    private float _strafeSpeed = 5f;
    private float _rotateSpeed = 1f;

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

        //_controller.SimpleMove(mover.Translate());
        //rigi.velocity = Vector3.zero;
        //rigi.MovePosition(transform.position + mover.Translate() * Time.deltaTime);
        //Move();
        //Strafe();

        if (Input.GetAxis("Vertical") != 0 && _camera.IsLocked)
        {
            transform.LookAt(_camera.cam.transform.position.y(transform.position.y));
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }

    private void Move()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = _speed * Input.GetAxis("Vertical");
        if(curSpeed != 0 && _camera.IsLocked) 
        {
            transform.LookAt(_camera.cam.transform.position.y(transform.position.y));
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
        _controller.SimpleMove(forward * curSpeed);
    }

    private void Strafe()
    {
        Vector3 strafe = transform.TransformDirection(Vector3.left);
        float curSpeed = _strafeSpeed * Input.GetAxis("Horizontal");
        _controller.SimpleMove(strafe * curSpeed);
    }

}
