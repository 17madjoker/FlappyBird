using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[DefaultExecutionOrder(100)]
public class GameManager : MonoBehaviour
{   
    [Inject] 
    private Player _player;

    [Inject] 
    private SignalBus _signalBus;
    
    private bool _isGameOver = false;

    public bool IsGameOver
    {
        get { return _isGameOver; }
        private set { _isGameOver = value; }
    }

    private void Start()
    {
//        PlayerPrefs.SetInt("MaxScore", 0);
        _signalBus.Fire(new LoadDataSignal());
        
        _signalBus.Subscribe<GameSignal.LaunchGame>(LaunchGame);
        _signalBus.Subscribe<GameSignal.StartGame>(StartGame);
        _signalBus.Subscribe<GameSignal.GameOver>(GameOver);
        _signalBus.Subscribe<GameSignal.QuitGame>(Quit);
        
        _signalBus.Fire(new GameSignal.LaunchGame());
    }

    private void OnDestroy()
    {
        _signalBus.TryUnsubscribe<GameSignal.LaunchGame>(LaunchGame);
        _signalBus.TryUnsubscribe<GameSignal.StartGame>(StartGame);
        _signalBus.TryUnsubscribe<GameSignal.StartGame>(GameOver);
        _signalBus.TryUnsubscribe<GameSignal.QuitGame>(Quit);
    }

    private void LaunchGame()
    {
        Time.timeScale = 0;
    }
    
    private void StartGame()
    {
        Time.timeScale = 1;
        IsGameOver = false;
        
        _signalBus.Fire(new LoadDataSignal());
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        IsGameOver = true;
    }

    private void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    
}
