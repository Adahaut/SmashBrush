using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack 
{
    protected int m_damage;
    protected float m_range;
    protected float m_speed;
    protected float m_recoil;
    protected float m_stun;
    protected Vector3 m_position;
    protected bool m_direction;
    public int GetDamage()
    {
        return m_damage;
    }
    public float GetRange()
    {
        return m_range;
    }
    public float GetSpeed()
    {
        return m_speed;
    }
    public float GetRecoil()
    {
        return m_recoil;
    }
    public float GetStun()
    {
        return m_stun;
    }


    public virtual void AttackEnnemi(PlayerCharacter enemy)
    {
        Debug.Log("Attack : " + m_damage + " damage");
        enemy._percent += m_damage;
        enemy.GetComponentInParent<PlayerMovement>()._velocity.x = m_direction ? -m_recoil : m_recoil;
        return;
    }
}
