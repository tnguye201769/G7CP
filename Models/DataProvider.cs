using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G7CP.Models
{
    [Obsolete("Not used any more, use 'using context' instead", false)]
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
        }

        public G7CPDBContext Db { get; private set; }

        private DataProvider()
        {
            Db = new G7CPDBContext();
        }
    }
}
