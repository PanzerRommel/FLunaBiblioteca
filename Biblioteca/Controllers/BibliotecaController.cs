using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
 // Ajusta el espacio de nombres según tu estructura de carpetas

namespace Biblioteca.Controllers
{
    public class BibliotecaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Biblioteca biblioteca = new ML.Biblioteca(); // Ajusta la instancia según el espacio de nombres correcto
            var result = BL.Libro.GetAll();

            if (result.Correct)
            {
                biblioteca.Libros = result.Objects;
            }

            return View(biblioteca);
        }

        [HttpGet]
        public ActionResult Form(int? IdLibro)
        {
            ML.Biblioteca biblioteca = new ML.Biblioteca(); // Ajusta la instancia según el espacio de nombres correcto

            if (IdLibro != null)
            {
                var result = BL.Libro.GetById(IdLibro.Value);
                if (result.Correct)
                {
                    biblioteca = (ML.Biblioteca)result.Object;
                }
            }

            return View(biblioteca);
        }

        [HttpPost]
        public ActionResult Form(ML.Biblioteca biblioteca, HttpPostedFileBase imagenFile)
        {
            try
            {
                if (imagenFile != null && imagenFile.ContentLength > 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(imagenFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(imagenFile.ContentLength);
                    }
                    biblioteca.Imagen = imageData;
                }

                if (biblioteca.IdLibro == 0)
                {
                    var result = BL.Libro.Add(biblioteca);

                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Registro exitoso";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrió un problema al agregar el registro: " + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
                else
                {
                    var result = BL.Libro.Update(biblioteca);

                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Actualización exitosa";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrió un problema al actualizar el registro: " + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
            }
            catch (Exception ex)
            {
                // Loguea o maneja el error según tus necesidades
                ViewBag.Mensaje = "Ocurrió un problema: " + ex.Message;
            }

            // Asegúrate de devolver la vista con el modelo actualizado
            return View(biblioteca);
        }

        [HttpGet]
        public ActionResult Delete(int idLibro)
        {
            var result = BL.Libro.Delete(idLibro);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro eliminado exitosamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrió un problema al eliminar el registro";
            }

            return PartialView("Modal");
        }
    }
}
