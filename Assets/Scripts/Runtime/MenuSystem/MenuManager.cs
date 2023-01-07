using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace QuizGame.Runtime.MenuSystem
{
    public class MenuManager : MonoBehaviour
    {
        private Queue<Command> _pendingCommands = new Queue<Command>();
        private Command _commandOnExecution;
        private Menu _currentMenu;
        private Menu _predictedMenu;

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

        public void OpenMenu(Menu menu)
        {
            if (_predictedMenu)
            {
                if (_predictedMenu == menu)
                {
                    return;
                }
                
                CloseMenu();
            }

            _predictedMenu = menu;
            ExecuteOrQueueCommand(new OpenMenuCommand(menu, this, OnOpenMenuCommandExecuted));
        }

        public void CloseMenu()
        {
            Assert.IsTrue(_predictedMenu);
            ExecuteOrQueueCommand(new CloseMenuCommand(_predictedMenu, this, OnCloseMenuCommandExecuted));
            _predictedMenu = null;
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
            _currentMenu = menu;
            _commandOnExecution = null;
            ExecuteNextCommand();
        }
        
        private void OnCloseMenuCommandExecuted(Menu menu)
        {
            _currentMenu = null;
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

        private abstract class Command
        {
            protected readonly Menu _menu;
            protected readonly Action<Menu> _onCommandExecuted;
            protected readonly MonoBehaviour _monoBehaviour;

            public Command(Menu menu, MonoBehaviour monoBehaviour, Action<Menu> onCommandExecuted)
            {
                _menu = menu;
                _monoBehaviour = monoBehaviour;
                _onCommandExecuted = onCommandExecuted;
            }
            
            public abstract void Execute();
        }
        
        private class OpenMenuCommand : Command
        {
            public OpenMenuCommand(Menu menu, MonoBehaviour monoBehaviour, Action<Menu> onCommandExecuted) 
                : base(menu, monoBehaviour, onCommandExecuted) { }

            public override void Execute()
            {
                _monoBehaviour.StartCoroutine(OpenMenuRoutine());
            }

            private IEnumerator OpenMenuRoutine()
            {
                _menu.gameObject.SetActive(true);
                yield return _menu.EntryAnimationRoutine();
                _onCommandExecuted.Invoke(_menu);
            }
        }
        
        private class CloseMenuCommand : Command
        {
            public CloseMenuCommand(Menu menu, MonoBehaviour monoBehaviour, Action<Menu> onCommandExecuted) 
                : base(menu, monoBehaviour, onCommandExecuted) { }
            
            public override void Execute()
            {
                _monoBehaviour.StartCoroutine(CloseMenuRoutine());
            }

            private IEnumerator CloseMenuRoutine()
            {
                yield return _menu.OutroAnimationRoutine();
                _menu.gameObject.SetActive(false);
                _onCommandExecuted.Invoke(_menu);
            }
        }
    }
}