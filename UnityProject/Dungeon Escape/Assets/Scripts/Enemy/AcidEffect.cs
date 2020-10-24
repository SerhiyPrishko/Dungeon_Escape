using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    void Update()
    {
        transform.Translate(Vector3.right * 3 *Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IDamageadle hit = other.GetComponent<IDamageadle>();

            if (hit != null)
            {
                hit.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}
