using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected int m_damage;
    protected float m_range;
    protected float m_speed;
    protected float m_recoil;
    protected float m_stun;
    protected Vector3 m_position;
    protected bool m_doingAction = false;

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
        return;
    }
}
