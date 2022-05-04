using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player_Control : MonoBehaviour
{

    private CharacterController _controller;
    private Camera_Control _camera;
    [SerializeField]
    private float _speed = 5f;
    private float _strafeSpeed = 5f;
    private float _rotateSpeed = 1f;

    [SerializeField]
    private List<ScriptableBool> stopMovementBool;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _camera = GetComponent<Camera_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopMovementBool.Any(x => x.Value == true))
        {
            _controller.Move(Vector3.zero);
            return;
        }
        Rotate();
        Move();
        Strafe();
    }

    private void Rotate()
    {
        if(Input.GetAxis("Rotate") != 0)
            transform.Rotate(0, Input.GetAxis("Rotate") * _rotateSpeed, 0);
        else
        {
            transform.Rotate(0, 0, 0);
        }
    }

    private void Move()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = _speed * Input.GetAxis("Vertical");
        if(curSpeed != 0) 
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
