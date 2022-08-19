using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Units
{
    public enum WarriorMovingTypeE
    {
        Flying,
        GroundMove,
        Gybrid
    }

    public class Warrior : Unit
    {
        #region Fields

        [Header("Warrior stats")] [SerializeField]
        private Vector2Int _position;

        [FormerlySerializedAs("_healthWarrior")] [SerializeField]
        private HealthWarriorHandler _health;

        [SerializeField] private float _damage;
        [SerializeField] private WarriorMovingTypeE _movingType;
        [SerializeField] private float _periodBeforeAttackInSecconds;
        [SerializeField] private NavMeshAgent _agent;

        private Transform _goal;
        private int _reloadTimeInMS;
        private int _riffleCount;

        public HealthWarriorHandler Health => _health;
        public override float Speed => _agent.speed;
        public override Vector2Int Position => _position;
        public override float Damage => _damage;
        public WarriorMovingTypeE MovingType => _movingType;
        public override float PeriodBeforeAttackInSec => _periodBeforeAttackInSecconds;
        public bool InMeShooting => _health.AttackMeCoroutines.Count > 0;
        public bool AtTheDestination { get; set; }

        #endregion

        private void Start()
        {
            _goal = GameObject.FindGameObjectWithTag("Destination").transform;

            var agent = GetComponent<NavMeshAgent>();

            agent.SetDestination(_goal.position);

            _health.Dead += Dead;
        }

        private void Dead(float obj)
        {
            while (_health.AttackMeCoroutines.Count > 0 && _health.AttackMeCoroutines != null)
            {
                StopCoroutine(_health.AttackMeCoroutines.Pop());
            }

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _health.Dead -= Dead;
        }
    }
}