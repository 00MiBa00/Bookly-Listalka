using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class AllBooksDescriptionItemView : DescriptionBook
    {
        public Action<AllBooksDescriptionItemView> PressBtnAction { get; set; }

        [SerializeField] 
        private Button _btn;

        private void OnEnable()
        {
            _btn.onClick.AddListener(Notification);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveAllListeners();
        }

        private void Notification()
        {
            PressBtnAction?.Invoke(this);
        }
    }
}