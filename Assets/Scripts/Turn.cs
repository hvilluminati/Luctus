namespace TurnSystem {

    using UnityEngine;

    public abstract class Turn : MonoBehaviour
    {
        private bool isDead;
        private bool turnEnded = false;

        void Start()
        {
            isDead = false;
        }

        public bool getDeath() {
            return isDead;
        }

        public bool getTurnEnded()
        {
            return turnEnded;
        }

        public void setTurnEnded(bool turnStatus)
        {
            turnEnded = turnStatus;
        }
    }
}