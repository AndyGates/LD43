using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoExitStateBehaviour : StateBehaviour
{
    [SerializeField]
    float m_delay = 0.0f;

    float m_entryTime = 0.0f;

    public override void OnEnterState()
    {
        base.OnEnterState();
        m_entryTime = Time.time;
    }

    public override string ShouldExitAt()
    {
        if(m_entryTime != 0.0f && (Time.time - m_entryTime) > m_delay)
        {
            return m_exitState;
        }

        return null;
    }
}
