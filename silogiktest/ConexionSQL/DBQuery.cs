using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Data;

namespace silogiktest.ConexionSQL
{
    public class DBQuery : Controller
    {
        DBContext db;

        public int ExecuteNoQuery(string _stored, SqlParameter[] _SqlParameter)
        {
            db = new DBContext();
            int resp;
            db.AbrirConexion(false);

            try
            {
                db.dtsComando.CommandType = CommandType.StoredProcedure;

                db.dtsComando.CommandText = _stored;
                db.dtsComando.Parameters.AddRange(_SqlParameter);

                resp = db.dtsComando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR ExecuteNoQuery" + e.ToString());
            }
            finally
            {
                db.CerrarConexion();
            }
            return resp;
        }

        public DataTable ExecuteQuery(string _stored, SqlParameter[] _SqlParameter)
        {
            db = new DBContext();

            SqlDataAdapter _SqlDataReader = null;
            DataTable _DataTable = null;
            db.AbrirConexion(false);

            try
            {
                db.dtsComando.CommandType = CommandType.StoredProcedure;

                db.dtsComando.CommandText = _stored;
                db.dtsComando.Parameters.AddRange(_SqlParameter);
                _SqlDataReader = new SqlDataAdapter();
                _DataTable = new DataTable();
                _SqlDataReader.SelectCommand = db.dtsComando;
                _SqlDataReader.Fill(_DataTable);

            }
            catch (Exception e)
            {
                throw new Exception("ERROR ExecuteQuery" + e.ToString());
            }
            finally
            {
                db.CerrarConexion();
            }
            return _DataTable;

        }

        public DataTable ExecuteQueryNoParameters(string _stored)
        {
            db = new DBContext();

            SqlDataAdapter _SqlDataReader = null;
            DataTable _DataTable = null;
            db.AbrirConexion(false);

            try
            {
                db.dtsComando.CommandType = CommandType.StoredProcedure;

                db.dtsComando.CommandText = _stored;
                _SqlDataReader = new SqlDataAdapter();
                _DataTable = new DataTable();
                _SqlDataReader.SelectCommand = db.dtsComando;
                _SqlDataReader.Fill(_DataTable);

            }
            catch (Exception e)
            {
                throw new Exception("ERROR ExecuteQuery" + e.ToString());
            }
            finally
            {
                db.CerrarConexion();
            }
            return _DataTable;

        }

    }

}
