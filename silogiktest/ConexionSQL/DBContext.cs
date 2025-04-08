using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;


namespace silogiktest.ConexionSQL
{
    public class DBContext : Controller
    {
        private SqlConnection dtsConexion;
        public SqlCommand dtsComando { get; set; }
        public SqlTransaction dtsTransaccion { get; set; }
        public DBContext()
        {
            //TODO: QUITAR ESTO DEL CODIGO
            dtsConexion = new SqlConnection();
            dtsConexion.ConnectionString = "Data Source=sql1003.site4now.net;Initial Catalog=db_ab7437_ineritos31;User ID=db_ab7437_ineritos31_admin;Password=ett.2704;MultipleActiveResultSets=True";
        }
        public void AbrirConexion(Boolean _Transaccion)
        {
            if (dtsConexion.State == ConnectionState.Closed)
            {
                try
                {
                    dtsConexion.Open();
                    dtsComando = dtsConexion.CreateCommand();
                    dtsComando.CommandTimeout = 150;
                    if (_Transaccion)
                    {
                        dtsTransaccion = dtsConexion.BeginTransaction();
                        dtsComando.Transaction = dtsTransaccion;
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
            }
        }
        public void CerrarConexion()
        {
            if (dtsConexion.State == ConnectionState.Open)
            {
                dtsConexion.Close();
                dtsConexion.Dispose();
                if (dtsTransaccion != null)
                    dtsTransaccion.Dispose();
                dtsComando.Dispose();
            }
        }
        public SqlConnection ObtenerCadenaConexion()
        {
            return dtsConexion;
        }
    }
}
