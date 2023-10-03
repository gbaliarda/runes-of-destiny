using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffRune : Rune, IBuffRune
{
    #region PRIVATE_PROPERTIES
    [SerializeField] private GameObject _buffPrefab;
    [SerializeField] private Transform _buffContainer;
    [SerializeField] protected CharacterStats buffStats;
    protected float duration;
    #endregion

    #region IRUNE_PROPERTIES

    public GameObject BuffPrefab => _buffPrefab;

    public Transform BuffContainer => _buffContainer;

    public CharacterStats BuffStats => buffStats;

    public float Duration => duration;
    #endregion

    #region IBUFF_METHODS

    public void Buff()
    {
        if (BuffPrefab != null)
        {
            GameObject buffSpell = Instantiate(BuffPrefab, transform.position, transform.rotation, BuffContainer);
            if (buffSpell.GetComponent<IBuff>() == null ) { 
                Destroy(buffSpell);
                return;
            }
            buffSpell.GetComponent<Buff>().SetOwner(this);
        }
    }
    #endregion

    #region IRUNE_METHODS
    public override void Shoot()
    {
        Buff();
    }

    public override void ShootAtDirection(Vector3 direction)
    {
        Buff();
    }
    #endregion

}
