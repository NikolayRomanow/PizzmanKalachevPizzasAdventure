using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    enum CannonPosition
    {
        Left, Right, CenterLeft, CenterRight
    }
    public class FruitCannon : MonoBehaviour
    {
        [SerializeField] private List<GameObject> fruits;
        [SerializeField] private CannonPosition cannonPosition;
        private Coroutine _coroutine;
        private bool _isFirstShot;
        public bool IsFirstShot
        {
            set => _isFirstShot = value;
        }

        private void Awake()
        {
            _isFirstShot = false;
        }

        private void Start()
        {
            TimeManager.StartCannon.AddListener(StartCannon);
            TimeManager.StopCannon.AddListener(StopCannon);
        }

        private void StartCannon()
        {
            _coroutine =  StartCoroutine(TurnOnFruit());
            Debug.Log("Cannon started");
        }

        private void StopCannon()
        {
            StopCoroutine(_coroutine);
            Debug.Log("Cannon stopped");
        }
        
        IEnumerator TurnOnFruit()
        {
            while (true)
            {
                if (_isFirstShot)
                {
                    yield return new WaitForSeconds(0f);
                    _isFirstShot = false;
                }
                else 
                    yield return new WaitForSeconds(Random.Range(2f, 6f));
                GameObject turnedFruit = Instantiate(fruits[Random.Range(0, fruits.Count)], transform.position, Quaternion.identity);
                Rigidbody2D rigidbody2D = turnedFruit.GetComponent<Rigidbody2D>();
                switch (cannonPosition)
                {
                    case CannonPosition.Left:
                        rigidbody2D.AddForce(new Vector2(0.5f, 1) * 300, ForceMode2D.Force);
                        break;
                    case CannonPosition.Right:
                        rigidbody2D.AddForce(new Vector2(-0.5f, 1) * 300, ForceMode2D.Force);
                        break;
                    case CannonPosition.CenterLeft:
                        rigidbody2D.AddForce(new Vector2(0.1f, 1f) * 300, ForceMode2D.Force);
                        break;
                    case CannonPosition.CenterRight:
                        rigidbody2D.AddForce(new Vector2(-0.1f, 1f) * 300, ForceMode2D.Force);
                        break;
                }
            }
        }
    }
}
