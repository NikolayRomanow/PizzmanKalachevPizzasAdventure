using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.MiniGamesMechanics.CookingScene
{
    [Serializable]
    public class DoughOnTableEvent : UnityEvent<int>
    {
        
    }

    public class CookingTableManager : MonoBehaviour
    {
        private Camera _camera;
        private bool _doughOnTable = false;
        [SerializeField] private DoughOnTableEvent doughOnTableEvent;
        [SerializeField] private Transform bakingSheet;
        public bool DoughOnTable
        {
            get => _doughOnTable;
            set => _doughOnTable = value;
        }

        [SerializeField] private GameObject lightDoughPrefab, darkDoughPrefab;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip softDough, kickOnBowl, kickOnEmptyBowl, ketchupSound;
        private PizzaDough _pizzaDough;
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint
                            (Input.GetTouch(0).position), Vector3.forward);
                    if (hit.collider)
                    {
                        switch (hit.collider.tag)
                        {
                            case "LightDough":
                                if (!_doughOnTable)
                                {
                                    audioSource.PlayOneShot(softDough);
                                    Instantiate(lightDoughPrefab, bakingSheet);
                                    _doughOnTable = true;
                                    _pizzaDough = FindObjectOfType<PizzaDough>();
                                    doughOnTableEvent.Invoke(0);
                                }
                                break;
                            
                            case "DarkDough":
                                if (!_doughOnTable)
                                {
                                    audioSource.PlayOneShot(softDough);
                                    Instantiate(darkDoughPrefab, bakingSheet);
                                    _doughOnTable = true;
                                    _pizzaDough = FindObjectOfType<PizzaDough>();
                                    doughOnTableEvent.Invoke(0);
                                }
                                break;
                            
                            case "Bowl":
                                if (_doughOnTable && hit.collider.GetComponent<BowlWithIngredients>().IngredientsInBowl > 0)
                                {
                                    bool trueOrFalse = hit.collider.GetComponent<BowlWithIngredients>().SetIngredientOnDough();
                                    if (trueOrFalse)
                                        audioSource.PlayOneShot(kickOnBowl);
                                    else
                                    {
                                        audioSource.PlayOneShot(kickOnEmptyBowl);
                                    }
                                    doughOnTableEvent.Invoke(1);
                                }
                                break;
                            
                            case "KetchupOnTable":
                                audioSource.PlayOneShot(ketchupSound);
                                doughOnTableEvent.Invoke(4);
                                _pizzaDough.SetUpKetchup();
                                break;
                        }
                    }
                }
            }
        }
    }
}
