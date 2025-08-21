using UnityEngine;

public class RotateButton : MonoBehaviour
{
    [SerializeField] private ButtonsManager _manager;

    /// <summary>Переключает редактор в режим поворота объектов.</summary>
    public void OnClick()
    {
        _manager.OnRotate();
    }
}