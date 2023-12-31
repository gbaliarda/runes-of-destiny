using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRune : Rune
{
    [SerializeField] private MeleeRuneStats _meleeRuneStats;

    protected new void Start()
    {
        runeStats = _meleeRuneStats;
        base.Start();
    }

    public override void Shoot()
    {
        if (RunePrefab != null)
        {
            GameObject meleeSpell = Instantiate(RunePrefab, transform.position, transform.rotation, RuneContainer);
            meleeSpell.GetComponent<MeleeSpell>()?.SetOwner(this);
        }
    }

    public override void ShootAtDirection(Vector3 direction)
    {
        Shoot();
    }


}
