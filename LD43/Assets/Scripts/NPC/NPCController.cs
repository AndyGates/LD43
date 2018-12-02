using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class NPCController : MonoBehaviour {

    Animator m_animator;
    SpriteRenderer m_sprite;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();
    }

    public void ResetNPC()
    {
        m_animator.SetTrigger("Reset");
        m_sprite.sortingLayerName = "Background";
    }
    
    public void StartSacrifice()
    {
        m_animator.SetTrigger("Sacrifice");
        m_sprite.sortingLayerName = "Foreground";
    }
}
