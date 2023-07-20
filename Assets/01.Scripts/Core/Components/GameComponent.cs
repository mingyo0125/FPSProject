using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameComponent : IGameComponents
{
    protected readonly GameManager gameManager;

    protected GameComponent(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public virtual void UpdateState(GameState state)
    {
        switch (state)
        {
            case GameState.Init:
                Init();
                break;
            case GameState.Playing:
                Playing();
                break;
            case GameState.Pause:
                Pause();
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }

    protected virtual void Init() { }
    protected virtual void Playing() { }
    protected virtual void Pause() { }
    protected virtual void GameOver() { }

}
