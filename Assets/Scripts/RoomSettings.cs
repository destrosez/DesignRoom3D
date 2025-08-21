using UnityEngine;

public class RoomSettings : MonoBehaviour
{
    public static RoomSettings Instance;

    [Header("Room Settings")]
    [SerializeField] private string _projectName;
    [SerializeField] private int _roomWidth;
    [SerializeField] private int _roomHeight;

    public string ProjectName { get => _projectName; set => _projectName = value; }
    public int RoomWidth      { get => _roomWidth;  set => _roomWidth  = value; }
    public int RoomHeight     { get => _roomHeight; set => _roomHeight = value; }

    /// <summary>Инициализирует синглтон и сохраняет объект между сценами.</summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>Проверяет валидность размеров комнаты.</summary>
    public bool IsRoomValid()
    {
        return _roomWidth >= 1 && _roomHeight >= 1;
    }
}