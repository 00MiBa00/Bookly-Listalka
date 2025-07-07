using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class ProgressModel
    {
        private int _sec;

        public int Hour => _sec / 3600;
        public int Minute => _sec % 3600 / 60;
        public int Sec => _sec % 60;

        public int HistoryCount => LoadTimes().Count;

        public bool CanUpdateAllTimes => LoadTimes().Count > 0;
        public bool CanCheckTime { get; set; }

        private const string TimeListKey = "ProgressModel.Times";
        private const string PageListKey = "ProgressModel.Page";

        public ProgressModel()
        {
            ResetTime();
        }

        public void AddTime()
        {
            List<int> timesInSeconds = new List<int>(LoadTimes()) { _sec };

            SaveTimes(timesInSeconds);
        }
        
        public void AddPage(int count)
        {
            List<int> pages = new List<int>(LoadPages()) { count };
            
            SavePages(pages);
        }
        
        public void ResetTime()
        {
            _sec = 0;
        }

        public void UpdateTime()
        {
            _sec++;
        }
        
        public List<int> LoadTimes()
        {
            List<int> timesInSeconds = new List<int>();
            
            string timesString = PlayerPrefs.GetString(TimeListKey, string.Empty);
            
            if (!string.IsNullOrEmpty(timesString))
            {
                string[] timesArray = timesString.Split(',');
                foreach (var time in timesArray)
                {
                    if (int.TryParse(time, out int result))
                    {
                        timesInSeconds.Add(result);
                    }
                }
            }

            Debug.Log("Время загружено.");
            return timesInSeconds;
        }
        
        public List<int> LoadPages()
        {
            List<int> pages = new List<int>();
            
            string pagesString = PlayerPrefs.GetString(PageListKey, string.Empty);
            
            if (!string.IsNullOrEmpty(pagesString))
            {
                string[] pagesArray = pagesString.Split(',');
                foreach (var page in pagesArray)
                {
                    if (int.TryParse(page, out int result))
                    {
                        pages.Add(result);
                    }
                }
            }

            Debug.Log("Pages загружено.");
            return pages;
        }
        
        private void SavePages(List<int> pages)
        {
            string timesString = string.Join(",", pages);

            PlayerPrefs.SetString(PageListKey, timesString);

            Debug.Log("Время сохранено.");
        }

        private void SaveTimes(List<int> timesInSeconds)
        {
            string timesString = string.Join(",", timesInSeconds);

            PlayerPrefs.SetString(TimeListKey, timesString);
        }
    }
}