using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactObject : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc)
        {
            pc.Kill();
        }
    }
}
