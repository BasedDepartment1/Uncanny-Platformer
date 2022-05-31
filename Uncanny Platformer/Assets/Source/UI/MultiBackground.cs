using System;
using UnityEngine;

namespace Source.UI
{
    public class MultiBackground : MonoBehaviour
    {
        [SerializeField] private GameObject background1;
        [SerializeField] private GameObject background2;

        private void Start()
        {
            background1.SetActive(true);
            background2.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Zone1"))
            {
                background1.SetActive(true);
                background2.SetActive(false);
            }
            else if (other.CompareTag("Zone2"))
            {
                background1.SetActive(false);
                background2.SetActive(true);
            }
        }

        private void Swap()
        {
            background1.SetActive(!background1.activeSelf);
            background2.SetActive(!background2.activeSelf);
        }
    }
}
