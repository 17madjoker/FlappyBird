using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        IsOutScreen();
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.left * Time.deltaTime * 2f;
    }
    
    private void IsOutScreen()
    {
        Vector3 point = _camera.WorldToViewportPoint(transform.position); 
        
        if(point.x == 0f || point.x < 0f) 
            transform.position = new Vector3(0, -4, 0);
    }
}
