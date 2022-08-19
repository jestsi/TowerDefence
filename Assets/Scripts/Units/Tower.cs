namespace Units
{
    public class Tower : TowerUsing
    {
        /*private void OnTriggerEnter(Collider other)
        {
            SetTarget(other);
        }

        private void OnTriggerStay(Collider other)
        {
            SetTarget(other);
        }*/

        /*
        private void Awake()
        {
            _target = null;

            _partForRotation = gameObject.transform.Find("Установка");
        }*/

        /*private void FixedUpdate()
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
        }*/

        /*private void SetTarget(Collider other)
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
        }*/

        /*private void RotateToWarriorDirection()
        {
            if (_target == null) return;

            var quat = Quaternion.LookRotation(_partForRotation.transform.position - _target.transform.position );
            quat.x = quat.z = 0;

            _partForRotation.rotation = quat;
        }*/

        /*public IEnumerator ShootInWarrior(Warrior warrior)
        {
            while (true)
            {
                yield return new WaitForSeconds(PeriodAttack);

                if (warrior.Health.AttackMeCoroutines.Count == 0)
                    yield break;

                warrior.Health.HealthChange(-(int)Damage);
            }
        }*/

        /*private bool CheckBeforeSetTarget(Collider other, IEnumerable<Warrior> warriors, out Warrior warriorEx)
        {
            warriorEx = null;
            if (!ExtensionsClasses.IsWarrior(other, out var warrior)) return false;
            if (warrior.InMeShooting && warriors.Count() > 1) return false;
            if (_currentTargets >= _maximumTargets) return false;
            warriorEx = warrior;
            return true;
        }*/

        /*public int Price
        {
            get => _price;
            set => _price = value >= 0 ? value : 0;
        }

        public event UnityAction<int> OnBuy;

        public int CountTargets { get => _currentTargets; set => _currentTargets = value; }

        public int MaximumTargets { get => _maximumTargets; set => _maximumTargets = value; }

        public float PeriodAttack { get => _periodBeforeAttackInSec; set => _periodBeforeAttackInSec = value; }

        public float TriggerRadius { get => _sizeRadiusTrigger; set => _sizeRadiusTrigger = value; }
        
        public float Damage { get => _damage; set => _damage = value ; }*/
    }
}