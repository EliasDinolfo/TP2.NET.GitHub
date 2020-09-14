using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class ModuloUsuario : BusinessEntity
    {
        private int _IDUsuario, _IDModulo;
        private bool _PermiteAlta, _PermiteBaja, _PermiteModificacion, _PermiteConsulta;
        #region Propiedades
        public int IdUsuario
        {
            get => _IDUsuario;
            set { _IDUsuario = value; }
        }
        public int IdModulo
        {
            get => _IDModulo;
            set { _IDModulo = value; }
        }
        public bool PermiteAlta
        {
            get => _PermiteAlta;
            set { _PermiteAlta = value; }
        }
        public bool PermiteBaja
        {
            get => _PermiteBaja;
            set { _PermiteBaja = value; }
        }
        public bool PermiteModificacion
        {
            get => _PermiteModificacion;
            set { _PermiteModificacion = value; }
        }
        public bool PermiteConsulta
        {
            get => _PermiteConsulta;
            set { _PermiteConsulta = value; }
        }
        #endregion
    }
}
