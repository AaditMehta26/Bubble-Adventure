using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Manager
{
    public class SceneController : Singleton<SceneController>
    {
        private Animator _sceneAnimator;
        private List<string> _inAnimations = new List<string>{"FadeIn","SlideIn"};
        private List<string> _outAnimations = new List<string>{"FadeOut","SlideOut"};

        protected override void Awake()
        {
            base.Awake();
            _sceneAnimator = GetComponent<Animator>();
        }

        public void ReLoadScene()
        {
            StartCoroutine(LoadLevel(0));
        }

        public void LoadScene(string sceneName)
        {
            
            StartCoroutine(LoadLevel(sceneName));
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
        
        IEnumerator LoadLevel(string sceneName)
        {
            _sceneAnimator.Play("FadeIn");
            yield return new WaitForSeconds(1.0f);
 
                SceneManager.LoadSceneAsync(sceneName).completed += ctx => _sceneAnimator.Play("FadeOut");;
            
        }
    }
}