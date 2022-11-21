using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buttons : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup _layout;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _parent;
    // Start is called before the first frame update
    void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_layout.transform);

        float width = _prefab.GetComponent<RectTransform>().rect.width;
        var transforms = new List<RectTransform>();

        for (int i = 0; i < _layout.transform.childCount; i++)
        {
            transforms.Add((RectTransform)_layout.transform.GetChild(i));
        }

        for (int i = 0; i < 3; i++)
        {
            var gameObject = Instantiate(_prefab, _parent.transform);
            var tr = (RectTransform)gameObject.transform;
            tr.anchoredPosition = new Vector3(-0.5f * width * i - 0.5f * width, transforms[i].anchoredPosition.y, 0);
            LeanTween.moveX(gameObject, transforms[i].anchoredPosition.x, 1f).setEaseOutBounce();
        }
    }
}
