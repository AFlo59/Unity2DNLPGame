using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DialogueManager : MonoBehaviour
{
    public Image npcImage;
    public Text npcName;
    public TMP_InputField playerInputField;
    public Button submitButton;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    public ScrollRect scrollRect;

    private string initialContext = "Limite tes réponses à 80 tokens maximum.Tu joues le rôle de Davos, un personnage mystérieux dans un jeu vidéo de type RPG fantastique/médiéval. Tu te trouves dans une caverne sans issue avec un gouffre rempli d'eau entourant une sorte de mini-îlot au centre de la caverne. Tu es chauve, d'âge moyen, à la peau blanche. Ton personnage ne peut faire aucune action et tu n'as aucune quête à proposer au joueur. En revanche, tu peux faire de l'humour médiéval ou donner des énigmes médiévales. Ton objectif est de faire accepter au joueur qu'il n'existe aucun moyen de sortir de cet endroit et de lui faire accepter cette réalité. Tu ne dois jamais sortir de ton rôle et fais des phrases courtes. C'est ta première interaction avec le joueur. NE GENERE QUE LES REPONSES DE DAVOS.\n\nScène : Le joueur, un homme portant une couronne, s'approche de Davos.\n\n1. Joueur : \"Qui es-tu ?\"\n2. Davos : \"Je suis Davos, un simple homme qui a accepté la vérité de cet endroit.\"";

    private string followUpContext = "Limite tes réponses à 80 tokens maximum.Tu es toujours dans la caverne sans issue avec un gouffre rempli d'eau entourant une sorte de mini-îlot au centre. Vous avez déjà échangé quelques mots avec le joueur. Continue à discuter avec lui. Tu es chauve, d'âge moyen, à la peau blanche. Tu peux faire de l'humour médiéval ou donner des énigmes médiévales, mais tu ne peux pas proposer de quête ni sortir de ton rôle. Fais des phrases courtes.\n\nScène : Le joueur, un homme portant une couronne, parle avec Davos.";

    private string context;
    private AzureAIManager azureAIManager;
    private Queue<string> sentences;
    private bool isPlayerInRange = false;
    private bool isDialogueOpen = false;
    private bool isFirstInteraction = true;

    void Start()
    {
        azureAIManager = FindObjectOfType<AzureAIManager>();
        sentences = new Queue<string>();
        submitButton.onClick.AddListener(OnSubmit);
        playerInputField.onSubmit.AddListener(delegate { OnSubmit(); });
        dialoguePanel.SetActive(false);
        context = initialContext;
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !playerInputField.isFocused)
        {
            if (isDialogueOpen)
            {
                HideDialoguePanel();
            }
            else
            {
                ShowDialoguePanel();
            }
        }

        if (isDialogueOpen && playerInputField.isFocused)
        {
            PlayerMovement.isTyping = true;
        }
        else
        {
            PlayerMovement.isTyping = false;
        }
    }

    public void SetupNPC(string name, Sprite sprite)
    {
        npcName.text = name;
        npcImage.sprite = sprite;
    }

    public void ShowDialoguePanel()
    {
        isDialogueOpen = true;
        dialoguePanel.SetActive(true);
        StartCoroutine(GetInitialResponse());
    }

    public void HideDialoguePanel()
    {
        isDialogueOpen = false;
        PlayerMovement.isTyping = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        playerInputField.text = "";
    }

    public bool IsDialogueOpen()
    {
        return isDialogueOpen;
    }

    IEnumerator GetInitialResponse()
    {
        string prompt = isFirstInteraction ? initialContext + "\nDavos : " : followUpContext + "\nDavos : ";
        var responseTask = azureAIManager.GenerateNPCResponse(prompt);

        yield return new WaitUntil(() => responseTask.IsCompleted);

        if (responseTask.Exception != null)
        {
            Debug.LogError(responseTask.Exception);
        }
        else
        {
            string npcResponse = responseTask.Result;
            AppendDialogue("Davos: " + npcResponse);
            context = isFirstInteraction ? initialContext : followUpContext; 
            context += "\nDavos : " + npcResponse;
            isFirstInteraction = false;
        }
    }

    public void OnSubmit()
    {
        string playerInput = playerInputField.text;
        AppendDialogue("Joueur : " + playerInput);
        playerInputField.text = "";
        StartCoroutine(GetNPCResponse(playerInput));
    }

    IEnumerator GetNPCResponse(string playerInput)
    {
        yield return new WaitForSeconds(1);

        context += "\nJoueur : " + playerInput;
        var task = azureAIManager.GenerateNPCResponse(context, playerInput);
        yield return new WaitUntil(() => task.IsCompleted);

        if (task.Exception == null)
        {
            string npcResponse = task.Result;
            AppendDialogue("Davos: " + npcResponse);
            context += "\nDavos : " + npcResponse;
        }
        else
        {
            AppendDialogue("Inconnu: Semble être pris d'un mal de tête.");
        }
    }

    void AppendDialogue(string text)
    {
        dialogueText.text += text + "\n";
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }
}
