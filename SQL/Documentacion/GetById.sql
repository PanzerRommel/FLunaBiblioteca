CREATE PROCEDURE LibroGetById
	@IdLibro INT
AS
SELECT
	IdLibro,
	Titulo,
	Autor,
	A�oPublicacion,
	Genero,
	Precio,
	Imagen
FROM Biblioteca
WHERE IdLibro = @IdLibro