using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
     
    public byte[] ToByteArray()
    {
        byte[] bytes = new byte[Size];
        //todo
        return bytes ;
    }

        /// <summary>
        /// Metoda naplni danu class datami z array of bytov
        /// </summary>
        /// <param name="byteArray">pole bytov<param>
        /// <param name="hasAdress"></param>
        public void FromByteArray(byte[] byteArray, bool hasAdress = true)
        {
            //todo
        }


    public bool Equals(object obj)
        {
            return false;
        }


    }
}
