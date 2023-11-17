CREATE PROCEDURE LibroDelete
    @IdLibro INT
AS
BEGIN
    DELETE FROM Biblioteca WHERE IdLibro = @IdLibro;
END;
