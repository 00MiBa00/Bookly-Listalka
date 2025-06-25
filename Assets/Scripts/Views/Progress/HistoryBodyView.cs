using System.Collections.Generic;
using UnityEngine;

namespace Views.Progress
{
    public class HistoryBodyView : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _historyItemPrefab;
        [SerializeField] 
        private Transform _contenttransform;

        private List<GameObject> _historyItemObjects;

        public void AddHistory(int sec, int page)
        {
            _historyItemObjects ??= new List<GameObject>();
            
            GameObject go = Instantiate(_historyItemPrefab, _contenttransform);
            
            _historyItemObjects.Add(go);

            HistoryItemView itemView = go.GetComponent<HistoryItemView>();
            
            itemView.SetPages(page);
            itemView.SetTime(sec/3600, sec % 3600 / 60, sec % 60);
        }

        public void Reset()
        {
            if (_historyItemObjects == null)
            {
                return;
            }

            foreach (var item in _historyItemObjects)
            {
                Destroy(item);
            }
            
            _historyItemObjects.Clear();
        }
    }
}