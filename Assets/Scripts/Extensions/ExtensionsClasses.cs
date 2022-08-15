using Units;
using UnityEngine;

namespace Extensions
{
    public static class ExtensionsClasses
    {
        public static (int, int) Sum(this (int, int) tuple, (int, int) tuple2)
        {
            return (tuple.Item1 + tuple.Item1, tuple.Item2 + tuple2.Item2);
        }

        public static (int, int) AddNumber(this (int, int) tuple, int number)
        {
            return (tuple.Item1 + number, tuple.Item2 + number);
        }

        public static (int, int) MinusNumber(this (int, int) tuple, int number)
        {
            return (tuple.Item1 - number, tuple.Item2 - number);
        }

        public static bool IsWarrior(Collider collider) => collider.CompareTag("Warrior");
        
        public static bool IsWarrior(Collider collider, out Warrior warrior)
        {
            var isWarrior = collider.CompareTag("Warrior");
            warrior = isWarrior ? collider.GetComponent<Warrior>() : null;
            return isWarrior;
        }
    }
}
