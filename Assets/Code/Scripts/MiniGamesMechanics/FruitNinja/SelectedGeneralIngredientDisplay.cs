using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.LanguageSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    [Serializable]
    public class VariablesOfNameGeneralIngredient
    {
        [SerializeField] private List<string> english, russian;
        [SerializeField] private List<Color32> colorsForText;
        private int _indexOfNameInList;

        private void SetCurrentIndexOfNameList(string ingredient)
        {
            switch (ingredient)
            {
                case "Bacon":
                    _indexOfNameInList = 0;
                    break;
                case "Cheese":
                    _indexOfNameInList = 1;
                    break;
                case "YellowPepper":
                    _indexOfNameInList = 2;
                    break;
                case "Mushroom":
                    _indexOfNameInList = 3;
                    break;
                case "Olivies":
                    _indexOfNameInList = 4;
                    break;
                case "Pepper":
                    _indexOfNameInList = 5;
                    break;
                case "Tomato":
                    _indexOfNameInList = 6;
                    break;
            }
        }
        
        public string CorrectName(OpinionsLanguages opinionsLanguages, string ingredient)
        {
            SetCurrentIndexOfNameList(ingredient);
            switch (opinionsLanguages)
            {
                case OpinionsLanguages.English:
                    return english[_indexOfNameInList];
                case OpinionsLanguages.Russian:
                    return russian[_indexOfNameInList];
            }

            return string.Empty;
        }

        public Color32 CorrectColor(string ingredient)
        {
            SetCurrentIndexOfNameList(ingredient);
            return colorsForText[_indexOfNameInList];
        }
    }
    
    public class SelectedGeneralIngredientDisplay : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SupportedLanguages supportedLanguages;

        [SerializeField]
        private TextMeshProUGUI weNeedMoreText, backgroundIngredientText, ingredientText, moreScoresFor;

        [SerializeField] private VariablesOfNameGeneralIngredient variablesOfNameGeneralIngredient;

        private ScoreManager _scoreManager;
        private string _generalIngredient;
        

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            TimeManager.StartCannon.AddListener(TurnOffTextOnDisplay);
            TimeManager.StopCannon.AddListener(SetEndGameText);
            supportedLanguages = FindObjectOfType<SupportedLanguages>();
            _scoreManager = FindObjectOfType<ScoreManager>();
            _generalIngredient = _scoreManager.generalIngredient;
            SetTextOnDisplay();
        }

        private void SetTextOnDisplay()
        {
            switch (supportedLanguages.ValidLanguage)
            {
                case OpinionsLanguages.English:
                    weNeedMoreText.text = "We need more";
                    moreScoresFor.text = "Doubled points for each cut";
                    break;
                case OpinionsLanguages.Russian:
                    weNeedMoreText.text = "Нам нужно больше";
                    moreScoresFor.text = "Удвоенные очки за каждый порезанныи ингредиент";
                    break;
            }

            backgroundIngredientText.text = ingredientText.text =
                variablesOfNameGeneralIngredient.CorrectName(supportedLanguages.ValidLanguage, _generalIngredient);

            ingredientText.color = variablesOfNameGeneralIngredient.CorrectColor(_generalIngredient);
            
            animator.Play("SetUpSelectedIngredientTextGroup");
        }

        private void TurnOffTextOnDisplay()
        {
            animator.Play("TurnOffSelectedIngredientTextGroup");
        }

        private void SetEndGameText()
        {
            weNeedMoreText.text = moreScoresFor.text = String.Empty;
            switch (supportedLanguages.ValidLanguage)
            {
                case OpinionsLanguages.English:
                    backgroundIngredientText.text =
                        ingredientText.text = "Scores earned: " + _scoreManager.TotalScores.ToString();
                    break;
                case OpinionsLanguages.Russian:
                    backgroundIngredientText.text =
                        ingredientText.text = "Очков набрано: " + _scoreManager.TotalScores.ToString();
                    break;
            }
            animator.Play("SetUpSelectedIngredientTextGroup");
        }
    }
}
