using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Blade : MonoBehaviour
    {
        private Vector3 _touchPosition;
        private Transform _transform;
        private Camera _mainCamera;
        private CircleCollider2D _circleCollider2D;
        private GameObject _bladeTrail;
        private GameObject _currentBladeTRail;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _circleCollider2D.enabled = false;
            _mainCamera = Camera.main;
            _bladeTrail = Resources.Load<GameObject>("Prefabs/MiniGames/FruitNinja/BladeTrail");
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                _touchPosition = _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
                _transform.position = _touchPosition;

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _circleCollider2D.enabled = true;
                    _currentBladeTRail = Instantiate(_bladeTrail, transform);
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
                {
                    _circleCollider2D.enabled = false;
                    Destroy(_currentBladeTRail);
                }
            }
        }
    }
}
