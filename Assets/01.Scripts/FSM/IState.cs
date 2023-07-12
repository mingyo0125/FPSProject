using UnityEngine;

public interface IState
{
    public abstract void EnterState(AIBrain AiBrain);
    public abstract void UpdateState(AIBrain AiBrain);
}
