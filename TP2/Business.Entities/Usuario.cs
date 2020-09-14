using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Business.Entities
{
    public class Usuario : BusinessEntity
    {
        private string _NombreUsuario, _Clave;
        private int _IDPersona;
        private bool _Habilitado;
       #region Propiedades
        public string NombreUsuario
        {
            get => _NombreUsuario;
            set { _NombreUsuario = value; }
        }
        public string Clave
        {
            get => _Clave;
            set { _Clave = value; }
        }
        public int IDPersona
        {
            get => _IDPersona;
            set { _IDPersona = value; }
        }
        public bool Habilitado
        {
            get => _Habilitado;
            set { _Habilitado = value; }
        }
        #endregion
    }
}
