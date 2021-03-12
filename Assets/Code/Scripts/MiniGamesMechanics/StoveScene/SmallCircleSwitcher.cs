using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.MiniGamesMechanics.StoveScene
{
    public class SmallCircleSwitcher : SpinningSwitcher
    {
        public override void WhirlSwitch()
        {
            innerSwitch.RotateAround(transform.position, new Vector3(0,0,-1f), speedOfMovement);
        }

        public override bool PressedSwitch()
        {
            switchPressed = true;
            if (innerSwitch.eulerAngles.z > yellowZoneAngleMin && innerSwitch.eulerAngles.z < yellowZoneAngleMax)
            {
                if (innerSwitch.eulerAngles.z > greenZoneAngleMin && innerSwitch.eulerAngles.z < greenZoneAngleMax)
                {
                    Debug.Log("GreenZone");
                    turnedEvent.Invoke(ColorZone.Green);
                }
            
                else
                {
                    Debug.Log("YellowZone");
                    turnedEvent.Invoke(ColorZone.Yellow);
                }

                return true;
            }
            
            else
            {
                Debug.Log("RedZone");
                turnedEvent.Invoke(ColorZone.Red);
                return false;
            }
        }
        
        private void FixedUpdate()
        {
            if (!switchPressed) 
                WhirlSwitch();
        }
    }
}
