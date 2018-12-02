using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathStateBehaviour : StateBehaviour {

    [SerializeField]
    PlayerController m_player;

    bool m_needsExit = false;

	// Use this for initialization
	void Start () {
        m_player.Death += OnPlayerDeath;
	}

    public void OnPlayerDeath()
    {
        m_needsExit = true;
    }

    public override string ShouldExitAt()
    {
        if(m_needsExit)
        {
            m_needsExit = false;
            return m_exitState;
        }

        return null;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
