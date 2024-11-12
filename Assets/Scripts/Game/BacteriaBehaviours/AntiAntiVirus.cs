using UnityEngine;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GooberBacteria
{
    public class AntiAntiVirus : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1.0f;  // Movement speed of the HunterVirus

        private void Update()
        {
            MoveTowardsClosestAntiVirus();
        }

        private void MoveTowardsClosestAntiVirus()
        {
            AntiVirus closestAntiVirus = FindClosestAntiVirus();

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
            AntiVirus antiVirus = other.gameObject.GetComponent<AntiVirus>();
            if (antiVirus != null)
            {
                antiVirus.transform.localScale /= 2;
                Destroy(gameObject);
            }
        }
    }
}
