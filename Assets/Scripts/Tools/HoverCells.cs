using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class HoverCells : MonoBehaviour
    {
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            /*var ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && !hit.collider.CompareTag("Ignore Raycast"))
            {
                if (hit.rigidbody.gameObject.TryGetComponent<Cell>(out Cell cell))
                {
                    cell.ChangeColor(cell.HoverColor);
                }
            }*/
        }
    }
}