﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace CnControls
{
    public class Dpad : MonoBehaviour
#if !UNITY_EDITOR
        , IPointerDownHandler, IPointerUpHandler
#endif
    {
        public DpadAxis[] DpadAxis;

        /// <summary>
        /// Current event camera reference. Needed for the sake of Unity Remote input
        /// </summary>
        public Camera CurrentEventCamera { get; set; }

        private void Awake()
        {
#if UNITY_EDITOR
            gameObject.AddComponent<DpadInputHelper>();
#endif
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            CurrentEventCamera = eventData.pressEventCamera ?? CurrentEventCamera;

            foreach (var dpadAxis in DpadAxis)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(dpadAxis.RectTransform, eventData.position,
                    CurrentEventCamera))
                {
                    dpadAxis.Press(eventData.position, CurrentEventCamera, eventData.pointerId);
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            foreach (var dpadAxis in DpadAxis)
            {
                dpadAxis.TryRelease(eventData.pointerId);
            }
        }
    }
}