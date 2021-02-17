using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Fruit : MonoBehaviour
    {
        [SerializeField] private GameObject croppedPrefab;
        private CircleCollider2D _circleCollider2D;
        private bool _rightRotation;
        private Vector3 _rotateDirection;
        private void Awake()
        {
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _circleCollider2D.enabled = false;
            _rightRotation = (Random.Range(0f, 1f) > 0.5f);
            if (_rightRotation)
                _rotateDirection = new Vector3(0, 0, -1);
            else
                _rotateDirection = new Vector3(0, 0, 1);
            StartCoroutine(TurnOnCollider());
        }

        private void Start()
        {
            Destroy(gameObject, 2f);
        }

        private void Update()
        {
            transform.Rotate(_rotateDirection);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Blade>() && croppedPrefab != null)
            {
                if (!croppedPrefab.activeSelf)
                    return;
                Instantiate(croppedPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1)* 100, ForceMode2D.Force);
                Instantiate(croppedPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1)* 100, ForceMode2D.Force);
                Destroy(gameObject);
            }
        }

        IEnumerator TurnOnCollider()
        {
            yield return new WaitForSeconds(0.15f);
            _circleCollider2D.enabled = true;
        }
    }
}
