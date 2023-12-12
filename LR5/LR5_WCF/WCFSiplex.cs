using LR5_WCF.Models;

namespace LR5_WCF
{
    public class WCFSiplex : IWCFSiplex
    {
        public int Add(int x, int y) => x + y;

        public string Concat(string s, double d) => $"{s}{d}";

        public A Sum(A a1, A a2) => a1 + a2;
    }
}
