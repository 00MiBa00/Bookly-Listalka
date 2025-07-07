using System.IO;
using UnityEngine;
using UnityEngine.UI;

using Views.Create;
using Models;
using Enums;

namespace Controllers.Scenes
{
    public class CreateSceneController : AbstractSceneController
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
        
        private CreateBookModel _model;
        
        protected override void OnSceneEnable()
        {
            CheckStateBackBtn();
        }

        protected override void OnSceneStart()
        {
            string path = Path.Combine(Application.persistentDataPath, "Books.Path");
            
            _model.SetPath(path);
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new CreateBookModel();
        }

        protected override void Subscribe()
        {
            _saveBtn.onClick.AddListener(OnPressSaveBtn);
            _backBtn.onClick.AddListener(OnPressBackBtn);
            
            _nameInputField.onEndEdit.AddListener(OnNameChanged);
            _descriptionInputField.onEndEdit.AddListener(OnDescriptionChanged);
            _descriptionInputField.onValueChanged.AddListener(OnDescriptionCharCountUpdated);

            _genresBodyView.PressBtnAction += OnPressGenreBtn;
            _rateBodyView.PressBtnAction += OnPressRateBtn;
        }

        protected override void Unsubscribe()
        {
            _saveBtn.onClick.RemoveAllListeners();
            _backBtn.onClick.RemoveAllListeners();
            
            _nameInputField.onEndEdit.RemoveAllListeners();
            _descriptionInputField.onEndEdit.RemoveAllListeners();
            _descriptionInputField.onValueChanged.RemoveAllListeners();
            
            _genresBodyView.PressBtnAction -= OnPressGenreBtn;
            _rateBodyView.PressBtnAction -= OnPressRateBtn;
        }

        private void OnNameChanged(string name)
        {
            _model.UpdateBookName(name);
            
            CheckSaveBtnActive();
        }

        private void OnDescriptionChanged(string description)
        {
            _model.UpdateDescription(description);
        }

        private void OnPressGenreBtn(int index)
        {
            switch (_model.TryAddGenre(index))
            {
                case true:
                    base.SetClickClip();
                    _genresBodyView.SetBtnState(index, true);
                    break;
                case false:
                    base.SetClickClip();
                    _genresBodyView.SetBtnState(index, false);
                    break;
                case null:
                    break;
            }
            
            CheckSaveBtnActive();
        }

        private void OnPressRateBtn(int value)
        {
            base.SetClickClip();
            
            value++;
            
            _model.UpdateRate(value);
            
            _rateBodyView.SetStates(value);
            
            CheckSaveBtnActive();
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
                LoadGameScene();
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
                LoadGameScene();
            }
        }

        private void LoadGameScene()
        {
            base.LoadScene(SceneName.Game.ToString());
        }

        private void OnDescriptionCharCountUpdated(string value)
        {
            _charCountView.UpdateCount(value.Length);
        }

        private void CheckSaveBtnActive()
        {
            _saveBtn.gameObject.SetActive(_model.IsCanSave());
        }

        private void CheckStateBackBtn()
        {
            _backBtn.gameObject.SetActive(_model.IsBackBtnActive);
        }

        private void ShowAd()
        {
            //CoreContainer.Instance.UnityAdsService.ShowAd();

            LoadGameScene();
        }
    }
}