namespace TurnSystem
{
    using UnityEngine;

    public class EnemyTurn : Turn
    {

        public void StartTurn()
        {

            Debug.Log("Enemy turn started\n");
            // TODO: INSERT ENEMY TURN LOGIC

        }

        public void EndTurn()
        {
            
            Debug.Log("Enemy turn ended\n");
            setTurnEnded(true);
        }
    }
}