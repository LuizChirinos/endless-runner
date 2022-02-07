using UnityEngine;

namespace Triplano.Screens
{
    [CreateAssetMenu(fileName = nameof(ScreenData), menuName = "StrangeLoops/Screens/ScreenData")]
    public class ScreenData : ScriptableObject
    {
        #region Delegates
        public delegate void OnRequestScreenChange();

        public OnRequestScreenChange OnRequestShow;
        public OnRequestScreenChange OnRequestHide;
        #endregion

        public void RequestShow()
        {
            OnRequestShow?.Invoke();
        }

        public void RequestHide()
        {
            OnRequestHide?.Invoke();
        }
    }
}
