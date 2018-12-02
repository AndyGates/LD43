using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public Action Death { get; internal set; }
    public Action ActivateAltar { get; internal set; }

    public bool NextToAltar { get; set; } = false;
    public Transform AltarTransform { get; set; }

    [SerializeField]
    float m_speed = 2.0f;

    Animator m_animController;
    Rigidbody2D m_rigidbody;

    bool m_dir = false;
    bool m_jumping = false;
    bool m_onAltar = false;

    SpriteRenderer[] m_renderers;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animController = GetComponent<Animator>();
        m_renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        bool activateAltar = !m_onAltar && NextToAltar && AltarTransform != null && Input.GetButton("Submit");
        if(activateAltar)
        {
            ActivateAltar?.Invoke();
            m_animController.SetTrigger("Lie");
            m_onAltar = true;
            m_rigidbody.isKinematic = true;
            m_rigidbody.MovePosition(AltarTransform.position);
            m_rigidbody.velocity = Vector2.zero;
        }
        else if(!m_onAltar)
        {
            float moveX = Input.GetAxis("Horizontal");
            bool jump = Input.GetButton("Jump");
            bool duck = Input.GetButton("Duck");

            float velX = moveX * m_speed;
            m_rigidbody.velocity = new Vector2(velX, m_rigidbody.velocity.y);

            if (jump && !m_jumping)
            {
                m_rigidbody.AddForce(new Vector2(0, 5.0f), ForceMode2D.Impulse);
                m_jumping = true;
            }
            else if (m_jumping)
            {
                if (OnGround())
                {
                    m_jumping = false;
                }
            }

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
    }

    private void SetDirection(bool left)
    {
        foreach (SpriteRenderer r in m_renderers)
        {
            r.flipX = left;
        }
    }

    private bool OnGround()
    {
        LayerMask mask = LayerMask.GetMask("Environment");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.45f, mask.value);
        return hit.collider != null;
    }

    public void Kill()
    {
        StartCoroutine(KillCoroutine()); 
    }

    private IEnumerator KillCoroutine()
    {
        m_animController.SetTrigger("Convert");
        yield return new WaitForSeconds(2);
        if(Death != null) Death.Invoke();
    }

    public void Respawn()
    {
        m_animController.SetTrigger("Respawn");
        m_onAltar = false;
        m_rigidbody.isKinematic = false;
    }
}
