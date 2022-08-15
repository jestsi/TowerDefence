using Extensions;
using UnityEngine;

#pragma warning disable CS0114 


namespace Units
{
    public class Base : Unit
    {
        [SerializeField] private HealthHandlerBase _health;

        public HealthHandlerBase Health => _health;


        private void Start()
        {       
            _health.Dead += f => OnBreakBase();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!ExtensionsClasses.IsWarrior(other, out Warrior warrior)) return;

            warrior.AtTheDestination = true;

            _health.OnWarriorAttack(warrior);
        
            warrior.Health.Death();
        }

        public void OnBreakBase()
        {
            StopAllCoroutines();
        }
    }
}
