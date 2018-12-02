using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateBehaviour : MonoBehaviour {

    [SerializeField]
    UnityEvent m_onEnter;

    [SerializeField]
    UnityEvent m_onExit;

    [SerializeField]
    protected string m_exitState = "any";

    public virtual void Tick()
    {
    }

    public virtual void OnEnterState()
    {
        m_onEnter.Invoke();
    }

    public virtual void OnExitState()
    {
        m_onExit.Invoke();
    }

    public virtual string ShouldExitAt()
    {
        return null;
    }
}
