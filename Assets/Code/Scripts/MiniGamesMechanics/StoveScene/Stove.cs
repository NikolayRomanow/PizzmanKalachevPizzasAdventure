using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.MiniGamesMechanics.StoveScene
{
    public class Stove : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Animator _animator;
        private int _alpha;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetStateOfStove(ColorZone colorZone)
        {
            switch (colorZone)
            {
                case ColorZone.Red:
                    _animator.Play("ElectricError");
                    if (_alpha >= 25)
                        _alpha -= 25;
                    break;
                
                case ColorZone.Yellow:
                    if (_alpha <= 75)
                        _alpha += 25;
                    break;
                
                case ColorZone.Green:
                    if (_alpha <= 50)
                        _alpha += 50;
                    else if (_alpha <= 75)
                        _alpha += 25;
                    break;
            }
            
            _animator.SetInteger("Alpha", _alpha);
        }
    }
}
