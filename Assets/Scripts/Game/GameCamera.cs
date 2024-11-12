using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GooberBacteria
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] Vector3 cameraOffset = new Vector3(0, 0, -10);
        [SerializeField] float lerpWeight = .7f;

        Transform followTarget;

        void Start()
        {
            followTarget = Player.instance.transform;
        }


        void Update()
        {
            transform.position = (Vector3)Vector2.Lerp(transform.position, followTarget.transform.position, lerpWeight*Time.deltaTime) + cameraOffset;
        }

    }
}
