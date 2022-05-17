using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;
    
        [Header("Images")]
        [SerializeField] private Image totalHealthBar;
        [SerializeField] private Image currentHealthBar;
    
        void Start()
        {
            totalHealthBar.fillAmount = playerHealth.CurrentHealth / 100;
        }
    
        void Update()
        {
            currentHealthBar.fillAmount = playerHealth.CurrentHealth / 100;
        }
    }
}
