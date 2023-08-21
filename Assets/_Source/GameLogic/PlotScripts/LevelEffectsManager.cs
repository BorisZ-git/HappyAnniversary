using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Platformer.Plot
{
    public class LevelEffectsManager : MonoBehaviour
    {
        [SerializeField] private Image _panel;
        [SerializeField] private float _delayFade;
        [SerializeField] private float _fadeSpeed;
        [SerializeField] private Color _fadeColor;
        private float _valueTo;

        private bool _fade;
        private void Update()
        {
            if (_fade)
            {
                _panel.color = new Color(_panel.color.r,_panel.color.g,_panel.color.b, Mathf.Lerp(_panel.color.a, _valueTo, _fadeSpeed * Time.deltaTime));
                if(_panel.color.a == _valueTo)
                {
                    _fade = false;
                }
            }
        }
        public void FadeINScreen(float speed)
        {
            _fadeSpeed = speed;
            _panel.gameObject.SetActive(true);
            _valueTo = 1;
            _fade = true;
        }
        public void FadeOutScreen(float speed)
        {
            _fadeSpeed = speed;
            _panel.gameObject.SetActive(true);
            _panel.color = _fadeColor;
            _valueTo = 0;
            _fade = true;
        }
    }
}

