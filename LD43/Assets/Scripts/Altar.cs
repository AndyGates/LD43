using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour {

    [SerializeField]
    GameObject m_altarUI;

    [SerializeField]
    Transform m_playerTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc)
        {
            m_altarUI.SetActive(true);
            pc.NextToAltar = true;
            pc.AltarTransform = m_playerTransform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc)
        {
            m_altarUI.SetActive(false);
            pc.NextToAltar = false;
            pc.AltarTransform = null;
        }
    }
}
