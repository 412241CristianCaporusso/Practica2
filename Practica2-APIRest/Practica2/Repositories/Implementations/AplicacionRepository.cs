using Practica2.Models;
using Practica2.Repositories.Contracts;
using Practica2.Utilities;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization.Json;

namespace Practica2.Repositories.Implementations
{
    public class AplicacionRepository : IAplicacion
    {
       

        public bool Add(Article article)
        {
            bool result = true;
            SqlConnection cnn = DataHelper.GetInstance().GetConnection();
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();

            var cmd = new SqlCommand("Crear_Articulo", cnn, t);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_articulo", article.id_article);
            cmd.Parameters.AddWithValue("@nombre", article.Name);
            cmd.Parameters.AddWithValue("@precio_unitario", article.UnitPrice);

            cmd.ExecuteNonQuery();

            t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                    t.Rollback();

                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }

        
            return result;
        }

        public bool Delete(int id)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@id_articulo", id));
            int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("Eliminar_Articulo", parameters);

            return filasAfectadas > 0;
        }

        public bool Edit(Article article)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@id_articulo", article.id_article));
            parameters.Add(new ParameterSQL("@nombre", article.Name));
            parameters.Add(new ParameterSQL("@precio_unitario", article.UnitPrice));


            int filasAfectadas = DataHelper.GetInstance().ExecuteSPDML("Actualizar_articulo",parameters);

            return filasAfectadas > 0;

        }

        public List<Article> GetAll()
        {
            var articles = new List<Article>();
            DataTable table = DataHelper.GetInstance().ExecuteSPQuery("Obtener_articulos", null);

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    var article = new Article
                    {
                        id_article = Convert.ToInt32(row["id_articulo"]),
                        Name = row["nombre"].ToString(),
                        UnitPrice = Convert.ToInt32(row["precio_unitario"])

                    };
                    articles.Add(article);
                }
            }
            return articles;
        }
    }
}
