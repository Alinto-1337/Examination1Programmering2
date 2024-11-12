using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GooberBacteria
{
    internal class GameManager : MonoBehaviour
    {
        // Static Fields
        internal static GameManager instance { get; private set; }

        // Asset References
        [SerializeField, Scene] internal string mainMenuScene;
        [SerializeField, Scene] internal string winScene;

        // Scene References
        [SerializeField] internal Image healthBarImage;
        [SerializeField] internal CircleCollider2D mapBoundsCollider;
        [SerializeField] internal TMP_Text timerText;

        private float startTime; 
        private float timePlayed = 0;


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                gameObject.SetActive(false);
                return;
            }

            instance = this;

            startTime = Time.time;
        }

        private void Update()
        {
            timePlayed = Time.time - startTime;
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            timerText.text = Mathf.Floor(timePlayed).ToString() + "s";
            if (timePlayed > 30f)
            {
                timerText.color = Color.green;
            }
        }

        internal void BackToMenu()
        {
            HunterVirus.ResetMoveSpeed();
            if (timePlayed > 30)
            {
                SceneManager.LoadScene(winScene);
            }
            else
            {
                SceneManager.LoadScene(mainMenuScene);
            }
        }
    }
}
