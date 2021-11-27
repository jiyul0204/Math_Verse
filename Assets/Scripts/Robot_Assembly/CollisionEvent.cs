using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionEvent : MonoBehaviour
{
    public static bool IsCollision = false;

    public int AnsNumber;
    public int MidNumber;
    public int FinNumber;
    public int DragNumber;

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.tag == "Ans")
        {
            IsCollision = true;
        }
        else
        {
            IsCollision = false;
        }
    }
}
