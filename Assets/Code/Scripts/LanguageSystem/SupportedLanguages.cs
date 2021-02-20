using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.LanguageSystem
{
    public enum OpinionsLanguages
    {
        Russian, English
    }

    public class SupportedLanguages : MonoBehaviour
    {
        [SerializeField] private OpinionsLanguages validLanguage;

        public OpinionsLanguages ValidLanguage
        {
            get => validLanguage;
            set => validLanguage = value;
        }
    }
}
