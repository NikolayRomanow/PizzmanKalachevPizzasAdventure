using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.MiniGamesMechanics.CookingScene
{
    public class FirstLaunchManager : MonoBehaviour
    {
        [SerializeField] private GameObject handTutorial;
        private bool _isFirstLaunch;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("IsFirstLaunch"))
                _isFirstLaunch = false;
            else
            {
                _isFirstLaunch = true;
                PlayerPrefs.SetString("IsFirstLaunch", "IsNotFirstLaunch");
            }
            
            if (_isFirstLaunch)
                handTutorial.SetActive(true);
        }

        private void Start()
        {

        }
    }
}
