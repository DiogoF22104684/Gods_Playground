using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstraintMovement : MonoBehaviour
{
    [SerializeField]
    private bool isActive;

    [SerializeField]
    private Agent constraintObject;
   
    [SerializeField]
    private List<ScriptableBool> movementBool;


    [Header("Movement Values")]
    [SerializeField]
    [ReadOnly]
    private Vector3 previousPos;
    [SerializeField]
    [ReadOnly]
    private Vector3 currentPos;

    private CharacterController controller;
    private Rigidbody rigi;
    private Mover mover;

    // Start is called before the first frame update
    void Start()
    {
        
        rigi = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();

        mover = constraintObject.Mover;

    }

    private void FixedUpdate()
    {
        if (movementBool.Any(x => x.Value == true))
        {
            rigi.velocity = Vector3.zero;
            return;
        }

        if (!isActive) return;
        if (constraintObject == null) return;
        mover = constraintObject.Mover;
        rigi.velocity = Vector3.zero;
        rigi.MovePosition(transform.position - mover.Translate() * Time.deltaTime);
    }
}
