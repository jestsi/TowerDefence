using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

namespace Units
{
    public class Tower : Unit
    {
        private TowerClass _tower;

        [Header("Tower stats")]
        [SerializeField] private SphereCollider _radiusTriggerColider;
        [SerializeField] private float _sizeRadiusTrigger;
        [SerializeField] private Vector2Int _position;
        [SerializeField] private float _damage;
        [SerializeField] private float _periodBeforeAttackInSec;
        [SerializeField] private int _maximumTargets;
        private int _currentTargets;
        private Warrior _target;
        private Transform _partForRotation;

        private void Start()
        {
            _radiusTriggerColider = GetComponent<SphereCollider>();
            _tower = gameObject.AddComponent<TowerClass>();
            _tower.Damage = _damage;
            _tower.PeriodBeforeAttackInSec = _periodBeforeAttackInSec;
            _tower.Position = _position;
            _tower.RadiusTriggerZone = _sizeRadiusTrigger;
            _radiusTriggerColider.radius = _tower.RadiusTriggerZone;
            _radiusTriggerColider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            SetTarget(other);
        }

        private void OnTriggerStay(Collider other)
        {
            SetTarget(other);
        }

        private void Awake()
        {
            _target = null;

            _partForRotation = gameObject.transform.Find("Установка");
        }

        private void FixedUpdate()
        {
            RotateToWarriorDirection();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!ExtensionsClasses.IsWarrior(other, out var warrior)) return;

            _target = null;

            if (!warrior.InMeShooting) return;
        
            StopCoroutine(warrior.Health.AttackMeCoroutines.Pop());
            --_currentTargets;
        }

        private void SetTarget(Collider other)
        {
            var warriros = GameObject.FindGameObjectsWithTag("Warrior")
                .Select(x => x.GetComponent<Warrior>())
                .Where(x => !x.InMeShooting);

            if (!CheckBeforeSetTarget(other, warriros, out var warrior)) return;

            ++_currentTargets;

            _target = warrior;
            
            warrior.Health.Dead += val => 
            { 
                --_currentTargets;
                _target = null;
            };
            warrior.Health.AttackMeCoroutines.Push(StartCoroutine(nameof(MinusHp), warrior));
        }

        private void RotateToWarriorDirection()
        {
            if (_target == null) return;

            var quat = Quaternion.LookRotation(_partForRotation.transform.position - _target.transform.position );
            quat.x = quat.z = 0;

            _partForRotation.rotation = quat;
        }

        public IEnumerator MinusHp(Warrior warrior)
        {
            while (true)
            {
                yield return new WaitForSeconds(_tower.PeriodBeforeAttackInSec);

                if (warrior.Health.AttackMeCoroutines.Count == 0)
                    yield break;

                warrior.Health.HealthChange(-(int)_tower.Damage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(_partForRotation.transform.position,
                -_partForRotation.forward * _sizeRadiusTrigger);
        }

        private bool CheckBeforeSetTarget(Collider other, IEnumerable<Warrior> warriors, out Warrior warriorEx)
        {
            warriorEx = null;
            if (!ExtensionsClasses.IsWarrior(other, out var warrior)) return false;
            if (warrior.InMeShooting && warriors.Count() > 1) return false;
            if (_currentTargets >= _maximumTargets) return false;
            warriorEx = warrior;
            return true;
        }
    }
}
