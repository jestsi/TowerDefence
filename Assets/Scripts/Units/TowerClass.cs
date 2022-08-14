using UnityEngine;

public class TowerClass : Unit
{
    private int _price;
    private Vector3 _maxRotateAngles;
    private Vector3 _minRotateAngles;
    private float _radiusTriggerZone;
    private float _rotateSpeed;
    private int _riffleCount;
    private int _reloadTimeInMS;

    public int ReloadTimeInMS { get => _reloadTimeInMS; set => _reloadTimeInMS = value; }
    public int RiffleCount
    {
        get { return _riffleCount; }
        set { _riffleCount = value; }
    }
    public float RotateSpeed
    {
        get { return _rotateSpeed; }
        set { _rotateSpeed = value; }
    }
    public float RadiusTriggerZone { get => _radiusTriggerZone; set => _radiusTriggerZone = value > 0 ? value : 1; }
    public Vector3 MaxRotateAngles { get => _maxRotateAngles; set => _maxRotateAngles = value; }
    public Vector3 MinRotateAngles { get => _minRotateAngles; set => _minRotateAngles = value; }
    public int Price { 
        get => _price; 
        set {
            _price = value >= 0 ? value : 0;
        }
    }

    public TowerClass()
    {
        Damage = 1;
        _price = 1;
        _maxRotateAngles = new Vector3(360, 360, 360);
        _minRotateAngles = new Vector3(-360, -360, -360);
        _radiusTriggerZone = 1;
        _rotateSpeed = 1;
        PeriodBeforeAttackInSec = 500;
        _riffleCount = 50;
        _reloadTimeInMS = 300;
        Position = new Vector2Int(0, 0);
        Health = -1;
    }

    public TowerClass(Vector3 maxRotateAngle, Vector3 minRotateAngle, Vector2Int position,
        float damage = 1, int price = 1, float radiusTriggerZone = 1,
        int reloadTimeInMS = 300, float rotateSpeed = 3, float periodBeforeAttackInMs = 500, int riffleCount = 50) : this()
    {
        _maxRotateAngles = maxRotateAngle;
        _minRotateAngles = minRotateAngle;
        Damage = damage;
        _price = price;
        _radiusTriggerZone = radiusTriggerZone;
        PeriodBeforeAttackInSec = periodBeforeAttackInMs;
        _rotateSpeed = rotateSpeed;
        _reloadTimeInMS = reloadTimeInMS;
        _riffleCount = riffleCount;
        Position = position;
    }

}
