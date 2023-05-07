namespace Bomberman.AI.States
{
    public class WaitingForBombs : IState
    {
        private AIBombermanController aiBombermanController;

        public WaitingForBombs(AIBombermanController aiBombermanController)
        {
            this.aiBombermanController = aiBombermanController;
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }
    }
}