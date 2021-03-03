using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.LanguageSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeOnScreen;
        [SerializeField] private int leftSeconds;
        [SerializeField] private List<FruitCannon> cannons;
        [SerializeField] private SupportedLanguages supportedLanguages;
        [SerializeField] private AudioSource audioGuitar, audioClock;
        private TimeSpan _leftTime;

        private Coroutine _clockCoroutine, _guitarCoroutine;

        public static UnityEvent StartCannon;
        public static UnityEvent StopCannon;
        
        private void Awake()
        {
            StartCannon = new UnityEvent();
            StopCannon = new UnityEvent();
            supportedLanguages = FindObjectOfType<SupportedLanguages>();
            _leftTime = TimeSpan.FromSeconds(leftSeconds);
        }

        private void Start()
        {
            StartCannon.AddListener(OnClockTemped);
            StartCoroutine(StartTypingTimeOnScreen());
            audioClock.Play();
            _clockCoroutine = StartCoroutine(TurnDownClock());
        }

        private string CheckValidLanguage()
        {
            string phraze = String.Empty;
            switch (supportedLanguages.ValidLanguage)
            {
                case OpinionsLanguages.Russian:
                    phraze = "Время: ";
                    break;
                
                case OpinionsLanguages.English:
                    phraze = "Time: ";
                    break;
            }
            return phraze;
        }

        private void OnClockTemped()
        {
            StopCoroutine(_clockCoroutine);
            audioClock.Stop();
            audioGuitar.Play();
            StartCoroutine(TurnUpGuitar());
        }

        IEnumerator StartTypingTimeOnScreen()
        {
            string phraze = CheckValidLanguage();
            phraze += _leftTime.ToString(@"mm\:ss");
            for (int i = 0; i <= phraze.Length; i++)
            {
                timeOnScreen.text = phraze.Substring(0, i);
                yield return new WaitForSeconds(0.2f);
            }
            cannons[Random.Range(0, cannons.Count)].IsFirstShot = true;
            StartCannon.Invoke();
            StartCoroutine(Countdown());
        }

        IEnumerator Countdown()
        {
            while (_leftTime > TimeSpan.Zero)
            {
                yield return new WaitForSeconds(1f);
                _leftTime -= new TimeSpan(0, 0, 1);
                string phraze = CheckValidLanguage();
                timeOnScreen.text = phraze + _leftTime.ToString(@"mm\:ss");
            }
            StopCannon.Invoke();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(0);
        }

        IEnumerator TurnDownClock()
        {
            yield return new WaitForSeconds(0.5f);
            while (audioClock.volume > 0)
            {
                yield return new WaitForSeconds(0.4f);
                audioClock.volume -= 0.15f;
            }
        }

        IEnumerator TurnUpGuitar()
        {
            while (audioGuitar.volume < 1)
            {
                yield return new WaitForSeconds(0.2f);
                audioGuitar.volume += 0.1f;
            }
        }
    }
}
