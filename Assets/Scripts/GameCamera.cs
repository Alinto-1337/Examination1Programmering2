using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] Vector3 cameraOffset = new Vector3(0, 0, -10);

    Transform followTarget;


    #region UNITY_EVENTS
    void Start()
    {
        followTarget = Player.instance.transform;
    }


    void Update()
    {
        transform.position = followTarget.transform.position + new Vector3(0,0,-10);
    }

    #endregion
}
