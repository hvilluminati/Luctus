using UnityEngine;

public class OpenWordCardManager : MonoBehaviour
{
    public GameObject AddCardPanel;
    public GameObject CardObject;
    public Card card;
    public GameObject thisCard;
    public GameObject Player;

    public AnimationCurve myCurve;
    public float distance = 0.5f;
    private Vector3 initialPosition;

    private void Start()
    {
        // Store the initial position of the GameObject
        initialPosition = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(initialPosition.x , initialPosition.y + myCurve.Evaluate((Time.time % myCurve.length)) * distance, initialPosition.z);

        if (AddCardPanel != null)
        {
            if (AddCardPanel.activeInHierarchy)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    AddCardPanel.SetActive(false);
                    Player.GetComponent<PlayerMovement>().enabled = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AddCardPanel.SetActive(true);
            Player.GetComponent<PlayerMovement>().enabled = false;
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            DeckManager.instance.AddCard(card);
            CardObject.GetComponent<CardDecoratorSimple>().Initiate(card);
            thisCard.SetActive(false);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}