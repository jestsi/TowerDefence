namespace Units
{
    public class HealthHandlerBase : HealthHandler
    {
        public void OnWarriorAttack(Warrior warrior)
        {
            HealthChange(-(int)warrior.Damage);
        }

        public override void Death()
        {
            base.Death();
            Destroy(gameObject);
        }
    }
}