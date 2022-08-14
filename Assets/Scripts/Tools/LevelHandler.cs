using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class LevelHandler : MonoBehaviour
    {
        [Header("Warrior - 1")]
        [SerializeField] private GameObject _warriorPrefab;
        [SerializeField] private float _secondsForSpawn;
        [SerializeField] private Transform _spawnTransformPosition;
        [SerializeField] private int _countWarriors;

        private static bool _isPaused;
        private static Timer _timer;
        private Ray _ray;

        public static bool IsPaused { get => _isPaused; set => _isPaused = value; }

        private void Start()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            _isPaused = false;
            _warriorPrefab.transform.position = _spawnTransformPosition.position;

            _timer = gameObject.AddComponent<Timer>();
            int currentCountWarriors = 0;
            Coroutine corout = null;
            corout = _timer.StartTimer(() => {
                var disableBadClosure = currentCountWarriors;
                if (disableBadClosure >= _countWarriors)
                    StopCoroutine(corout);
                Instantiate(_warriorPrefab);
                ++currentCountWarriors;
            }, _secondsForSpawn);

            foreach (var obj in GameObject.FindGameObjectsWithTag("Hideble"))
            {
                if (obj.TryGetComponent(out MeshRenderer rend))
                    rend.enabled = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _ray.direction *= 100f;
            Gizmos.DrawRay(_ray);
        }

        private void Update()
        {
            _ray.direction *= 10;
            RayMouse();
        }

        private void RayMouse ()
        {
            if (Physics.Raycast(_ray, out var hit, 100f, 3))
            {
                if (hit.transform.TryGetComponent(out Cell cell))
                {
                    cell.ChangeColor(cell.HoverColor);
                    _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.Log("msg");
                    return;

                }
                else
                {
                    _ray.origin = hit.transform.position;
                    Debug.Log("msg2");
                    return;
                }
            } else
            {
                _ray.origin = hit.transform.position;
                Debug.Log("msg3");
                return;

            }
        }
    }
}
