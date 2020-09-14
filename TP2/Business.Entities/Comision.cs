using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Comision : BusinessEntity
    {
        private string _Descripcion;
        private int _IDPlan;
        private int _AnioEspecialidad;
        private string _DataFull;
        public int IDPlan
        {
            get => _IDPlan;
            set
            {
                _IDPlan = value;
            }
        }

        public string Descripcion
        {
            get => _Descripcion;
            set
            {
                _Descripcion = value;
            }
        }

        public int AnioEspecialidad
        {
            get => _AnioEspecialidad;
            set
            {
                _AnioEspecialidad = value;
            }
        }

        public string DataFull
        {
            get => _DataFull;
            set
            {
                _DataFull = value;
            }
        }
    }
}
