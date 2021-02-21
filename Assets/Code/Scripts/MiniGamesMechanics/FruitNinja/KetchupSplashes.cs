using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    public class KetchupSplashes : MonoBehaviour
    {
        [SerializeField] private List<GameObject> ketchupSplashes;
        private Coroutine _coroutine;
        private bool _coroutineIsStarted = false;

        public bool CoroutineIsStarted => _coroutineIsStarted;

        private void Awake()
        {
            foreach (var VARIABLE in ketchupSplashes)
            {
                VARIABLE.SetActive(false);
            }
        }

        private void Start()
        {

        }

        public void TurnOnSplashes()
        {
            _coroutine =  StartCoroutine(TurnOnSplashesNumerator());
            _coroutineIsStarted = true;
        }

        public void RestartSplashes()
        {
            StopCoroutine(_coroutine);
            foreach (var VARIABLE in ketchupSplashes)
            {
                VARIABLE.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            _coroutine = StartCoroutine(TurnOnSplashesNumerator());
        }

        IEnumerator TurnOnSplashesNumerator()
        {
            foreach (var VARIABLE in ketchupSplashes)
            {
                //yield return new WaitForSeconds(0.2f);
                VARIABLE.SetActive(true);
            }

            float alpha = 1;
            while (alpha - 0.2f > 0)
            {
                foreach (var VARIABLE in ketchupSplashes)
                {
                    yield return new WaitForSeconds(0.5f);
                    SpriteRenderer spriteRenderer = VARIABLE.GetComponent<SpriteRenderer>();
                    float alphaImage = spriteRenderer.color.a;
                    alphaImage -= 0.2f;
                    spriteRenderer.color = new Color(1, 1, 1, alphaImage);
                    if (ketchupSplashes.IndexOf(VARIABLE) == ketchupSplashes.Count - 1)
                        alpha = alphaImage;
                }
            }

            foreach (var VARIABLE in ketchupSplashes)
            {
                VARIABLE.SetActive(false);
                VARIABLE.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }

            _coroutineIsStarted = false;
        }
    }
}
