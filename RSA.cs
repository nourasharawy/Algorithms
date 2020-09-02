using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    //steps
    //n = p * q;
    //fn= (p - 1) * (q - 1)
    //d = e^(-1) mod fn
    //m = c ^ d mod n   Decryption 
    //c = m ^ e mod n   Encryption
    public class RSA
    {
        static int modInverse(int a, int m) //e^(-1) mod fn
        {
            a = a % m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }
        static int x(int a, int b, int c) //to get a ^ c mod b
        {
            int res = 1;
            for (int i = 0; i < c; i++)
                res = (a * res) % b;

            return res;
        }
        public int Encrypt(int p, int q, int M, int e)
        {
            int  n, fn, d, c;
            n = p * q;
            fn = (p - 1) * (q - 1);
            d = modInverse(e, fn);
            c = x(M, n, e);
            
            return c;
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            int n, fn, d, M;
            n = p * q;
            fn = (p - 1) * (q - 1);
            d = modInverse(e, fn);
            M = x(C, n, d);
            return M;
        }
    }
}
