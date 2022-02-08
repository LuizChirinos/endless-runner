using UnityEngine;

namespace Triplano.Screens
{
    public abstract class ScreenObject : MonoBehaviour
    {
        [SerializeField] private ScreenData screenData;

        protected virtual void Start()
        {
            screenData.OnRequestShow += ShowScreen;
            screenData.OnRequestHide += HideScreen;
        }
        protected virtual void OnDestroy()
        {
            screenData.OnRequestShow -= ShowScreen;
            screenData.OnRequestHide -= HideScreen;
        }

        public virtual void ShowScreen()
        {
            Show();
        }

        protected void Hide()
        {
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(false);
            }
        }

        protected void Show()
        {
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(true);
            }
        }

        public virtual void HideScreen()
        {
            Hide();
        }
    }
}
