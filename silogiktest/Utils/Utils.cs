using Newtonsoft.Json;
using System.Collections;
using System.Data;

namespace silogiktest.Utils
{
    public class Utilidades
    {
        public ArrayList ConvertirAObjeto(DataTable data)
        {
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    dic.Add(data.Columns[j].ColumnName, data.Rows[i].ItemArray[j]);
                }
                arrayList.Add(dic);
            }
            return arrayList;
        }
    }
}
