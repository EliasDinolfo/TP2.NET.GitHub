using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class DocenteCurso : BusinessEntity
    {
        private TiposCargos _Cargo;
        private int _IDCurso;
        private int _IDDocente;

        public enum TiposCargos
        {
            Auxiliar,
            Docente
        }
        public TiposCargos Cargo
        {
            get => _Cargo;
            set
            {
                _Cargo = value;   
            }
        }

        public int IDCurso
        {
            get => _IDCurso;
            set
            {
                _IDCurso = value;
            }
        }

        public int IDDocente
        {
            get => _IDDocente;
            set
            {
                _IDDocente = value;
            }
        }
    }
}
