using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f;
    public KeyCode interactKey = KeyCode.E;
    public TextMeshProUGUI interactionText;

    private Camera playerCamera;
    private Harvestable currentHarvestable;

    void Start()
    {
        playerCamera = Camera.main;
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // Masquer le texte d'interaction au départ
        }
        else
        {
            Debug.LogError("interactionText n'est pas assigné dans l'inspecteur.");
        }
    }

    void Update()
    {
        CheckForHarvestable();
        if (currentHarvestable != null && Input.GetKeyDown(interactKey))
        {
            InteractWithHarvestable();
        }
    }

    void CheckForHarvestable()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit; 

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Harvestable harvestable = hit.collider.GetComponent<Harvestable>();
            if (harvestable != null)
            {
                currentHarvestable = harvestable;
                if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(true);
                    interactionText.text = "Appuyez sur E pour interagir";
                }
                return;
            }
        }

        currentHarvestable = null;
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void InteractWithHarvestable()
    {
        Debug.Log("Interacting with " + currentHarvestable.name);
        // Ajoutez ici les actions à réaliser lorsque l'objet est récolté
        Destroy(currentHarvestable.gameObject);
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // Masquer le texte après l'interaction
        }
    }
}
