using UnityEngine;

public enum EditMode { None, Rotate, Delete, PaintWall, SelectObject }

public class RoomEditController : MonoBehaviour
{
    [Header("Raycast & Camera")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayDistance = 200f;

    [Header("Spawn")]
    [SerializeField] private float _spawnYOffset = 0.01f;

    private EditMode _mode = EditMode.None;
    private Material _pendingWallMaterial;
    private GameObject _pendingPrefab;

    /// <summary>Устанавливает текущий режим и очищает временные данные если нужно.</summary>
    public void SetMode(EditMode mode)
    {
        _mode = mode;
        if (mode != EditMode.PaintWall) _pendingWallMaterial = null;
        if (mode != EditMode.SelectObject) _pendingPrefab = null;
    }

    /// <summary>Включает режим покраски стен выбранным материалом.</summary>
    public void SetPaintWall(Material mat)
    {
        _pendingWallMaterial = mat;
        _mode = EditMode.PaintWall;
    }

    /// <summary>Готовит выбранный префаб для установки на пол.</summary>
    public void SetPendingPrefab(GameObject prefab)
    {
        _pendingPrefab = prefab;
        _mode = EditMode.SelectObject;
    }

    /// <summary>Кэширует ссылку на камеру, если не задана вручную.</summary>
    private void Awake()
    {
        if (_camera == null) _camera = Camera.main;
    }

    /// <summary>Обрабатывает клик мышью в зависимости от режима.</summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Raycast();
            if (hit.transform == null) return;

            if (_mode == EditMode.Delete) TryDelete(hit.transform);
            else if (_mode == EditMode.PaintWall) TryPaintWall(hit.transform);
            else if (_mode == EditMode.SelectObject) TrySpawnOnFloorCell(hit);
            else if (_mode == EditMode.Rotate) TryRotate(hit.transform);
        }
    }

    /// <summary>Делает Raycast из позиции курсора.</summary>
    private RaycastHit Raycast()
    {
        if (_camera == null) return default;
        Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, _rayDistance, ~0, QueryTriggerInteraction.Collide);
        return hit;
    }

    /// <summary>Возвращает корневой объект с тегом Object, если клик был по объекту или его дочернему.</summary>
    private Transform GetObjectRootIfValid(Transform t)
    {
        if (t == null) return null;
        if (t.CompareTag("Object")) return t;
        var p = t.GetComponentInParent<Transform>();
        return (p != null && p.CompareTag("Object")) ? p : null;
    }

    /// <summary>Удаляет кликнутый объект с тегом Object.</summary>
    private void TryDelete(Transform t)
    {
        var obj = GetObjectRootIfValid(t);
        if (obj != null) Destroy(obj.gameObject);
    }

    /// <summary>Применяет выбранный материал к сегменту стены.</summary>
    private void TryPaintWall(Transform t)
    {
        if (_pendingWallMaterial == null) return;
        Transform target = t.CompareTag("Wall") ? t : t.GetComponentInParent<Transform>();
        if (target == null || !target.CompareTag("Wall")) return;
        var r = target.GetComponent<Renderer>();
        if (r != null) r.material = _pendingWallMaterial;
    }

    /// <summary>Спавнит выбранный префаб по центру клетки пола.</summary>
    private void TrySpawnOnFloorCell(RaycastHit hit)
    {
        if (_pendingPrefab == null) return;
        if (!hit.transform.CompareTag("Floor")) return;
        var col = hit.collider as BoxCollider;
        if (col == null) return;
        var b = col.bounds;
        var pos = new Vector3(b.center.x, b.max.y + _spawnYOffset, b.center.z);
        var go = Instantiate(_pendingPrefab, pos, Quaternion.identity);
        go.tag = "Object";
    }

    /// <summary>Поворачивает кликнутый объект на 90° вокруг оси Y.</summary>
    private void TryRotate(Transform t)
    {
        var obj = GetObjectRootIfValid(t);
        if (obj != null) obj.Rotate(0f, 90f, 0f, Space.World);
    }
}
