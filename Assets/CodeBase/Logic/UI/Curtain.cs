using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Logic.UI
{
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;

        private void Awake() => DontDestroyOnLoad(gameObject);

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1f;
        }

        public void Hide() => StartCoroutine(FaderCurtain());

        IEnumerator FaderCurtain()
        {
            while (_curtain.alpha > 0f)
            {
                yield return new WaitForSeconds(0.05f);
                _curtain.alpha = Mathf.Clamp(_curtain.alpha - 0.05f, 0f, 1f);
            }

            gameObject.SetActive(false);
        }
    }
}
