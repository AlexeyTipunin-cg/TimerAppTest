using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine;

public class MyButton : Button
{
    public event Action<float> onTimeChange;
    public event Action onPointerUp;
    public event Action onPointerDown;

    public float _reactionTime = 1f;
    private bool _isPressed;
    private float _elapsedTime;
    public override void OnPointerDown(PointerEventData eventData)
    {

        base.OnPointerDown(eventData);
        onPointerDown?.Invoke();
        _isPressed = true;
        _elapsedTime = 0;
        StartCoroutine(CountElapsedTime());
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        onPointerUp?.Invoke();
        _isPressed = false;
    }

    private IEnumerator CountElapsedTime()
    {
        onTimeChange?.Invoke(_elapsedTime);
        yield return new WaitForSeconds(_reactionTime);

        while (_isPressed)
        {
            _elapsedTime += Time.deltaTime;
            onTimeChange?.Invoke(_elapsedTime);
            yield return null;
        }
    }
}
