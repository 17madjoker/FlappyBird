using UnityEngine;
using Zenject;

[DefaultExecutionOrder(99)]
public class AudioManager : MonoBehaviour
{
    [Inject] 
    private GameSettings _gameSettings;

    [Inject] 
    private UIManager _uiManager;

    [Inject] 
    private SaveLoadManager _saveLoadManager;

    [Inject] 
    private SignalBus _signalBus;

    private AudioSource _audioSource;

    private void Start()
    {
        _signalBus.Subscribe<PlayerSignal.Score>(PlayScoreSFX);
        _signalBus.Subscribe<PlayerSignal.Fly>(PlayFlySFX);
        _signalBus.Subscribe<PlayerSignal.Hit>(PlayHitSFX);
        _signalBus.Subscribe<VolumeChangeSignal>(SetVolume);
        _signalBus.Subscribe<AudioStateSingal>(SetAudio);
        _signalBus.Subscribe<LoadDataSignal>(LoadAudioSettings);
        
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        _signalBus.TryUnsubscribe<PlayerSignal.Score>(PlayScoreSFX);
        _signalBus.TryUnsubscribe<PlayerSignal.Fly>(PlayFlySFX);
        _signalBus.TryUnsubscribe<PlayerSignal.Hit>(PlayHitSFX);
        _signalBus.TryUnsubscribe<VolumeChangeSignal>(SetVolume);
        _signalBus.TryUnsubscribe<AudioStateSingal>(SetAudio);
        _signalBus.TryUnsubscribe<LoadDataSignal>(LoadAudioSettings);
    }

    private void SetVolume(VolumeChangeSignal volumeChangeSignal)
    {
        _audioSource.volume = volumeChangeSignal.Volume;
        _signalBus.Fire(new SaveDataSignal(volumeChangeSignal.Volume));
    }

    private void SetAudio(AudioStateSingal audioState)
    {
        _audioSource.enabled = audioState.AudioState;
        _signalBus.Fire(new SaveDataSignal(audioState.AudioState));
    }

    private void LoadAudioSettings(LoadDataSignal loadDataSignal)
    {
        _audioSource.volume = loadDataSignal.Volume;
        _audioSource.enabled = loadDataSignal.AudioStatus;
    }

    private void PlayScoreSFX()
    {
        if (_audioSource.enabled)
            _audioSource.PlayOneShot(_gameSettings.CollectSfx);
    }

    private void PlayFlySFX()
    {
        if (_audioSource.enabled)
            _audioSource.PlayOneShot(_gameSettings.WingSfx);
    }

    private void PlayHitSFX()
    {
        if (_audioSource.enabled)
            _audioSource.PlayOneShot(_gameSettings.HitSfx);
    }
}
