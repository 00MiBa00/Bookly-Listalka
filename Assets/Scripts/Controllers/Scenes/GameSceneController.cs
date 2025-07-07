using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Controllers.Game;
using Views.Game;
using Views.General;

using Models;
using Enums;

namespace Controllers.Scenes
{
    public class GameSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Controllers")] 
        [SerializeField]
        private PreviewBooksController _previewBooksController;

        [Space(5)] [Header("Views")] 
        [SerializeField]
        private VIPView _vipView;
        [SerializeField] 
        private StatusView _statusView;
        [SerializeField] 
        private AllBooksPanel _allBooksPanel;
        [SerializeField] 
        private BookDescriptionPanel _bookDescriptionPanel;
        [SerializeField] 
        private PanelView _confirmationPanel;
        [SerializeField] 
        private PanelView _privacyPanel;

        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private BtnView _progressBtn;
        [SerializeField] 
        private BtnView _rewardBtn;
        [SerializeField] 
        private BtnView _subscriptionBtn;
        [SerializeField] 
        private Button _createBtn;
        [SerializeField] 
        private Button _privacyBtn;
        
        private GameModel _model;
        private EditModel _editModel;
        
        protected override void OnSceneEnable()
        {
            SetBooks();
            SetState();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new GameModel();
            _editModel = new EditModel();
        }

        protected override void Subscribe()
        {
            _previewBooksController.Subscribe();
            _previewBooksController.PressAllBtnAction += OnPressAllBtn;
            _previewBooksController.PressReadBtnAction += OnPressReadBtn;
            
            _privacyBtn.onClick.AddListener(OnPressPrivacyBtn);
            _createBtn.onClick.AddListener(OnPressCreateBtn);
            
            _progressBtn.PressBtnAction += OnPressProgressBtn;
            _rewardBtn.PressBtnAction += OnPressRewardBtn;
            _subscriptionBtn.PressBtnAction += OnPressPremiumBtn;

            EditSceneController.OnEditSceneClosed += OnEditSceneClosed;
        }

        protected override void Unsubscribe()
        {
            _previewBooksController.Unsubscribe();
            _previewBooksController.PressAllBtnAction -= OnPressAllBtn;
            _previewBooksController.PressReadBtnAction -= OnPressReadBtn;
            
            _privacyBtn.onClick.RemoveAllListeners();
            _createBtn.onClick.RemoveAllListeners();
            
            _progressBtn.PressBtnAction -= OnPressProgressBtn;
            _rewardBtn.PressBtnAction -= OnPressRewardBtn;
            _subscriptionBtn.PressBtnAction -= OnPressPremiumBtn;
            
            EditSceneController.OnEditSceneClosed -= OnEditSceneClosed;
        }

        private void SetBooks()
        {
            List<BookModel> models = new List<BookModel>(_model.GetBookModels());

            _previewBooksController.Initialize(models);
        }

        private void SetState()
        {
            bool isVip = _model.IsVipActive;
            
            _vipView.SetState(!isVip);
            _statusView.UpdateStatus(_model.StatusString);

            _progressBtn.gameObject.SetActive(isVip);
            _rewardBtn.gameObject.SetActive(isVip);
            _subscriptionBtn.gameObject.SetActive(!isVip);
        }

        private void OnPressRewardBtn(BtnView btn)
        {
            base.LoadScene(SceneName.Reward.ToString());
        }

        private void OnPressProgressBtn(BtnView btn)
        {
            base.LoadScene(SceneName.Progress.ToString());
        }

        private void OnPressPremiumBtn(BtnView btn)
        {
            base.LoadScene(SceneName.Subscription.ToString());
        }

        private void OnPressCreateBtn()
        {
            base.LoadScene(SceneName.Create.ToString());
        }

        private void OnPressAllBtn()
        {
            base.SetClickClip();
            
            _allBooksPanel.SetBooks(_model.GetBookModels());
            _allBooksPanel.PressBtnAction += OnReceiveAnswerAllBooksPanel;
            _allBooksPanel.PressItemAction += OnPressReadBtn;
            _allBooksPanel.gameObject.SetActive(true);
        }

