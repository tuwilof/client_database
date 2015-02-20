using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDatabase
{
    public class Model
    {
        public List<Table> table;
    }

    public class Table
    {
        public string nameRu;
        public string nameEn;
        public List<Column> column;
    }

    public class Column
    {
        public string type;
        public string nameRu;
        public string nameEn;
        public string parentTable;
    }
}
