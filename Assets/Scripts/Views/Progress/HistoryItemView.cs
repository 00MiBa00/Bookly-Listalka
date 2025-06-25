using UnityEngine;
using UnityEngine.UI;

namespace Views.Progress
{
    public class HistoryItemView : MonoBehaviour
    {
        [SerializeField] 
        private Text _pageText;
        [SerializeField] 
        private Text _timeText;

        public void SetPages(int value)
        {
            _pageText.text = $"{value} pages";
        }

        public void SetTime(int hour, int min, int sec)
        {
            _timeText.text = $"{hour}:{min:00}:{sec:00}";
        }
    }
}