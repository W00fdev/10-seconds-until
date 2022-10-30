using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LoadingTextAnimation : MonoBehaviour
{
    private List<string> _textsAnimation;

    private Coroutine _loadingTextAnimation;
    private TextMeshProUGUI _text;

    private const float SecondsPerFrame = 0.3f;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textsAnimation = new List<string>()
        {
            "Loading .  ",
            "Loading .. ",
            "Loading ...",
            "Loading  ..",
            "Loading   .",
        };
    }

    private void OnEnable() => _loadingTextAnimation = StartCoroutine(LoadingTextCoroutine());

    public void OnDisable() => StopCoroutine(_loadingTextAnimation);

    IEnumerator LoadingTextCoroutine()
    {
        int textIndex = 0;
        WaitForSeconds waitingTime = new WaitForSeconds(SecondsPerFrame);
        
        while (true)
        {
            yield return waitingTime;
            
            _text.text = _textsAnimation[textIndex];

            textIndex++;
            if (textIndex >= _textsAnimation.Count)
                textIndex = 0;
        }
    }
}
