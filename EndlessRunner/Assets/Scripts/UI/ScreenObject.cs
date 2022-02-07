using UnityEngine;

namespace Triplano.Screens
{
    public abstract class ScreenObject : MonoBehaviour
    {
        [SerializeField] private ScreenData screenData;

        protected virtual void Start()
        {
            screenData.OnRequestShow += Show;
            screenData.OnRequestHide += Hide;
        }
        protected virtual void OnDestroy()
        {
            screenData.OnRequestShow -= Show;
            screenData.OnRequestHide -= Hide;
        }

        public virtual void Show()
        {
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(true);
            }
        }

        public virtual void Hide()
        {
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
