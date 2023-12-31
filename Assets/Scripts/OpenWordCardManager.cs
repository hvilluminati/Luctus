using UnityEngine;

public class OpenWordCardManager : MonoBehaviour
{
    public GameObject AddCardPanel;
    public GameObject CardObject;
    public Card card;
    public GameObject thisCard;
    public GameObject closeButton;

    public AnimationCurve myCurve;
    public float distance = 0.5f;
    private Vector3 initialPosition;

    private void Start()
    {
        // Store the initial position of the GameObject
        initialPosition = transform.position;
        closeButton.SetActive(false);
    }

    private void Update()
    {
        transform.position = new Vector3(initialPosition.x , initialPosition.y + myCurve.Evaluate((Time.time % myCurve.length)) * distance, initialPosition.z);
        if (AddCardPanel.activeInHierarchy)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                AddCardPanel.SetActive(false);
                closeButton.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AddCardPanel.SetActive(true);
            closeButton.SetActive(true);
            DeckManager.instance.AddCard(card);
            CardObject.GetComponent<CardDecoratorSimple>().Initiate(card);
            thisCard.SetActive(false);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void CloseButton()
    {
        AddCardPanel.SetActive(false);
        closeButton.SetActive(false);
    }
}