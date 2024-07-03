using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public GameObject axePrefab; // Le prefab de la hache à équiper
    public KeyCode equipKey = KeyCode.A; // La touche pour équiper la hache

    private GameObject equippedAxe;
    private bool isEquipped = false;

    void Update()
    {
        if (Input.GetKeyDown(equipKey))
        {
            EquipAxe();
        }
    }

    void EquipAxe()
    {
        if (isEquipped)
        {
            // Déséquipe la hache
            Destroy(equippedAxe);
            Debug.Log("La hache a été déséquipée.");
        }
        else
        {
            // Équipe la hache
            equippedAxe = Instantiate(axePrefab, transform.position, transform.rotation);
            // Optionnel : Faites de la hache un enfant du joueur pour qu'elle suive ses mouvements
            equippedAxe.transform.SetParent(transform);
            Debug.Log("La hache a été équipée.");
        }

        isEquipped = !isEquipped;
    }
}
