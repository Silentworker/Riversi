using UnityEngine;

namespace Assets.Scripts.sw.core.pool
{
    public class PoolSubject: MonoBehaviour
    {
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
