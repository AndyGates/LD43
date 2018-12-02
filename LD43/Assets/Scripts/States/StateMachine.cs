using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    [SerializeField]
    State m_startState;

    private State m_currentState;

    private void Start()
    {
        SetState(m_startState);
    }

    private void Update()
    {
        m_currentState.Tick();

        State exitState = m_currentState.ExitAt();
        if (exitState != null)
        {
            SetState(exitState);
        }
    }

    public void SetState(State state)
    {
        if (m_currentState != null)
            m_currentState.OnExitState();

        m_currentState = state;
        gameObject.name = "StateMachine - " + state.name;

        if (m_currentState != null)
            m_currentState.OnEnterState();
    }
}
