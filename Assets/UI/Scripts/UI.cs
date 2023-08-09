using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UI : MonoBehaviour
{
    private UIDocument _document;
    private Label _healthBar;
    private Button _buttonLeft;
    private Button _buttonRight;
    private VisualElement _containerStatistics;
    private Label _statisticAteOfApples;
    private Label _statisticKilledEnemies;
    private Label _textLevelPassed;
    private Label _textLevelFailed;
    private float _horizontal;

    public float Horizontal => _horizontal;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _healthBar = _document.rootVisualElement.Q<Label>("HP");
        _buttonLeft = _document.rootVisualElement.Q<Button>("LeftButton");
        _buttonRight = _document.rootVisualElement.Q<Button>("RightButton");
        _containerStatistics = _document.rootVisualElement.Q<VisualElement>("Statistics");
        _statisticAteOfApples = _containerStatistics.Q<Label>("EatenApples");
        _statisticKilledEnemies = _containerStatistics.Q<Label>("KilledEnemies");
        _textLevelPassed = _containerStatistics.Q<Label>("TextLevelPassed");
        _textLevelFailed = _containerStatistics.Q<Label>("TextLevelFailed");
    }

    private void Start()
    {
        _buttonLeft.RegisterCallback<PointerDownEvent, float>(OnButtonClick, -1f, TrickleDown.TrickleDown);
        _buttonRight.RegisterCallback<PointerDownEvent, float>(OnButtonClick, 1f, TrickleDown.TrickleDown);
        _buttonLeft.RegisterCallback<PointerUpEvent, float>(OnButtonClick, 0f);
        _buttonRight.RegisterCallback<PointerUpEvent, float>(OnButtonClick, 0f);
    }

    private void OnButtonClick<T>(T evl, float horizontal)
    {
        _horizontal = horizontal;
    }

    public void SetHealthBar(int health)
    {
        _healthBar.text = health.ToString() + " HP";
    }

    public void SetActiveStatistics(bool isActive)
    {
        _containerStatistics.style.display = isActive ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void SetStatisticApples(int numberEatenApples, int numberApples)
    {
        _statisticAteOfApples.text = numberEatenApples.ToString() + " / " + numberApples.ToString();
    }

    public void SetStatisticEnemies(int numberDiedEnemies, int numberEnemies)
    {
        _statisticKilledEnemies.text = numberDiedEnemies.ToString() + " / " + numberEnemies.ToString();
    }

    public void SetLevelPassed(bool isPassed)
    {
        if (isPassed)
        {
            _textLevelFailed.style.display = DisplayStyle.None;
            _textLevelPassed.style.display = DisplayStyle.Flex;
        }
        else
        {
            _textLevelPassed.style.display = DisplayStyle.None;
            _textLevelFailed.style.display = DisplayStyle.Flex;
        }
    }
}
