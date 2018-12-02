using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveNext : MonoBehaviour {

    RoomCamera m_cam;

    private void Awake()
    {
        m_cam = FindObjectOfType<RoomCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_cam.MoveNext();
    }
}
