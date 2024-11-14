using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GooberBacteria
{
    public class AntiVirus : BacteriaBase
    {
        static List<AntiVirus> antiVirusesList = new List<AntiVirus>();
        internal static List<AntiVirus> AntiVirusesList => antiVirusesList;

        [SerializeField] private float moveSpeed = 0.5f;
        [SerializeField] private Vector2 timeRangeUntilNewTargetDirection = new Vector2(1f, 3f);  // Time range between 1 and 3 seconds
        [SerializeField] private Vector2 scaleIncrease = new Vector2(.1f, .1f);

        private Vector2 targetPosition;  // Target position to move towards
        private float timeToNextChange;  // Time until next direction change
        private CircleCollider2D mapBoundsCollider;

        void Start()
        {
            antiVirusesList.Add(this);

            mapBoundsCollider = GameManager.instance.mapBoundsCollider;

            // Initialize the first target position
            SetNewTargetPosition();

            // Start the coroutine for random target changes
            StartCoroutine(ChangeTargetDirection());
        }

        void Update()
        {
            // Move towards the target position
            MoveTowardsTarget();
        }

        private void OnDestroy()
        {
            antiVirusesList.Remove(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            HunterVirus huntervirus = other.gameObject.GetComponent<HunterVirus>();
            if (huntervirus == null) return;

            huntervirus.Die();
            transform.localScale += (Vector3)scaleIncrease;

        }

        private void MoveTowardsTarget()
        {
            // Move towards the target using MoveTowards for smooth movement
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        private void SetNewTargetPosition()
        {
            targetPosition = GameHelper.GetRandomPosInCollider(mapBoundsCollider);
        }

        private IEnumerator ChangeTargetDirection()
        {
            while (true)
            {
                // Wait for a random time between the range specified in timeRangeUntilNewTargetDirection
                timeToNextChange = Random.Range(timeRangeUntilNewTargetDirection.x, timeRangeUntilNewTargetDirection.y);
                yield return new WaitForSeconds(timeToNextChange);

                // Change target position after the wait
                SetNewTargetPosition();
            }
        }
    }
}
