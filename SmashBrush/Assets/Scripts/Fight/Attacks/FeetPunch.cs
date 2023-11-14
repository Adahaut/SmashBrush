using UnityEngine;

public class FeetPunch : Attack
{

    public FeetPunch(Vector3 pos, bool direction)
    {
        m_damage = 5;
        m_range = 2f;
        m_speed = 1f;
        m_stun = 0.5f;
        m_recoil = 20f;
        m_forward = 2f;
        m_position = pos;
        m_direction = direction;
    }

    public void Execute()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_position, m_direction ? Vector3.left : Vector3.right, out hit, m_range))
        {

            if (hit.transform.tag == "Player")
            {
                AttackEnnemi(hit.transform.GetComponent<PlayerCharacter>());
            }
        }
    }
}
