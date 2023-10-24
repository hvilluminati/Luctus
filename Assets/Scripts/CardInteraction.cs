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
		public GameObject ArrowPrefab;
		private GameObject arrowInstance;


		private void Start()
		{

		}

		public void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			canvas = GetComponent<Canvas>();

			//Arrow = GameObject.FindGameObjectsWithTag("Arrow");
			//Arrow = GameObject.Find("Arrow");
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
			Debug.Log("Inside OnPointerDown");
			//originalPosition = rectTransform.anchoredPosition;
			//RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, eventData.pressEventCamera, out pointerOffset);

			// Get the RectTransform of the clicked card and pass it to the arrow
			RectTransform cardRectTransform = GetComponent<RectTransform>();
			arrowInstance = Instantiate(ArrowPrefab, cardRectTransform.position, Quaternion.identity, canvas.transform);
			Debug.Log("Arrow instantiated");

			// Set the arrow's origin to the position of the card
			CubicCurveArrow cubicArrowScript = arrowInstance.GetComponent<CubicCurveArrow>();
			if (cubicArrowScript != null)
			{
				cubicArrowScript.SetArrowOrigin(cardRectTransform.position);
				//cubicArrowScript.origin = cardRectTransform;
			}
			else
			{
				Debug.LogWarning("CubicCurveArrow component not found on the arrow GameObject.");
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			Debug.Log("Inside onPointerUp");
			// Deactivate the arrow
			Destroy(arrowInstance);
			arrowInstance = null;
		}

	}
}
