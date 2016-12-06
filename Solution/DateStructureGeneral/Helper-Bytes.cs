using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateStructureGeneral
{
   public static class Helper_Bytes
    {
        public static byte[] _get_pom_pole(int pocetBytov, byte[] tempPole)
        {
            byte[] pomPole;
            int rozdiel;
            int index;
            pomPole = new byte[pocetBytov];
            rozdiel = pomPole.Length - tempPole.Length;
            index = tempPole.Length;

            if (rozdiel > 0)
            {
                Array.Copy(tempPole, 0, pomPole, 0, index);
                tempPole = new byte[rozdiel];
                Array.Copy(tempPole, 0, pomPole, index, tempPole.Length);
            }
            else
            {
                pomPole = tempPole;
            }
            return pomPole;
        }

    }
}
