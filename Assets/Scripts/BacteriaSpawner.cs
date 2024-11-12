using System;
using UnityEngine;

namespace GooberBacteria
{


    internal class BacteriaSpawner : MonoBehaviour
    {
        [Serializable]
        private struct WaveSpawnData
        {

        }

        [SerializeField] WaveSpawnData waveSpawnData;

    }
}
