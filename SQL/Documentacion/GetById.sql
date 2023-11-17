CREATE PROCEDURE LibroGetById
	@IdLibro INT
AS
SELECT
	IdLibro,
	Titulo,
	Autor,
	AñoPublicacion,
	Genero,
	Precio,
	Imagen
FROM Biblioteca
WHERE IdLibro = @IdLibro