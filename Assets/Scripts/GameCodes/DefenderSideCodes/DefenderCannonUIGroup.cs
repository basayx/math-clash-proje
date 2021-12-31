using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DefenderCannonUIGroup : MonoBehaviour
{
    public Image cannonImage;
    public Sprite inIdleCannonSprite;
    public Sprite inAttackCannonSprite;

    public TextMeshProUGUI ammoText;
    //public Image ammoImage;
    //public Sprite[] ammoSprites;

    void Start()
    {
        ammoText.text = "Mermi:\n0";
        //ammoText.gameObject.SetActive(false);
        cannonImage.sprite = inIdleCannonSprite;
        //ammoImage.sprite = ammoSprites[0];
    }

    public void AmmoDecrease(int currentAmmo)
    {
        ammoText.text = "Mermi:\n" + currentAmmo.ToString();
        if (currentAmmo <= 0)
        {
            //ammoText.gameObject.SetActive(false);
            cannonImage.sprite = inIdleCannonSprite;
        }
        else
        {
            ammoText.gameObject.SetActive(true);
            cannonImage.sprite = inAttackCannonSprite;
        }

        //ammoImage.sprite = ammoSprites[currentAmmo];
    }

    public void AmmoIncrease(int currentAmmo)
    {
        ammoText.text = "Mermi:\n" + currentAmmo.ToString();
        ammoText.gameObject.SetActive(true);
        //ammoImage.sprite = ammoSprites[currentAmmo];
    }
}
