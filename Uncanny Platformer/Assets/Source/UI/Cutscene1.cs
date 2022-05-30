
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Source.UI
{
    public class Cutscene1 : MonoBehaviour
    {
        
        [SerializeField] private string scene;
        public void NextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 
    }
}