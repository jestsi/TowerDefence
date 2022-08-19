using Assets.Scripts;
using UnityEngine;

namespace Tools
{
    public class SelectCell : MonoBehaviour
    {
        [SerializeField] private RectTransform _menu;

        private Camera _mainCam;
        public static Cell LastCellSelect { get; set; }
        private static bool CanSelectCell => LastCellSelect != null;

        private void Awake()
        {
            _mainCam = Camera.main;
        }

        private void FixedUpdate()
        {
            if (!LevelHandler.LevelStarted) return;
            MouseDownHandler();
            KeyboardHandler();
        }

        private void MouseDownHandler()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RayMouse();
            }
        }

        private void KeyboardHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && CanSelectCell)
            {
                LastCellSelect.ChangeColor(LastCellSelect.DefoultColor);
                LastCellSelect.IsSelect = false;
                LastCellSelect = null;
                SetStateMenu(CanSelectCell);
            }
        }

        private void RayMouse()
        {
            var origin = _mainCam.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(origin, out var hit, 1000f)) return;
            if (hit.transform.TryGetComponent(out Cell selected) && !CanSelectCell && selected.CanSelectThis)
            {
                LastCellSelect = selected;
                LastCellSelect.IsSelect = CanSelectCell;
                SetStateMenu(CanSelectCell);
            }
        }

        private void SetStateMenu(bool enable)
        {
            if (enable)
            {
                var position = LastCellSelect.transform.position;
                _menu.anchoredPosition = new Vector2(position.x, position.z);
            }

            _menu.gameObject.SetActive(enable);
        }
    }
}