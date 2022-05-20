using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintMovement : MonoBehaviour
{
    [SerializeField]
    private bool isActive;

    [SerializeField]
    private Agent constraintObject;
    [SerializeField]
    private List<GameObject> movingObject;

    Mover mover;

    [Header("Movement Values")]
    [SerializeField]
    [ReadOnly]
    private Vector3 previousPos;
    [SerializeField]
    [ReadOnly]
    private Vector3 currentPos;

    private CharacterController controller;
    private Rigidbody rigi;



    // Start is called before the first frame update
    void Start()
    {
        
        rigi = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();

        mover = constraintObject.Mover;

    }

    private void FixedUpdate()
    {
        if (!isActive) return;
        if (constraintObject == null) return;
        //controller.SimpleMove(-mover.Translate().y(transform.position.y));
        //rigi.AddForce(-mover.Translate().y(0) * Time.deltaTime * 400);

        rigi.velocity = Vector3.zero;
        rigi.MovePosition(transform.position - mover.Translate() * Time.deltaTime);
        
        //controller.Move(-mover.Translate().y(transform.position.y) * Time.deltaTime);  
    }
}
