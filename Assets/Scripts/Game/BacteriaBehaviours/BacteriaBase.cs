using UnityEngine;

namespace GooberBacteria
{
    public class BacteriaBase : MonoBehaviour
    {
        [SerializeField] private GameObject deathVFX_Prefab;

        public void Die()
        {
            Destroy(gameObject);
            gameObject.SetActive(false);

            DeathVFX();
        }

        void DeathVFX()
        {
            if (deathVFX_Prefab != null) Destroy(Instantiate(deathVFX_Prefab, transform.position, Quaternion.identity), 3f); // 3f hardcoded. the time unti the vfx ressidue is cleaned up.
        }
    }
}
