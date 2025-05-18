using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroEmpresas.Application
{
    public static class IdGenerator
    {
        private static int _currentId = 0;
        private static readonly object _lock = new object();

        public static int GerarId()
        {
            lock (_lock)
            {
                _currentId++;
                return _currentId;
            }
        }
    }

}
