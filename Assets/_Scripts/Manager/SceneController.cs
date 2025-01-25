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
            if(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + index)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
            else
                SceneManager.LoadScene(0);
            _sceneAnimator.Play("FadeOut");
        }
    }
}