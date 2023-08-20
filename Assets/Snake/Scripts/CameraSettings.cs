using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] private List<TriggerChangeOrbit> _triggers;

    private CinemachineFreeLook _freeLook;
    private bool _isOpenDoor = false;

    void Start()
    {
        _freeLook = GetComponent<CinemachineFreeLook>();

        foreach (var trigger in _triggers)
        {
            trigger.ChangeOrbit += OnChangeOrbit;
        }

        SetUpOrbit();
    }

    private void OnDisable()
    {
        foreach (var trigger in _triggers)
        {
            trigger.ChangeOrbit -= OnChangeOrbit;
        }
    }

    private void OnChangeOrbit()
    {
        if (!_isOpenDoor)
        {
            _freeLook.m_YAxis.m_InputAxisValue = 0;
            _freeLook.m_YAxis.Value = 0.5f;
            _isOpenDoor = true;
        }
        else
        {
            SetUpOrbit();
        }
    }

    private void SetUpOrbit()
    {
        _freeLook.m_YAxis.m_InputAxisValue = 1;
        _freeLook.m_YAxis.Value = 1;
        _isOpenDoor = false;
    }
}
