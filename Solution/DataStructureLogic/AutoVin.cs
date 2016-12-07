using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresLibrary.Extendible_Hashing;

namespace DataStructureLogic
{
    class AutoVin : Record
    {
        public string EvidencneCislo { get; set; }
        public const int _pocet_bajtov_evc = 7;
        public string VinCislo { get; set; }
        public const int _pocet_bajtov_vin = 17;
        public override int GetSize()
        {
            return _pocet_bajtov_evc + _pocet_bajtov_vin;
        }

        public override int GetHash()
        {
            return VinCislo.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var temp = (AutoVin)obj;
            if (temp != null)
            {
                //porovnam ci sa rovnaku evidencne cisla
                if ((temp.EvidencneCislo.Equals(EvidencneCislo)))
                {
                    return true;
                }
            }
            return false;
        }

        public override byte[] ToByteArray()
        {
            throw new NotImplementedException();
        }

        public override void FromByteArray(byte[] byteArray)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
