using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MossGiant : Enemy, IDamageadle
{
    public int Health { get; set; }

    //Use for initialize
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();
        
       
    }

    public void Damage()
    {
        if(isDead == true)
            return;
        
        Debug.Log("MossGiant::Demage()");
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("inCombat", true);
        
        if (Health < 1)
        {
            
            isDead = true;
            
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefabs, transform.position,Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
}