using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TopDownShooter
{
    public class Mainmenu : MonoBehaviour
    {
        public void OnStartButtonClic()
        {
            SceneManager.LoadScene(1);
        }

        public void OnExitButtonClic()
        {
            Application.Quit();
        }
    }
}