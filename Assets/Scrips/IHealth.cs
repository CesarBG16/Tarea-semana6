using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    event Action OnDeath;
    void TakeDamage(int amount);
}
