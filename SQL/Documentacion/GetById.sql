CREATE PROCEDURE LibroGetById
	@IdLibro INT
AS
SELECT
	IdLibro,
	Titulo,
	Autor,
	AņoPublicacion,
	Genero,
	Precio,
	Imagen
FROM Biblioteca
WHERE IdLibro = @IdLibro