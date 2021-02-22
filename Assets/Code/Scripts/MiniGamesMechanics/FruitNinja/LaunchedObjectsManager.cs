using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    [Serializable]
    public class LaunchedObject
    {
        public string name;
        public int amount;

        public LaunchedObject(string _name, int _amount)
        {
            name = _name;
            amount = _amount;
        }
    }
    
    [Serializable]
    public class LaunchedObjectsList
    {
        public List<LaunchedObject> launchedObjects;
    }
    
    public class LaunchedObjectsManager : MonoBehaviour
    {
        //public LaunchedObjectsList LaunchedObjectsList;
        
        [SerializeField] private List<LaunchedObject> launchedObjects;

        public List<LaunchedObject> LaunchedObjects
        {
            get => launchedObjects;
            set => launchedObjects = value;
        }

        [SerializeField] private List<FruitCannon> fruitCannons;

        private void Awake()
        {
            //PlayerPrefs.SetString("LaunchedObjectsToFruitNinja", JsonUtility.ToJson(LaunchedObjectsList));
            LoadObjects();
        }

        private void LoadObjects()
        {
            LaunchedObjectsList launchedObjectsList = JsonUtility.FromJson<LaunchedObjectsList>(PlayerPrefs.GetString("LaunchedObjectsToFruitNinja"));
            launchedObjects = launchedObjectsList.launchedObjects;

            while (launchedObjects.Count > 0)
            {
                LaunchedObject launchedObject = launchedObjects[Random.Range(0, launchedObjects.Count)];
                LaunchedObject launchedObjectToSend = new LaunchedObject(launchedObject.name, 0);
                launchedObjectToSend.amount = Random.Range(1, launchedObject.amount);
                launchedObject.amount -= launchedObjectToSend.amount;
                if (launchedObject.amount <= 0)
                    launchedObjects.Remove(launchedObject);
                
                fruitCannons[Random.Range(0, fruitCannons.Count)].LaunchedObjects.Add(launchedObjectToSend);
            }
        }
    }
}
