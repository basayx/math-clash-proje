using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DefenderTower : MonoBehaviour, ICanGetDamage
{
    public MatchManager matchManager;

    public float healthAmount = 100f;
    public Image healthBarImage;
    public TextMeshProUGUI healthBarText;

    public GameObject GetObjectInfo()
    {
        return gameObject;
    }

    public float GetCurrentHealthAmount()
    {
        return healthAmount;
    }

    public bool GetDamage(ICanAttack from, float damageAmount)
    {
        if (healthAmount <= 0f)
            return false;

        healthAmount -= damageAmount;
        healthBarImage.fillAmount = (float)healthAmount / 100f;
        healthBarText.text = ((int)healthAmount).ToString();

        if(healthAmount <= 0f)
        {
            Die();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Die()
    {
        matchManager.RoundEnd();
    }
}
