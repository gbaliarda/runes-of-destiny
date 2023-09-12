using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBallRune : RangedRune
{
    public override void Shoot()
    {
        if (_cooldownLeft >  0) return;
        if (RuneStats.ManaCost > Player.Mana) return;

        Debug.Log($"{name} mana left {Player.Mana}");

        int numberOfProjectiles = RuneStats.Projectiles;
        float angleBetweenProjectiles = 5f;

        if (360 / angleBetweenProjectiles < numberOfProjectiles) numberOfProjectiles = Mathf.RoundToInt(360 / angleBetweenProjectiles);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);

        Vector3 mainDirection = (hit.point - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(mainDirection.x, 0, mainDirection.z));
        Player.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1f);

        float maxRotationAngle = angleBetweenProjectiles*(numberOfProjectiles - 1)/2;

        for (float rotationAngle = -maxRotationAngle; rotationAngle < maxRotationAngle; rotationAngle += angleBetweenProjectiles)
        {
            Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);
            Vector3 direction = rotation * mainDirection;

            GameObject frostBall = Instantiate(RunePrefab, transform.position, Quaternion.LookRotation(direction), RuneContainer);
            frostBall.GetComponent<FrostBall>()?.SetOwner(this);
        }

        _cooldownLeft = RuneStats.Cooldown;
        Player.SpendMana(RuneStats.ManaCost);
    }


}
