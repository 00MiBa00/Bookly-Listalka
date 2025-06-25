using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Progress
{
    public class ResultPanel : StopwatchView
    {
        public Action<int> PressBtnAction { get; set; }

        [SerializeField] 
        private InputField _inputField;
        [SerializeField] 
        private Button _saveBtn;

        private void OnEnable()
        {
            Reset();
            
            _saveBtn.onClick.AddListener(Notification);
            _inputField.onValueChanged.AddListener(OnInputCountChanged);
        }

        private void OnDisable()
        {
            _saveBtn.onClick.RemoveAllListeners();
            _inputField.onValueChanged.RemoveAllListeners();
        }

        private void OnInputCountChanged(string value)
        {
            _saveBtn.interactable = !string.IsNullOrEmpty(value);
        }

        private void Notification()
        {
            if (int.TryParse(_inputField.text, out var number))
            {
                PressBtnAction?.Invoke(number);
            }
        }

        private void Reset()
        {
            _inputField.text = "";
        }
    }
}