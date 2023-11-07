using UnityEngine;

public class TurnManager : MonoBehaviour
{
	public TurnState turn = TurnState.princessTurn;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void EndTurn()
	{
		turn = TurnState.enemyTurn;
	}
}
