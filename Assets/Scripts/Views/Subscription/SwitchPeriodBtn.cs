using UnityEngine;
using Views.General;

namespace Views.Subscription
{
    public class SwitchPeriodBtn : BtnView
    {
        [SerializeField]
        private Sprite _activeSprite;
        [SerializeField] 
        private Sprite _disableSprite;

        public void SetSelectedState()
        {
            SetState(true);
        }

        public void SetUnselectedState()
        {
            SetState(false);
        }

        private void SetState(bool value)
        {
            base.SetActive(!value);
            
            Sprite sprite = value ? _activeSprite : _disableSprite;

            base.Btn.image.sprite = sprite;
        }
    }
}