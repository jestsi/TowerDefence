using System;
using System.Collections.Generic;
using UnityEngine;

namespace Units
{
    public class HealthHandler : MonoBehaviour
    {
        [Header("Health stats")] [SerializeField]
        private int _maxHealth = 100;

        public virtual Stack<Coroutine> AttackMeCoroutines { get; set; }
        private int _currentHealth;

        public virtual int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public virtual int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public virtual event Action<float> HealthChanged;
        public virtual event Action<float> Dead;

        public HealthHandler()
        {
            AttackMeCoroutines = new Stack<Coroutine>();
            _currentHealth = _maxHealth;
        }

        private void Start()
        {
            AttackMeCoroutines = new Stack<Coroutine>();
            _currentHealth = _maxHealth;
        }

        public virtual void HealthChange(int health)
        {
            _currentHealth += health;
            if (_currentHealth <= 0 || _currentHealth > _maxHealth)
            {
                Death();
            }
            else
            {
                var currentHealthAsPercent = (float)_currentHealth / _maxHealth;
                HealthChanged?.Invoke(currentHealthAsPercent);
            }
        }

        public virtual void Death()
        {
            HealthChanged?.Invoke(0);
            StopAllCoroutines();
            Dead?.Invoke(0);
            Destroy(this);
        }
    }
}