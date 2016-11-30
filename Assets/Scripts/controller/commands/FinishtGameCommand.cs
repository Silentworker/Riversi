using Assets.Scripts.model.playfield;
using UnityEngine;
using Zenject;
using Command = Assets.Scripts.sw.core.command.Command;

namespace Assets.Scripts.controller.commands
{
    public class FinishtGameCommand : Command
    {
        [Inject]
        private IPlayFieldModel playFieldModel;
        public override void Execute(object data = null)
        {
            base.Execute();

            Debug.LogFormat("Game result. white: {0}  black: {1}", playFieldModel.scoreWhite, playFieldModel.scoreBlack);
        }
    }
}