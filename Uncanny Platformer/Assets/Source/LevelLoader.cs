using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source
{
    public class LevelLoader : MonoBehaviour
    {

        public void OnTriggerEnter2D(Collider2D collider2D)
        {
            var colliderGameObject = collider2D.gameObject;

            if (colliderGameObject.CompareTag("Player"))
            {
                Cursor.visible = true;
                LoadScene();
            }
        }

        void LoadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
} 
    
