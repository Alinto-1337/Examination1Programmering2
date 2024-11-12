using System.Collections;
using UnityEditor;
using UnityEngine;

namespace GooberBacteria
{
    internal class BacteriaSpawner : MonoBehaviour
    {
        [SerializeField] private float TimeBetweenWaves = 2f;
        [SerializeField] private PrefabWithProbability[] prefabsWithProbabilities;
        [SerializeField] private float timeBetweenWavesMultiplierPerWave = 0.991f;
        [SerializeField] private float HunterMoveSpeedMultiplierPerWave = 1.001f;

        private GameManager gameManager;
        private CircleCollider2D mapCollider;


        private void Start()
        {
            gameManager = GameManager.instance;
            mapCollider = gameManager.mapBoundsCollider;
            StartCoroutine(SpawnWaveRoutine());
        }


        private IEnumerator SpawnWaveRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(TimeBetweenWaves);
                SpawnPrefabBasedOnProbability();
                TimeBetweenWaves *= timeBetweenWavesMultiplierPerWave; // Difficulty Scaling
                HunterVirus.MultiplyMoveSpeed(HunterMoveSpeedMultiplierPerWave);
            }
        }


        internal void SpawnPrefabBasedOnProbability()
        {
            float randomValue = Random.value;
            float cumulativeProbability = 0f;

            foreach (var prefabData in prefabsWithProbabilities)
            {
                cumulativeProbability += prefabData.spawnProbability;

                if (randomValue <= cumulativeProbability)
                {
                    Vector2 spawnPosition = GameHelper.GetRandomPosInCollider(mapCollider);
                    Instantiate(prefabData.prefab, spawnPosition, Quaternion.identity);
                    break;
                }
            }
        }


        [System.Serializable]
        public struct PrefabWithProbability
        {
            [SerializeField] internal GameObject prefab;
            [SerializeField, Range(0, 1)] internal float spawnProbability;
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(BacteriaSpawner))]
    internal class BacteriaSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Spawn Prefab"))
            {
                BacteriaSpawner spawner = (BacteriaSpawner)target;
                spawner.SpawnPrefabBasedOnProbability();
            }
        }
    }
#endif
}
