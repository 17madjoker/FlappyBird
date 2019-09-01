using UnityEngine;

public class SaveDataSignal
{
    private SaveLoadManager _saveLoadManager = GameObject.FindObjectOfType<SaveLoadManager>();
    
//    public float Volume { get; set; }
//    
//    public bool AudioStatus { get; set; }
//    
//    public int MaxScore { get; set; }

    public SaveDataSignal(float volume)
    {
        _saveLoadManager.SaveData(volume);
    }
    
    public SaveDataSignal(bool audioStatus)
    {
        _saveLoadManager.SaveData(audioStatus);
    }
    
    public SaveDataSignal(int maxScore)
    {
        _saveLoadManager.SaveData(maxScore);
    }
    
//    [Inject] 
//    private SaveLoadManager _saveLoadManager;
//
//    public SaveDataSignal(bool audioStatus)
//    {
//        _saveLoadManager.SaveData(audioStatus);
//    }
//    
//    public SaveDataSignal(float volume)
//    {
//        _saveLoadManager.SaveData(volume);
//    }
//
//    public SaveDataSignal(int maxScore)
//    {
//        _saveLoadManager.SaveData(maxScore);
//    }
}
