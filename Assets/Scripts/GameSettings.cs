using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Settings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [Header("Audio settings")]
    [Range(0, 1f)]
    [SerializeField]
    private float _defaultVolume;
    public float DefaultVolume { get { return _defaultVolume; } }

    [SerializeField]
    private bool _defaultAudioStatus;
    public bool DefaultAudioStatus { get { return _defaultAudioStatus; } }

    [SerializeField] 
    private AudioClip _dieSfx;
    public AudioClip DieSfx { get { return _dieSfx; } }
    
    [SerializeField] 
    private AudioClip _hitSfx;
    public AudioClip HitSfx { get { return _hitSfx; } }
    
    [SerializeField] 
    private AudioClip _collectSfx;
    public AudioClip CollectSfx { get { return _collectSfx; } }
    
    [SerializeField] 
    private AudioClip _wingSfx;
    public AudioClip WingSfx { get { return _wingSfx; } }
    
    [SerializeField] 
    private AudioClip _fallingSfx;
    public AudioClip FallingSfx { get { return _fallingSfx; } }

    [Header("Game settings")] 
    [Range(1f, 6f)] 
    [SerializeField]
    private float _jumpForce;
    public float JumpForce { get { return _jumpForce; } }
    
    [Range(-1.5f, 0f)]
    [SerializeField]
    private float _minPipeOffset;
    public float MinPipeOffset { get { return _minPipeOffset; } }
    
    [Range(0f, 1.5f)]
    [SerializeField]
    private float _maxPipeOffset;
    public float MaxPipeOffset { get { return _maxPipeOffset; } }
    
    [Range(1f, 2f)]
    [SerializeField]
    private float _pipeSpeed;
    public float PipeSpeed { get { return _pipeSpeed; } }
    
    [Range(1f, 2f)]
    [SerializeField]
    private float _pipeTimeSpawn;
    public float PipeTimeSpawn { get { return _pipeTimeSpawn; } }

    [SerializeField] 
    private GameObject _pipePrefab;
    public GameObject PipePrefab { get { return _pipePrefab; } }
}
