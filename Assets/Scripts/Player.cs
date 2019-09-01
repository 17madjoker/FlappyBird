using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject] 
    private GameSettings _gameSettings;

    [Inject] 
    private SignalBus _signalBus;

    private bool _canFly = false;
    
    private int _score;

    public int Score
    {
        get { return _score; }
        private set
        {
            _score = value;
            
            if (_score > MaxScore)
                _signalBus.Fire(new PlayerSignal.SetRecord());
            
            _signalBus.Fire(new PlayerSignal.Score { CurrentScore = _score });
        }
    }

    public int MaxScore { get; private set; }
    
    private void Start()
    {       
        _signalBus.Subscribe<LoadDataSignal>(LoadPlayer);
        _signalBus.Subscribe<GameSignal.StartGame>(SetPlayer);
        _signalBus.Subscribe<GameSignal.GameOver>(GameOver);
        _signalBus.Subscribe<PlayerSignal.SetRecord>(SetRecord);
    }

    private void OnDestroy()
    {
        _signalBus.TryUnsubscribe<LoadDataSignal>(LoadPlayer);
        _signalBus.TryUnsubscribe<GameSignal.StartGame>(SetPlayer);
        _signalBus.TryUnsubscribe<GameSignal.GameOver>(GameOver);
        _signalBus.TryUnsubscribe<PlayerSignal.SetRecord>(SetRecord);
    }

    private void Update()
    {
        Fly();
    }

    private void SetPlayer()
    {
        _score = 0;
        _canFly = true;
        
        transform.position = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void LoadPlayer(LoadDataSignal loadDataSignal)
    {
        MaxScore = loadDataSignal.MaxScore;
    }

    private void SetRecord()
    {
        _signalBus.Fire(new SaveDataSignal(Score));
    }

    private void GameOver()
    {
        ///
    }

    private void Fly()
    {
        if (_canFly)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {                
                if (transform.position.y < 4f)
                    GetComponent<Rigidbody2D>().velocity = Vector2.up * _gameSettings.JumpForce; 
            
                GetComponent<Animator>().SetBool("Fly", true);
            
                _signalBus.Fire<PlayerSignal.Fly>();
            }

            else if (Input.GetKeyUp(KeyCode.Space) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                GetComponent<Animator>().SetBool("Fly", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _signalBus.Fire<PlayerSignal.Hit>();
        _signalBus.Fire(new GameSignal.GameOver());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "ScoreCollider")
            Score++;
    }
}
