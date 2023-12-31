USE [FLunaBiblioteca]
GO
/****** Object:  StoredProcedure [dbo].[LibroAdd]    Script Date: 16/11/2023 07:05:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[LibroAdd]'prueba','prueba','1996-09-13','Macho',99.99,NULL
    @Titulo VARCHAR(100),
    @Autor VARCHAR(50),
    @AñoPublicacion DATE,
    @Genero VARCHAR(50),
    @Precio DECIMAL(10,2),
    @Imagen VARBINARY(MAX)
AS
BEGIN
    INSERT INTO Biblioteca (Titulo, Autor, AñoPublicacion, Genero, Precio, Imagen)
    VALUES (@Titulo, @Autor, @AñoPublicacion, @Genero, @Precio, @Imagen);
END;
