using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    public string ID;
    public string accessName;
    public string classInfo;
    public int areaCount = 1;

    public bool triggered = false;

    public virtual void Triggered()
    {
        triggered = true;
    }
}
