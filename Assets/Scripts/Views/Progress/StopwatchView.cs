using UnityEngine;
using UnityEngine.UI;

namespace Views.Progress
{
    public class StopwatchView : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;

        public void UpdateTime(int hour, int min, int sec)
        {
            _text.text = $"{hour}:{min:00}:{sec:00}";
        }
    }
}