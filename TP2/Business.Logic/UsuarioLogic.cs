using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Data.Database;

namespace Business.Logic
{
    public class UsuarioLogic : BusinessLogic
    {
        private UsuarioAdapter _UsuarioAdapter;
        public UsuarioLogic()
        {
            UsuarioAdapter = new UsuarioAdapter();
        }
        public UsuarioAdapter UsuarioAdapter
        {
            get => _UsuarioAdapter;
            set { _UsuarioAdapter = value; }
        }
        public List<Usuario> GetAll()
        {
            return _UsuarioAdapter.GetAll();
        }
        public Usuario GetOne(int ID)
        {
            return _UsuarioAdapter.GetOne(ID);
        }
        public List<Usuario> GetPorPersona(int ID)
        {
            return _UsuarioAdapter.GetPorPersona(ID);
        }
        public void Delete(int ID)
        {
            _UsuarioAdapter.Delete(ID);
        }
        public void Save(Usuario usuarioDeEntities)
        {
            _UsuarioAdapter.Save(usuarioDeEntities);
        }
    }
}
