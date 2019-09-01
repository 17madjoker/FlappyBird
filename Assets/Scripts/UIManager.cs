using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[DefaultExecutionOrder(99)]
public class UIManager : MonoBehaviour
{
    private List<GameObject> _uiPanels;

    [Header("Score panel")]
    [SerializeField] 
    private Text _scoreText;
    public Text ScoreText { get { return _scoreText; } set { _scoreText = value; } }   
    
    [SerializeField] 
    private Text _maxScoreText;
    public Text MaxScoreText { get { return _maxScoreText; } set { _maxScoreText = value; } }   

    [Header("Main menu panel")]
    [SerializeField] 
    private GameObject _mainMenuPanel;
    public GameObject MainMenuPanel { get { return _mainMenuPanel; } }

    [SerializeField]
    private Button _playButton;
    public Button PlayButton { get { return _playButton; } }
    
    [SerializeField]
    private Button _settingButton;
    public Button SettingButton { get { return _settingButton; } }
    
    [SerializeField]
    private Button _quitButton;
    public Button QuitButton { get { return _quitButton; } }

    [Header("Game over panel")]
    [SerializeField] 
    private GameObject _gameOverPanel;
    public GameObject GameOverPanel { get { return _gameOverPanel; } }
    
    [SerializeField]
    private Button _playAgainButton;
    public Button PlayAgainButton { get { return _playAgainButton; } }
    
    [SerializeField]
    private Button _menuButton;
    public Button MenuButton { get { return _menuButton; } }

    [Header("Setting panel")]
    [SerializeField] 
    private GameObject _settingsPanel;
    public GameObject SettingsPanel{ get { return _settingsPanel; } }
    
    [SerializeField]
    private Button _settingMenuButton;
    public Button SettingMenuButton { get { return _settingMenuButton; } }

    [SerializeField]
    private Slider _volumeSlider;
    public Slider VolumeSlider { get { return _volumeSlider; } }
    
    [SerializeField]
    private Toggle _audioToggle;
    public Toggle AudioToggle { get { return _audioToggle; } }

    [Inject] 
    private SignalBus _signalBus;

    private void Start()
    {
        CreatePanelList();
        
        VolumeSlider.onValueChanged.AddListener(delegate { SetVolume();});
        AudioToggle.onValueChanged.AddListener(delegate { SetAudio();});

        DelegateButtons();
        
        _signalBus.Subscribe<PlayerSignal.Score>(PlayerScore);
        _signalBus.Subscribe<PlayerSignal.SetRecord>(PlayerSetRecord);
        _signalBus.Subscribe<LoadDataSignal>(LoadSettings);
        _signalBus.Subscribe<GameSignal.LaunchGame>(LaunchGame);
        _signalBus.Subscribe<GameSignal.GameOver>(GameOver);
    }

    private void OnDestroy()
    {
        _signalBus.TryUnsubscribe<PlayerSignal.Score>(PlayerScore);
        _signalBus.TryUnsubscribe<PlayerSignal.SetRecord>(PlayerSetRecord);
        _signalBus.TryUnsubscribe<LoadDataSignal>(LoadSettings);
        _signalBus.TryUnsubscribe<GameSignal.LaunchGame>(LaunchGame);
        _signalBus.TryUnsubscribe<GameSignal.GameOver>(GameOver);
    }

    private void SetVolume()
    {
        _signalBus.Fire(new VolumeChangeSignal { Volume = VolumeSlider.value });
    }

    private void SetAudio()
    {
        _signalBus.Fire(new AudioStateSingal { AudioState = AudioToggle.isOn });
    }

    private void LoadSettings(LoadDataSignal loadDataSignal)
    {
        VolumeSlider.value = loadDataSignal.Volume;
        AudioToggle.isOn = loadDataSignal.AudioStatus;
        MaxScoreText.text = "Max score: " + loadDataSignal.MaxScore;
    }

    private void PlayerScore(PlayerSignal.Score _playerScoreSignal)
    {
        _scoreText.text = _playerScoreSignal.CurrentScore.ToString();
    }

    private void PlayerSetRecord()
    {
        _scoreText.color = new Color32(129,199,132, 255);
        _maxScoreText.color = new Color32(229, 115, 115, 255);
    }

    private void LaunchGame()
    {
        SelectUIPanel(MainMenuPanel);
    }

    private void StartGame()
    {
        ScoreText.text = "0";
        _signalBus.Fire(new GameSignal.StartGame());
        
        HideUIPanels();
        
        _scoreText.color = new Color32(229, 115, 115, 255);
        _maxScoreText.color = Color.white;
    }

    private void SettingMenu()
    {
        _signalBus.Fire(new GameSignal.SettingMenu());
        
        SelectUIPanel(SettingsPanel);
    }

    private void GameOver()
    {
        SelectUIPanel(GameOverPanel);
    }

    private void QuitGame()
    {
        _signalBus.Fire(new GameSignal.QuitGame());
    }

    private void SelectUIPanel(GameObject uiPanel)
    {
        foreach (var panel in _uiPanels)
        {
            if (uiPanel != null && uiPanel.name == panel.name)
                panel.SetActive(true);

            else
                panel.SetActive(false);
        }
    }

    private void HideUIPanels()
    {
        foreach (var panel in _uiPanels)
        {
            panel.SetActive(false);
        }
    }

    private void CreatePanelList()
    {
        _uiPanels = new List<GameObject>();
        _uiPanels.Add(MainMenuPanel);
        _uiPanels.Add(SettingsPanel);
        _uiPanels.Add(GameOverPanel);
    }

    private void DelegateButtons()
    {
        // Main menu panel
        PlayButton.onClick.AddListener(delegate { StartGame(); });
        SettingButton.onClick.AddListener(delegate { SettingMenu(); });
        QuitButton.onClick.AddListener(delegate { QuitGame(); });
        
        // Game over panel
        PlayAgainButton.onClick.AddListener(delegate { StartGame(); });
        MenuButton.onClick.AddListener(delegate { LaunchGame(); });
        
        // Setting panel
        SettingMenuButton.onClick.AddListener(delegate { LaunchGame(); });
    }
}
