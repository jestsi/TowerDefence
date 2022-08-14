using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public enum AttackType
{
    CloseCombat, // ближний бой
    RangedCombat // дальний
}

public class Warrior : Unit
{
#region Fields
    [Header("Warrior stats")]
    [SerializeField] private float _speed;
    [SerializeField] private Vector2Int _position;
    [SerializeField] private HealthWarriorHandler _healthWarrior;
    [SerializeField] private float _damage;
    [SerializeField] private AttackType _attackType;
    [SerializeField] private float _periodBeforeAttackInSecconds;

    private Transform _goal;
    private int _reloadTimeInMS;
    private int _riffleCount;

    public HealthWarriorHandler Health => _healthWarrior;
    public override float Speed => _speed;
    public override Vector2Int Position => _position;
    public override float Damage => _damage;
    public override AttackType AttackType => _attackType;
    public override float PeriodBeforeAttackInSec => _periodBeforeAttackInSecconds;
    public bool InMeShootiong { get; set; }
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

        CorrectPosition();
        _healthWarrior.Dead += DeadWarrior;
    }

    private void DeadWarrior(float obj)
    {
        if (_healthWarrior.AttackMeCoroutines.Count != 0)
        {
            while(_healthWarrior.AttackMeCoroutines.Count > 0)
            {
                StopCoroutine(_healthWarrior.AttackMeCoroutines.Pop());
            }
        }
        Destroy(gameObject);    
    }

    private void OnDestroy()
    {
        _healthWarrior.Dead -= DeadWarrior;
    }

    private void CorrectPosition()
    {
        if (!Physics.Raycast(transform.position, -transform.up, out var hit, 5f)) return;
        if (hit.collider.isTrigger) return;
        
        CellOnStateing = hit.collider.GetComponent<Cell>();

        Position = _position = new Vector2Int(CellOnStateing.X, CellOnStateing.Y);
    }
}
