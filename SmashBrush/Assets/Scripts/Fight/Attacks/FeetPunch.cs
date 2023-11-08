using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetPunch : Attack
{

    public FeetPunch()
    {
        m_damage = 5;
        m_range = 1f;
        m_speed = 1f;
        m_stun = 0.3f;
        m_recoil = 0.5f;
        m_collider = GameObject.Find("FeetPunchHitBox").GetComponent<CapsuleCollider>();
    }
}
