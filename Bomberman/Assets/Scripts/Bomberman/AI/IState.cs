namespace Bomberman.AI
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}