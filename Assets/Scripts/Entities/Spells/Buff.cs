using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour, IBuff
{
    #region PRIVATE_PROPERTEIS
    protected float duration;
    protected IBuffRune owner;
    protected CmdBuff command;
    #endregion

    #region I_BUFF_PROPERTIES

    public IBuffRune Owner => owner;

    public float Duration => duration;
    #endregion

    #region I_BUFF_PROPERTIES

    public void Init()
    {
        command = new CmdBuff(owner.Player, this);
        EventQueueManager.instance.AddCommand(command);
        duration = Owner.Duration;
    }

    public void InitSound()
    {
        SpellSound spellSound = GetComponent<SpellSound>();
        EventQueueManager.instance.AddCommand(new CmdPlaySound(spellSound));
    }

    public void Die() {
        Destroy(this.gameObject);
    }

    public void SetOwner(BuffRune owner) => this.owner = owner;

    #endregion

    #region UNITY_EVENTS
    private void OnDestroy()
    {
        EventQueueManager.instance.AddUndoCommand(command);
    }
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Spell");

        Init();

        InitSound();
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            Die();
        }
    }
    #endregion
}
