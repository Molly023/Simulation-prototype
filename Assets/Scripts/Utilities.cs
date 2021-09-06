using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public static class Utilities {

    #region Accessing From Resources
    public static string GetLine(string fileName, int lineNumber, string folder = "") {
        string name;
        string path = "Assets/Resources/";
        if (folder != "") {
            path += folder + "/";
        }
        path += fileName + ".txt";

        string[] lines = System.IO.File.ReadAllLines(path);

        name = lines[lineNumber];

        return name;
    }

    public static string[] GetAllLines(string fileName, string folder = "") {
        string path = "Assets/Resources/";
        if (folder != "") {
            path += folder + "/";
        }
        path += fileName + ".txt";

        string[] lines = System.IO.File.ReadAllLines(path);

        return lines;
    }
    

    
    public static Sprite GetSprite(string name) {
        return Resources.Load<Sprite>("Sprites/" + name);
    }

    #endregion

    #region Pause/Play
    
    public static void Pause() {
        Time.timeScale = 0;
    }

    public static void Resume() {
        Time.timeScale = 1;
    }

    #endregion

    #region SceneLoading/Management
    public static IEnumerator LoadAdditiveSceneAsync(string sceneName, Action AfterLoad = null) {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        
        while (!loadingScene.isDone) yield return null;
        AfterLoad?.Invoke();
    }

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

    public static IEnumerator UnloadSceneAsync(string sceneName, Action AfterRemoved = null) {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);

        while (!asyncUnload.isDone) yield return null;
        AfterRemoved?.Invoke();
    }
    #endregion

    public static IEnumerator LoadEndOfFrame(Action action) {

        for (int i = 0; i < 2; i++) {
            yield return new WaitForEndOfFrame();
        }

        action.Invoke();
    }
}
