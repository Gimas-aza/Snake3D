using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private float _timeClicked = 0.5f;
    [SerializeField] private float _multiplierHorizontal = 3;

    private Scenes _scenes;
    private UIDocument _document;
    private VisualElement _blockInput;
    private Label _healthBar;
    private float _healthBarFontSize;
    private Button _buttonLeft;
    private Button _buttonRight;
    private VisualElement _containerStatistics;
    private Label _statisticAteOfApples;
    private Label _statisticKilledEnemies;
    private Label _textLevelPassed;
    private Label _textLevelFailed;
    private float _horizontal;
    private Coroutine _coroutineHorizontalMovement;
    private Button _resetLevel;

    public float Horizontal => _horizontal;

    private void Awake()
    {
        _scenes = new Scenes();
        _document = GetComponent<UIDocument>();
        _blockInput = _document.rootVisualElement.Q<VisualElement>("BlockInput");
        _healthBar = _document.rootVisualElement.Q<Label>("HP");
        _buttonLeft = _document.rootVisualElement.Q<Button>("LeftButton");
        _buttonRight = _document.rootVisualElement.Q<Button>("RightButton");
        _containerStatistics = _document.rootVisualElement.Q<VisualElement>("Statistics");
        _statisticAteOfApples = _containerStatistics.Q<Label>("EatenApples");
        _statisticKilledEnemies = _containerStatistics.Q<Label>("KilledEnemies");
        _textLevelPassed = _containerStatistics.Q<Label>("TextLevelPassed");
        _textLevelFailed = _containerStatistics.Q<Label>("TextLevelFailed");
        _resetLevel = _containerStatistics.Q<Button>("ResetLevel");
    }

    private void Start()
    {
        _coroutineHorizontalMovement = null;
        _healthBarFontSize = _healthBar.resolvedStyle.fontSize;
        SetActiveBlockInput(true);
        _buttonLeft.RegisterCallback<PointerDownEvent, float>(OnButtonDown, -1f, TrickleDown.TrickleDown);
        _buttonRight.RegisterCallback<PointerDownEvent, float>(OnButtonDown, 1f, TrickleDown.TrickleDown);
        _buttonLeft.RegisterCallback<PointerUpEvent, float>(OnButtonUp, 0f);
        _buttonRight.RegisterCallback<PointerUpEvent, float>(OnButtonUp, 0f);
        _resetLevel.RegisterCallback<PointerDownEvent>(OnButtonResetLevel, TrickleDown.TrickleDown);
    }

    private void OnButtonDown<T>(T evl, float horizontal)
    {
        if (_coroutineHorizontalMovement != null)
            StopCoroutine(_coroutineHorizontalMovement);
        
        _coroutineHorizontalMovement = StartCoroutine(HorizontalMovement(horizontal, _timeClicked));
    }

    private void OnButtonUp<T>(T evl, float horizontal)
    {
        if (_coroutineHorizontalMovement != null)
            StopCoroutine(_coroutineHorizontalMovement);
        
        _coroutineHorizontalMovement = StartCoroutine(HorizontalMovement(horizontal, 0.2f));
    }

    private IEnumerator HorizontalMovement(float horizontal, float timeClicked)
    {
        float multiplierSpeed = _multiplierHorizontal;

        for (float i = 0; i < timeClicked; i += Time.deltaTime)
        {
            _horizontal = Mathf.Lerp(_horizontal, horizontal, Time.deltaTime * multiplierSpeed);
            multiplierSpeed += Time.deltaTime * 2f;
            yield return new WaitForEndOfFrame();
        }
        _horizontal = horizontal;
        yield return new WaitForEndOfFrame();
    } 

    private void OnButtonResetLevel(PointerDownEvent evt)
    {
        _scenes.ReastartLevel();
    }

    public void SetHealthBar(int health)
    {
        if (health < Int32.Parse(_healthBar.text.Trim(new char[]  { 'H', 'P' })))
        {
            _healthBar.style.color = new Color(255, 0, 0);
            _healthBar.style.fontSize = _healthBarFontSize * 1.4f;
        }

        _healthBar.text = health.ToString() + " HP"; 
        Invoke(nameof(ReturnHealthBar), 0.5f);
    }

    private void ReturnHealthBar()
    {
        _healthBar.style.color = new Color(0, 0, 0);
        _healthBar.style.fontSize = _healthBarFontSize;
    }

    public void SetActiveBlockInput(bool isActive)
    {
        _blockInput.style.display = isActive ? DisplayStyle.Flex : DisplayStyle.None;
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
