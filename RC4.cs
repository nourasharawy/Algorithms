using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecurityLibrary.RC4
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    /// steps
    /// 1-Initialization of S and T
    /// 2-Initial permutation of S  
    /// 3-Generation of Key stream K 
    /// 4-Encryption/ Decryption (XOR with K)


    public class RC4 : CryptographicTechnique
    {
        static int[] swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
            return arr;
        }
        static public bool IsHexa = false;
        static string ConvertToString(string HexaString)//takes hexa and converts it to string
        {
            IsHexa = true;
            string tmpP = "";
            for (int i = 2; i < HexaString.Length; i += 2)//strats with 2 because in case of hexa  i[0]='0' , i[1]='x' ; inc =2 : 1 char has =8 bits  in binary = 2 digits in hexa 
            {
                //to convert hexa to string we have to :
                // 1- convert hexa to integer 
                // 2- convert integer to string
 
                tmpP += char.ConvertFromUtf32(Convert.ToInt32(HexaString[i].ToString() + HexaString[i + 1].ToString(), 16));
            }
            return tmpP;
        }
        public override string Decrypt(string cipherText, string key)
        {
            return (Encrypt(cipherText, key));
        }

        public override string Encrypt(string plainText, string key)
        {
            //If the string starts with 0x.... then it's Hexadecimal not string

            if (plainText[0] == '0' && plainText[1] == 'x')
                plainText = ConvertToString(plainText);

            if (key[0] == '0' && key[1] == 'x')
                key = ConvertToString(key);
            

            int[] S = new int[256];
            int[] T = new int[256];

            //step 1:Initialization of S and T
            for (int i = 0; i < 256; i++)
            {
                S[i] = i;
                //If the key K is 256 byte, then K is transferred to T.  
                //otherwise, for a key of length keylen bytes, the first keylen elements of T are copied from K then K is repeated as many times necessary to fill out T
                T[i] = key[i % key.Length]; 
            }


            //step 2 :Initial permutation of S
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + T[i]) % 256;
                swap(S, i, j);
            }


            //step 3:Generation of Key stream K
            int x = 0, z = 0, t = 0;
            int[] k = new int[key.Length];
            string cipher = "";

            for (int i = 0; i < plainText.Length; i++)
            {
                x = (x + 1) % 256;
                z = (z + S[x]) % 256;
                swap(S, x, z);
                t = (S[x] + S[z]) % 256;
                k[i] = S[t];
            }


            //step 4:Encryption/ Decryption (XOR with K)
            for (int i = 0; i < plainText.Length; i++)
            {
                cipher += char.ConvertFromUtf32(plainText[i] ^ k[i]);
            }

            //if PL was Hexa ,we have to convert it again to hexa 
            if (IsHexa)
            {
                cipher = string.Join("", cipher.Select(c => ((int)c).ToString("x2")));
                cipher = "0x" + cipher;
            }
         
            return cipher; 
        }
    }
}
