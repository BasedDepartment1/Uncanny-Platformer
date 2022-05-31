using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class AnimatedButtonWithAnimator : MonoBehaviour
    {
        private Image image;
        private Animator animator;
        void Start()
        {
            image = GetComponent<Image>();
            image.enabled = false;
            animator = GetComponent<Animator>();
            StartCoroutine(nameof(Cross));
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private IEnumerator Cross()
        {
            yield return new WaitForSeconds(1);
            animator.Play("Regular");
            var time = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(time);
            image.enabled = true;
        }
    }
}
