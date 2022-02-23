using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Interfaces
{
    public interface IDatabase
    {
        public void Insert();
        public void Update();
        public void Read();

    }
}
