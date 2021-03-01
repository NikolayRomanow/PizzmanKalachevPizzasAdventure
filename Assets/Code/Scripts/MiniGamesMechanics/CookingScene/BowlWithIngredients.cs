using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.MiniGamesMechanics.CookingScene
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BowlWithIngredients : MonoBehaviour
    {
        [SerializeField] private List<GameObject> ingredientsInBowl;
        [SerializeField] private GameObject ingredientPrefab;

        private void Awake()
        {
            gameObject.tag = "Bowl";
            for (int i = 0; i < 10; i++)
                    ingredientsInBowl.Add(Instantiate(ingredientPrefab, new Vector3(
                        Random.Range(transform.position.x - 0.1f, transform.position.x + 0.1f),
                        Random.Range(transform.position.y - 0.2f, transform.position.y + 0.2f), 0f),
                   Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f)), transform));
        }

        public void SetIngredientOnDough()
        {
            if (ingredientsInBowl.Count <= 0)
                return;
            ingredientsInBowl[0].transform.position =
                new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
            ingredientsInBowl[0].transform.SetParent(GameObject.FindWithTag("DoughOnTable").transform);
            ingredientsInBowl.RemoveAt(0);
        }
    }
}
