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
    protected CapsuleCollider m_collider;

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
        enemy.m_life -= m_damage;
    }
}
