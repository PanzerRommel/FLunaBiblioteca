USE [FLunaBiblioteca]
GO
/****** Object:  StoredProcedure [dbo].[LibroUpdate]    Script Date: 16/11/2023 07:18:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[LibroUpdate]
   @IdLibro INT,
    @Titulo VARCHAR(100),
    @Autor VARCHAR(50),
    @AñoPublicacion DATE,
    @Genero VARCHAR(50),
    @Precio DECIMAL(10,2),
    @Imagen VARBINARY(MAX)
AS
BEGIN
    UPDATE Biblioteca
    SET
        Titulo = @Titulo,
        Autor = @Autor,
        AñoPublicacion = @AñoPublicacion,
        Genero = @Genero,
        Precio = @Precio,
        Imagen = @Imagen
    WHERE IdLibro = @IdLibro;
END;