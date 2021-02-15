using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Fruit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Blade>())
            {
                Destroy(gameObject);
            }
        }
    }
}
