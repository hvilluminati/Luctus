using UnityEngine;

public class TurnManager : MonoBehaviour
{
	public TurnState turn = TurnState.princessTurn;
	public GameManager gm;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void EndPlayerTurn()
	{
		turn = TurnState.enemyTurn;
		gm.EnemyTurn();
	}

	public void StartPlayerTurn()
	{
		turn = TurnState.princessTurn;

	}
}
