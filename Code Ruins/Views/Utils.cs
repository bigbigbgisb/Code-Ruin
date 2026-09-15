using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Code_Ruins.Views {
    public static class Utils {
        public async static Task WaitUntil(Func<bool> condition) {
            int timeMs = 0;
            while (!condition()) {
                if (timeMs > 36000000) throw new TimeoutException("WaitUntil waits for 10 minutes but condition is still not true");
                await Task.Delay(16);
                timeMs += 16;
            }
        }
    }
}
