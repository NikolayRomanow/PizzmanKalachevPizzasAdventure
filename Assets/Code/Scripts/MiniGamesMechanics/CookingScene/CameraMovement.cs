﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Code.Scripts.MiniGamesMechanics.CookingScene
{
    public class CameraMovement : MonoBehaviour
    {
        private Vector2 _firstTouchPosition, _secondTouchPosition;
        private Vector2 _touchOffset, _touchDirection;
        private Camera _mainCamera;
        private bool _touchOnDisplay = false;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _firstTouchPosition = Input.GetTouch(0).position;
                    _secondTouchPosition = Input.GetTouch(0).position;
                    _touchOnDisplay = true;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Moved ||
                    Input.GetTouch(0).phase == TouchPhase.Stationary)
                {
                    _secondTouchPosition = Input.GetTouch(0).position;
                }

                if (Input.GetTouch((0)).phase == TouchPhase.Ended ||
                    Input.GetTouch(0).phase == TouchPhase.Canceled)
                {
                    _touchOnDisplay = false;
                    _firstTouchPosition = Vector2.zero;
                    _secondTouchPosition = Vector2.zero;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_touchOnDisplay)
            {
                _touchOffset = Vector2.ClampMagnitude(_secondTouchPosition - _firstTouchPosition, 1000f);
                _touchDirection = new Vector2(transform.position.x + _touchOffset.x, 0);

                var speed = _touchOffset.x / 10000;
                if (_touchOffset.x < -1)
                {
                    if (transform.position.x < 3.4f)
                    {
                        speed *= -1;
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(3.4f, 0), speed);
                    }
                }
                
                else if (_touchOffset.x > 1)
                {
                    if (transform.position.x > -1f)
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-1f, 0), speed);
                    
                }

            }
        }
    }
}