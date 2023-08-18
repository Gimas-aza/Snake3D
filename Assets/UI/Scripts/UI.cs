using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UI : MonoBehaviour
{
    [SerializeField] private float _timeClicked = 0.5f;
    [SerializeField] private float _multiplierHorizontal = 3;

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
    private Coroutine _coroutineHorizontalMovement = null;

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
        _buttonLeft.RegisterCallback<PointerDownEvent, float>(OnButtonDown, -1f, TrickleDown.TrickleDown);
        _buttonRight.RegisterCallback<PointerDownEvent, float>(OnButtonDown, 1f, TrickleDown.TrickleDown);
        _buttonLeft.RegisterCallback<PointerUpEvent, float>(OnButtonUp, 0f);
        _buttonRight.RegisterCallback<PointerUpEvent, float>(OnButtonUp, 0f);
    }

    private void OnButtonDown<T>(T evl, float horizontal)
    {
        if (_coroutineHorizontalMovement != null)
            StopCoroutine(_coroutineHorizontalMovement);
        
        _coroutineHorizontalMovement = StartCoroutine(HorizontalMovement(horizontal));
    }

    private void OnButtonUp<T>(T evl, float horizontal)
    {
        if (_coroutineHorizontalMovement != null)
            StopCoroutine(_coroutineHorizontalMovement);
        
        _horizontal = 0;
    }

    private IEnumerator HorizontalMovement(float horizontal)
    {
        float multiplierSpeed = _multiplierHorizontal;

        for (float i = 0; i < _timeClicked; i += Time.deltaTime)
        {
            _horizontal = Mathf.Lerp(_horizontal, horizontal, Time.deltaTime * multiplierSpeed);
            multiplierSpeed += Time.deltaTime * 2f;
            yield return new WaitForEndOfFrame();
        }
        _horizontal = horizontal;
        yield return new WaitForEndOfFrame();
    } 

    public void SetHealthBar(int health)
    {
        if (health < Int32.Parse(_healthBar.text.Trim(new char[]  { 'H', 'P' })))
        {
            _healthBar.style.color = new Color(255, 0, 0);
            _healthBar.style.fontSize = 70;
        }

        _healthBar.text = health.ToString() + " HP"; 
        Invoke(nameof(ReturnHealthBar), 0.5f);
    }

    private void ReturnHealthBar()
    {
        _healthBar.style.color = new Color(0, 0, 0);
        _healthBar.style.fontSize = 50;
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
