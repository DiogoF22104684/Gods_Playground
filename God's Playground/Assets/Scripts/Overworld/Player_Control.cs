using UnityEngine;

public class Player_Control : MonoBehaviour
{

    private CharacterController _controller;

    private float _speed = 2f;
    private float _strafeSpeed = 5f;
    private float _rotateSpeed = 1f;



    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
        _controller.SimpleMove(forward * curSpeed);
    }

    private void Strafe()
    {
        Vector3 strafe = transform.TransformDirection(Vector3.left);
        float curSpeed = _strafeSpeed * Input.GetAxis("Horizontal");
        _controller.SimpleMove(strafe * curSpeed);
    }

}
