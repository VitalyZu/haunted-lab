using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelComponent : MonoBehaviour
{
    public void Restart()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int index = Random.Range(3, sceneCount);
        SceneManager.LoadScene(index);
    }
}
