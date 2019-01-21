using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{
    public int level = 1;

    public int health = 100;
    public int maxHealth;

    public int strengh = 1;
    public int speed = 1;
    public int stamina = 1;
    public int magic = 1;
    public int attack = 1;
    public int attackDefence = 1;
    public int dodge = 1;
    public int spellDefence = 1;
    public int spellDodge = 1;

    public bool alive = true;

    public bool flying = false;

    public enum UnitStatus
    {
        None,
        Confused,
        Poison,
        Doomed,
        Weak,
        Haste,
        Slow,
        Hate,
        Rage,
        Scared,
        Broken,
        Unconscious,
        Asleep,
        Transformed
    }
    public UnitStatus unitStatus = UnitStatus.None;

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            Die();
        }

        // -- show damage
        EffectsController.instance.ShowDamage(this, amount);
    }

    protected void Die()
    {
        alive = false;

        EffectsController.instance.ShowDeath(this);
    }
}
