using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Code.Scripts.MiniGamesMechanics.FruitNinja
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Blade : MonoBehaviour
    {
        private Vector3 _touchPosition;
        private Transform _transform;
        private Camera _mainCamera;
        private CircleCollider2D _circleCollider2D;
        private GameObject _bladeTrail;
        private GameObject _currentBladeTRail;
        private KetchupSplashes _ketchupSplashes;
        private ScoreManager _scoreManager;
        private string _generalIngredient;

        [SerializeField] private AudioSource audioBladeSweep;
        [SerializeField] private AudioClip sweepBlade, softBoiled;

        private void Awake()
        {
            _scoreManager = FindObjectOfType<ScoreManager>();
            _generalIngredient = _scoreManager.generalIngredient;
            _transform = GetComponent<Transform>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _circleCollider2D.enabled = false;
            _mainCamera = Camera.main;
            _bladeTrail = Resources.Load<GameObject>("Prefabs/MiniGames/FruitNinja/BladeTrail");
            _ketchupSplashes = FindObjectOfType<KetchupSplashes>();
        }

        private void Start()
        {
            TimeManager.StopCannon.AddListener(TurnOffBlade);
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                _touchPosition = _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
                _transform.position = _touchPosition;

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _circleCollider2D.enabled = true;
                    _currentBladeTRail = Instantiate(_bladeTrail, transform);
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
                {
                    _circleCollider2D.enabled = false;
                    Destroy(_currentBladeTRail);
                }
            }
        }

        private void TurnOffBlade()
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Fruit fruit) && !other.GetComponent<Ketchup>())
                if (fruit.HasCroppedPrefab)
                {
                    audioBladeSweep.PlayOneShot(sweepBlade);
                    int scores = 10;
                    switch (other.name)
                    {
                        case string buffer when buffer.Contains("X2"):
                            scores = 20;
                            break;
                    }
                    var nameOfIngredient = _generalIngredient;
                    if (other.name.Contains(nameOfIngredient))
                        scores *= 2;
                    Debug.Log(scores);
                    _scoreManager.AddScore(scores);
                }
            
            if (other.TryGetComponent(out Ketchup ketchup))
            {
                if (ketchup.gameObject.CompareTag("KetchupCuted"))
                    return;
                else
                {
                    audioBladeSweep.PlayOneShot(softBoiled);
                    
                    if (_ketchupSplashes.CoroutineIsStarted)
                        _ketchupSplashes.RestartSplashes();
                    else _ketchupSplashes.TurnOnSplashes();
                    
                    _scoreManager.ClearScore();
                }
            }
        }
    }
}
