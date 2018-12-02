using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [SerializeField]
    Transform m_playerSpawn;

    [SerializeField]
    PlayerController m_player;

    public void Respawn()
    {
        m_player.transform.position = m_playerSpawn.transform.position;
        m_player.Respawn();
    }
}
