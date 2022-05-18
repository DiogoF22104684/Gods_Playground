using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mover 
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float strafeSpeed;
    [SerializeField]
    private float rotateSpeed;

    GameObject entity;

 
    public Mover(GameObject entity, float speed, float strafeSpeed, float rotateSpeed)
    {
        this.speed = speed;
        this.strafeSpeed = strafeSpeed;
        this.rotateSpeed = rotateSpeed;
        this.entity = entity;
    }

    public Vector3 Translate()
    {
        Vector3 returnVect = Vector3.zero;

        Vector3 forward = entity.transform.forward;        
        Vector3 strafe = -entity.transform.right;
 
        float mvSpeed = speed * Input.GetAxis("Vertical");
        float stSpeed = strafeSpeed * Input.GetAxis("Horizontal");
        
        returnVect = (forward * mvSpeed + strafe * stSpeed);

        return returnVect;
    }

    public Vector3 Rotate()
    {
        Vector3 returnVect = Vector3.zero;
        return returnVect;
    }
}
