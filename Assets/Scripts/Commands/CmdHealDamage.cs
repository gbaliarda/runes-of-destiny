using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdHealDamage : ICommand
{
    private IDamageable _damageble;
    private int _damage;

    public CmdHealDamage(IDamageable damageble, int damage)
    {
        _damageble = damageble;
        _damage = damage;
    }

    public void Execute()
    {
        _damageble.HealDamage(_damage);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
