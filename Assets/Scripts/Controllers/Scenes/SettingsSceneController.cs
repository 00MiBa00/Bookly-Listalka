using UnityEngine;
using UnityEngine.UI;
using Enums;

namespace Controllers.Scenes
{
    public class SettingsSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _backBtn;
        [SerializeField] 
        private Button _privacyBtn;
        [SerializeField]
        private Button _termsBtn;
        
        protected override void OnSceneEnable()
        {
            
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            
        }

        protected override void Subscribe()
        {
            _backBtn.onClick.AddListener(OnPressBackBtn);
            _privacyBtn.onClick.AddListener(OnPressPrivacyBtn);
            _termsBtn.onClick.AddListener(OnPressTermsBtn);
        }

        protected override void Unsubscribe()
        {
            _backBtn.onClick.RemoveAllListeners();
            _privacyBtn.onClick.RemoveAllListeners();
            _termsBtn.onClick.RemoveAllListeners();
        }

        private void OnPressBackBtn()
        {
            base.LoadScene(SceneName.Game.ToString());
        }

        private void OnPressPrivacyBtn()
        {
            base.SetClickClip();
        }

        private void OnPressTermsBtn()
        {
            base.SetClickClip();
        }
    }
}