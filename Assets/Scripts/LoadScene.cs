using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Simple script to make easily change change scenes.
/// </summary>
public class LoadScene : MonoBehaviour
{
    public void HandleLoadScene(string pSceneName) => SceneManager.LoadScene(pSceneName);

}

