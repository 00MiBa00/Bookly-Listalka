using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Views.Progress;
using Models;
using Enums;

namespace Controllers.Scenes
{
    public class ProgressSceneController : AbstractSceneController
    {
        [Space(5)] [Header("views")] 
        [SerializeField]
        private StopwatchView _stopwatchView;
        [SerializeField] 
        private ResultPanel _resultPanel;
        [SerializeField] 
        private HistoryBodyView _historyBodyView;
        
        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _startBtn;
        [SerializeField] 
        private Button _stopBtn;
        [SerializeField] 
        private Button _backBtn;

        private ProgressModel _model;
        private Coroutine _stopwatchCoroutine;
        
        protected override void OnSceneEnable()
        {
            SetState(true);
            UpdateHistory();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new ProgressModel();
        }

        protected override void Subscribe()
        {
            _backBtn.onClick.AddListener(OnPressBackBtn);
            _startBtn.onClick.AddListener(OnPressStartBtn);
            _stopBtn.onClick.AddListener(OnPressStopBtn);
        }

        protected override void Unsubscribe()
        {
            _backBtn.onClick.RemoveAllListeners();
            _startBtn.onClick.RemoveAllListeners();
            _stopBtn.onClick.RemoveAllListeners();
        }

        private void SetState(bool value)
        {
            _startBtn.gameObject.SetActive(value);
            _stopBtn.gameObject.SetActive(!value);

            if (value)
            {
                _model.ResetTime();
                _stopwatchView.UpdateTime(_model.Hour, _model.Minute, _model.Sec);
            }
        }

        private void OnPressBackBtn()
        {
            base.LoadScene(SceneName.Game.ToString());
        }

        private void OnPressStartBtn()
        {
            base.SetClickClip();
            
            _model.CanCheckTime = true;
            _stopwatchCoroutine = StartCoroutine(StartStopWatch());
            
            SetState(false);
        }

        private void OnPressStopBtn()
        {
            base.SetClickClip();
            
            _model.CanCheckTime = false;
            StopCoroutine(_stopwatchCoroutine);
            OpenResultPanel();
        }

        private void OpenResultPanel()
        {
            _resultPanel.UpdateTime(_model.Hour, _model.Minute, _model.Sec);
            _resultPanel.PressBtnAction += OnReceiveAnswerResultPanel;
            _resultPanel.gameObject.SetActive(true);
        }

        private void OnReceiveAnswerResultPanel(int number)
        {
            base.SetClickClip();
            
            _resultPanel.PressBtnAction -= OnReceiveAnswerResultPanel;

            _resultPanel.gameObject.SetActive(false);

            _model.AddTime();
            _model.AddPage(number);
            
            SetState(true);
            
            UpdateHistory();
        }

        private void UpdateHistory()
        {
            if (!_model.CanUpdateAllTimes)
            {
                return;
            }
            
            _historyBodyView.Reset();

            for (var i = _model.HistoryCount-1; i >= 0 ; i--)
            {
                _historyBodyView.AddHistory(_model.LoadTimes()[i], _model.LoadPages()[i]);
            }
        }

        private IEnumerator StartStopWatch()
        {
            while (_model.CanCheckTime)
            {
                _stopwatchView.UpdateTime(_model.Hour, _model.Minute, _model.Sec);

                yield return new WaitForSeconds(1);
                
                _model.UpdateTime();
            }
        }
    }
}