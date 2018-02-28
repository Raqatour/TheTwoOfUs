using Flusk.Patterns;
using UnitySceneManager = UnityEngine.SceneManagement;

namespace TwoOfUs.Management.Flow
{
    public class SceneManager : PersistentSingleton<SceneManager>
    {
        public string CurrentScene
        {
            get { return UnitySceneManager.SceneManager.GetActiveScene().name; }
        }
        
        public void Load(string sceneName)
        {
            UnitySceneManager.SceneManager.LoadScene(sceneName);
        }
    }
}