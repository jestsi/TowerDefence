namespace Units
{
    public class HealthHandlerBase : HealthHandler
    {
        public void OnWarriorAttack(Warrior warrior)
        {
            HealthChange(-(int)warrior.Damage);
        }
    }
}
