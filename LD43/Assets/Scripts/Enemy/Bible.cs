using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Bible : MonoBehaviour {

    Animator m_animator;

    [SerializeField]
    float m_attackDelay = 5.0f;

    float m_lastTime;

    [SerializeField]
    bool m_attacking;
    
    [SerializeField]
    bool m_doDamage;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_lastTime = Time.time;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_attacking && Time.time - m_lastTime > m_attackDelay)
        {
            StartCoroutine(Attack());
        }
	}

    private IEnumerator Attack()
    {
        m_animator.SetTrigger("Attack");
        m_attacking = true;
        yield return new WaitForSeconds(0.8f);

        m_doDamage = true;

        yield return new WaitForSeconds(1.8f);

        m_doDamage = false;
        m_attacking = false;
        m_lastTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_doDamage)
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc)
            {
                pc.Kill();
            }
        }
    }
}
