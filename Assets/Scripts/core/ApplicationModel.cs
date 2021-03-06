﻿using Assets.Scripts.controller.events;
using Assets.Scripts.model.playfield;
using Assets.Scripts.sw.core.eventdispatcher;
using Assets.Scripts.sw.core.model;
using DG.Tweening;
using Zenject;

namespace Assets.Scripts.core
{
    public class ApplicationModel : Model
    {
        [Inject]
        private IPlayFieldModel _playFieldModel;

        public ApplicationModel(IEventDispatcher dispatcher, DiContainer container) : base(dispatcher, container)
        {
        }

        public void Init()
        {
            eventDispatcher.DispatchEvent(GameEvent.InitMainMenu);
        }
    }
}