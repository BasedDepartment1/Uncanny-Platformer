using UnityEngine;

namespace Source.Traps
{
    public class CleverPlatforms : MonoBehaviour
    {
        private PlatformEffector2D effector;
        private float waitTime;
        
        private void Start()
        {
            effector = GetComponent<PlatformEffector2D>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.S))
            {
                waitTime = .5f;
            }
                
            if (Input.GetKey(KeyCode.S))
            {
                if (waitTime <= 0)
                {
                    effector.rotationalOffset = 180f;
                    waitTime = .5f;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                effector.rotationalOffset = 0f;
            }
        }
    }
}