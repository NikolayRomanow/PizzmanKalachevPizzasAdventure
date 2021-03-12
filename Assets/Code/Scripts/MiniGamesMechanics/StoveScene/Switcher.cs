using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Scripts.MiniGamesMechanics.StoveScene
{
    interface ISwitcher
    {
        void WhirlSwitch();
        bool PressedSwitch();
    }
    
    public enum ColorZone
    {
        Red,
        Yellow,
        Green
    }

    [Serializable]
    public class TurnedEvent : UnityEvent<ColorZone>
    {
        
    }
    
    public abstract class Switcher : MonoBehaviour, ISwitcher
    {
        [SerializeField] protected Transform innerSwitch;
        [SerializeField] protected float speedOfMovement;
        [SerializeField] protected TurnedEvent turnedEvent;
        public bool switchPressed = false;

        private void Awake()
        {
            turnedEvent.AddListener(FindObjectOfType<Stove>().SetStateOfStove);
        }

        public virtual void WhirlSwitch()
        {

        }

        public virtual bool PressedSwitch()
        {
            return true;
        }
    }

    public abstract class SpinningSwitcher : Switcher
    {
        [SerializeField] protected float greenZoneAngleMin, greenZoneAngleMax, yellowZoneAngleMin, yellowZoneAngleMax;
    }
}
