using UnityEngine;

namespace Tools
{
    public class TowerOrMineSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        public void SetMine(GameObject minePrefab)
        {
            if (!SelectCell.LastCellSelect.CanMinePlace) return;

            var position = SelectCell.LastCellSelect.transform.position;
            Instantiate(minePrefab, position, Quaternion.identity, _parent);
        }
    }
}