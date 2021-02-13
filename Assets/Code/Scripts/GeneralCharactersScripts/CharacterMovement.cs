using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.Scripts.ControlElements;

namespace Code.Scripts.GeneralCharactersScripts
{

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float speedMove;

        private bool _canMove;
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private Vector2 _movement;
        private Joystick _joystick;

        private void Awake()
        {
            _joystick = FindObjectOfType<Joystick>();
            _canMove = true;
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_canMove)
                Movement();
        }

        private void Movement()
        {
            _movement = _joystick.Direction;
            _movement /= 200;
            
            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);

            _rigidbody.MovePosition(_rigidbody.position + _movement * (speedMove * Time.fixedDeltaTime));
        }
    }
}
