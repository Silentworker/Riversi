using Assets.Scripts.model.playfield;
using UnityEngine;

namespace Assets.Scripts.controller.commands
{
    public class ResultGameCommand : StartGameCommand
    {
        public override void Execute(object data = null)
        {
            base.Execute();
            var result = (GameResult)data;

            Debug.LogFormat("Game result. White: {0}  Black: {1}", result.ScoreWhite, result.ScoreBlack);
        }
    }
}