using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Manager
{
    public class SceneController : Singleton<SceneController>
    {
        private Animator _sceneAnimator;

        protected override void Awake()
        {
            base.Awake();
            _sceneAnimator = GetComponent<Animator>();
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

        public void NextLevel()
        {
            StartCoroutine(LoadLevel());
        }

        IEnumerator LoadLevel()
        {
            _sceneAnimator.Play("FadeIn");
            yield return new WaitForSeconds(1.0f);
            if(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(0);
            _sceneAnimator.Play("FadeOut");
        }
    }
}