using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetPunch : Attack
{

    public FeetPunch(Vector3 pos)
    {
        m_damage = 5;
        m_range = 2f;
        m_speed = 1f;
        m_stun = 0.3f;
        m_recoil = 0.5f;
        m_position = pos;
    }

    public void Execute()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_position, Vector3.right, out hit, m_range))
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