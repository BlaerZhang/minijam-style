using UnityEngine;
using UnityEngine.SceneManagement;

namespace Globals
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public AudioManager audioManager;
        public LevelManager levelManager;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            audioManager = GetComponentInChildren<AudioManager>();
            levelManager = GetComponentInChildren<LevelManager>();
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
