using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace TopDownShooter
{
    public class GameoverPanel : UIPanel
    {
        [SerializeField] private CanvasGroup canvas = null;
        [SerializeField] private Text _scoreLabel = null;
        [SerializeField] private Text _timeLabel = null;

        private void OnEnable()
        {
            canvas.DOFade(1.0f, 2.0f);
        }

        public void OnRestartButtonClic()
        {
            this.enabled = false;
            SceneManager.LoadScene(1);
        }

        public void OnExitButtonClic()
        {
            Application.Quit();
        }

        public void OnExitToMenuButtonClic()
        {
            SceneManager.LoadScene(0);
        }

    }
}
