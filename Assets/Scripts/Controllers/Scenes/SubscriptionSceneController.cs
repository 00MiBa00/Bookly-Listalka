using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Views.General;
using Views.Subscription;
using Models;

namespace Controllers.Scenes
{
    public class SubscriptionSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Buttons")] 
        [SerializeField] 
        private Button _nextBtn;
        [SerializeField] 
        private List<SwitchPeriodBtn> _switchPeriodBtns;

        private SubscriptionModel _model;

        protected override void OnSceneEnable()
        {
            UpdateStateSwitchPeriodBtns();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new SubscriptionModel();
        }

        protected override void Subscribe()
        {
            _nextBtn.onClick.AddListener(OnPressNextBtn);
            
            foreach (var btn in _switchPeriodBtns)
            {
                btn.PressBtnAction += OnPressSwitchPeriodBtn;
            }
        }

        protected override void Unsubscribe()
        {
            _nextBtn.onClick.RemoveAllListeners();
            
            foreach (var btn in _switchPeriodBtns)
            {
                btn.PressBtnAction -= OnPressSwitchPeriodBtn;
            }
        }

        private void UpdateStateSwitchPeriodBtns()
        {
            int selectedIndex = _model.SubscriptionPeriodIndex;

            for (int i = 0; i < _switchPeriodBtns.Count; i++)
            {
                if (i == selectedIndex)
                {
                    _switchPeriodBtns[i].SetSelectedState();
                }
                else
                {
                    _switchPeriodBtns[i].SetUnselectedState();
                }
            }
        }

        private void OnPressSwitchPeriodBtn(BtnView btn)
        {
            base.SetClickClip();
            
            int index = _switchPeriodBtns.IndexOf((SwitchPeriodBtn)btn);
            
            _model.ChangeSubPeriodIndex(index);
            
            UpdateStateSwitchPeriodBtns();
        }

        private void OnPressNextBtn()
        {
            base.SetClickClip();
            
            OpenNextScene();
        }

        private void OnPurchaseSuccess()
        {
            _model.SetSubscription();
            
            OpenNextScene();
        }

        private void OpenNextScene()
        {
            string sceneName = _model.GetNextSceneName();
            
            base.LoadScene(sceneName);
        }
    }
}