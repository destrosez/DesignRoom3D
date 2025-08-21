using TMPro;
using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _floorPrefab;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _platformPrefab;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _projectNameText;

    private int _widthTiles;
    private int _heightTiles;

    private Transform _wallsRoot;
    private Transform _floorRoot;

    /// <summary>Строит комнату при запуске сцены.</summary>
    private void Start()
    {
        InitRoomData();
        GenerateFloor();
        GenerateWalls();
        GeneratePlatform();
    }

    /// <summary>Читает настройки комнаты и подготавливает родительские объекты.</summary>
    private void InitRoomData()
    {
        var settings = RoomSettings.Instance;

        if (_projectNameText != null)
            _projectNameText.text = settings.ProjectName;

        _widthTiles  = settings.RoomWidth;
        _heightTiles = settings.RoomHeight;

        _floorRoot = new GameObject("FloorRoot").transform;
        _floorRoot.SetParent(transform, false);

        _wallsRoot = new GameObject("WallsRoot").transform;
        _wallsRoot.SetParent(transform, false);
    }

    /// <summary>Генерирует плитки пола.</summary>
    private void GenerateFloor()
    {
        for (int x = 0; x < _widthTiles; x++)
        {
            for (int z = 0; z < _heightTiles; z++)
            {
                Vector3 pos = new Vector3(x, 0f, z);
                var tile = Instantiate(_floorPrefab, pos, Quaternion.identity, _floorRoot);
            }
        }
    }

    /// <summary>Генерирует внешние стены комнаты.</summary>
    private void GenerateWalls()
    {
        for (int x = 0; x < _widthTiles; x++)
        {
            var w = Instantiate(_wallPrefab, new Vector3(x, 1.5f, -1f), Quaternion.identity, _wallsRoot);
        }

        for (int z = 0; z < _heightTiles; z++)
        {
            var w = Instantiate(_wallPrefab, new Vector3(-1f, 1.5f, z), Quaternion.identity, _wallsRoot);
        }

        var corner = Instantiate(_wallPrefab, new Vector3(-1f, 1.5f, -1f), Quaternion.identity, _wallsRoot);
    }

    /// <summary>Создаёт платформу-основание под комнатой.</summary>
    private void GeneratePlatform()
    {
        float centerX = (_widthTiles - 1) * 0.5f;
        float centerZ = (_heightTiles - 1) * 0.5f;

        var platform = Instantiate(_platformPrefab, new Vector3(centerX, -1f, centerZ), Quaternion.identity, transform);
        platform.transform.localScale = new Vector3(Mathf.Max(12f, _widthTiles + 3f), 1f, Mathf.Max(12f, _heightTiles + 3f));
    }
}
