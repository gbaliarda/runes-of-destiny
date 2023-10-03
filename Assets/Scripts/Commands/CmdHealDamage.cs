using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdHealDamage : ICommand
{
    private IDamageable _damageable;
    private int _damage;

    public CmdHealDamage(IDamageable damageable, int damage)
    {
        _damageable = damageable;
        _damage = damage;
    }

    public void Execute()
    {
        _damageable.HealDamage(_damage);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
