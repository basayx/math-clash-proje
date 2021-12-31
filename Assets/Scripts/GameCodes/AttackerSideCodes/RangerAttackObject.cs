using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttackObject : MonoBehaviour
{
    public Rigidbody rb;
    public AttackerCharacterCode from;
    public ICanGetDamage target;
    public float damageAmount;

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject != from.GetObjectInfo())
        {
            if (col.gameObject == target.GetObjectInfo())
            {
                target.GetDamage(from, damageAmount);

                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
