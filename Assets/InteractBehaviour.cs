using System.Collections;
using UnityEngine;

public class InteractBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject axeVisual;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private float interactRange = 5f;

    [SerializeField]
    private GameObject interactText;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Item"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    axeVisual.SetActive(true);
                    StartCoroutine(HarvestAfterWait(hit.collider));
                }
                interactText.SetActive(true);
            }
            else
            {
                interactText.SetActive(false);
            }
        }
    }

    private IEnumerator HarvestAfterWait(Collider itemCollider)
    {
        // Déclencher l'animation de récolte
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Harvest");
            Debug.Log("Animation de récolte déclenchée");
        }
        else
        {
            Debug.LogWarning("playerAnimator est nul, animation non déclenchée");
        }

        // Attendre 3 secondes avant de commencer le fondu et la destruction
        yield return new WaitForSeconds(1.5f);

        // Vérifier si l'objet ou son parent a un composant Harvestable et appeler la méthode Harvest
        Harvestable harvestable = itemCollider.GetComponentInParent<Harvestable>();
        if (harvestable != null)
        {
            harvestable.Harvest(itemCollider.gameObject);
        }
        else
        {
            Debug.LogWarning("L'objet détecté n'a pas de composant Harvestable");
        }
    }
}
