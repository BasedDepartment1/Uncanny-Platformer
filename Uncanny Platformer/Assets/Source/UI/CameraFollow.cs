using UnityEngine;

namespace Source.UI
{
    public class CameraFollow : MonoBehaviour
    {
        public float cameraFollowSpeed = 2f;
        public float yOffset = 1f;
        public Transform target;
        
        void Update()
        {
            var position = target.position;
            var newPos = new Vector3(position.x, position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, 
                newPos, cameraFollowSpeed * Time.deltaTime);
        }
    }
}
