using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanGetDamage
{
    bool GetDamage(ICanAttack fromInstantiate, float damageAmount);
    float GetCurrentHealthAmount();
    GameObject GetObjectInfo();
    void Die();
}
