using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string LevelToLoad;

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        var colliderGameObject = collider2D.gameObject;

        if (colliderGameObject.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    void LoadScene() => SceneManager.LoadScene(LevelToLoad);
} 
    
