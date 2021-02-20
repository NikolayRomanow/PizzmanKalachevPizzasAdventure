using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action = Code.Scripts.GeneralCore.Action;

namespace Code.Scripts.MainCharacterScripts
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class MainCharacterAction : MonoBehaviour
    {
        private CircleCollider2D _circleCollider2D;
        [SerializeField] private List<GameObject> actionableObjects;
        [SerializeField] private List<GameObject> actionButtons;
        [SerializeField] private List<Vector3> actionableObjectsOffsets;
        [SerializeField] private GameObject actionButtonPrefab;
        private Transform _canvasTransform;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            actionButtonPrefab = Resources.Load<GameObject>("Prefabs/UIElements/ActionButton");
            _canvasTransform = FindObjectOfType<Canvas>().transform;
        }

        private void Update()
        {
            if (actionableObjects.Count > 0)
            {
                int index = 0;
                foreach (var VARIABLE in actionButtons)
                {
                    VARIABLE.transform.position =
                        _camera.WorldToScreenPoint(actionableObjects[index].transform.position +
                                                   actionableObjectsOffsets[index]);
                    index++;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Action action))
            {
                GameObject newActionButton = Instantiate(actionButtonPrefab, _canvasTransform);
                newActionButton.GetComponent<Button>().onClick.AddListener(action.OnAction);
                actionButtons.Add(newActionButton);
                actionableObjects.Add(action.gameObject);
                actionableObjectsOffsets.Add(action.Offset);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Action action))
            {
                int index = actionableObjects.IndexOf(action.gameObject);
                actionableObjects.RemoveAt(index);
                Destroy(actionButtons[index]);
                actionButtons.RemoveAt(index);
                actionableObjectsOffsets.RemoveAt(index);
            }
        }
    }
}
