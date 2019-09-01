using System;
using UnityEngine;
using Zenject;

public class SaveLoadManager : MonoBehaviour
{
    [Inject] 
    private GameSettings _gameSettings;

    public bool LoadAudioStatus()
    {
        int defaultAudioStatus = _gameSettings.DefaultAudioStatus ? 1 : 0;
        bool audioStatus = Convert.ToBoolean(PlayerPrefs.GetInt("AudioStatus", defaultAudioStatus));
        
        return audioStatus;
    }
    
    public float LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat("Volume", _gameSettings.DefaultVolume);
        
        return volume;
    }

    public int LoadMaxScore()
    {
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);

        return maxScore;
    }
    
    public void SaveData(bool audioStatus)
    {
        int _audioStatus = audioStatus ? 1 : 0;
        PlayerPrefs.SetInt("AudioStatus", _audioStatus);
    }

    public void SaveData(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SaveData(int maxScore)
    {
        PlayerPrefs.SetInt("MaxScore", maxScore);
    }
}
