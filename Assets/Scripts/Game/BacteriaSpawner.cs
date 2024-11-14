using System.Collections;
using UnityEditor;
using UnityEngine;

namespace GooberBacteria
{
    internal class BacteriaSpawner : MonoBehaviour
    {
        #region Fields

        [Header("Runtime Bacteria Spaawning")]
        [SerializeField] private float TimeBetweenWaves = 2f;
        [SerializeField] private PrefabWithProbability[] prefabsWithProbabilities;


        [Header("Static Bacteria Spaawning")]
        [SerializeField] private Sprite[] staticBacteriaSprites;
        [SerializeField] private int amountOfStaticBacteriaToSpawn;
        [SerializeField] private GameObject staticBacteriaPrefab;
        [SerializeField] private Vector2 staticBacteriaSizeRange;

        [Header("Difficulty Scaling")]
        [SerializeField] private float timeBetweenWavesMultiplierPerWave = 0.991f;
        [SerializeField] private float HunterMoveSpeedMultiplierPerWave = 1.001f;

        [Header("Bacteria Containers")]
        [Tooltip("RuntimeBacteria will be spawned as children of this transform")]
        [SerializeField] Transform runtimeBacteriaParent;
        [Tooltip("StaticBacteria will be spawned as children of this transform")]
        [SerializeField] Transform staticBacteriaParent;


        private GameManager gameManager;
        private CircleCollider2D mapCollider;

        #endregion

        #region Initialization

        private void Start()
        {
            gameManager = GameManager.instance;
            mapCollider = gameManager.mapBoundsCollider;

            // Setup static bacteria
            SpawnStaticBacteria();

            // Start Spawning Runtime Bacteria
            StartCoroutine(SpawnWaveRoutine());
        }

        #endregion

        #region StaticBacteria

        private void SpawnStaticBacteria()
        {
            for (int i = 0; i < amountOfStaticBacteriaToSpawn; i++)
            {
                // Spawn bacteria at random position
                Vector2 spawnPos = GameHelper.GetRandomPosInCollider(mapCollider);
                GameObject bacteriaInstance = Instantiate(staticBacteriaPrefab, spawnPos, Quaternion.identity, staticBacteriaParent);

                // Randomize the sprite of the bacteria
                Sprite sprite = GetRandomStaticBacteriaSprite();
                bacteriaInstance.GetComponent<SpriteRenderer>().sprite = sprite;

                // Rndomize the size of the bacteria
                float scale = Random.Range(staticBacteriaSizeRange.x, staticBacteriaSizeRange.y);
                bacteriaInstance.transform.localScale = new Vector2(scale, scale);
            }
        }

        private Sprite GetRandomStaticBacteriaSprite()
        {
            return staticBacteriaSprites[UnityEngine.Random.Range(0, staticBacteriaSprites.Length-1)]; 
        }

        #endregion

        #region RuntimeBacteria

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
                    Instantiate(prefabData.prefab, spawnPosition, Quaternion.identity, runtimeBacteriaParent);
                    break;
                }
            }
        }

        #endregion

        #region Structs

        [System.Serializable]
        public struct PrefabWithProbability
        {
            [SerializeField] internal GameObject prefab;
            [SerializeField, Range(0, 1)] internal float spawnProbability;
        }

        #endregion
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
