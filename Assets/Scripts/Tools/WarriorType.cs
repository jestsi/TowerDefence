using UnityEngine;

namespace Tools
{
    public class WarriorType : MonoBehaviour
    {
        public GameObject WarriorPrefab
        {
            get => _warriorPrefab;
            set => _warriorPrefab = value;
        }

        public float SecondsForSpawn
        {
            get => _secondsForSpawn;
            set => _secondsForSpawn = value;
        }

        public int CountWarriors
        {
            get => _countWarriors;
            set => _countWarriors = value;
        }

        public Vector3 SpawnPosition
        {
            get => _spawnPosition;
            set => _spawnPosition = value;
        }
        
        [SerializeField] private GameObject _warriorPrefab;
        [SerializeField] private float _secondsForSpawn;
        [SerializeField] private int _countWarriors;
        [SerializeField] private Vector3 _spawnPosition;
    }
}