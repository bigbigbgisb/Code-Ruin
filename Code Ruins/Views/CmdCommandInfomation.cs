using System;
using System.Collections.Generic;
using System.Text;

namespace Code_Ruins.Views
{
    public class CmdCommandInfomation
    {
        private string _returnValue;
        private Action<string[]> _action;
        public string ReturnValue { get => _returnValue; }
        public Action<string[]> Action { get => _action; }
        public CmdCommandInfomation(string returnValue, Action<string[]>? action)
        {
            _returnValue = returnValue;
            _action = action ?? Dummy;
        }
        private void Dummy(string[] _) { }
    }
}
