using Enums;
using Values;

namespace Models
{
    public class OnboardingModel
    {
        public string GetNameNextScene()
        {
            if (Subscription.HasSubscription())
            {
                return BooksInfo.HaveBooks ? SceneName.Game.ToString() : SceneName.Create.ToString();
            }
            else
            {
                return SceneName.Subscription.ToString();
            }
        }
    }
}