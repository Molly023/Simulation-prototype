using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DialogueManager : MonoBehaviour {

    Queue<string> sentences = new Queue<string>();
    public Action Evt_EndOfDialogue;
    public Action Evt_StartOfDialogue;
    public Action Evt_HideDialogue;

    [Header("Dialogue UI")]
    public GameObject DialogueUI;
    public TextMeshProUGUI NameUI;
    public TextMeshProUGUI DialogueText;

    private void Awake() {
        SingletonManager.Register(this);
    }

    private void Start() {
        //StartCoroutine(Utilities.LoadEndOfFrame(SetControllerEvents));
        SingletonManager.Evt_Registered += SetControllerEvents;
    }

    public void StartDialogue(Dialogue dialogue) {

        DialogueUI.SetActive(true);
        NameUI.text = dialogue.Speaker;
       

        sentences.Clear();

        foreach(string x in dialogue.GetDialogue()) {
            sentences.Enqueue(x);
        }

        DisplayNextSentence();
        Evt_StartOfDialogue?.Invoke();
        Utilities.Pause();
    }

    void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        DialogueText.text = sentence;
    }

    void EndDialogue() {
        DialogueUI.SetActive(false);

        Evt_HideDialogue?.Invoke();
        Evt_EndOfDialogue?.Invoke();
        Evt_EndOfDialogue = null;
        Utilities.Resume();
    }

    public void SetControllerEvents(MonoBehaviour obj) {
        if (obj is PlayerController) {
            PlayerController playerController = obj as PlayerController;
            Evt_StartOfDialogue += () => playerController.State = ControlState.Dialogue;
            Evt_HideDialogue += () => playerController.State = ControlState.Player;
            playerController.Evt_Next += DisplayNextSentence;
        }

        SingletonManager.Evt_Registered -= SetControllerEvents;
    }
}
