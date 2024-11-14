using UnityEngine;


namespace GooberBacteria
{
    public class HunterVirus : BacteriaBase
    {
        [SerializeField] private static float startMoveSpeed = 5f;  // Serialized move speed
        private static float initialMoveSpeed = 0;  // Stores the original speed for resetting
        [SerializeField] private float moveSpeed;
        private Player player;

        void Start()
        {
            // Store the original move speed for reset functionality
            if (initialMoveSpeed == 0)
                initialMoveSpeed = startMoveSpeed;

            moveSpeed = startMoveSpeed;

            player = Player.instance;

        }

        void Update()
        {
            if (player == null) return;
            transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)player.transform.position, moveSpeed * Time.deltaTime);

        }

        internal static void MultiplyMoveSpeed(float mul)
        {
            startMoveSpeed *= mul; // Difficulty scaling
        }

        internal static void ResetMoveSpeed()
        {
            startMoveSpeed = initialMoveSpeed;  // Reset to the original speed
        }
    }
}
