using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedRune : Rune
{
    [SerializeField] private RangedRuneStats _rangedRuneStats;

    public RangedRuneStats RangedRuneStats => _rangedRuneStats;

    protected new void Start()
    {
        runeStats = _rangedRuneStats;
        base.Start();
    }

    public override void Shoot()
    {
        ShootAtDirection(transform.forward);
    }

    public override void ShootAtDirection(Vector3 direction)
    {

        int numberOfProjectiles = _rangedRuneStats.Projectiles;
        float angleBetweenProjectiles = 5f;

        if (360 / angleBetweenProjectiles < numberOfProjectiles) numberOfProjectiles = Mathf.RoundToInt(360 / angleBetweenProjectiles);

        Vector3 mainDirection = (direction - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(mainDirection.x, 0, mainDirection.z));
        Player.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);

        float maxRotationAngle = angleBetweenProjectiles * (numberOfProjectiles - 1) / 2;

        for (float rotationAngle = -maxRotationAngle; rotationAngle <= maxRotationAngle; rotationAngle += angleBetweenProjectiles)
        {

            Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);
            Vector3 auxDirection = rotation * mainDirection;

            GameObject spellProjectile = Instantiate(RunePrefab, transform.position, Quaternion.LookRotation(auxDirection), RuneContainer);
            spellProjectile.GetComponent<SpellProjectile>()?.SetOwner(this);
        }
    }


}
