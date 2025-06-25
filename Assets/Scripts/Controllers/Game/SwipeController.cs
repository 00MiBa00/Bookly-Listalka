using System;
using UnityEngine;

namespace Controllers.Game
{
    public class SwipeController : MonoBehaviour
    {
        public Action<int> SwipeAction { get; set; }
        
        [SerializeField] private float swipeThreshold = 50f; // Минимальная дистанция свайпа (в пикселях)
        [SerializeField] private float timeThreshold = 0.3f; // Максимальное время для свайпа

        private Vector2 fingerDownPosition;
        private Vector2 fingerUpPosition;
        private float fingerDownTime;
        private float fingerUpTime;
        private bool _canCheckSwipe;

        private void Update()
        {
            if (!_canCheckSwipe)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                fingerDownPosition = Input.mousePosition;
                fingerUpPosition = Input.mousePosition;
                fingerDownTime = Time.time;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                fingerUpPosition = Input.mousePosition;
                fingerUpTime = Time.time;

                DetectSwipe();
            }
        }

        public void SetCanCheck(bool value)
        {
            _canCheckSwipe = value;
        }

        private void DetectSwipe()
        {
            float duration = fingerUpTime - fingerDownTime;

            if (duration > timeThreshold) return;

            float deltaX = fingerUpPosition.x - fingerDownPosition.x;
            float deltaY = fingerUpPosition.y - fingerDownPosition.y;

            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                if (Mathf.Abs(deltaX) >= swipeThreshold)
                {
                    if (deltaX > 0)
                        SwipeAction?.Invoke(-1);
                    else
                        SwipeAction?.Invoke(1);
                }
            }
        }
    }
}