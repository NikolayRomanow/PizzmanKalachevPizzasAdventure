using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.Scripts.ControlElements;

namespace Code.Scripts.GeneralCharactersScripts
{

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour // Класс отвечает за реализацию направления движения персонажа
    {
        [SerializeField] private float speedMove; // В переменной хранится значение скорости персонажа

        private bool _canMove; // В переменной хранится значение - может ли персонаж ходить
        private Animator _animator; // В переменной хранится компонент "аниматор" персонажа
        private Rigidbody2D _rigidbody; // В переменной хранится компонент "Rigidbogy2D" персонажа
        private Vector2 _movement; // В переменной хранится направление движение персонажа
        private Joystick _joystick; // В переменной хранится ссылка на джойстик управления, расположенный на экране

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

        private void Movement() // Функция обращается к джойстику и забирает с него значения напрваления движения персонажа и двигает персонажа в эту точку
        {
            _movement = _joystick.Direction;
            _movement /= 200;
            
            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);

            _rigidbody.MovePosition(_rigidbody.position + _movement * (speedMove * Time.fixedDeltaTime));
        }
    }
}
