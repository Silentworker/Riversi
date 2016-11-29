using Assets.Scripts.sw.core.command;
using UnityEngine;

namespace Assets.Scripts.controller.commands
{
    public class ResultGameCommand : Command
    {
        public override void Execute(object data = null)
        {
            base.Execute();
            var result = (object[])data;

            Debug.LogFormat("Game result. white: {0}  black: {1}", result[0], result[1]);
        }
    }
}