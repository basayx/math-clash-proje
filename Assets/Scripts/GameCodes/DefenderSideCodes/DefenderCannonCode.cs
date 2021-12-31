using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DefenderCannonCode : MonoBehaviour, ICanAttack
{
    public string accessName;

    public string classInfo;
    public bool targetSearch = false;

    public int ammoAmount = 0;
    public int maxAmmoAmount = 3;
    public DefenderCannonUIGroup cannonUIGroup;

    public float attackCooldown = 1f;
    [HideInInspector]
    public float currentAttackCooldown = 0f;
    public float damageAmount = 10f;

    public float maxZDistance;
    public List<AttackerCharacterCode> attackersOnTheLine = new List<AttackerCharacterCode>();

    public CannonBallCode cannonBallPrefab;
    public Transform cannonBallSpawnPoint;
    public float cannonPorjectileSpeed;

    public int starLevelValue = -1;
    public GameObject[] starObjects;

    private void Start()
    {
         if(DataManager.Instance.enemyIsNonPlayer == 0 && MainNetworkManager.Instance.matchManager.roundNo != 0)
            DontDestroyOnLoad(this.gameObject);
    }

    public void SetReady()
    {
        transform.localScale += Vector3.one * (starLevelValue / 10f);
        damageAmount *= starLevelValue + 1;
        maxZDistance *= starLevelValue + 1;
        maxAmmoAmount *= starLevelValue + 1;
        attackCooldown /= starLevelValue + 1;

        currentAttackCooldown = attackCooldown;
        for (int i = 0; i < starLevelValue; i++)
        {
            starObjects[i].SetActive(true);
        }
    }

    void Update()
    {
        if (currentAttackCooldown > 0f)
        {
            currentAttackCooldown -= 1f * Time.deltaTime;
        }
        else if (currentAttackCooldown < 0f)
        {
            if(targetSearch)
                StartCoroutine(WaitingInAttackCase());
            currentAttackCooldown = 0f;
        }
    }

    public GameObject GetObjectInfo()
    {
        return gameObject;
    }

    IEnumerator WaitingInAttackCase()
    {
        bool searching = true;
        AttackerCharacterCode target = null;
        while (searching)
        {
            foreach (AttackerCharacterCode attacker in attackersOnTheLine)
            {
                if (Mathf.Abs(transform.position.z - attacker.transform.position.z) < maxZDistance)
                {
                    target = attacker;
                    searching = false;
                    break;
                }
            }

            yield return new WaitForEndOfFrame();
        }

        if (target != null)
            DoAttack(target);
    }

    public AttackerCharacterCode GetNearestAttackerOnTheLine()
    {
        AttackerCharacterCode nearestAttacker = null;
        float distance = 0f;

        for(int i = 0; i < attackersOnTheLine.Count; i++)
        {
            float newDistance = Mathf.Abs(attackersOnTheLine[i].transform.position.z - transform.position.z);
            if (nearestAttacker == null || newDistance < distance)
            {
                nearestAttacker = attackersOnTheLine[i];
                distance = newDistance;
            }
        }

        return nearestAttacker;
    }

    public void DoAttack(ICanGetDamage targetInstantiate)
    {
        DoAttack(targetInstantiate as AttackerCharacterCode);

        void DoAttack(AttackerCharacterCode target)
        {
            if(ammoAmount > 0)
            {
                CannonBallCode cannonBall = Instantiate(cannonBallPrefab, cannonBallSpawnPoint.position, cannonBallSpawnPoint.rotation);
                cannonBall.transform.parent = null;

                cannonBall.from = this;
                cannonBall.damageAmount = damageAmount;
                cannonBall.speed = cannonPorjectileSpeed;
                cannonBall.target = target;

                //cannonBall.rb.velocity = ((target.transform.position + new Vector3(0f, target.animator.transform.localScale.y, 0f)) - cannonBallSpawnPoint.position).normalized * cannonPorjectileSpeed;

                ammoAmount--;
                if (cannonUIGroup != null)
                    cannonUIGroup.AmmoDecrease(ammoAmount);
            }

            currentAttackCooldown = attackCooldown;
        }
    }

    [PunRPC]
    public void AmmoIncreased()
    {
        ammoAmount++;
    }

    [PunRPC]
    public void SetTag(string tag)
    {
        gameObject.tag = tag;
    }
}
