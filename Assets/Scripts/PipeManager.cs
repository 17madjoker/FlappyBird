using System.Collections;
using UnityEngine;
using Zenject;

[DefaultExecutionOrder(98)]
public class PipeManager : MonoBehaviour
{
    [Inject] 
    private Pipe.PipeFactory _pipeFactory;

    [Inject] 
    private GameSettings _gameSettings;

    [Inject] 
    private GameManager _gameManager;

    [Inject] 
    private SignalBus _signalBus;

    private void Start()
    {
        _signalBus.Subscribe<GameSignal.StartGame>(StartGame);
        _signalBus.Subscribe<GameSignal.GameOver>(GameOver);
    }

    private void OnDestroy()
    {
        _signalBus.TryUnsubscribe<GameSignal.StartGame>(StartGame);
        _signalBus.TryUnsubscribe<GameSignal.StartGame>(GameOver);
    }

    private void StartGame()
    {
        ClearPipes();
            
        StartCoroutine(SpawnPipe());
    }

    private void GameOver()
    {
        ClearPipes();
    }
    
    private IEnumerator SpawnPipe()
    {
        while (!_gameManager.IsGameOver)
        {
            Pipe p = _pipeFactory.Create();
            p.transform.SetParent(transform, false);
        
            yield return new WaitForSeconds(_gameSettings.PipeTimeSpawn);
        }
    }
    
    private void ClearPipes()
    {
        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i) != null)
                Destroy(transform.GetChild(i).gameObject);
    }
}
