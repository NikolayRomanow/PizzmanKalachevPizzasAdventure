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
        public int IngredientsInBowl => ingredientsInBowl.Count;
        [SerializeField] private GameObject ingredientPrefab;
        [SerializeField] private Sprite bowlWithIngredients, bowlWithoutIngredients;
        private PizzaDough _pizzaDough;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            gameObject.tag = "Bowl";
            for (int i = 0; i < 10; i++)
                    ingredientsInBowl.Add(Instantiate(ingredientPrefab, new Vector3(
                        Random.Range(transform.position.x - 0.1f, transform.position.x + 0.1f),
                        Random.Range(transform.position.y - 0.2f, transform.position.y + 0.2f), 0f),
                   Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f)), transform));
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = bowlWithIngredients;
        }

        public bool SetIngredientOnDough()
        {
            if (ingredientsInBowl.Count <= 0)
                return false;
            if (!_pizzaDough)
                _pizzaDough = FindObjectOfType<PizzaDough>();
            bool trueOrFalse = _pizzaDough.SetUpIngredientOnPizza(ingredientsInBowl[0].transform);
            if (!trueOrFalse)
                return false;
            ingredientsInBowl.RemoveAt(0);
            if (ingredientsInBowl.Count <= 0)
                _spriteRenderer.sprite = bowlWithoutIngredients;
            return true;
        }
    }
}
