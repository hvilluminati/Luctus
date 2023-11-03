using UnityEngine;
using System.Collections;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public GameObject player;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;

    public float wordSpeed;
    private bool dialogueAllowed;

    void Start()
    {
        dialogueText.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueAllowed = false;
            closeText();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && dialogueAllowed)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                player.GetComponent<PlayerMovement>().enabled = false;
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }
        }

        if (Input.GetButtonDown("Cancel") && dialoguePanel.activeInHierarchy)
        {
            closeText();
        }
    }

    public void closeText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            closeText();
        }
    }
}
