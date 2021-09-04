using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {

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


    public static Sprite GetSprite(string name) {
        return Resources.Load<Sprite>("Sprites/" + name);
    }

    public static void Pause() {
        Time.timeScale = 0;
    }

    public static void Resume() {
        Time.timeScale = 1;
    }
}
