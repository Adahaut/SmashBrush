using UnityEngine;

public abstract class Attack 
{
    protected int m_damage;
    protected float m_range;
    protected float m_speed;
    protected float m_recoil;
    protected float m_stun;
    protected float m_forward;
    protected Vector3 m_position;
    protected bool m_direction;
    
    private float m_ejectionPercent = 0.1f;
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
    public float GetForward()
    {
        return m_forward;
    }


    public virtual void AttackEnnemi(PlayerCharacter enemy)
    {
        Debug.Log("Attack : " + m_damage + " damage");
        m_ejectionPercent *= enemy._percent;
        if (Random.Range(0,100) < m_ejectionPercent)
        {
            Debug.Log("eject");
            enemy.GetComponentInParent<PlayerMovement>()._velocity.y = enemy._percent;
        }
        else
        {
            enemy.GetComponentInChildren<ParticleSystem>().Play();
            enemy._percent += m_damage;
            enemy.GetComponentInParent<PlayerMovement>()._velocity.y = 10;
            enemy.transform.Translate(new Vector2(m_direction ? -m_recoil : m_recoil, 0) * Time.deltaTime * 3);
            enemy._controller._isStun = true;
            enemy._controller._stunTime = Time.time + m_stun;
        }
        return;
    }
}
