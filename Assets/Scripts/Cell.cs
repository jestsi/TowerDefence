using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public enum CellTypeE
    {
        Red,
        Green,
        Blue
    }

    public class Cell : MonoBehaviour
    {
        [Header("Colors on mouse events")] [SerializeField]
        private Color _defaoultColor;

        [SerializeField] private Color _hoverColor;
        [SerializeField] private Color _selectColor;

        [SerializeField] private MeshRenderer _meshRenderer;

        [Header("State info")] [SerializeField]
        private bool _isEmpty;

        [SerializeField] private CellTypeE _cellType;
        [SerializeField] [UsedImplicitly] private bool _canTowerPlace;
        [SerializeField] [UsedImplicitly] private bool _canMin�Place;
        [SerializeField] [UsedImplicitly] private bool _canWarriorMovingThis;
        [SerializeField] private bool _canSelectThis;

        private GameObject _unit;
        private bool _isSelect;

        public int X { get; set; }
        public int Y { get; set; }
        public bool IsEmpty => _isEmpty;

        public bool IsSelect
        {
            get => _isSelect;
            set
            {
                _isSelect = value;
                ChangeColor(value ? SelectColor : DefoultColor);
            }
        }

        public Color DefoultColor => _defaoultColor;
        public Color HoverColor => _hoverColor;
        public Color SelectColor => _selectColor;
        public CellTypeE CellType => _cellType;

        public bool CanTowerPlace => _canTowerPlace;
        public bool CanMinePlace => _canMin�Place;
        public bool CanSelectThis => _canSelectThis;

        private void Start()
        {
            _unit = null;

            X = gameObject.name.Split(' ')[0][^1];
            Y = gameObject.name.Split(' ')[1][^1];
            SetSettingsForOneOfType();
            ApplyingSettingsForCellType();
        }

        private void Update()
        {
            if (Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 5f) &&
                !hit.collider.isTrigger &&
                !hit.collider.CompareTag("Ignore Raycast"))
            {
                _unit = hit.transform.gameObject;
            }
            else
                _unit = null;

            _isEmpty = _unit == null;
        }

        private void SetSettingsForOneOfType()
        {
            _canTowerPlace = false;
            _canWarriorMovingThis = false;
            _canSelectThis = false;
            _canMin�Place = false;
            switch (_cellType)
            {
                case CellTypeE.Green:
                    _canMin�Place = true;
                    _canWarriorMovingThis = true;
                    _canSelectThis = true;
                    break;
                case CellTypeE.Blue:
                    _canTowerPlace = true;
                    _canWarriorMovingThis = true;
                    _canSelectThis = true;
                    _canMin�Place = true;
                    break;
            }
        }

        private void ApplyingSettingsForCellType()
        {
        }

#if DEBUG
        private void OnDrawGizmos()
        {
            var alpha = 0.2f;

            Gizmos.color = _cellType switch
            {
                CellTypeE.Red => Color.red,
                CellTypeE.Blue => Color.blue,
                CellTypeE.Green => Color.green,
                _ => Color.grey
            };

            Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, alpha);

            var position = transform.position;

            Gizmos.DrawCube(position, new Vector3(2, .1f, 2));
            Gizmos.color = Color.gray;
            Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, alpha);
            Gizmos.DrawCube(position, new Vector3(1.8f, .01f, 1.8f));
        }
#endif
        public static bool CellIsEmptyOrNull(Cell cell) => cell != null && cell.IsEmpty;
        public bool CellIsEmptyOrNull() => this != null && IsEmpty;

        public void ChangeColor(Color color)
        {
            _meshRenderer.material.color = color;
        }
    }
}