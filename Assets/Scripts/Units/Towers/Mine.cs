using System.Collections.Generic;
using System.Linq;
using Extensions;
using Units;
using UnityEngine;
using UnityEngine.Events;

public class Mine : Unit, ISellUnit
{
    [SerializeField] private int _damage;
    private List<Transform> _warriorsInBombRadius;
    [SerializeField] private float _boomMult = 2.5f;

    private void Awake()
    {
        _warriorsInBombRadius = new();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ExtensionsClasses.IsWarrior(other, out var warrior) && warrior.MovingType != WarriorMovingTypeE.Flying)
            _warriorsInBombRadius.Add(other.transform);
    }

    public void Boom()
    {
        var warriors = _warriorsInBombRadius.Select(t => t.GetComponent<Warrior>());

        foreach (var warrior in warriors)
        {
            var exitDamage = ValidatingDamage(warrior);

            warrior.Health.HealthChange(-(int)exitDamage);
        }

        Destroy(gameObject);
    }

    private float ValidatingDamage(Warrior warrior)
    {
        var heading = warrior.transform.position - transform.position;
        var distancePercent = (heading.magnitude * _boomMult) * 100;

        return ((_damage * 100) - distancePercent) / 100;
    }

    private void OnTriggerExit(Collider other)
    {
        _warriorsInBombRadius.Remove(other.transform);
    }

    public int Price { get; set; }
    public event UnityAction<int> OnBuy;
}