using UnityEngine;
using Views.General;
using Models;

namespace Controllers.Scenes
{
    public class OnboardingSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private PanelView _firstPanel;
        [SerializeField] 
        private PanelView _secondPanel;

        private OnboardingModel _model;
        private InitModel _initModel;
        
        protected override void OnSceneEnable()
        {
            _firstPanel.PressBtnAction += OnReceiveAnswerFirstPanel;
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new OnboardingModel();
            _initModel = new InitModel();
        }

        protected override void Subscribe()
        {
            
        }

        protected override void Unsubscribe()
        {
            
        }

        private void OpenSecondPanel()
        {
            _secondPanel.PressBtnAction += OnReceiveAnswerSecondPanel;
            _secondPanel.gameObject.SetActive(true);
        }

        private void OnReceiveAnswerFirstPanel(int answer)
        {
            _firstPanel.PressBtnAction -= OnReceiveAnswerFirstPanel;

            switch (answer)
            {
                case 0:
                    OpenNextScene();
                    break;
                case 1:
                    base.SetClickClip();
                    _firstPanel.gameObject.SetActive(true);
                    OpenSecondPanel();
                    break;
            }
        }
        
        private void OnReceiveAnswerSecondPanel(int answer)
        {
            _secondPanel.PressBtnAction -= OnReceiveAnswerSecondPanel;
            
            OpenNextScene();
        }

        private void OpenNextScene()
        {
            string sceneName = _model.GetNameNextScene();
            
            _initModel.SetIsNotFirstTime();
            
            base.LoadScene(sceneName);
        }
    }
}