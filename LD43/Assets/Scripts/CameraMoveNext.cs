using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveNext : MonoBehaviour {

    RoomCamera m_cam;
    bool m_used;

    public void ResetUsed()
    {
        m_used = false;
    }

    private void Awake()
    {
        m_cam = FindObjectOfType<RoomCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!m_used)
        {
            m_cam.MoveNext();
            m_used = true;
        }
    }
}
