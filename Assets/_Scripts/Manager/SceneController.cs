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
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            _sceneAnimator.Play("FadeOut");
        }
    }
}