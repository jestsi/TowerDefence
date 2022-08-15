using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Units
{
    public enum AttackType
    {
        CloseCombat, // ближний бой
        RangedCombat // дальний
    }

    public class Warrior : Unit
    {
        #region Fields
        [Header("Warrior stats")]
        [SerializeField] private Vector2Int _position;
        [FormerlySerializedAs("_healthWarrior")] [SerializeField] private HealthWarriorHandler _health;
        [SerializeField] private float _damage;
        [SerializeField] private AttackType _attackType;
        [SerializeField] private float _periodBeforeAttackInSecconds;
        [SerializeField] private NavMeshAgent _agent;
        
        private Transform _goal;
        private int _reloadTimeInMS;
        private int _riffleCount;

        public HealthWarriorHandler Health => _health;
        public override float Speed => _agent.speed;
        public override Vector2Int Position => _position;
        public override float Damage => _damage;
        public override AttackType AttackType => _attackType;
        public override float PeriodBeforeAttackInSec => _periodBeforeAttackInSecconds;
        public bool InMeShooting => _health.AttackMeCoroutines.Count > 0;
        public bool AtTheDestination { get; set; }
        public int RiffleCount
        {
            get { return _riffleCount; }
            set { _riffleCount = value; }
        }
        public int ReloadTimeInMS { get => _reloadTimeInMS; set => _reloadTimeInMS = value; }
        #endregion
        private void Start()
        {
            _goal = GameObject.FindGameObjectWithTag("Destination").transform;

            var agent = GetComponent<NavMeshAgent>();

            agent.SetDestination(_goal.position);

            // CorrectPosition();
            _health.Dead += Dead;
        }

        private void Dead(float obj)
        {
            while(_health.AttackMeCoroutines.Count > 0)
            {
                StopCoroutine(_health.AttackMeCoroutines.Pop());
            }
            
            Destroy(gameObject);    
        }

        private void OnDestroy()
        {
            _health.Dead -= Dead;
        }

        /*
        private void CorrectPosition()
        {
            if (!Physics.Raycast(transform.position, -transform.up, out var hit, 5f)) return;
            if (hit.collider.isTrigger) return;
        
            CellOnStateing = hit.collider.GetComponent<Cell>();

            Position = _position = new Vector2Int(CellOnStateing.X, CellOnStateing.Y);
        }*/
    }
}