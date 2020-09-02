using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman 
    {
        //steps
        //user A :
        //Ya  = alpha ^Xa % q
        //key = Yb ^Xa % q

        //user B :
        //Yb  = alpha ^Xb % q
        //key = Ya ^Xb % q
        static int getkey(int alpha, int q, int x, int y)
        {
            int res = 1, t = 1;
            for (int i = 0; i < y; i++)
                t = (alpha * t) % q;
              
            for (int i = 0; i < x; i++)
                res = (t * res) % q;
              
            return res;
        }
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            int k1, k2; 
            List<int> k = new List<int>();
            k1 = getkey(alpha, q, xa, xb);
            k2 = getkey(alpha, q, xb, xa); 
            k.Add(k1);
            k.Add(k2);
            return k;
        }
    }
}
