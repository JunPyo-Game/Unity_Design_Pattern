using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private DateTime _sessoinStartTime;
    private DateTime _sessionEndTime;

    private void Start()
    {
        _sessoinStartTime = DateTime.Now;
        Debug.Log($"Game session start : {DateTime.Now}");
    }

    private void OnApplicationQuit()
    {
        _sessionEndTime = DateTime.Now;
        TimeSpan timeDiff = _sessionEndTime - _sessoinStartTime;
        Debug.Log($"Game Sessoin ended : {DateTime.Now}");
        Debug.Log($"Game Session lasted : {timeDiff}");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Next Scene"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void SayHello()
    {
        Debug.Log("Hello");
    }
}
