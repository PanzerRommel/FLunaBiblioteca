using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class Libro
    {
        public static ML.Result Add(ML.Biblioteca biblioteca)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "LibroAdd";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros del procedimiento almacenado
                        cmd.Parameters.AddWithValue("@Titulo", biblioteca.Titulo);
                        cmd.Parameters.AddWithValue("@Autor", biblioteca.Autor);
                        cmd.Parameters.AddWithValue("@AñoPublicacion", biblioteca.AñoPublicacion);
                        cmd.Parameters.AddWithValue("@Genero", biblioteca.Genero);
                        cmd.Parameters.AddWithValue("@Precio", biblioteca.Precio);

                        // Asegúrate de manejar la imagen adecuadamente, asumiendo que biblioteca.Imagen es un byte[]
                        SqlParameter imagenParam = new SqlParameter("@Imagen", SqlDbType.VarBinary);
                        imagenParam.Value = (object)biblioteca.Imagen ?? DBNull.Value;
                        cmd.Parameters.Add(imagenParam);

                        context.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo agregar el registro.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false; 
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Update(ML.Biblioteca biblioteca)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "LibroUpdate";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros del procedimiento almacenado
                        cmd.Parameters.AddWithValue("@IdLibro", biblioteca.IdLibro);
                        cmd.Parameters.AddWithValue("@Titulo", biblioteca.Titulo);
                        cmd.Parameters.AddWithValue("@Autor", biblioteca.Autor);
                        cmd.Parameters.AddWithValue("@AñoPublicacion", biblioteca.AñoPublicacion);
                        cmd.Parameters.AddWithValue("@Genero", biblioteca.Genero);
                        cmd.Parameters.AddWithValue("@Precio", biblioteca.Precio);

                        // Asegurarme de manejar la imagen adecuadamente, asumiendo que biblioteca.Imagen es un byte[]
                        SqlParameter imagenParam = new SqlParameter("@Imagen", SqlDbType.VarBinary);
                        imagenParam.Value = (object)biblioteca.Imagen ?? DBNull.Value;
                        cmd.Parameters.Add(imagenParam);

                        context.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo actualizar el registro. Puede ser que el ID no exista.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Delete(int idLibro)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "LibroDelete";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdLibro", idLibro);

                        context.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo eliminar el registro. Puede ser que el ID no exista.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "LibroGetAll";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable tablebiblioteca = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(tablebiblioteca);
                        }

                        if (tablebiblioteca.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tablebiblioteca.Rows)
                            {
                                ML.Biblioteca biblioteca = new ML.Biblioteca();

                                biblioteca.IdLibro = Convert.ToInt32(row["IdLibro"]);
                                biblioteca.Titulo = row["Titulo"].ToString();
                                biblioteca.Autor = row["Autor"].ToString();
                                biblioteca.AñoPublicacion = Convert.ToDateTime(row["AñoPublicacion"]);
                                biblioteca.Genero = row["Genero"].ToString();
                                biblioteca.Precio = Convert.ToDecimal(row["Precio"]);

                                if (row["Imagen"] != DBNull.Value)
                                {
                                    // Verifica si el campo de la imagen no es nulo antes de intentar convertirlo
                                    byte[] bytes = (byte[])row["Imagen"];
                                    biblioteca.Imagen = bytes;
                                }

                                result.Objects.Add(biblioteca);
                            }

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.ToString(); // Cambiado a ex.ToString() para obtener más detalles.
            }

            return result;
        }
        public static ML.Result GetById(int idLibro)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "LibroGetById";
                    using (SqlCommand cmd = new SqlCommand(query, context))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdLibro", idLibro);

                        context.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            result.Objects = new List<object>();

                            while (reader.Read())
                            {
                                ML.Biblioteca biblioteca = new ML.Biblioteca();

                                biblioteca.IdLibro = Convert.ToInt32(reader["IdLibro"]);
                                biblioteca.Titulo = reader["Titulo"].ToString();
                                biblioteca.Autor = reader["Autor"].ToString();
                                biblioteca.AñoPublicacion = Convert.ToDateTime(reader["AñoPublicacion"]);
                                biblioteca.Genero = reader["Genero"].ToString();
                                biblioteca.Precio = Convert.ToDecimal(reader["Precio"]);

                                if (reader["Imagen"] != DBNull.Value)
                                {
                                    byte[] bytes = (byte[])reader["Imagen"];
                                    biblioteca.Imagen = bytes;
                                }

                                result.Objects.Add(biblioteca);
                            }

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se encontró el registro con el ID proporcionado.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

    }
}
