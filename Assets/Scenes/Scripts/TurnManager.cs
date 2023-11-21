using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{

	public GameManager gm;

	public UnityEvent turnChanged = new UnityEvent();

	public TurnState currentTurn;

	public void Start()
	{
		currentTurn = TurnState.princessTurn; // Start game with player turn
	}

	public void EndPlayerTurn()
	{
		currentTurn = TurnState.enemyTurn;
		turnChanged.Invoke();
		//gm.EnemyTurn();
	}

	public void StartPlayerTurn()
	{
		currentTurn = TurnState.princessTurn;
		turnChanged.Invoke();

	}
}
