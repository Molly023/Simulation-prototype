using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour {

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;

    private void Update() {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(-0.1f,0.25f, -10);
    }

    public void Show(string name, string description) {
        Name.text = name;
        Description.text = description;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.SetActive(true);
    }

    public void Show() {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.SetActive(true);
    }

    public void SetName(string name) {
        Name.text = name;
    }

    public void SetDescription(string description) {
        Description.text = description;
    }

    public void Hide() {
        gameObject.SetActive(false) ;
    }
}
