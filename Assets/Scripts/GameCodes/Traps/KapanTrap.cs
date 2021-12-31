﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KapanTrap : TrapObject
{
    AttackerCharacterCode target;

    public Animator animator;
    public GameObject particleEffect;
    public float effectTime = 1f;

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<AttackerCharacterCode>() && !triggered)
        {
            target = col.gameObject.GetComponent<AttackerCharacterCode>();
            Triggered();
        }
    }

    public override void Triggered()
    {
        if (triggered || target == null)
            return;

        target.currentMovementSpeed = 0;
        target.animator.SetBool("running", false);

        animator.SetTrigger("effect");
        particleEffect.transform.parent = null;
        particleEffect.SetActive(true);
        Destroy(particleEffect, 5f);

        StartCoroutine(EffectDelay());
        
        base.Triggered();
    }

    IEnumerator EffectDelay()
    {
        float delay = effectTime;
        while(delay > 0f)
        {
            delay -= 1f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        target.currentMovementSpeed = target.defaultMovementSpeed;
        target.animator.SetBool("running", true);

        Destroy(gameObject, 0.5f);
    }
}
