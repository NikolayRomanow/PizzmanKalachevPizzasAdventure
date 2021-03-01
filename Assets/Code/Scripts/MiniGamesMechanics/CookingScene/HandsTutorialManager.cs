using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Code.Scripts.MiniGamesMechanics.CookingScene
{
    public class HandsTutorialManager : MonoBehaviour
    {
        [SerializeField] private GameObject finger;
        [SerializeField] private Transform doughTouchPosition, scrollTouchPosition, ingredientTouchPosition;
        [SerializeField] private CookingTableManager cookingTableManager;
        [SerializeField] private Camera cameraMain;
        private bool _tutorialIngredientEnabled = false;
        private Animator _animator;

        private void Start()
        {
            finger.SetActive(true);
            _animator = finger.GetComponent<Animator>();
            _animator.Play("TapAnim");
            finger.transform.position = doughTouchPosition.position;
            if (!gameObject.activeSelf)
                _tutorialIngredientEnabled = true;
        }

        private void Update()
        {
            if (!_tutorialIngredientEnabled && cameraMain.transform.position.x > 2f)
                NextFingerPosition(1);
        }

        public void NextFingerPosition(int index)
        {
            if (!gameObject.activeSelf)
                return;
            
            switch (index)
            {
                case 0:
                    _animator.Play("ScrollRight");
                    finger.transform.position = scrollTouchPosition.position;
                    break;
                case 1:
                    _animator.Play("TapAnim");
                    finger.transform.position = ingredientTouchPosition.position;
                    _tutorialIngredientEnabled = true;
                    break;
                case 2:
                    if (_tutorialIngredientEnabled)
                    {
                        finger.SetActive(false);
                        gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}
