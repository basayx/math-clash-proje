using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerCharacterView : MonoBehaviour
{
    public AttackerCharacterCode main;
    public List<ICanGetDamage> attackTargetsInView = new List<ICanGetDamage>();

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject != main.GetObjectInfo() && col.gameObject.tag != main.gameObject.tag)
        {
            ICanGetDamage canGetDamage = col.gameObject.GetComponent(typeof(ICanGetDamage)) as ICanGetDamage;
            if (canGetDamage != null && !attackTargetsInView.Contains(canGetDamage))
            {
                attackTargetsInView.Add(canGetDamage);
            }
        }
    }

    public float GetZRange()
    {
        return gameObject.GetComponent<BoxCollider>().size.z;
    }
}
