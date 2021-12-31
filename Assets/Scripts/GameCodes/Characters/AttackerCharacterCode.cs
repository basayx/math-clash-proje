using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Photon.Pun;

public class AttackerCharacterCode : MonoBehaviour, ICanGetDamage, ICanAttack
{
    public MatchManager matchManager;

    public string accessName;

    [HideInInspector]
    public string classInfo;

    public Animator animator;

    public int maxHealthAmount = 100;
    public float currentHealthAmount = 0f;

    public float attackCooldown = 1f;
    protected float currentAttackCooldown = 0f;

    public float attackDamageAmount = 10f;

    public float defaultMovementSpeed = 10f;
    [HideInInspector]
    public float currentMovementSpeed = 10f;

    public AttackerCharacterView targetView;

    [HideInInspector]
    public RoadLine targetRoad;

    public int starLevelValue;
    public GameObject[] starObjects;
    public List<Image> healthBars = new List<Image>();
    public GameObject canvasObject;

    public void SetReady()
    {
        transform.localScale += Vector3.one * (starLevelValue / 10f);
        defaultMovementSpeed *= starLevelValue + 1;
        attackDamageAmount *= starLevelValue + 1;
        maxHealthAmount *= starLevelValue + 1;
        currentMovementSpeed = defaultMovementSpeed;
        currentHealthAmount = maxHealthAmount;
        currentAttackCooldown = attackCooldown;
        for(int i = 0; i < starLevelValue; i++)
        {
            starObjects[i].SetActive(true);
        }

        int healthUnitAmount = maxHealthAmount / 10;
        for(int i = 1; i < healthUnitAmount; i++)
        {
            Image healthBar = Instantiate(healthBars[0].gameObject, healthBars[0].transform.parent).GetComponent<Image>();
            healthBars.Add(healthBar);
        }

        MovementCase();
    }

    public GameObject GetObjectInfo()
    {
        return gameObject;
    }

    public float GetCurrentHealthAmount()
    {
        return currentHealthAmount;
    }

    public bool GetDamage(ICanAttack from, float damageAmount)
    {
        if (currentHealthAmount <= 0f)
            return false;

        currentHealthAmount -= damageAmount;

        float healthBarValue = currentHealthAmount / 10f;
        for (int i = 0; i < healthBars.Count; i++)
        {
            healthBars[i].DOKill();
            healthBars[i].DOFillAmount(Mathf.Clamp(healthBarValue, 0f, 1f), 0.5f);
            healthBarValue -= 1f;
        }

        if (currentHealthAmount <= 0f)
        {
            Die();
            return true;
        }

        return false;
    }

    public void Die()
    {
        animator.SetTrigger("die");
        StopAllCoroutines();
        targetRoad.defenderCannon.attackersOnTheLine.Remove(this);

        if (matchManager.attackerGameplayManager.gameObject.activeSelf)
        {
            matchManager.attackerGameplayManager.ASpawnedCharacterDied(this);
        }
        else if (matchManager.enemyAttackerManager.enemyAttackerController.gameObject.activeSelf)
        {
            matchManager.enemyAttackerManager.enemyAttackerController.ASpawnedCharacterDied(this);
        }

        canvasObject.SetActive(false);
        Destroy(gameObject, 4f);
    }

    public void MovementCase()
    {
        animator.SetBool("running", true);
        StartCoroutine(Moving());

        IEnumerator Moving()
        {
            while (true)
            {
                transform.position += transform.forward * currentMovementSpeed * Time.deltaTime;

                if(targetView.attackTargetsInView.Count > 0)
                {
                    break;
                }

                yield return new WaitForEndOfFrame();
            }

            animator.SetBool("running", false);
            if (targetView.attackTargetsInView.Count > 0)
                AttackCase();
            else
                Debug.Log("HATA?- Hiç saldırılabilir birim bulunamadı.");
        }
    }

    public void AttackCase()
    {
        animator.SetTrigger("attack");
        StartCoroutine(Attacking());

        IEnumerator Attacking()
        {
            while (true)
            {
                if (currentHealthAmount <= 0f)
                    break;
                else if (targetView.attackTargetsInView.Count <= 0 || Mathf.Abs(transform.position.z - targetView.attackTargetsInView[0].GetObjectInfo().transform.position.z) > targetView.GetZRange())
                {
                    MovementCase();
                    break;
                }

                if(currentAttackCooldown > 0f)
                {
                    currentAttackCooldown -= 1f * Time.deltaTime;
                }
                else
                {
                    if (targetView.attackTargetsInView.Count > 0)
                    {
                        DoAttack(targetView.attackTargetsInView[0]);
                    }
                    break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }

    public virtual void DoAttack(ICanGetDamage target)
    {
        currentAttackCooldown = attackCooldown;
        if (target.GetDamage(this, attackDamageAmount))
        {
            targetView.attackTargetsInView.Remove(target);
            MovementCase();
        }
        else
        {
            AttackCase();
        }
    }

    [PunRPC]
    public void SetVariables(string classInfo, int roadNo, int starLevelValue)
    {
        if(gameObject.tag == "Untagged")
            gameObject.tag = "EnemysCharacter";
        matchManager = GameObject.FindObjectOfType<MatchManager>();
        this.classInfo = classInfo;
        if (gameObject.tag == "EnemysCharacter")
            targetRoad = matchManager.defenderGameplayManager.roadLines[roadNo];
        else
            targetRoad = matchManager.enemyDefenderManager.roadLines[roadNo];
        this.starLevelValue = starLevelValue;
        matchManager.AttackerSpawnedANewCharacter(this, targetRoad);
        SetReady();
    }
}
