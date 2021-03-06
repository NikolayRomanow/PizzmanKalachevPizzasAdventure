using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.LanguageSystem;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private List<string> generalIngredients;
        [SerializeField] private int totalScores;
        public int TotalScores => totalScores;
        
        [SerializeField] private TextMeshProUGUI scorePanel;
        [SerializeField] private Animator scorePanelAnimator;
        [SerializeField] private SupportedLanguages supportedLanguages;

        public string generalIngredient;

        private void Awake()
        {
            SetRandomGeneralIngredient();
        }

        private void Start()
        {
            supportedLanguages = FindObjectOfType<SupportedLanguages>();
        }

        private void SetRandomGeneralIngredient()
        {
            generalIngredient = generalIngredients[Random.Range(0, generalIngredients.Count - 1)];
        }

        public void AddScore(int scores)
        {
            scorePanelAnimator.Play("ScorePanelPulsed");
            totalScores += scores;
            scorePanel.text = SetScoreTextLanguage() + totalScores.ToString();
            scorePanelAnimator.Play("scorePanelPulsed");
        }

        public void ClearScore()
        {
            scorePanelAnimator.Play("ScorePanelPulsed");
            totalScores = 0;
            scorePanel.text = SetScoreTextLanguage() + totalScores.ToString();
            scorePanelAnimator.Play("scorePanelPulsed");
        }

        private string SetScoreTextLanguage()
        {
            switch (supportedLanguages.ValidLanguage)
            {
                case OpinionsLanguages.English:
                    return "Scores: ";
                case OpinionsLanguages.Russian:
                    return "Очки: ";
            }
            return String.Empty;
        }
    }
}