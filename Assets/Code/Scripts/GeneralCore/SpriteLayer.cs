using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Code.Scripts.GeneralCore
{
    public class SpriteLayer : MonoBehaviour
    {
        private SpriteRenderer _playersSprite;

        private void Awake()
        {
            _playersSprite = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                if (!spriteRenderer.CompareTag("GeneralRoom"))
                {
                    //Debug.Log("object");
                    //Debug.Log(spriteRenderer.transform.position.y + " " + transform.position.y);
                    if (spriteRenderer.transform.position.y > transform.position.y)
                        spriteRenderer.sortingOrder = _playersSprite.sortingOrder - 1;

                    if (spriteRenderer.transform.position.y < transform.position.y)
                        spriteRenderer.sortingOrder = _playersSprite.sortingOrder + 1;
                }
            }
        }
    }
}
