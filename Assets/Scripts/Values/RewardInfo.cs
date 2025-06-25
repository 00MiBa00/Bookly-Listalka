using System.Collections.Generic;
using UnityEngine;

namespace Values
{
    public static class RewardInfo
    {
        private const string RewardsKey = "RewardsInfo";

        public static List<int> GetRewards()
        {
            List<int> rewards = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                int reward = PlayerPrefs.GetInt(RewardsKey + i, 0);
                
                rewards.Add(reward);
            }

            return rewards;
        }

        public static void SetFirstBookReward()
        {
            SetReward(1);
        }
        
        public static void SetFanHorrorAndFantasyReward()
        {
            SetReward(2);
        }
        
        public static void SetTopReaderReward()
        {
            SetReward(3);
        }
        
        public static void SetReadingTwoBooksReward()
        {
            SetReward(4);
        }
        
        public static void SetNewStatusReward()
        {
            SetReward(5);
        }
        
        public static void SetReadingFiveBooksReward()
        {
            SetReward(6);
        }

        private static void SetReward(int reward)
        {
            List<int> rewards = new List<int>(GetRewards());

            for (int i = 0; i < rewards.Count; i++)
            {
                if (rewards[i] == reward)
                {
                    return;
                }

                if (rewards[i] != 0)
                {
                    continue;
                }
                
                rewards[i] = reward;
                SaveRewards(rewards);
                return;
            }
        }

        private static void SaveRewards(List<int> rewards)
        {
            for (int i = 0; i < rewards.Count; i++)
            {
                PlayerPrefs.SetInt(RewardsKey+i, rewards[i]);
            }
        }
    }
}