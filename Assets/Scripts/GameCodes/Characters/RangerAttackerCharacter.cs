using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttackerCharacter : AttackerCharacterCode
{
    [Header("Ranger Properties")]
    public RangerAttackObject attackObjectPrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10f;

    public override void DoAttack(ICanGetDamage target)
    {
        currentAttackCooldown = attackCooldown;
        if (target.GetCurrentHealthAmount() <= 0f)
        {
            targetView.attackTargetsInView.Remove(target);
            MovementCase();
        }
        else
        {
            RangerAttackObject attackObject = Instantiate(attackObjectPrefab, projectileSpawnPoint.position, Quaternion.identity);
            attackObject.transform.parent = null;

            attackObject.from = this;
            attackObject.damageAmount = attackDamageAmount;
            attackObject.target = target;

            attackObject.rb.velocity = (new Vector3(projectileSpawnPoint.position.x, projectileSpawnPoint.position.y, target.GetObjectInfo().transform.position.z) - projectileSpawnPoint.position).normalized * projectileSpeed;

            currentAttackCooldown = attackCooldown;

            AttackCase();
        }
    }
}
