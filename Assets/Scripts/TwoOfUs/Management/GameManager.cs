using Flusk.Patterns;
using UnityEngine;

namespace TwoOfUs.Management
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        [SerializeField]
        protected LevelManager levelManagerTemplate;
        
        protected virtual void Start()
        {
            if (LevelManager.Instance == null)
            {
                LevelManager levelManager = Instantiate(levelManagerTemplate);
                levelManager.Force();
            }
            
        }
    }
}