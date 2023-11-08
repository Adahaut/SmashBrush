using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPunch : Attack
{

    public FistPunch()
    {
        m_damage = 2;
        m_range = 0.5f;
        m_speed = 0.5f;
        m_stun = 0.2f;
        m_recoil = 0.2f;
        m_collider = GameObject.Find("FistPunchHitBox").GetComponent<CapsuleCollider>();
    }
}
