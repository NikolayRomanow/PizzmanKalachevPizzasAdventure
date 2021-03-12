using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.MiniGamesMechanics.StoveScene
{
    public class TouchDetector : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip turnedSound, electricErrorSound;
        
        private Camera _camera;
        private Stove stove;

        private void Awake()
        {
            _camera = Camera.main;
            stove = FindObjectOfType<Stove>();
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint
                        (Input.GetTouch(0).position), Vector3.forward);

                    if (hit.collider.TryGetComponent(out Switcher switcher))
                    {
                        if (!switcher.switchPressed)
                        {
                            if (switcher.PressedSwitch())
                                audioSource.PlayOneShot(turnedSound);
                            else
                            {
                                audioSource.PlayOneShot(electricErrorSound);
                            }
                        }
                    }
                }
            }
        }
    }
}
