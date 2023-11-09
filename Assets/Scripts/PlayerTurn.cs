namespace TurnSystem
{
    using UnityEngine;
    public class PlayerTurn : Turn
    {

        public void StartTurn()
        {

            Debug.Log("Player turn started\n");

        }

        public void EndTurn()
        {
            Debug.Log("Player turn ended\n");
            setTurnEnded(true);
        }


    }
}