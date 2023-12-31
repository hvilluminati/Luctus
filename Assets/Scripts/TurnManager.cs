using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{

	public GameManager gm;

	public UnityEvent beginEnemyTurn = new UnityEvent();
	public UnityEvent beginPlayerTurn = new UnityEvent();

	public TurnState currentTurn;

	public void Start()
	{
		currentTurn = TurnState.princessTurn; // Start game with player turn
	}

	public void EndPlayerTurn()
	{
		currentTurn = TurnState.enemyTurn;
		beginEnemyTurn.Invoke();
		//gm.EnemyTurn();
	}

	public void StartPlayerTurn()
	{
		currentTurn = TurnState.princessTurn;
		beginPlayerTurn.Invoke();

	}
}
