using System;
using System.Collections;
using Assets._3rdParty;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets._Game.Scripts.Managers
{

    public class SceneChanger : AManager<SceneChanger>
    {
        private string currentLevel;
        [SerializeField] private GameObject buttons;
        [SerializeField] private Button switchButton;

        private bool buttonsShown = true;

        private void Awake()
        {
            switchButton.onClick.AddListener((ShowButtons));
        }

        public void LoadScene(string level)
        {
            this.currentLevel = level;
            StartCoroutine(this.LoadSceneAsync());
        }

        private IEnumerator LoadSceneAsync()
        {
            yield return new WaitForSeconds(.1f);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(this.currentLevel);
            while (!asyncLoad.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();
            ShowButtons();
        }

        public void ShowButtons()
        {
            buttons.SetActive(!buttonsShown);
            buttonsShown = !buttonsShown;
        }
    }
}