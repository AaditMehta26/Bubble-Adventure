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

        public void ReLoadScene()
        {
            StartCoroutine(LoadLevel(0));
        }

        public void NextLevel()
        {
            StartCoroutine(LoadLevel(1));
        }

        IEnumerator LoadLevel(int index)
        {
            _sceneAnimator.Play("FadeIn");
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + index);
            _sceneAnimator.Play("FadeOut");
        }
    }
}