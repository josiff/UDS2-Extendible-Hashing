using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLibrary.Extendible_Hashing
{
    public class Block
    {
        /// <summary>
        /// Bloky dat
        /// </summary>
        public Record[] PoleRecordov { get; set; }

        public int PocetZaznamov { get; set; }

        public int PocetPlatnych { get; set; }

        public Block DalsiVolny { get; set; }
        /// <summary>
        /// Hlbka bloku
        /// </summary>
        public int Hlbka { get; set; }


        public int Size { get; set; }

        

        /// <summary>
        /// Zo suboru co mi prislo, tak naplnim record. 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byteArray ToByteArray(object data)
        {
            // return BitConverter.GetBytes(data);          
            throw new NotImplementedException();

        }

        public object FromByteArray(byteArray byteArray)
        {
            throw new NotImplementedException();
        }

        public bool Equals(object obj)
        {
            return false;
        }


    }
}
