using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.GeneralCore
{
    public class AudioEnhancer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            StartCoroutine(AudioVolumeEnhancer());
        }

        IEnumerator AudioVolumeEnhancer()
        {
            while (audioSource.volume < 1)
            {
                yield return new WaitForSeconds(0.5f);
                audioSource.volume += 0.1f;
            }
        }
    }
}
