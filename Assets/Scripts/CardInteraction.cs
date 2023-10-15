using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
	public class CardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		private RectTransform rectTransform;
		private Canvas canvas;

		public RectTransform canvasRectransform;
		public GameObject Arrow;


		private void Start()
		{
			// Find the Arrow by name
			Arrow = GameObject.Find("Arrow");
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
			// Activate the arrow
			Arrow.SetActive(true);

			//originalPosition = rectTransform.anchoredPosition;
			//RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, eventData.pressEventCamera, out pointerOffset);

			// Get the RectTransform of the clicked card and pass it to the arrow
			RectTransform cardRectTransform = GetComponent<RectTransform>();

			// Set the arrow's origin to the position of the card
			CubicCurveArrow cubicArrowScript = Arrow.GetComponent<CubicCurveArrow>();

			cubicArrowScript.SetArrowOrigin(cardRectTransform.position);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			// Deactivate the arrow
			Arrow.SetActive(false);
		}

	}
}
