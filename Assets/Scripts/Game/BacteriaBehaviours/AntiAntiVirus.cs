using UnityEngine;


namespace GooberBacteria
{
    public class AntiAntiVirus : BacteriaBase
    {
        [SerializeField] private float moveSpeed = 1.0f;  // Movement speed of the HunterVirus


        AntiVirus closestAntiVirus;

        private void Start()
        {
            closestAntiVirus = FindClosestAntiVirus();
        }
        private void Update()
        {
            MoveTowardsClosestAntiVirus();
        }

        private void MoveTowardsClosestAntiVirus()
        {
            if (closestAntiVirus != null)
            {
                // Move towards the closest AntiVirus position
                Vector2 direction = (closestAntiVirus.transform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, closestAntiVirus.transform.position, moveSpeed * Time.deltaTime);
            }
        }

        private AntiVirus FindClosestAntiVirus()
        {
            AntiVirus closestAntiVirus = null;
            float shortestDistance = Mathf.Infinity;

            foreach (AntiVirus antiVirus in AntiVirus.AntiVirusesList)
            {
                if (antiVirus == null) continue;

                float distance = Vector2.Distance(transform.position, antiVirus.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestAntiVirus = antiVirus;
                }
            }

            return closestAntiVirus;
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            // AntiVirus Detection.. Kills Antiviruses
            AntiVirus antiVirus = other.gameObject.GetComponent<AntiVirus>();
            if (antiVirus != null)
            {
                antiVirus.transform.localScale /= 2;
                this.Die();
            }

            // Player Detection.. Kills this on contact
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                this.Die();
            }
        }
    }
}
