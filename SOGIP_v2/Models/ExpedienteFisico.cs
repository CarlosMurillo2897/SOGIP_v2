using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace SOGIP_v2.Models
{
    public class ExpedienteFisico
    {
        public int ExpedienteFisicoId { get; set; }
        public byte[] InBody { get; set; }
        public byte[] PruebaFuerza { get; set; }
        public Atleta Atleta { get; set; }

        public static bool GuardarArchivo(string archivo)
        {
            // Leemos todos los bytes del archivo y luego lo guardamos como Base64 en un string.
            string resultado = Convert.ToBase64String(File.ReadAllBytes(archivo));

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString))
            {
                using (var cmd = cn.CreateCommand())
                {
                    // Se invoca a un StoreProcedure para insertar el registro.
                    cmd.CommandText = "usp_InsertArchivo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Con la ayuda de FileInfo devolvemos unicamente el nombre del archivo.
                    cmd.Parameters.AddWithValue("@nombreArchivo", new FileInfo(archivo).Name);
                    // El resultado serializado del archivo pasa como un varchar cualquiera.
                    cmd.Parameters.AddWithValue("@contenido", resultado);

                    cn.Open();

                    int cantidad = cmd.ExecuteNonQuery();

                    // Si es mayor a 0 entonces se guardo correctamente.
                    return (cantidad > 0);
                }
            }
        }

        /*
        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection("CONNECTION STRING"))
            {
                cn.Open();
                FileStream fStream = File.OpenRead("C:\\MyFiles\\TempReport.pdf");
                byte[] contents = new byte[fStream.Length];
                fStream.Read(contents, 0, (int)fStream.Length);
                fStream.Close();
                using (SqlCommand cmd = new SqlCommand("insert into SavePDFTable " + "(PDFFile)values(@data)", cn))
                {
                    cmd.Parameters.Add("@data", contents);
                    cmd.ExecuteNonQuery();
                    Response.Write("Pdf File Save in Dab");
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string ToSaveFileTo = Server.MapPath("~\\File\\Report.pdf");
            using (SqlConnection cn = new SqlConnection("CONNECTION STRING"))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("select PDFFile from SavePDFTable  where ID='" + "1" + "' ", cn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.Default))
                    {
                        if (dr.Read())
                        {
                            byte[] fileData = (byte[])dr.GetValue(0);
                            using (System.IO.FileStream fs = new System.IO.FileStream(ToSaveFileTo, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
                            {
                                using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs))
                                {
                                    bw.Write(fileData);
                                    bw.Close();
                                }
                            }
                        }
                        dr.Close();
                        Response.Redirect("~\\File\\Report.pdf");
                    }
                }
            }
        }
        */
    }
}