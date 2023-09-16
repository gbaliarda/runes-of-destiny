using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRune : Rune
{
    [SerializeField] private MeleeRuneStats _meleeRuneStats;

    protected new void Start()
    {
        base.Start();
        runeStats = _meleeRuneStats;
    }

    public override void Shoot()
    {
        if (_cooldownLeft > 0) return;
        if (RuneStats.ManaCost > Player.Mana) return;

        if (RunePrefab != null)
        {
            GameObject meleeSpell = Instantiate(RunePrefab, transform.position, transform.rotation, RuneContainer);
            meleeSpell.GetComponent<MeleeSpell>()?.SetOwner(this);
        }

        _cooldownLeft = RuneStats.Cooldown;
        Player.SpendMana(RuneStats.ManaCost);
    }

    public override void ShootAtDirection(Vector3 direction)
    {
        Shoot();
    }


}
