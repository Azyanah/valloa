using System.Collections;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public float fadeDuration = 3f; // Durée du fondu en secondes

    public void Harvest(GameObject item)
    {
        Debug.Log("Récolte effectuée sur : " + item.name);
        StartCoroutine(FadeOutAndDestroy(item));
    }
    private IEnumerator FadeOutAndDestroy(GameObject item)
    {
        // Ajouter et configurer le Rigidbody
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;

        // Attendre 3 secondes avant de commencer le fondu
        yield return new WaitForSeconds(3f);

        // Début de l'animation de disparition
        float startTime = Time.time;
        fadeDuration = 10f;
        float endTime = startTime + fadeDuration;
        Vector3 startPosition = item.transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, -2, startPosition.z);

        // Désactiver le Rigidbody pour permettre la manipulation de la position
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.detectCollisions = false;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / fadeDuration;
            item.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null; // Attendre le prochain frame
        }

        // Déplacer l'objet directement à la position finale au cas où la boucle ne l'a pas fait
        item.transform.position = endPosition;

        // Détruire l'objet après le fondu
        Destroy(item);
    }

}
