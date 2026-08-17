using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Code_Ruins.Views
{
    public class ChattingLine
    {
        private Dictionary<int, string> nameDict = new()
        {
            [1] = "赛佛",
            [2] = "希伦·格雷",
            [3] = "区长 提米",
        };
        private string _finalMessage;
        private Action _function;
        public string Message { get { return _finalMessage; } }
        public Action Function { get { return _function; } }
        private void Dummy()
        {

        }
        public ChattingLine(string message,Action? function, object name)
        {
            _function = function ?? Dummy;

            if (name.GetType() == typeof(int))
            {
                try
                {
                    _finalMessage = nameDict[(int)name] + "\n" + message;
                }
                catch
                {
                    throw new KeyNotFoundException();
                }
                
            }
            else if (name.GetType() == typeof(string))
            {
                _finalMessage = (string)name + "\n" + message;
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }
}
