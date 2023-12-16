using UnityEngine;

public class CheckCardEffects : MonoBehaviour
{
    public bool isPoisoned = false;
    public bool isBurning = false;
    public bool isFreezing = false;
    public bool isBleeding = false;

    public GameObject poison;
    public GameObject burn;
    public GameObject freeze;
    public GameObject bleed;

    private void Start()
    {
        poison.SetActive(false);
        burn.SetActive(false);
        freeze.SetActive(false);
        bleed.SetActive(false);
    }

    private void Update()
    {
        if (isPoisoned)
        {
            poison.SetActive(true);
        } else
        {
            poison.SetActive(false);
        }

        if (isBurning)
        {
            burn.SetActive(true);
        }
        else
        {
            burn.SetActive(false);
        }

        if (isFreezing)
        {
            freeze.SetActive(true);
        }
        else
        {
            freeze.SetActive(false);
        }

        if (isBleeding)
        {
            bleed.SetActive(true);
        }
        else
        {
            bleed.SetActive(false);
        }
    }

}
