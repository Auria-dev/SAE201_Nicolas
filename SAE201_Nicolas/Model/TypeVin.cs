using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_Nicolas.Model
{
    public class TypeVin
    {
        private int numType;
        private string nomType;

        public int NumType
        {
            get
            {
                return this.numType;
            }

            set
            {
                this.numType = value;
            }
        }

        public string NomType
        {
            get
            {
                return this.nomType;
            }

            set
            {
                this.nomType = value;
            }
        }
    }
}
