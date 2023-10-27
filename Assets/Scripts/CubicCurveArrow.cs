using System.Collections.Generic;
using UnityEngine;

public class CubicCurveArrow : MonoBehaviour
{
	[Tooltip("Prefab of arrow head")]
	public GameObject arrowHeadPrefab;

	[Tooltip("Prefab of arrow node")]
	public GameObject arrowNodePrefab;

	[Tooltip("Number of arrow nodes")]
	public int arrowNodeNum;

	[Tooltip("Scale multiplier for arrow nodes")]
	public float scaleFactor = 1f;

	// Position of P0 (arrows emitter point)
	public RectTransform rectTransform;

	// List of arrow node's transform
	private List<RectTransform> arrowNodes = new List<RectTransform>();

	// List of control points
	private List<Vector2> controlPoints = new List<Vector2>();

	// Factors to determine the position of control point P1, P2
	private readonly List<Vector2> controlPointFactors = new List<Vector2> { new Vector2(-0.3f, 0.8f), new Vector2(0.1f, 1.4f) };

	public void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}


	public void Start()
	{
		Debug.Log("Inside Arrow start");
		// Get the RectTransform of the Canvas
		RectTransform canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

		// Set the initial position of the origin to the center of the screen
		//rectTransform.position = canvasRect.position + new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f);
		//Debug.Log("Position: " + origin.position);
		//Debug.Log("Origin: " + origin);

		for (int i = 0; i < arrowNodeNum; i++)
		{
			arrowNodes.Add(Instantiate(arrowNodePrefab, this.transform).GetComponent<RectTransform>());
		}

		arrowNodes.Add(Instantiate(arrowHeadPrefab, this.transform).GetComponent<RectTransform>());

		// Initialize the control points list
		for (int i = 0; i < 4; i++)
		{
			controlPoints.Add(Vector2.zero);
		}

		//gameObject.SetActive(false);
	}

	private void Update()
	{
		Debug.Log("Inside Arrow Update + Origin position: " + rectTransform.position);
		// P0 is the arrow emitter point
		controlPoints[0] = new Vector2(rectTransform.position.x, rectTransform.position.y);

		// P3 is at the mouse position
		controlPoints[3] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		// P1, P2 determines by P0 and P3
		// P1 = P0 + (P3 - P0) * Vector2(-0.3f, 0.8f)
		// P2 = P0 + (P3 - P0) * Vector2(0.1f, 1.4f)
		controlPoints[1] = controlPoints[0] + (controlPoints[3] - controlPoints[0]) * controlPointFactors[0];
		controlPoints[2] = controlPoints[0] + (controlPoints[3] - controlPoints[0]) * controlPointFactors[1];

		for (int i = 0; i < arrowNodes.Count; i++)
		{
			var t = Mathf.Log(1f * i / (arrowNodes.Count - 1) + 1f, 2f);

			// Cubic Benzier curve
			// B(t) = (1-t)^3 * P0 + 3 * (1-t)^2 * t * P1 + 3 * (1-t) * t^2 * P2 + t^3 * P3
			arrowNodes[i].position =
				Mathf.Pow(1 - t, 3) * controlPoints[0] +
				3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1] +
				3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2] +
				Mathf.Pow(t, 3) * controlPoints[3];

			// Calculate rotations for each arrow node
			if (i > 0)
			{
				var euler = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, arrowNodes[i].position - arrowNodes[i - 1].position));
				arrowNodes[i].rotation = Quaternion.Euler(euler);
			}

			// Calculates scales for each arrow node
			var scale = scaleFactor * (1f - 0.03f * (arrowNodes.Count - 1 - i));
			arrowNodes[i].localScale = new Vector3(scale, scale, 1f);

		}

		// First arrow node's rotation
		arrowNodes[0].transform.rotation = arrowNodes[1].transform.rotation;
	}

	public void SetArrowOrigin(Vector3 originPosition)
	{
		Debug.Log("Setting arrow origin");
		rectTransform.position = originPosition;
	}
}
