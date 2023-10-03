using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    List<IBuff> Buffs { get; }
    void AddBuff(IBuff buff);
    void RemoveBuff(IBuff buff);
}
