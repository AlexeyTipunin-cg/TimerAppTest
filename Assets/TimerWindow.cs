using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerWindow : MonoBehaviour
{
    [SerializeField] private Button _addBtn;
    [SerializeField] private Button _decreaseBtn;
    [SerializeField] private Button _startBtn;
    [SerializeField] private TMP_Text _timerText;

    void Start()
    {
        _addBtn.onClick.AddListener(AddTime);
        _startBtn.onClick.AddListener(StartTimer);
        _decreaseBtn.onClick.AddListener(DecreaseTime);
    }

    private void StartTimer()
    {

    }

    private void AddTime()
    {

    }

    private void DecreaseTime()
    {

    }

    void Update()
    {
        
    }
}
