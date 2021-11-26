using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    public bool IsCollision = false;

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.tag == "Ans")
        {
            IsCollision = true;
        }
    }
}
