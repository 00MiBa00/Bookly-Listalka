using UnityEngine;
using Views.General;

namespace Views.Create
{
    public class SelectorBtn : BtnView
    {
        [SerializeField]
        private Sprite _defaultSprite;
        [SerializeField]
        private Sprite _activeSprite;

        public void SetDefaultState()
        {
            SetState(_defaultSprite);
        }

        public void SetActiveState()
        {
            SetState(_activeSprite);
        }

        private void SetState(Sprite sprite)
        {
            base.Btn.image.sprite = sprite;
        }
    }
}