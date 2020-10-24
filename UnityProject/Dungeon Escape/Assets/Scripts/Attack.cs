using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Name: " + other.name);

        IDamageadle hit = other.GetComponent<IDamageadle>();

        if (hit != null)
        {
            if (_canDamage == true)
            {
                hit.Damage();
                _canDamage = false;
                StartCoroutine(ResetDamage());
            }
        }
    }

    IEnumerator ResetDamage()
    {
        yield return  new WaitForSeconds(.5f);
        _canDamage = true;
    }
}
