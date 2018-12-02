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
    bool m_stop = false;

    SpriteRenderer[] m_renderers;
    Vector3 m_startPosition;
    LayerMask m_playerMask;

    public void ResetEnemy()
    {
        transform.position = m_startPosition;
        m_animController.SetTrigger("Reset");
        m_currentWaypoint = 0;
        m_stop = false;
    }

    private void Awake()
    {
        m_playerMask = LayerMask.GetMask("Player");
        m_startPosition = transform.position;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animController = GetComponent<Animator>();
        m_renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(!m_stop)
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

            RaycastHit2D hit = Physics2D.Raycast(transform.position, m_dir ? Vector2.left : Vector2.right, 0.45f, m_playerMask.value);
            if (hit.collider != null)
            {
                PlayerController pc = hit.collider.GetComponent<PlayerController>();
                if (pc)
                {
                    StartCoroutine(AttackPlayer(pc));
                }
            }
        }
    }

    private void SetDirection(bool left)
    {
        m_dir = left;

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
            StartCoroutine(AttackPlayer(pc));
        }
    }

    private IEnumerator AttackPlayer(PlayerController pc)
    {
        if(!m_stop)
        {
            m_stop = true;
            m_rigidbody.velocity = Vector2.zero;
            m_animController.SetTrigger("Attack");
            m_animController.SetBool("Running", false);

            yield return new WaitForSeconds(0.5f);

            pc.Kill();
        }
    }
}
