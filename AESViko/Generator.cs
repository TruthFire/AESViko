using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESViko
{
    public class Generator
    {
        public Generator()
        {

        }

        public string generate(int size)
        {
            Random random = new Random();

            string rez = "";

            for(int i = 0; i < size/8; i++)
            {
                rez += (char)random.Next(33, 122);
            }

            return rez;
        }
        

    }
}
