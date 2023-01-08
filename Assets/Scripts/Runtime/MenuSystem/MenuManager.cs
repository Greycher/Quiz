using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace QuizGame.Runtime.MenuSystem
{
    public class MenuManager : MonoBehaviour
    {
        private readonly Queue<Command> _pendingCommands = new Queue<Command>();
        private Command _commandOnExecution;
        private Menu _menuAfterAllCommandsExecuted;

        private static MenuManager _instance;

        public static MenuManager Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<MenuManager>();
                    if (!_instance)
                    {
                        _instance = new GameObject($"{nameof(MenuManager)} (Auto Created)").AddComponent<MenuManager>();
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
                return;
            }

            _instance = this;
        }

        public void OpenMenu(Menu menu)
        {
            if (_menuAfterAllCommandsExecuted)
            {
                if (_menuAfterAllCommandsExecuted == menu)
                {
                    return;
                }
                
                CloseMenu();
            }

            _menuAfterAllCommandsExecuted = menu;
            ExecuteOrQueueCommand(new OpenMenuCommand(menu, OnOpenMenuCommandExecuted));
        }

        public void CloseMenu()
        {
            Assert.IsTrue(_menuAfterAllCommandsExecuted);
            ExecuteOrQueueCommand(new CloseMenuCommand(_menuAfterAllCommandsExecuted, OnCloseMenuCommandExecuted));
            _menuAfterAllCommandsExecuted = null;
        }

        private void ExecuteOrQueueCommand(Command command)
        {
            if (_commandOnExecution != null)
            {
                _pendingCommands.Enqueue(command);
            }
            else
            {
                ExecuteCommand(command);
            }
        }

        private void OnOpenMenuCommandExecuted(Menu menu)
        {
            _commandOnExecution = null;
            ExecuteNextCommand();
        }
        
        private void OnCloseMenuCommandExecuted(Menu menu)
        {
            _commandOnExecution = null;
            ExecuteNextCommand();
        }
        
        private void ExecuteNextCommand()
        {
            if (_pendingCommands.Count > 0)
            {
                ExecuteCommand(_pendingCommands.Dequeue());
            }
        }

        private void ExecuteCommand(Command command)
        {
            _commandOnExecution = command;
            _commandOnExecution.Execute();
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        private abstract class Command
        {
            protected readonly Menu _menu;
            protected readonly Action<Menu> _onCommandExecuted;

            public Command(Menu menu, Action<Menu> onCommandExecuted)
            {
                _menu = menu;
                _onCommandExecuted = onCommandExecuted;
            }
            
            public abstract void Execute();
        }
        
        private class OpenMenuCommand : Command
        {
            public OpenMenuCommand(Menu menu, Action<Menu> onCommandExecuted) 
                : base(menu, onCommandExecuted) { }

            public override void Execute()
            {
                OpenMenuRoutine();
            }

            private async void OpenMenuRoutine()
            {
                _menu.gameObject.SetActive(true);
                await _menu.EntryAnimationRoutine();
                _onCommandExecuted.Invoke(_menu);
            }
        }
        
        private class CloseMenuCommand : Command
        {
            public CloseMenuCommand(Menu menu, Action<Menu> onCommandExecuted) 
                : base(menu, onCommandExecuted) { }
            
            public override void Execute()
            {
                CloseMenuRoutine();
            }

            private async void CloseMenuRoutine()
            {
                await _menu.OutroAnimationRoutine();
                _menu.gameObject.SetActive(false);
                _onCommandExecuted.Invoke(_menu);
            }
        }
    }
}