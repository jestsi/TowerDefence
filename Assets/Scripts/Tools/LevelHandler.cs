using System.Collections;
using System.Globalization;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Tools
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private Text _countdownText;

        [Header("Warriors")] [SerializeField] private WarriorType[] _warriorTypes;

        [Header("Level settings")] [SerializeField]
        private float _secondBeforeStartLevel;


        private static bool _isPaused;
        private Ray _ray;
        public static bool LevelStarted;

        public static bool IsPaused
        {
            get => _isPaused;
            set => _isPaused = value;
        }

        private void Awake()
        {
            _isPaused = false;
            _countdownText.text = _secondBeforeStartLevel.ToString(CultureInfo.InvariantCulture);
        }

        private void Start()
        {
            GameObject.FindWithTag("Destination").GetComponent<Base>().Health.Dead += Gameover;

            StartCoroutine(nameof(StartPeriodBeforeStartLevel));
        }

        private IEnumerator Countdown()
        {
            while (!LevelStarted)
            {
                yield return new WaitForSeconds(1);

                var text = int.Parse(_countdownText.text);
                _countdownText.text = (--text).ToString();
            }
        }

        private IEnumerator StartPeriodBeforeStartLevel()
        {
            StartCoroutine(Countdown());
            yield return new WaitForSeconds(_secondBeforeStartLevel);

            LevelStarted = true;

            StartSpawnWarriors();

            HideHideblesObjects();
        }

        private void Gameover(float f)
        {
        }

        private static void HideHideblesObjects()
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag("Hideble"))
            {
                if (obj.TryGetComponent(out MeshRenderer rend))
                    rend.enabled = false;
            }
        }

        private void StartSpawnWarriors()
        {
            foreach (var warrior in _warriorTypes)
            {
                StartCoroutine(nameof(SpawnWarrior), warrior);
            }
        }

        private IEnumerator SpawnWarrior(WarriorType war)
        {
            var countSpawnWarriors = 0;

            while (war.CountWarriors > countSpawnWarriors)
            {
                yield return new WaitForSeconds(war.SecondsForSpawn);

                Instantiate(war.WarriorPrefab, war.SpawnPosition, war.WarriorPrefab.transform.rotation);

                ++countSpawnWarriors;
            }
        }
    }
}