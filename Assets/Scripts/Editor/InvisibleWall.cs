using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tools.UnityEditor
{
    public class InvisibleWall : MonoBehaviour
    {

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var alpha = 0.2f;
            Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, alpha);
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        }
    }
}