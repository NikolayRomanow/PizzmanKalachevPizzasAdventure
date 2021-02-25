using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.LanguageSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.GeneralCore
{
    public abstract class Action : MonoBehaviour
    {
        [SerializeField] private SupportedLanguages supportedLanguages;
        
        [SerializeField] protected Vector3 offset;
        [SerializeField] protected string russianTextOnButton;
        [SerializeField] protected string englishTextOnButton;

        private void Start()
        {

        }

        private void OnEnable()
        {
            supportedLanguages = FindObjectOfType<SupportedLanguages>();
        }

        public string TextOnButton()
        {
            if (supportedLanguages.ValidLanguage == OpinionsLanguages.Russian)
                return russianTextOnButton;
            if (supportedLanguages.ValidLanguage == OpinionsLanguages.English)
                return englishTextOnButton;
            
            return String.Empty;
        }

        public Vector3 Offset => offset;

        public virtual void OnAction()
        {

        }
    }
}
