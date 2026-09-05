using System;
namespace Code_Ruins.Views
{
    public class CmdCommandInformation
    {
        public string ReturnValue { get; }
        public Action<string[]> Action { get; }
        public CmdCommandInformation(string returnValue, Action<string[]>? action)
        {
            ReturnValue = returnValue;
            Action = action ?? Dummy;
        }
        private readonly Action<string[]> Dummy = _ => { };
    }
}