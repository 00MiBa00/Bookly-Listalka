using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.General
{
    [RequireComponent(typeof(Button))]
    public class BtnView : MonoBehaviour
    {
        public Action<BtnView> PressBtnAction { get; set; }
        
        [SerializeField]
        private Button _btn;

        protected Button Btn => _btn == null ? GetComponent<Button>() : _btn;

        private void Awake()
        {
            _btn ??= GetComponent<Button>();
        }

        private void OnEnable()
        {
            _btn.onClick.AddListener(Notification);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveAllListeners();
        }

        public void SetActive(bool value)
        {
            _btn ??= GetComponent<Button>();
            
            _btn.interactable = value;
        }

        private void Notification()
        {
            PressBtnAction?.Invoke(this);
        }
    }
}