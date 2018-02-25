using Flusk.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoOfUs.Management
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        [SerializeField]
        protected LevelManager levelManagerTemplate;

        protected virtual void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        protected virtual void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected virtual void Start()
        {
            CreateLevelManager();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            CreateLevelManager();
        }

        private void CreateLevelManager()
        {
            if (LevelManager.Instance == null)
            {
                LevelManager levelManager = Instantiate(levelManagerTemplate);
                levelManager.Force();
            }
        }
    }
}