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
        // Activer la gravité pour faire tomber l'objet
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody>();
        }
        rb.useGravity = true;

        // Commencer le fondu
        Renderer renderer = item.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color originalColor = renderer.material.color;
            float fadeSpeed = 0.2f / fadeDuration;

            for (float t = 0; t < 1f; t += Time.deltaTime * fadeSpeed)
            {
                Color newColor = originalColor;
                newColor.a = Mathf.Lerp(1f, 0f, t);
                renderer.material.color = newColor;
                yield return null;
            }
        }

        // Détruire l'objet après le fondu
        Destroy(item);
    }
}
