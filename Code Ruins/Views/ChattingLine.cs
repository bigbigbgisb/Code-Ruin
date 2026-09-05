using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;

namespace Code_Ruins.Views
{
    public class ChattingLine
    {
        private readonly Dictionary<int, string> nameDict = new()
        {
            [1] = "赛佛",
            [2] = "希伦·格雷",
            [3] = "区长 提米",
        };
        public string Message { get; }
        public Action Function { get; }
        public ChattingLine(string message, Action? function, object name)
        {
            Function = function ?? (() => { });
            if (name is int index)
            {
                Message = nameDict.GetValueOrDefault(index, "Cannot Find Resource") + "\n" + message;
            }
            else if (name is string personName)
            {
                Message = personName + "\n" + message;
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }
}