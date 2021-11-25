using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
    private Vector3 invisdefaultposition;
    private Vector3 ansdefaultposition;

    CDragnDrop DragnDrop;

    public Transform Invisialble;

    void Start()
    {
        invisdefaultposition = Invisialble.position;
        ansdefaultposition = DragnDrop.transform.position;
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if (o.gameObject.CompareTag("Ans"))
        {
            DragnDrop.transform.position = invisdefaultposition;
            Invisialble.position = ansdefaultposition;
        }
    }
}
