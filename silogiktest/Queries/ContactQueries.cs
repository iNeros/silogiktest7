using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using silogiktest.ConexionSQL;
using silogiktest.Models;
using silogiktest.Utils;
using System.Collections;
using System.Data;


namespace silogiktest.Queries
{
    public class ContactQueries : Controller
    {
        public ArrayList GetViewLabels(string lang)
        {
            ArrayList arrayList = new ArrayList();
            try
            {
                SqlParameter[] parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter("@lang", lang);

                arrayList = new Utilidades().ConvertirAObjeto(new DBQuery().ExecuteQuery("Contact_GetViewLabels", parametro));

                return arrayList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ArrayList GetViewRulesMessages(string lang)
        {
            ArrayList arrayList = new ArrayList();
            try
            {
                SqlParameter[] parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter("@lang", lang);

                arrayList = new Utilidades().ConvertirAObjeto(new DBQuery().ExecuteQuery("Contact_GetViewRulesMessages", parametro));

                return arrayList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void SendContactForm(ContactFormInput form,byte[]? bytes)
        {
            ArrayList arrayList = new ArrayList();
            try
            {
                SqlParameter[] parametro = new SqlParameter[5];
                parametro[0] = new SqlParameter("@ctEmail", form.ContactEmail);
                parametro[1] = new SqlParameter("@ctNombres", form.ContactName);
                parametro[2] = new SqlParameter("@ctApellidos", form.ContactLastNames);
                parametro[3] = new SqlParameter("@ctComentarios", form.ContactComments);
                parametro[4] = new SqlParameter("@ctAttachFile", bytes);

                new DBQuery().ExecuteNoQuery("Contact_SendContactForm", parametro);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public String ObtenerResponseMessage(int rmId,string lang)
        {
            try
            {
                SqlParameter[] _SqlParameter = new SqlParameter[2];
                _SqlParameter[0] = new SqlParameter("@rmId", rmId);
                _SqlParameter[1] = new SqlParameter("@lang", lang);

                DataTable dt = new DBQuery().ExecuteQuery("Contact_ObtenerResponseMessage", _SqlParameter);

                return dt.Rows[0][0].ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
