using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallCode : MonoBehaviour
{
    public Rigidbody rb;
    public DefenderCannonCode from;
    public ICanGetDamage target;
    [HideInInspector]
    public float damageAmount;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public Vector3 targetOffset;

    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.GetObjectInfo().transform.position + targetOffset, speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject != from.GetObjectInfo())
        {
            if (col.gameObject == target.GetObjectInfo())
            {
                target.GetDamage(from, damageAmount);

                Destroy(gameObject);
            }
        }
    }
}
