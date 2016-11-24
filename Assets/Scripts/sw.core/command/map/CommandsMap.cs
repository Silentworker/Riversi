using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.sw.core.command.macro.mapper;
using Assets.Scripts.sw.core.eventdispatcher;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.sw.core.command.map
{
    public class CommandsMap : ICommandsMap
    {
        [Inject]
        private IEventDispatcher eventDispatcher;
        [Inject]
        private DiContainer container;

        private readonly List<ISubCommandMapper> _eventCommandMappers = new List<ISubCommandMapper>();
        private readonly List<ISubCommandMapper> _directCommandMappers = new List<ISubCommandMapper>();

        public ISubCommandMapper Map(string eventType, Type commandType)
        {
            if (!IsCommandType(commandType))
            {
                Debug.LogErrorFormat("Incompatible direct command type: {0}", commandType);
                throw new Exception("Incompatible direct command type");
            }

            var commandMapper = container.Resolve<ISubCommandMapper>();
            commandMapper.CommandType = commandType;
            eventDispatcher.AddEventListener(eventType, commandMapper.Execute);
            _eventCommandMappers.Add(commandMapper);

            Debug.LogFormat("Command {0} maped to {1}", commandType, eventType);
            return commandMapper;
        }

        public void UnMap(string eventType, Type commandType)
        {
            foreach (var commandMapper in _eventCommandMappers.Where(commandMapper => commandMapper.CommandType == commandType))
            {
                eventDispatcher.RemoveEventListener(eventType, commandMapper.Execute);
                _eventCommandMappers.Remove(commandMapper);
            }
        }

        public void DirectCommand(Type commandType, object data = null)
        {
            if (!IsCommandType(commandType))
            {
                Debug.LogErrorFormat("Incompatible direct command type: {0}", commandType);
                throw new Exception("Incompatible direct command type");
            }

            ISubCommandMapper directMapper = _directCommandMappers.FirstOrDefault(mapper => mapper.CommandType == commandType);

            if (directMapper == null)
            {
                directMapper = container.Resolve<ISubCommandMapper>();
                directMapper.CommandType = commandType;
                _directCommandMappers.Add(directMapper);
            }

            directMapper.Execute(data);
        }

        private static bool IsCommandType(Type commandType)
        {
            return typeof(ICommand).IsAssignableFrom(commandType);
        }
    }
}