using UnityEngine;

public class FistPunch : Attack
{

    public FistPunch(Vector3 pos, bool direction)
    {
        m_damage = 2;
        m_range = 2.5f;
        m_speed = 0.4f;
        m_stun = 0.2f;
        m_recoil = 3f;
        m_forward = 0.5f;
        m_position = pos;
        m_direction = direction;
    }

    public void Execute()
    {
        RaycastHit hit;
        if(Physics.Raycast(m_position, m_direction ? Vector3.left : Vector3.right, out hit, m_range))
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
