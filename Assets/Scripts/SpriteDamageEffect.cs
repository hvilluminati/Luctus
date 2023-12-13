using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDamageEffect : MonoBehaviour
{
    [SerializeField] private Color flashColour = Color.red;
    [SerializeField] private float flashDuration = 0.2f;

    private SpriteRenderer spriteRenderer;
    private Material material;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
     
    }

    public void StartFlashRenderer()
    {
        StartCoroutine(FlashRenderer());
    }


    private IEnumerator FlashRenderer()
    {
        material.SetColor("_FlashColour", flashColour);

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / flashDuration));
            material.SetFloat("_FlashAmount", currentFlashAmount);

            yield return null;
        }

    }
}
