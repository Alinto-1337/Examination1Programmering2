using UnityEngine;
using UnityEngine.UI;

namespace GooberBacteria
{
    internal class GameManager : MonoBehaviour
    {
        internal static GameManager instance;

        internal Image healthBarImage;


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                gameObject.SetActive(false);
                return;
            }

            instance = this;
        }
        


    }
}
