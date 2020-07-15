using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownShooter
{
    public class PausePanel : UIPanel
    {
        private void OnEnable()
        {
            Time.timeScale = 0.0f;
        }

        private void OnDisable()
        {
            Time.timeScale = 1.0f;
        }

        public void OnResumeButtonClick()
        {
            UIManager.Instance.ShowPanel(UIPanelType.Gameplay);
        }
        public void OnExitToMenuButtonClick()
        {
            SceneManager.LoadScene(0);
        }
        public void OnExitButtonClick()
        {
            Application.Quit();
        }
    }
}
