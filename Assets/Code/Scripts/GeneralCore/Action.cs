using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.GeneralCore
{
    public class Action : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;

        public Vector3 Offset => offset;

        public virtual void OnAction()
        {
            SceneManager.LoadScene(1);
        }
    }
}
