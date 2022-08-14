using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tools.UnityEditor
{
    public class CellGizmoz : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(Vector3.zero, new Vector3(transform.localScale.x, 0, transform.localScale.z));
        }
    }
}