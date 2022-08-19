using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Units
{
    public abstract class TowerUsing : Unit, ITowerBased
    {
        [Header("Tower stats")] [SerializeField]
        private float _sizeRadiusTrigger;

        [SerializeField] private float _damage;
        [SerializeField] private float _periodBeforeAttackInSec;
        [SerializeField] private int _maximumTargets;
        [SerializeField] private int _price;
        [SerializeField] private TowerTerritoryTargetE _territoryTarget;

        private int _currentTargets;
        private Warrior _target;
        private Transform _partForRotation;

        private void Awake()
        {
            var radiusTriggerCollider = GetComponent<SphereCollider>();
            radiusTriggerCollider.radius = TriggerRadius;
            radiusTriggerCollider.isTrigger = true;
            _partForRotation = FindChildForRotate();
        }

        protected virtual Transform FindChildForRotate()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                if (child.CompareTag("Rotating")) return child;
            }

            throw new Exception();
        }

        private void FixedUpdate()
        {
            RotateToWarriorDirection();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!ExtensionsClasses.IsWarrior(other, out var warrior)) return;
            StopShoot(warrior);
        }

        protected virtual void SetTarget(Collider other)
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
            warrior.Health.AttackMeCoroutines.Push(StartCoroutine(nameof(ShootInWarrior), warrior));
        }

        protected virtual void StopShoot(Warrior warrior)
        {
            if (!warrior.InMeShooting) return;
            --_currentTargets;
            _target = null;
            StopCoroutine(warrior.Health.AttackMeCoroutines.Pop());
        }

        private void RotateToWarriorDirection()
        {
            if (_target == null) return;

            var quat = Quaternion.LookRotation(_partForRotation.transform.position - _target.transform.position);
            quat.x = quat.z = 0;

            _partForRotation.rotation = quat;
        }

        public IEnumerator ShootInWarrior(Warrior warrior)
        {
            while (true)
            {
                yield return new WaitForSeconds(PeriodAttack);

                if (warrior.Health.AttackMeCoroutines.Count == 0)
                    yield break;

                warrior.Health.HealthChange(-(int)Damage);
            }
        }

        protected virtual bool CheckBeforeSetTarget(Collider other, IEnumerable<Warrior> warriors,
            out Warrior warriorEx)
        {
            warriorEx = null;
            if (!ExtensionsClasses.IsWarrior(other, out var warrior)) return false;
            if (warrior.InMeShooting && warriors.Count() > 1) return false;
            if (_currentTargets >= _maximumTargets) return false;
            if ((int)_territoryTarget != -1 && (int)_territoryTarget != (int)warrior.MovingType) return false;

            warriorEx = warrior;
            return true;
        }

        private void OnTriggerEnter(Collider other)
        {
            SetTarget(other);
        }

        private void OnTriggerStay(Collider other)
        {
            SetTarget(other);
        }

        public int Price
        {
            get => _price;
            set => _price = value >= 0 ? value : 0;
        }

        public event UnityAction<int> OnBuy;

        public int CountTargets
        {
            get => _currentTargets;
            set => _currentTargets = value;
        }

        public int MaximumTargets
        {
            get => _maximumTargets;
            set => _maximumTargets = value;
        }

        public float PeriodAttack
        {
            get => _periodBeforeAttackInSec;
            set => _periodBeforeAttackInSec = value;
        }

        public float TriggerRadius
        {
            get => _sizeRadiusTrigger;
            set => _sizeRadiusTrigger = value;
        }

        public TowerTerritoryTargetE TerritoryTarget
        {
            get => _territoryTarget;
            set => _territoryTarget = value;
        }

        public float Damage
        {
            get => _damage;
            set => _damage = value;
        }
    }
}