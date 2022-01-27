using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    int right = 1;
    int Spaceshipright = 1;
    double dWholeAngle = 360.0f;
    public Transform trObject;
    public Transform Spaceship;
    Vector3 trPos;
    
    [Header("회전 속도")]
    public float speed = 30.0f; //How To Play
    public float Spaceshipspeed = 20.0f; //SpaceShip

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vRotatePos = trObject.transform.eulerAngles;
        Vector3 vSpaceShipRotatePos = Spaceship.transform.eulerAngles;

        if (dWholeAngle - 30 < vRotatePos.z)
            right = -1;
        if (vRotatePos.z <= 30)
            right = 1;

        if (dWholeAngle - 30 > vRotatePos.z)
            Spaceshipright = -1;
        if (vRotatePos.z >= 30)
            Spaceshipright = 1;


        //right = (vRotatePos.z >= 30 * right) ? (-1) : (1);
        //right = (Mathf.Abs(vRotatePos.z) >= 30 * right) ? (-1) : (1);

        //if (vRotatePos.z < dWholeAngle - 30)
        //{
        //    right = -1;
        //}
        //else if(vRotatePos.z >= 30)
        //{
        //    right =1;
        //right *= (vRotatePos.z >= 30) ? (-1) : (1);
        //right *= (vRotatePos.z < -30) ? (-1) : (1);
        trObject.transform.Rotate(0, 0, Time.deltaTime * speed* right);
        Spaceship.transform.Rotate(0, 0, Time.deltaTime * speed* Spaceshipright);
        //trObject.transform.rotation = (vRotatePos.z == dWholeAngle - 10) ?(QuaternionQuaternion.Slerp(from.rotation, to.rotation, Time.time * speed)):(Quaternion.Slerp(to.rotation, from.rotation, Time.time * speed;
        //trObject.rotation = (vRotatePos.z == dWholeAngle-10) ? Quaternion.AngleAxis(10, Vector3.forward) : Quaternion.AngleAxis(-10, Vector3.forward);
        
        Debug.Log("HowToPlay"+vRotatePos);
        Debug.Log("SpaceShip"+ vSpaceShipRotatePos);
    }
}