        private void OnPressPrivacyBtn()
        {
            base.SetClickClip();
            
            _privacyPanel.PressBtnAction += OnReceiveAnswerPrivacyPanel;
            OpenPrivacyPanel();
        }

        private void OnReceiveAnswerPrivacyPanel(int answer)
        {
            base.SetClickClip();
            
            _privacyPanel.PressBtnAction -= OnReceiveAnswerPrivacyPanel;
            ClosePrivacyPanel();
        }

        private void OnPressReadBtn(int index)
        {
            base.SetClickClip();
            
            OpenDescriptionPanel(index);
        }

        private void SetActivePrivacyPanel(bool value)
        {
            _privacyPanel.gameObject.SetActive(value);
        }

        private void OpenPrivacyPanel()
        {
            SetActivePrivacyPanel(true);
        }

        private void ClosePrivacyPanel()
        {
            SetActivePrivacyPanel(false);
        }

        private void OpenDescriptionPanel(int index)
        {
            _model.SetSelectedBookIndex(index);
            
            SetInfoDescriptionPanel();
        }

        private void OpenConfirmationPanel()
        {
            _confirmationPanel.PressBtnAction += OnReceiveAnswerConfirmationPanel;
            _confirmationPanel.gameObject.SetActive(true);
        }

        private void OnReceiveAnswerConfirmationPanel(int answer)
        {
            base.SetClickClip();
            
            _confirmationPanel.PressBtnAction -= OnReceiveAnswerConfirmationPanel;

            switch (answer)
            {
                case 0:
                    DeleteBook();
                    break;
                case 1:
                    _confirmationPanel.gameObject.SetActive(false);
                    break;
            }
        }

        private void OnReceiveAnswerAllBooksPanel(int answer)
        {
            base.SetClickClip();
            
            _allBooksPanel.Unsubscribe();
            
            _allBooksPanel.PressBtnAction -= OnReceiveAnswerAllBooksPanel;
            _allBooksPanel.PressItemAction -= OnPressReadBtn;
            
            if (answer == 0)
            {
                _allBooksPanel.gameObject.SetActive(false);
                _previewBooksController.SetCheckSwipe(true);
            }
            else
            {
                base.LoadScene(SceneName.Create.ToString());
            }
        }

        private void OnReceiveAnswerBookDescriptionPanel(int answer)
        {
            switch (answer)
            {
                case 0:
                    base.SetClickClip();
                    _bookDescriptionPanel.PressBtnAction -= OnReceiveAnswerBookDescriptionPanel;
                    _bookDescriptionPanel.gameObject.SetActive(false);
                    break;
                case 1:
                    base.SetClickClip();
                    OnPressDeleteBook();
                    break;
                case 2:
                    _editModel.SetIndex(_model.SelectedBookIndex);
                    base.LoadScene(SceneName.Edit.ToString(), false);
                    break;
            }
        }

        private void OnPressDeleteBook()
        {
            OpenConfirmationPanel();
        }

        private async void DeleteBook()
        {
            await _model.DeleteBook();

            SetBooks();
            SetState();

            List<BookModel> models = new List<BookModel>(_model.GetBookModels());

            _previewBooksController.Initialize(models);
            _allBooksPanel.SetBooks(models);
            
            _confirmationPanel.gameObject.SetActive(false);
            OnReceiveAnswerBookDescriptionPanel(0);
        }

        private void OnEditSceneClosed()
        {
            UpdateDescription();
        }

        private void SetInfoDescriptionPanel()
        {
            List<BookModel> models = new List<BookModel>(_model.GetBookModels());
            _bookDescriptionPanel.SetActiveDeleteBtn(_model.SelectedBookIndex > 0);
            _bookDescriptionPanel.SetDescription(models[_model.SelectedBookIndex]);
            _bookDescriptionPanel.PressBtnAction += OnReceiveAnswerBookDescriptionPanel;
            _bookDescriptionPanel.gameObject.SetActive(true);
        }

        private void UpdateDescription()
        {
            SetBooks();

            List<BookModel> models = new List<BookModel>(_model.GetBookModels());
            
            _allBooksPanel.SetBooks(models);
            
            SetInfoDescriptionPanel();
        }
    }
}