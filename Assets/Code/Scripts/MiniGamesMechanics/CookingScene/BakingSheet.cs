using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace Code.Scripts.MiniGamesMechanics.CookingScene
{
    public enum BakingSheetState
    {
        MoveToTable, OnTable, MoveFromTable
    }
    public class BakingSheet : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform, originTransform;
        private Vector3 _target, _origin;

        public BakingSheetState bakingSheetState;

        private void Awake()
        {
            _target = targetTransform.position;
            _origin = originTransform.position;
        }

        private void Update()
        {
            switch (bakingSheetState)
            {
                case BakingSheetState.OnTable:
                    return;
                case BakingSheetState.MoveToTable:
                    ToTable();
                    break;
                case BakingSheetState.MoveFromTable:
                    FromTable();
                    break;
            }

        }

        private void ToTable()
        {
            transform.position = Vector2.Lerp(transform.position, _target,
                1 * Time.deltaTime);
        }

        private void FromTable()
        {
            transform.position = Vector2.MoveTowards(transform.position, _origin,
                1 * Time.deltaTime);
            if (transform.position == _origin)
                SceneManager.LoadScene(0);
        }
    }
}
