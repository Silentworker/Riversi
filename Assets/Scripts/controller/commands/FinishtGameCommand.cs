using Assets.Scripts.controller.headsup;
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
        [Inject]
        private IHeadsUpController headsUpController;

        public override void Execute(object data = null)
        {
            base.Execute();

            var scoreW = playFieldModel.scoreWhite;
            var scoreB = playFieldModel.scoreBlack;

            string promo = scoreW > scoreB ? "lightside wins" : scoreW < scoreB ? "darkside wins" : "draw";

            headsUpController.ShowPromo(promo);

            Debug.LogFormat("Game result. white: {0}  black: {1}", scoreW, scoreB);
        }
    }
}