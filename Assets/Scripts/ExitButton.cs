using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [SerializeField] private ButtonsManager _manager;

    /// <summary>Сбрасывает режимы и закрывает все панели.</summary>
    public void OnClick()
    {
        _manager.OnExit();
    }
}