using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    [SerializeField]
    Vector2 m_startPosition;

    [SerializeField]
    Vector2 m_moveAmount;

    [SerializeField]
    float m_smoothTime = 0.3f;

    Vector3 m_velocity = Vector3.zero;
    Vector3 m_target;

    // Use this for initialization
    void Start()
    {
        MoveStart();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, m_target, ref m_velocity, m_smoothTime);
    }

    public void MoveNext()
    {
        m_target += new Vector3(m_moveAmount.x, m_moveAmount.y, 0);
    }

    public void MoveStart()
    {
        m_target = new Vector3(m_startPosition.x, m_startPosition.y, transform.position.z);
    }
}
