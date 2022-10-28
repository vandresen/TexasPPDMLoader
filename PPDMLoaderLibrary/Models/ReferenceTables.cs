using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.Models
{
    public class ReferenceTables
    {
        public List<ReferenceTable> RefTables { get;}
        public ReferenceTables()
        {
            this.RefTables = new List<ReferenceTable>() 
            {
                new ReferenceTable() 
                { KeyAttribute = "BUSINESS_ASSOCIATE_ID", Table = "BUSINESS_ASSOCIATE", ValueAttribute= "BA_LONG_NAME"},
                new ReferenceTable()
                { KeyAttribute = "FIELD_ID", Table = "FIELD", ValueAttribute= "FIELD_NAME"},
            };
        }
    }
}
