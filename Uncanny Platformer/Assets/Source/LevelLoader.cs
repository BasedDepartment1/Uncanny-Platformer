using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source
{
    public class LevelLoader : MonoBehaviour
    {
        public string levelToLoad;
        
        public void OnTriggerEnter2D(Collider2D collider2D)
        {
            var colliderGameObject = collider2D.gameObject;

            if (colliderGameObject.CompareTag("Player"))
            {
                LoadScene();
            }
        }

        void LoadScene() => SceneManager.LoadScene(levelToLoad);
    }
} 
    
