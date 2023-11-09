using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPunch : Attack
{

    public FistPunch(Vector3 pos)
    {
        m_damage = 2;
        m_range = 1.5f;
        m_speed = 0.5f;
        m_stun = 0.2f;
        m_recoil = 0.2f;
        m_position = pos;
    }

    public void Execute()
    {
        RaycastHit hit;
        if(Physics.Raycast(m_position, Vector3.right, out hit, m_range))
        {
            if (hit.transform.tag == "Player")
            {
                AttackEnnemi(hit.transform.GetComponent<PlayerCharacter>());
                return;
            }
            else { return; }
        }
        else { return; }
    }
}
