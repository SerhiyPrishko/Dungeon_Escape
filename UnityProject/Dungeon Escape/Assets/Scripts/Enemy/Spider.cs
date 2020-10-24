using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Spider : Enemy, IDamageadle
{
    public GameObject acidEffectPrefab;
    public int Health { get; set; }

    //Use for initialize
    public override void Init()
    {
        base.Init();
        health = base.health;
    }

    public override void Update()
    {
        
    }

    public void Damage()
    {
        if(isDead == true)
            return;
        
        Health--;
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            
            GameObject diamond = Instantiate(diamondPrefabs, transform.position,Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public override void Movement()
    {
        
    }

    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
