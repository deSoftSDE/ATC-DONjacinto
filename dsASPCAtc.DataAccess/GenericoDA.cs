using dsCore2.DataAccess;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace dsASPCAtc.DataAccess
{
    public class GenericoDA : BaseDataAccess
    {
        public GenericoDA(IConfiguration configuration)
        {
            var cc = configuration.GetConnectionString("DefaultConnection");
            _conexion = new SqlConnection(cc);
        }

    }
}
