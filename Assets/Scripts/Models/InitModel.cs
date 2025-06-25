using Enums;
using UnityEngine;
using Values;

namespace Models
{
    public class InitModel
    {
        private const string IsFirstTimeKey = "InitModel.IsFirstTime";

        private bool IsFirstTime => !PlayerPrefs.HasKey(IsFirstTimeKey);
        
        public string GetNameNextScene()
        {
            if (IsFirstTime)
            {
                return SceneName.Onboarding.ToString();
            }

            if (Subscription.HasSubscription())
            {
                return BooksInfo.HaveBooks ? SceneName.Game.ToString() : SceneName.Create.ToString();
            }
            else
            {
                return SceneName.Subscription.ToString();
            }
        }

        public void SetIsNotFirstTime()
        {
            PlayerPrefs.SetInt(IsFirstTimeKey, 0);
        }
    }
}