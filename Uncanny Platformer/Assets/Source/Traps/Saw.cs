using UnityEngine;

namespace Source.Traps
{
    public class Saw : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 2f;
        
        private void Update()
        {
            transform.Rotate(0f, 0f, 360 * Time.deltaTime * rotationSpeed);
        }
    }
}
