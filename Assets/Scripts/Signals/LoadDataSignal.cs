using UnityEngine;

public class LoadDataSignal
{   
    private SaveLoadManager _saveLoadManager = GameObject.FindObjectOfType<SaveLoadManager>();
    
    public float Volume { get; set; }
    
    public bool AudioStatus { get; set; }
    
    public int MaxScore { get; set; }

    public LoadDataSignal()
    {
        Volume = _saveLoadManager.LoadVolume();
        AudioStatus = _saveLoadManager.LoadAudioStatus();
        MaxScore = _saveLoadManager.LoadMaxScore();
    }

//    [Inject] 
//    private SaveLoadManager _saveLoadManager;
//   
//    public float Volume
//    {
//        get { return _saveLoadManager.LoadVolume(); }
//    }
//
//    public bool AudioStatus
//    {
//        get { return _saveLoadManager.LoadAudioStatus(); }
//    }
//
//    public int MaxScore
//    {
//        get { return _saveLoadManager.LoadMaxScore(); }
//    }
}
