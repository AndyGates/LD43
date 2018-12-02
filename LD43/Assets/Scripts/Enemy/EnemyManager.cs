using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    EnemyController[] m_enemies;

    // Use this for initialization
    public void GetEnemies()
    {
        m_enemies = FindObjectsOfType<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetEnemies()
    {
        foreach (EnemyController c in m_enemies)
        {
            c.ResetEnemy();
        }
    }
}
