using Assets.Scripts.controller.config;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.core
{
    public class ApplicationInit : MonoBehaviour
    {
        [Inject]
        private ICommandsConfig commandsConfig;

        [Inject]
        private ApplicationModel applicationModel;

        void Awake()
        {
            commandsConfig.Init();

            applicationModel.Init();
        }
    }
}
