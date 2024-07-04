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

    [SerializeField]
    private int numberOfRays = 5;
    [SerializeField]
    private float raySpreadAngle = 30f;

    void Update()
    {
        bool itemDetected = false;

        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = (i - (numberOfRays / 2f)) * (raySpreadAngle / numberOfRays);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 direction = rotation * transform.forward;

            Debug.DrawRay(transform.position, direction * interactRange, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, interactRange))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        axeVisual.SetActive(true);
                        StartCoroutine(HarvestAfterWait(hit.collider));
                    }
                    interactText.SetActive(true);
                    itemDetected = true;
                    break;
                }
            }
        }

        if (!itemDetected)
        {
            interactText.SetActive(false);
        }
    }

    private IEnumerator HarvestAfterWait(Collider itemCollider)
    {
        if (playerAnimator != null) {
            playerAnimator.SetTrigger("Harvest");
        }

        yield return new WaitForSeconds(3f);

        Harvestable harvestable = itemCollider.GetComponentInParent<Harvestable>();
        if (harvestable != null) {
            harvestable.Harvest(itemCollider.gameObject);
        }
        else
        {
            Debug.LogWarning("L'objet détecté n'a pas de composant Harvestable");
        }
    }
}