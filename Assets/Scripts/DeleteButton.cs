using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] private ButtonsManager _manager;

    /// <summary>Переключает редактор в режим удаления объектов.</summary>
    public void OnClick()
    {
        _manager.OnDelete();
    }
}