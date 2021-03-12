using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.MiniGamesMechanics.StoveScene
{
    public class CircleSwitcher : SpinningSwitcher
    {
        public override void WhirlSwitch()
        {
            innerSwitch.transform.Rotate(new Vector3(0,0,-1f), speedOfMovement);
        }
        private void FixedUpdate()
        {
            if (!switchPressed)
                WhirlSwitch();
        }

        public override bool PressedSwitch()
        {
            switchPressed = true;
            if (innerSwitch.localEulerAngles.z > yellowZoneAngleMin && innerSwitch.localEulerAngles.z < yellowZoneAngleMax)
            {
                 if (innerSwitch.localEulerAngles.z > greenZoneAngleMin && innerSwitch.localEulerAngles.z < greenZoneAngleMax)
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
    }
}
