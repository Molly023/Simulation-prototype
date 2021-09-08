using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public static class Utilities {

    #region Accessing From Resources
    /// <summary>
    /// Gets a line from a file in resources
    /// </summary>
    public static string GetLine(string fileName, int lineNumber, string folder = "") {
        string path = "";
        if (folder != "") {
            path += folder + "/";
        }
        path += fileName;

        TextAsset txt = Resources.Load<TextAsset>(path);

        string[] lines = txt.ToString().Split('\n');

        string name = lines[lineNumber];

        return name;
    }
    /// <summary>
    /// Gets all lines from a file in resources
    /// </summary>
    public static string[] GetAllLines(string fileName, string folder = "") {

        

        string path = "";
        if (folder != "") {
            path += folder + "/";
        }
        path += fileName;
        TextAsset txt = Resources.Load<TextAsset>(path);

        string[] lines = txt.ToString().Split('\n');

        return lines;
    }

    /// <summary>
    /// Gets a sprite from resources
    /// </summary>
    public static Sprite GetSprite(string name) {
        return Resources.Load<Sprite>("Sprites/" + name);
    }

    #endregion

    #region Pause/Play
    /// <summary>
    /// Pause Time
    /// </summary>
    public static void Pause() {
        Time.timeScale = 0;
    }
    /// <summary>
    /// Play Time
    /// </summary>
    public static void Resume() {
        Time.timeScale = 1;
    }

    #endregion

    #region SceneLoading/Management 
    /// <summary>
    /// Loads an Additive Scene, can run events after load
    /// </summary>
    public static IEnumerator LoadAdditiveSceneAsync(string sceneName, Action AfterLoad = null) {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        
        while (!loadingScene.isDone) yield return null;
        AfterLoad?.Invoke();
    }
    /// <summary>
    /// Loads multiple Additive Scene simultaneously, can run events after load
    /// </summary>
    public static IEnumerator LoadMultipleAdditiveSceneAsync(string[] sceneName, Action AfterLoad = null) {
        
        List<AsyncOperation> asyncOperations = new List<AsyncOperation>();
        
        foreach(string scene in sceneName) {
            asyncOperations.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive));
        }
        
        while (!asyncOperations.TrueForAll((op) => op.isDone)) {
            
            
            yield return null;
        }
        
        AfterLoad?.Invoke();
    }
    /// <summary>
    /// unloads Scene, can run events after load
    /// </summary>
    public static IEnumerator UnloadSceneAsync(string sceneName, Action AfterRemoved = null) {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);

        while (!asyncUnload.isDone) yield return null;
        AfterRemoved?.Invoke();
    }
    #endregion

    /// <summary>
    /// A coroutine that can be used to do actions after the end of frame
    /// </summary>
    public static IEnumerator LoadEndOfFrame(Action action) {

        for (int i = 0; i < 2; i++) {
            yield return new WaitForEndOfFrame();
        }

        action.Invoke();
    }
}
