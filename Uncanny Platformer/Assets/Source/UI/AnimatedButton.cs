using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class AnimatedButton : MonoBehaviour
    {
        [SerializeField] private Sprite regular;
        [SerializeField] private Sprite highlighted;
        private Image spriteRenderer;
        private Animator animator;
        
        
        void Start()
        {
            spriteRenderer = GetComponent<Image>();
            spriteRenderer.enabled = false;
            animator = GetComponent<Animator>();
            StartCoroutine(nameof(Cross));
        }
        
        void Update()
        {
        
        }

        private IEnumerator Cross()
        {
            yield return new WaitForSeconds(1);
            animator.Play("Normal");
            var time = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(time);
            spriteRenderer.enabled = true;
        }

        private void OnMouseEnter()
        {
            Debug.Log("навелся");
            spriteRenderer.sprite = highlighted;
        }

        private void OnMouseExit()
        {
            Debug.Log("съебал");
            spriteRenderer.sprite = regular;
        }

        private void OnMouseUpAsButton()
        {
            throw new NotImplementedException();
        }
    }
}
