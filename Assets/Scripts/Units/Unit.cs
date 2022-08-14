using Assets.Scripts;
using System;
using System.Linq;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public virtual int Health { get; set; }
    public virtual float Speed { get; set; }
    public virtual float Damage { get; set; }
    public virtual AttackType AttackType { get; set; }
    public virtual float PeriodBeforeAttackInSec { get; set; }
    public virtual Vector2Int Position { get; set; }
    public virtual Cell CellOnStateing { get; set; }

    private const int FNVOffsetBasis = 1469598;
    private const int FNVPrime = 1099511;

    public override int GetHashCode()
    {
        unchecked {
            var hash = FNVOffsetBasis;
            hash ^= Health;
            hash *= FNVPrime;

            hash ^= (int)Speed;
            hash *= FNVPrime;

            hash ^= (int)Damage;
            hash *= FNVPrime;

            hash ^= (int)PeriodBeforeAttackInSec;
            hash *= FNVPrime;

            hash ^= name.Length;
            hash *= FNVPrime;

            hash ^= base.gameObject.GetInstanceID();
            hash *= FNVPrime;

            return hash;
        }
    }
}
