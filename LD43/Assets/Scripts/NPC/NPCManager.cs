using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour {

    NPCController[] m_npcs;

	// Use this for initialization
	public void GetNPCs ()
    {
	    m_npcs = FindObjectsOfType<NPCController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartSacrifice()
    {
        foreach (NPCController c in m_npcs)
        {
            c.StartSacrifice();
        }
    }

    public void ResetNPCs()
    {
        foreach (NPCController c in m_npcs)
        {
            c.ResetNPC();
        }
    }
}
