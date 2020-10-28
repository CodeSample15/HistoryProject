using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] public int SceneIndex;
    [SerializeField] public Animator SceneTransition;
    [SerializeField] public float AnimationLength;

    public void changeScenes()
    {
        StartCoroutine(transition());
    }

    IEnumerator transition()
    {
        SceneTransition.SetTrigger("Transition");

        yield return new WaitForSeconds(AnimationLength);

        SceneManager.LoadScene(SceneIndex);
    }
}