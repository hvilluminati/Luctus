namespace TurnSystem
{
    using UnityEngine;

    public class EnemyTurn : Turn
    {

        public void StartTurn()
        {

            Debug.Log("Enemy turn started\n");

        }

        public void EndTurn()
        {
            
            Debug.Log("Enemy turn ended\n");
            setTurnEnded(true);
        }
    }
}