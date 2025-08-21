using UnityEngine;

public class SelectObjectItemButton : MonoBehaviour
{
    [SerializeField] private RoomEditController _edit;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _panelToClose;

    /// <summary>Назначает префаб для установки и закрывает текущую панель.</summary>
    public void OnClick()
    {
        if (_edit == null || _prefab == null) return;
        _edit.SetPendingPrefab(_prefab);
        if (_panelToClose != null) _panelToClose.SetActive(false);
    }
}