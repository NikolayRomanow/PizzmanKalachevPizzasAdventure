using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Ketchup : Fruit
    {
        protected override void Update()
        {
            base.Update();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Blade>() && croppedPrefabOne != null)
            {
                if (!croppedPrefabOne.activeSelf)
                    return;
                Instantiate(croppedPrefabOne, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
