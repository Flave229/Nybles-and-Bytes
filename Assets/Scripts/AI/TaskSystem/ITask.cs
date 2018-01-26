namespace Assets.Scripts.AI.TaskSystem
{
    public interface ITask
    {
        void Execute();
        void SetCompleted();
        bool IsComplete();
    }
}