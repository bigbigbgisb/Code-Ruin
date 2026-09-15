using System;
using System.Collections.Generic;

namespace Code_Ruins.Views;

public class ChattingLine
{
    private readonly Dictionary<int, string> _nameDict = new()
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
        Message = (name is int index) ? (_nameDict.GetValueOrDefault(index, "Cannot Find Resource") + "\n" + message) : (name is string personName) ? (personName + "\n" + message) : throw new InvalidCastException();
    }
}
