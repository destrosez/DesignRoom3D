using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] private RoomEditController _edit;

    [Header("Panels")]
    [SerializeField] private GameObject _panelChoiceMaterial;
    [SerializeField] private GameObject _panelChoiceCategories;
    [SerializeField] private GameObject _panelRest;
    [SerializeField] private GameObject _panelWorkplace;
    [SerializeField] private GameObject _panelDecor;

    /// <summary>Закрывает все панели и сбрасывает режим при запуске.</summary>
    private void Awake()
    {
        CloseAllPanels();
        if (_edit != null) _edit.SetMode(EditMode.None);
    }

    /// <summary>Закрывает панели и переводит в режим None.</summary>
    public void OnExit()
    {
        CloseAllPanels();
        if (_edit != null) _edit.SetMode(EditMode.None);
    }

    /// <summary>Закрывает панели и переводит в режим Rotate.</summary>
    public void OnRotate()
    {
        CloseAllPanels();
        if (_edit != null) _edit.SetMode(EditMode.Rotate);
    }

    /// <summary>Закрывает панели и переводит в режим Delete.</summary>
    public void OnDelete()
    {
        CloseAllPanels();
        if (_edit != null) _edit.SetMode(EditMode.Delete);
    }

    /// <summary>Открывает панель выбора материала стен.</summary>
    public void OnSelectMaterialOpen()
    {
        CloseAllPanels();
        _panelChoiceMaterial.SetActive(true);
    }

    /// <summary>Открывает панель выбора категорий объектов.</summary>
    public void OnSelectObjectOpen()
    {
        CloseAllPanels();
        _panelChoiceCategories.SetActive(true);
    }

    /// <summary>Открывает панель категории Rest.</summary>
    public void OnCategoryRest()
    {
        _panelChoiceCategories.SetActive(false);
        _panelRest.SetActive(true);
    }

    /// <summary>Открывает панель категории Workplace.</summary>
    public void OnCategoryWorkplace()
    {
        _panelChoiceCategories.SetActive(false);
        _panelWorkplace.SetActive(true);
    }

    /// <summary>Открывает панель категории Decor.</summary>
    public void OnCategoryDecor()
    {
        _panelChoiceCategories.SetActive(false);
        _panelDecor.SetActive(true);
    }

    /// <summary>Возвращает из панели предметов к списку категорий.</summary>
    public void OnBackToCategories()
    {
        _panelRest.SetActive(false);
        _panelWorkplace.SetActive(false);
        _panelDecor.SetActive(false);
        _panelChoiceCategories.SetActive(true);
    }

    /// <summary>Закрывает все панели редактора.</summary>
    public void CloseAllPanels()
    {
        _panelChoiceMaterial.SetActive(false);
        _panelChoiceCategories.SetActive(false);
        _panelRest.SetActive(false);
        _panelWorkplace.SetActive(false);
        _panelDecor.SetActive(false);
    }
}
