using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RestartLevelComponent : MonoBehaviour
{
    public void Restart()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int index = Random.Range(3, sceneCount);
        StartCoroutine(LoadLevel(index));    
    }

    private IEnumerator LoadLevel(int index)
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(index);
    }
}
