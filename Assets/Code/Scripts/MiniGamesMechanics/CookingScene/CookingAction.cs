using System.Collections;
using System.Collections.Generic;
using Code.Scripts.GeneralCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.MiniGamesMechanics.CookingScene
{
    public class CookingAction : Action
    {
        public override void OnAction()
        {
            SceneManager.LoadScene(2);
        }
    }
}