using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class TimedButton : MonoBehaviour
    {
        private float currentTime;
        [SerializeField] public Button button;
    
        public void Start()
        {
            currentTime = 0;
            button.interactable = false;
        }

        void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 15)
            {
                button.interactable = true;
            }
        }
    }
}
