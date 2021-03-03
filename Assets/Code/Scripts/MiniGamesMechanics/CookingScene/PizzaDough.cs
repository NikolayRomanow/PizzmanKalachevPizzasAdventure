using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.MiniGamesMechanics.CookingScene
{
    public class PizzaDough : MonoBehaviour
    {
        [SerializeField] private List<Transform> pointsForPizza;
        [SerializeField] private List<GameObject> ingredientsOnPizza;
        [SerializeField] private GameObject ketchupOnPizza;
        private int _currentPointIndex = 0;

        public bool SetUpIngredientOnPizza(Transform ingredientTransform)
        {
            if (_currentPointIndex > pointsForPizza.Count - 1)
                return false;
            ingredientTransform.position = pointsForPizza[_currentPointIndex].position;
            ingredientTransform.rotation = Quaternion.Euler(0, 0, Random.Range(-180f, 180f));
            _currentPointIndex++;
            ingredientTransform.SetParent(gameObject.transform);
            ingredientsOnPizza.Add(ingredientTransform.gameObject);
            return true;
        }

        public void SetUpKetchup()
        {
            ketchupOnPizza.SetActive(true);
            StartCoroutine(ReturnPizzaNumerator());
        }

        IEnumerator ReturnPizzaNumerator()
        {
            yield return new WaitForSeconds(1f);
            FindObjectOfType<BakingSheet>().bakingSheetState = BakingSheetState.MoveFromTable;
        }
    }
}