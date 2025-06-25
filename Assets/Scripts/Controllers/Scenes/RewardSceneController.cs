using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;
using Values;

namespace Controllers.Scenes
{
    public class RewardSceneController : AbstractSceneController
    {
        [Space(5)] [Header("UI")] 
        [SerializeField]
        private List<Image> _rewardImages;
        [SerializeField] 
        private List<Sprite> _rewardSprites;
        [SerializeField] 
        private Button _backBtn;

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
            SetRewards();
        }

        protected override void Subscribe()
        {
            _backBtn.onClick.AddListener(OnPressBackBtn);
        }

        protected override void Unsubscribe()
        {
            _backBtn.onClick.RemoveAllListeners();
        }

        private void SetRewards()
        {
            List<int> rewards = RewardInfo.GetRewards();

            for (int i = 0; i < rewards.Count; i++)
            {
                _rewardImages[i].sprite = _rewardSprites[rewards[i]];
            }
        }

        private void OnPressBackBtn()
        {
            base.LoadScene(SceneName.Game.ToString());
        }
    }
}