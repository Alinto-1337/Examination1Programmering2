using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GooberBacteria
{
    public class Player : MonoBehaviour
    {
        #region Fields

        [Tooltip("How Many Units per Second the Player moves")]
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private GameObject PlayerDeathVFX_Prefab;

        public static Player instance;

        private Vector2 moveVector;
        private float health;
        private bool damageTakenImunity;

        private GameManager gameManager;
        private GameCamera gameCamera;
        private Rigidbody2D rigidbody2d;
        private InputSystem_Actions inputActions;


        #endregion

        #region Initialization

        private void Awake()
        {
            if (instance == null || instance == this)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            gameManager = GameManager.instance;
            gameCamera = GameCamera.instance;
            rigidbody2d = GetComponent<Rigidbody2D>();
            InitiateInput();

            health = maxHealth;

        }

        private void InitiateInput()
        {
            inputActions = new InputSystem_Actions();
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += HandleMovePerformed;
            inputActions.Player.Move.canceled += HandleMoveCanceled;
        }

        #endregion

        #region Movement

        private void FixedUpdate()
        {
            if (moveVector != Vector2.zero)
            {
                MovePlayer();
            }
        }

        private void MovePlayer()
        {
            rigidbody2d.linearVelocity = moveVector * movementSpeed;

            // Rotation
            float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }

        private void HandleMovePerformed(InputAction.CallbackContext context)
        {
            moveVector = context.ReadValue<Vector2>();
        }

        private void HandleMoveCanceled(InputAction.CallbackContext context)
        {
            moveVector = Vector2.zero;
        }

        #endregion

        #region Damage Handling

        private void TakeDamage(int _dmg)
        {
            if (damageTakenImunity) return;

            gameCamera.DoScreenShake(5);

            health -= _dmg;
            if (health <= 0)
            {
                gameManager.BackToMenu();
                Die();
            }
            gameManager.healthBarImage.fillAmount = health / maxHealth;

            damageTakenImunity = true;
            Invoke(nameof(DisableImunity), 1f);
        }

        private void DisableImunity()
        {
            damageTakenImunity = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            HunterVirus hunterVirus = other.gameObject.GetComponent<HunterVirus>();
            if (hunterVirus != null)
            {
                TakeDamage(1);
            }
        }

        private void Die()
        {
            PlayDeathVFX();

            Destroy(gameObject);
            gameObject.SetActive(false);
        }

        private void PlayDeathVFX()
        {
            if (PlayerDeathVFX_Prefab != null) Destroy(Instantiate(PlayerDeathVFX_Prefab), 3f);
        }

        #endregion

        #region Cleanup

        private void OnDestroy()
        {
            inputActions.Disable();
        }

        #endregion
    }
}
