using UnityEngine;

public class SelectMaterialButton : MonoBehaviour
{
    [SerializeField] private RoomEditController _edit;
    [SerializeField] private Material _wallMaterial;
    [SerializeField] private GameObject _panelToClose;

    /// <summary>Устанавливает материал стен и закрывает панель выбора.</summary>
    public void OnClick()
    {
        if (_edit == null || _wallMaterial == null) return;
        _edit.SetPaintWall(_wallMaterial);
        if (_panelToClose != null) _panelToClose.SetActive(false);
    }
}