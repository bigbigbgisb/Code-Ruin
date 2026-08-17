using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Code_Ruins.Views
{
    public static class Utils
    {
        public async static Task WaitUntil(Func<bool> condition)
        {
            while (true)
            {
                if (condition())
                {
                    break;
                }
                else
                {
                    await Task.Delay(16);
                }
            }
        }
    }
}
