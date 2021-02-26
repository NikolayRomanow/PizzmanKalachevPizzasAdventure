using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.MainCharacterScripts;
using UnityEngine;

namespace Code.Scripts.GeneralCore
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        private Transform _cameraTransform;
        private bool _playerOverTrigger = false;

        private void Awake()
        {
            playerTransform = FindObjectOfType<MainCharacterAction>().transform;
            _cameraTransform = transform;
        }

        private void Update()
        {
            if (_playerOverTrigger)
                MoveToPlayer();
            if (playerTransform.position == _cameraTransform.position)
                _playerOverTrigger = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerOverTrigger = true;
            }
        }

        private void MoveToPlayer()
        {
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, playerTransform.position, 1.6f * Time.deltaTime);
        }
    }
}
