
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Source.UI
{
    public class Cutscene1 : MonoBehaviour
    {
        private string scene = "Leve2";
        public void NextScene()
        {
            SceneManager.LoadScene(scene);
        } 
    }
}