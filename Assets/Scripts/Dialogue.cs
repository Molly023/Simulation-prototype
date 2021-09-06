using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue {

    string folderPath;
    string txtFile;

    public string Speaker => folderPath;
    public string File {
        set{
            txtFile = value;
        }
    }

    public Dialogue(string owner) {
        folderPath = owner;
    }

    public string[] GetDialogue() {
        return Utilities.GetAllLines(txtFile, folderPath);
    }
    
}
