using UnityEngine;

namespace Units.Towers
{
    public class MineHelper : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (transform.parent != null)
                transform.parent.GetComponent<Mine>().Boom();
        }
    }
}