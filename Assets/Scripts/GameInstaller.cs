using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject] 
    private GameSettings _gameSettings;
    
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
            
        Container.BindInstance(FindObjectOfType<AudioManager>()).AsSingle();
        Container.BindInstance(FindObjectOfType<PipeManager>()).AsSingle();
        Container.BindInstance(FindObjectOfType<GameManager>()).AsSingle();
        Container.BindInstance(FindObjectOfType<UIManager>()).AsSingle();
        Container.BindInstance(FindObjectOfType<Player>()).AsSingle();
        Container.BindInstance(FindObjectOfType<SaveLoadManager>()).AsSingle();

        Container.DeclareSignal<PlayerSignal.Fly>();
        Container.DeclareSignal<PlayerSignal.Score>();
        Container.DeclareSignal<PlayerSignal.Hit>();
        Container.DeclareSignal<PlayerSignal.SetRecord>();
        
        Container.DeclareSignal<VolumeChangeSignal>();
        Container.DeclareSignal<AudioStateSingal>();

        Container.DeclareSignal<GameSignal.StartGame>();
        Container.DeclareSignal<GameSignal.LaunchGame>();
        Container.DeclareSignal<GameSignal.SettingMenu>();
        Container.DeclareSignal<GameSignal.GameOver>();
        Container.DeclareSignal<GameSignal.QuitGame>();
        
        Container.DeclareSignal<LoadDataSignal>();
        Container.DeclareSignal<SaveDataSignal>();
        
        Container.BindFactory<Pipe, Pipe.PipeFactory>().FromComponentInNewPrefab(_gameSettings.PipePrefab);
    }
}
