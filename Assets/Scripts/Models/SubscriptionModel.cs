using Enums;
using Values;

namespace Models
{
    public class SubscriptionModel
    {
        private int _subscriptionPeriodIndex;

        public int SubscriptionPeriodIndex => _subscriptionPeriodIndex;

        public SubscriptionModel()
        {
            _subscriptionPeriodIndex = 0;
        }

        public void ChangeSubPeriodIndex(int index)
        {
            _subscriptionPeriodIndex = index;
        }

        public void SetSubscription()
        {
            switch (_subscriptionPeriodIndex)
            {
                case 0:
                    Subscription.SetWeekPeriod();
                    break;
                case 1:
                    Subscription.SetMonthPeriod();
                    break;
                case 2:
                    Subscription.SetYearPeriod();
                    break;
            }
        }

        public string GetNextSceneName()
        {
            string sceneName = BooksInfo.HaveBooks ? SceneName.Game.ToString() : SceneName.Create.ToString();

            return sceneName;
        }
    }
}