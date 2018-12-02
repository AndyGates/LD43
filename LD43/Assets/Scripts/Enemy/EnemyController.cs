using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    float m_speed = 2.0f;

    [SerializeField]
    List<Transform> m_waypoints = new List<Transform>();

    [SerializeField]
    int m_currentWaypoint = 0; 

    Animator m_animController;
    Rigidbody2D m_rigidbody;

    bool m_dir = false;

    SpriteRenderer[] m_renderers;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animController = GetComponent<Animator>();
        m_renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float targetX = m_waypoints[m_currentWaypoint].transform.position.x;
        float moveX = targetX - transform.position.x;
        if (Mathf.Abs(moveX) > 0.1f)
        {
            float velX = (moveX > 0 ? 1 : -1) * m_speed;
            m_rigidbody.velocity = new Vector2(velX, m_rigidbody.velocity.y);

            float animSpeed = m_rigidbody.velocity.magnitude;
            bool running = animSpeed > 0.01;

            m_animController.SetBool("Running", running);

            if (!running)
            {
                animSpeed = 1.0f;
            }

            m_animController.speed = animSpeed;

            if (moveX != 0)
            {
                SetDirection(moveX < 0);
            }
        }

        else
        {
            m_rigidbody.velocity = new Vector2(0, 0);
            
            m_currentWaypoint++;
            if (m_currentWaypoint > m_waypoints.Count - 1)
            {
                m_currentWaypoint = 0;
            }
        }
    }

    private void SetDirection(bool left)
    {
        foreach (SpriteRenderer r in m_renderers)
        {
            r.flipX = left;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc)
        {
            pc.Kill();
        }
    }
}
