using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class State : MonoBehaviour
{
    StateBehaviour[] m_stateBehaviours;

    [SerializeField]
    State[] m_exitStates;

    Dictionary<string, State> m_exitStateMap = new Dictionary<string, State>();

    public void Awake()
    {
        m_stateBehaviours = GetComponentsInChildren<StateBehaviour>();

        foreach (State s in m_exitStates)
        {
            m_exitStateMap.Add(s.name, s);
        }
    }

    public State ExitAt()
    {
        foreach(StateBehaviour s in m_stateBehaviours)
        {
            string exit = s.ShouldExitAt();
            if(!string.IsNullOrEmpty(exit))
            {
                if (exit == "any")
                {
                    return m_exitStates.FirstOrDefault();
                }
                else if(m_exitStateMap.ContainsKey(exit))
                {
                    return m_exitStateMap[exit];
                }
            }
        }

        return null;
    }

    public void Tick()
    {
        foreach (StateBehaviour s in m_stateBehaviours)
        {
            s.Tick();
        }
    }

    public void OnEnterState()
    {
        foreach (StateBehaviour s in m_stateBehaviours)
        {
            s.OnEnterState();
        }
    }

    public void OnExitState()
    {
        foreach (StateBehaviour s in m_stateBehaviours)
        {
            s.OnExitState();
        }
    }
}
