using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBarFilling;
        [SerializeField] private HealthHandler _health;

        private Camera _camera;

        private void Awake()
        {
            _health.HealthChanged += OnHealthChanged;
            _camera = Camera.main;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float value)
        {
            _healthBarFilling.fillAmount = value;
        }

        private void LateUpdate()
        {
            var transform1Position = _camera.transform.position;
            transform.LookAt(new Vector3(transform1Position.x, transform1Position.y+30f, transform1Position.z));
            transform.Rotate(0, 180, 0);
        }
    }
}
