using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public bool targetDummy = false; // does this guy take damage?

    public override void TakeDamage(int amount)
    {
        if (!targetDummy)
        {
            health -= amount;
            if (health < 0)
            {
                Die();
            }
        }

        // -- show damage
        EffectsController.instance.ShowDamage(this, amount);
    }
}
