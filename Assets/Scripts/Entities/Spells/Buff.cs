using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour, IBuff
{
    #region PRIVATE_PROPERTEIS
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float lifetime = 3;
    [SerializeField] protected float duration = 5;
    protected BuffRune owner;
    #endregion

    #region I_SPELLPROJECTILE_PROPERTIES
    public float Speed => speed;
    public float LifeTime => lifetime;

    public BuffRune Owner => owner;

    public float Duration => duration;
    #endregion

    #region I_SPELLPROJECTILE_PROPERTIES

    public void Init()
    {
    }

    public void InitSound()
    {
        SpellSound spellSound = GetComponent<SpellSound>();
        EventQueueManager.instance.AddCommand(new CmdPlaySound(spellSound));
    }

    public void Die() => Destroy(this.gameObject);

    public void SetOwner(BuffRune owner) => this.owner = owner;

    public void ReduceDuration(float duration) => this.duration -= duration;
    #endregion

    #region UNITY_EVENTS
    private void OnDestroy()
    {
    }
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Spell");

        Init();

        InitSound();
    }

    void Update()
    {

        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Die();
    }
    #endregion
}
