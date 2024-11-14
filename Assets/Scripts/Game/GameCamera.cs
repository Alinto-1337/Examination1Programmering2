using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GooberBacteria
{
    public class GameCamera : MonoBehaviour
    {
        internal static GameCamera instance;

        [SerializeField] Vector3 cameraOffset = new Vector3(0, 0, -10);
        [SerializeField] float lerpWeight = .7f;
        [SerializeField] float shakeDuration = 0.5f;
        [SerializeField] float shakeMagnitude = 0.2f;

        Transform followTarget;

        private void Awake()
        {
            if (instance == null) 
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            followTarget = Player.instance.transform;
        }


        void Update()
        {
            if (followTarget == null) return;
            transform.position = (Vector3)Vector2.Lerp(transform.position, followTarget.transform.position, lerpWeight*Time.deltaTime) + cameraOffset;
        }

        public void DoScreenShake(float intensity = 1)
        {
            // Start the screen shake with the specified intensity
            StartCoroutine(ScreenShakeCoroutine(intensity));
        }

        private IEnumerator ScreenShakeCoroutine(float intensity)
        {
            float elapsed = 0.0f;

            while (elapsed < shakeDuration)
            {
                Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude * intensity;
                transform.position = transform.position + cameraOffset + randomOffset;

                elapsed += Time.deltaTime;
                yield return null;
            }

            if (followTarget == null) yield break;
            // Return camera to the intended position after shaking
            transform.position = followTarget.position + cameraOffset;
        }

    }
}
