using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdApplyDamage : ICommand
{
    private IDamageable _damageble;
    private int _damage;

    public CmdApplyDamage(IDamageable damageble, int damage)
    {
        _damageble = damageble;
        _damage = damage;
    }

    public void Execute()
    {
        _damageble.TakeDamage(_damage);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
