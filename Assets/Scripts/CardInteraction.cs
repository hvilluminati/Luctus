using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
	public class CardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		private RectTransform rectTransform;
		private Canvas canvas;
		private Card card;

		public RectTransform canvasRectransform;
		public GameObject ArrowPrefab;
		private GameObject arrowInstance;

		private GameObject enemy;
		public EnemyBehavior enemieBehaviour;

		public UnityEvent<CardInteraction> cardUsed = new UnityEvent<CardInteraction>();

		private void Start()
		{
			enemy = GameObject.FindGameObjectWithTag("Enemy");

			card = gameObject.GetComponent<CardDecorator>().card;
		}

		public void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			canvas = GetComponent<Canvas>();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			canvas.overrideSorting = true;
			rectTransform.DOScale(Vector3.one, 0.3f);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			canvas.overrideSorting = false;
			rectTransform.DOScale(Vector3.one * 0.75f, 0.3f);
		}
		public void OnPointerDown(PointerEventData eventData)
		{

			// Get the RectTransform of the clicked card and pass it to the arrow
			RectTransform cardRectTransform = GetComponent<RectTransform>();
			arrowInstance = Instantiate(ArrowPrefab, cardRectTransform.position, Quaternion.identity, canvas.transform);

			// Set the arrow's origin to the position of the card
			CubicCurveArrow cubicArrowScript = arrowInstance.GetComponent<CubicCurveArrow>();
			if (cubicArrowScript != null)
			{
				cubicArrowScript.SetArrowOrigin(cardRectTransform.position);
			}
			else
			{
				Debug.LogWarning("CubicCurveArrow component not found on the arrow GameObject.");
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			// Deactivate the arrow
			Destroy(arrowInstance);
			arrowInstance = null;

			if (enemieBehaviour != null && GameObject.FindWithTag("Arrow"))
			{
				enemieBehaviour.ManageCard(card.damage, card.damageType, card.statusDuration);
				cardUsed.Invoke(this);
			}

		}

	}

}

