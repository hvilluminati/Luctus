using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
	public class CardInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		private RectTransform rectTransform;
		private Canvas canvas;
		private bool isDragging;
		private Vector2 originalPosition;
		private Vector2 pointerOffset;
		public RectTransform canvasRectransform;

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
			//Vector2 normelizedPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

			isDragging = true;
			originalPosition = rectTransform.anchoredPosition;
			//pointerOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, eventData.pressEventCamera, out pointerOffset);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			isDragging = false;
			rectTransform.DOAnchorPos(originalPosition, 0.5f);
		}

		public void Update()
		{
			if (isDragging)
			{
				Vector2 cursorPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				//Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + pointerOffset;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectransform, Input.mousePosition, null, out Vector2 cursorPosition);
				rectTransform.anchoredPosition = cursorPosition - pointerOffset;
				Debug.Log(cursorPosition);
			}
		}
	}
}
