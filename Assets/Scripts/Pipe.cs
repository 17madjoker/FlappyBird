using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pipe : MonoBehaviour
{
    [Inject] 
    private GameSettings _gameSettings;

    [Inject] 
    private PipeManager _pipeManager;

    private Camera _camera;

    private void Start()
    {
        _camera = FindObjectOfType<Camera>();

        SetPosition();
    }

    private void Update()
    {
        Move();
        IsOutScreen();
    }

    private void Move()
    {
        transform.position += Vector3.left * _gameSettings.PipeSpeed * Time.deltaTime;
    }

    private void SetPosition()
    {
        transform.position = new Vector3(
            transform.position.x, 
            transform.position.y + Random.Range(_gameSettings.MinPipeOffset, _gameSettings.MaxPipeOffset),
            transform.position.z);
        
        transform.SetParent(_pipeManager.transform, false);
    }

    private void IsOutScreen()
    {
        Vector3 point = _camera.WorldToViewportPoint(transform.position); 
        
        if(point.x < 0f) 
            Destroy(gameObject, 2f);
    }

    public class PipeFactory : Factory<Pipe>
    {
    }
}
