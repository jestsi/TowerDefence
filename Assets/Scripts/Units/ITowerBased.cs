namespace Units
{
    public enum TowerTerritoryTargetE
    {
        AerialTargets,
        GroundTargets,
        Gybrid = -1
    }

    public interface ITowerBased : ISellUnit
    {
        public int CountTargets { get; set; }
        public int MaximumTargets { get; set; }
        public float PeriodAttack { get; set; }
        public float Damage { get; set; }
        public float TriggerRadius { get; set; }
        public TowerTerritoryTargetE TerritoryTarget { get; set; }
    }
}