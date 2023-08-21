using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Checkpoints
{
    public class StoreUIControls
    {
        private SpriteRenderer _uISprite;
        private Color _ledOn, _ledOff;
        public StoreUIControls(SpriteRenderer UISprite, Color LedOn, Color LedOff)
        {
            _uISprite = UISprite;
            _ledOn = LedOn;
            _ledOff = LedOff;
        }
        public void Check(bool isChecked, bool playerEnter)
        {
            if (!isChecked)
            {
                if (playerEnter)
                {
                    TurnOnLED();
                }
                else
                {
                    TurnOffLED();
                }
            }
        }
        private void TurnOnLED()
        {
            _uISprite.GetComponent<SpriteRenderer>().color = _ledOn;
        }
        private void TurnOffLED()
        {
            _uISprite.GetComponent<SpriteRenderer>().color = _ledOff;
        }
    }

}
