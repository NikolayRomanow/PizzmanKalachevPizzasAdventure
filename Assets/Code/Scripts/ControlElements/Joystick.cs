﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.ControlElements
{
    public class Joystick : MonoBehaviour
    {
        [SerializeField] private GameObject joystickUI;
        [SerializeField] private RectTransform plain;
        [SerializeField] private RectTransform ridger;
        
        private Camera _mainCamera;
        private Vector2 _firstTouchPosition;
        private Vector2 _secondTouchPosition;
        private Vector2 _touchOffset;
        private Vector2 _touchDirection;
        public Vector2 Direction => _touchDirection;

        private bool _touchStart;

        private void Awake()
        {
            joystickUI.SetActive(false);
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _touchStart = true;
                    _firstTouchPosition = Input.GetTouch(0).position;
                    _secondTouchPosition = Input.GetTouch(0).position;
                    plain.position = _firstTouchPosition;
                    ridger.position = _firstTouchPosition;
                    joystickUI.SetActive(true);
                }
                
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
                {
                    _secondTouchPosition = Input.GetTouch(0).position;
                    ridger.position = new Vector3(_firstTouchPosition.x + _touchDirection.x,
                        _firstTouchPosition.y + _touchDirection.y);
                }

                if (Input.GetTouch((0)).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
                {
                    _touchStart = false;
                    _firstTouchPosition = Vector2.zero;
                    _secondTouchPosition = Vector2.zero;
                    _touchDirection = Vector2.zero;
                    joystickUI.SetActive(false);
                }
            }
        }

        private void FixedUpdate()
        {
            if (_touchStart)
            {
                _touchOffset = _secondTouchPosition - _firstTouchPosition;
                _touchDirection = Vector2.ClampMagnitude(_touchOffset, 200f);
            }
        }
    }
}
