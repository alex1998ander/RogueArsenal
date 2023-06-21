using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterHealth
{
    public void InflictDamage(float damageAmount, bool fatal = false);
}