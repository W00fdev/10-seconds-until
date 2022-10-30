using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UNUSED
/// </summary>
public class CounterUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] List<Sprite> _sprites = new List<Sprite>(10);

    private void Awake()
    {
        _image = GetComponent<Image>();
        UpdateSprite(0);
    }

    public void UpdateSprite(int index)
    {
        _image.sprite = _sprites[index];
    }
}
