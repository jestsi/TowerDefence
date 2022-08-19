using UnityEngine;

namespace Units.Towers
{
    public class FiregunTower : TowerUsing
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem.Stop();
        }

        protected override void SetTarget(Collider other)
        {
            base.SetTarget(other);
            _particleSystem.Play();
        }

        protected override void StopShoot(Warrior warrior)
        {
            base.StopShoot(warrior);
            _particleSystem.Stop();
        }
    }
}