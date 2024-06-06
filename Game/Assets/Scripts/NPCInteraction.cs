using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public string npcName;
    public Sprite npcSprite;

    private bool playerInRange;

    void Start()
    {
        playerInRange = false;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !PlayerMovement.isTyping)
        {
            if (dialogueManager.IsDialogueOpen())
            {
                dialogueManager.HideDialoguePanel();
            }
            else
            {
                dialogueManager.SetupNPC(npcName, npcSprite);
                dialogueManager.ShowDialoguePanel();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueManager.HideDialoguePanel();
        }
    }
}
