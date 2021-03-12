using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.MiniGamesMechanics.StoveScene
{
    public class SwitcherButton : Switcher
    {
        [SerializeField] private float maxYSwitcherPosition, minYSwitcherPosition;
        [SerializeField] private float maxYYellowZone, minYYellowZone, maxYGreenZone, minYGreenZone;
        [SerializeField] private SpriteRenderer button;
        [SerializeField] private Sprite turnOnButtonSprite;

        private bool _moveUp = false;

        public override void WhirlSwitch()
        {
            if (_moveUp)
            {
                innerSwitch.localPosition = Vector3.MoveTowards(innerSwitch.localPosition,
                    new Vector3(-0.141f, maxYSwitcherPosition, 0), speedOfMovement);
                if (innerSwitch.localPosition.y >= maxYSwitcherPosition)
                    _moveUp = false;
            }

            else
            {
                innerSwitch.localPosition = Vector3.MoveTowards(innerSwitch.localPosition,
                    new Vector3(-0.141f, minYSwitcherPosition, 0), speedOfMovement);
                if (innerSwitch.localPosition.y <= minYSwitcherPosition)
                    _moveUp = true;
            }
        }

        private void FixedUpdate()
        {
            if (!switchPressed)
                WhirlSwitch();
        }

        public override bool PressedSwitch()
        {
            button.sprite = turnOnButtonSprite;
            switchPressed = true;

            if (innerSwitch.localPosition.y > minYYellowZone && innerSwitch.localPosition.y < maxYYellowZone)
            {
                if (innerSwitch.localPosition.y > minYGreenZone && innerSwitch.localPosition.y < maxYGreenZone)
                {
                    Debug.Log("Green Zone");
                    turnedEvent.Invoke(ColorZone.Green);
                }

                else
                {
                    Debug.Log("Yellow Zone");
                    turnedEvent.Invoke(ColorZone.Yellow);
                }

                return true;
            }

            else
            {
                Debug.Log("Red Zone");
                turnedEvent.Invoke(ColorZone.Red);
            }

            return false;
        }
    }
}
