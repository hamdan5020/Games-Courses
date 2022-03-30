using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizzard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObj = collision.gameObject;

        if (otherObj.GetComponent<Defender>())
        {
            GetComponent<Attacker>().Attack(otherObj);
        }

    }
}
