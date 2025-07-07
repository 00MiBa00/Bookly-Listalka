using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Views.Create;
using Models;
using Enums;

namespace Controllers.Scenes
{
    public class EditSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Input fields")] 
        [SerializeField]
        private InputField _nameInputField;
        [SerializeField] 
        private InputField _descriptionInputField;

        [Space(5)] [Header("Views")] 
        [SerializeField]
        private GenresBodyView _genresBodyView;
        [SerializeField] 
        private RateBodyView _rateBodyView;
        [SerializeField] 
        private CharCountView _charCountView;

        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _backBtn;
        [SerializeField] 
        private Button _saveBtn;

        private EditModel _model;
        
        public delegate void SceneClosedHandler();
        public static event SceneClosedHandler OnEditSceneClosed;
        
        protected override void OnSceneEnable()
        {
            SetBookInfo();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new EditModel();
        }

        protected override void Subscribe()
        {
            _backBtn.onClick.AddListener(OnPressBackBtn);
            _saveBtn.onClick.AddListener(OnPressSaveBtn);
            
            _nameInputField.onEndEdit.AddListener(OnNameChanged);
            _descriptionInputField.onEndEdit.AddListener(OnDescriptionChanged);
            _descriptionInputField.onValueChanged.AddListener(OnDescriptionCharCountUpdated);

            _genresBodyView.PressBtnAction += OnPressGenreBtn;
            _rateBodyView.PressBtnAction += OnPressRateBtn;
        }

        protected override void Unsubscribe()
        {
            _backBtn.onClick.RemoveAllListeners();
            _saveBtn.onClick.RemoveAllListeners();
            
            _nameInputField.onEndEdit.RemoveAllListeners();
            _descriptionInputField.onEndEdit.RemoveAllListeners();
            _descriptionInputField.onValueChanged.RemoveAllListeners();

            _genresBodyView.PressBtnAction -= OnPressGenreBtn;
            _rateBodyView.PressBtnAction -= OnPressRateBtn;
        }

        private void SetBookInfo()
        {
            _nameInputField.text = _model.BookName;
            _descriptionInputField.text = _model.BookDescription;

            List<int> genreIndexes = new List<int>(_model.GenreIndexes);

            for (int i = 0; i < genreIndexes.Count; i++)
            {
                _genresBodyView.SetBtnState(genreIndexes[i], true);
            }
            
            _rateBodyView.SetStates(_model.BookRate);
            
            OnDescriptionCharCountUpdated(_descriptionInputField.text);
        }
        
        private void OnNameChanged(string name)
        {
            _model.ChangeName(name);
            
            SetActiveSaveBtn();
        }

        private void OnDescriptionChanged(string description)
        {
            _model.ChangeDescription(description);
            
            SetActiveSaveBtn();
        }
        
        private void OnPressGenreBtn(int index)
        {
            bool? canChange = _model.TryAddGenre(index);
            
            switch (canChange)
            {
                case true:
                    base.SetClickClip();
                    _genresBodyView.SetBtnState(index, true);
                    SetActiveSaveBtn();
                    break;
                case false:
                    base.SetClickClip();
                    _genresBodyView.SetBtnState(index, false);
                    SetActiveSaveBtn();
                    break;
            }
        }

        private void OnPressBackBtn()
        {
            if (_model.CanShowAds)
            {
                ShowAd();
            }
            else
            {
                CloseScene();
            }
        }

        private async void OnPressSaveBtn()
        {
            _backBtn.interactable = false;
            _saveBtn.interactable = false;

            await _model.SaveBook();
            
            if (_model.CanShowAds)
            {
                ShowAd();
            }
            else
            {
                CloseScene();
            }
        }

        private void OnPressRateBtn(int value)
        {
            base.SetClickClip();
            
            value++;
            
            _model.ChangeRate(value);
            
            _rateBodyView.SetStates(value);
            
            SetActiveSaveBtn();
        }
        
        private void OnDescriptionCharCountUpdated(string value)
        {
            _charCountView.UpdateCount(value.Length);
        }

        private void SetActiveSaveBtn()
        {
            _saveBtn.gameObject.SetActive(_model.IsCanSave());
        }

        private void CloseScene()
        {
            OnEditSceneClosed?.Invoke();
            
            base.UnloadScene(SceneName.Edit.ToString());
        }
        
        private void ShowAd()
        {
            //CoreContainer.Instance.UnityAdsService.ShowAd();
            
            CloseScene();
        }
    }
}