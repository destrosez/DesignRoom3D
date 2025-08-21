using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _sliderWidth;
    [SerializeField] private Slider _sliderHeight;
    [SerializeField] private TextMeshProUGUI _textWidth;
    [SerializeField] private TextMeshProUGUI _textHeight;
    [SerializeField] private TMP_InputField _inputProjectName;

    /// <summary>Инициализирует слайдеры и обновляет подписи.</summary>
    private void Start()
    {
        _sliderWidth.minValue = 0;
        _sliderWidth.maxValue = 10;
        _sliderWidth.wholeNumbers = true;

        _sliderHeight.minValue = 0;
        _sliderHeight.maxValue = 10;
        _sliderHeight.wholeNumbers = true;

        UpdateWidth();
        UpdateHeight();
    }

    /// <summary>Обновляет подписи значений слайдеров каждый кадр.</summary>
    private void Update()
    {
        UpdateWidth();
        UpdateHeight();
    }

    /// <summary>Обновляет текст ширины комнаты.</summary>
    public void UpdateWidth()
    {
        if (_textWidth != null) _textWidth.text = $"Ширина: {_sliderWidth.value}";
    }

    /// <summary>Обновляет текст длины комнаты.</summary>
    public void UpdateHeight()
    {
        if (_textHeight != null) _textHeight.text = $"Длина: {_sliderHeight.value}";
    }

    /// <summary>Сохраняет настройки комнаты и загружает сцену редактора.</summary>
    public void StartProject()
    {
        var roomSettings = RoomSettings.Instance;
        roomSettings.ProjectName = _inputProjectName != null ? _inputProjectName.text : string.Empty;
        roomSettings.RoomWidth   = (int)_sliderWidth.value;
        roomSettings.RoomHeight  = (int)_sliderHeight.value;

        if (!roomSettings.IsRoomValid())
        {
            return;
        }

        SceneManager.LoadScene(1);
    }

    /// <summary>Выходит из приложения.</summary>
    public void Exit()
    {
        Application.Quit();
    }
}