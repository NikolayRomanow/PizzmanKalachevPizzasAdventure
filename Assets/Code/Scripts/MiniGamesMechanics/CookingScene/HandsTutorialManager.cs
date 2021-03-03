using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Code.Scripts.MiniGamesMechanics.CookingScene
{
    public enum HandState
    {
        OnDough, OnIngredient, ScrollRight, ScrollLeft, OnKetchup
    }
    
    public class HandsTutorialManager : MonoBehaviour
    {
        [SerializeField] private GameObject finger;
        [SerializeField] private Transform doughTouchPosition, scrollTouchPosition, scrollLeftTouchPosition, ingredientTouchPosition, ketchupTouchPosition;
        [SerializeField] private CookingTableManager cookingTableManager;
        [SerializeField] private Camera cameraMain;
        private HandState _handState;
        private int _countOfTaps = 0;
        private Animator _animator;

        private void Start()
        {
            finger.SetActive(true);
            _animator = finger.GetComponent<Animator>();
            _animator.Play("TapAnim");
            _handState = HandState.OnDough;
            finger.transform.position = doughTouchPosition.position;
        }

        private void Update()
        {
            if (_handState == HandState.ScrollRight && cameraMain.transform.position.x > 2f)
                NextFingerPosition(1);
            
            if (_handState == HandState.ScrollLeft && cameraMain.transform.position.x < 0.5f)
                NextFingerPosition(3);
        }

        public void NextFingerPosition(int index)
        {
            if (!gameObject.activeSelf)
                return;
            
            switch (index)
            {
                case 0:
                    _animator.Play("ScrollRight");
                    _handState = HandState.ScrollRight;
                    finger.transform.position = scrollTouchPosition.position;
                    break;
                case 1:
                    if (_countOfTaps < 7)
                    {
                        _countOfTaps++;
                        _animator.Play("TapAnim");
                        finger.transform.position = ingredientTouchPosition.position;
                        _handState = HandState.OnIngredient;
                    }
                    else 
                        NextFingerPosition(2);
                    break;
                case 2:
                    _animator.Play("ScrollLeft");
                    _handState = HandState.ScrollLeft;
                    finger.transform.position = scrollLeftTouchPosition.position;
                    break;
                case 3:
                    _animator.Play("TapAnim");
                    _handState = HandState.OnKetchup;
                    finger.transform.position = ketchupTouchPosition.position;
                    break;
                case 4:
                    finger.SetActive(false);
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
