using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller
{
    [SerializeField] 
    private GameSettings _gameSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_gameSettings).AsSingle();
    }
}
