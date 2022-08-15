using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Tools
{
    public class LevelHandler : MonoBehaviour
    {
        [Header("Warriors")] 
        [SerializeField] private WarriorType[] _warriorTypes;
        
        private static bool _isPaused;
        private static Timer _timer;
        private Ray _ray;

        public static bool IsPaused { get => _isPaused; set => _isPaused = value; }

        private void Awake()
        {
            _isPaused = false;

            _timer = gameObject.AddComponent<Timer>();
        }

        private void Start()
        {
            StartSpawnWarriors();

            HideHideblesObjects();
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
            for (var i = 0; i < _warriorTypes.Length; i++)
            {
                StartCoroutine(nameof(SpawnWarrior), _warriorTypes[i]);
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
        
        /*private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _ray.direction *= 100f;
            Gizmos.DrawRay(_ray);
        }*/

        private void Update()
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RayMouse();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void RayMouse ()
        {
            if (!Physics.Raycast(_ray, out var hit, 100f, 3)) return;
            
            if (hit.transform.TryGetComponent(out Cell cell))
            {
                cell.ChangeColor(cell.HoverColor);
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.Log("msg");
                return;
            }

            _ray.origin = hit.transform.position;
            Debug.Log("msg2");
        }
    }
}
