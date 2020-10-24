using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sceleton : Enemy, IDamageadle
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
        
        Debug.Log("Sceleton::Demage()");
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("inCombat", true);
        
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefabs, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }
}
