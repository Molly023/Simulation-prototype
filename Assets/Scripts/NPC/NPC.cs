using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour {

    public UnityEvent<Player> Evt_AfterTalk;
    [SerializeField] new string name;

    public string Name => name;
    Dialogue dialogues;
    
    Player playerTalkedTo;

    private void Start() {
        dialogues = new Dialogue(Name);
    }
    
    public void StartDialogue( string fileToSay) {

        DialogueManager dialogueManager = SingletonManager.Get<DialogueManager>();
        dialogues.File = fileToSay;
        dialogueManager.StartDialogue(dialogues);

        dialogueManager.Evt_EndOfDialogue += () => Evt_AfterTalk?.Invoke(playerTalkedTo);
        //playerTalkedTo = null;
    }

    public void SetInteraction(Player player) {
        playerTalkedTo = player;
    }
}
