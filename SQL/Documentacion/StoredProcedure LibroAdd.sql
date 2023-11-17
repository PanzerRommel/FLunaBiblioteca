CREATE TABLE Biblioteca(
IdLibro INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
Titulo VARCHAR(100) NOT NULL,
Autor VARCHAR(50) NOT NULL,
AñoPublicacion DATE NULL,
Genero VARCHAR(50) NOT NULL,
Precio DECIMAL(10,2) NOT NULL,
Imagen VARBINARY(MAX) NULL
)

-- INSERTAR REGISTROS

INSERT INTO Biblioteca (Titulo, Autor, AñoPublicacion, Genero, Precio, Imagen)
VALUES
('Las grandes batallas del siglo XX', 'Autor1', '2005-01-01', 'Historia', 19.99, NULL),
('El código Da Vinci', 'Dan Brown', '2003-03-18', 'Misterio', 29.99, NULL),
('La conspiración', 'Autor2', '2010-08-15', 'Thriller', 24.99, NULL),
('La Materia Oscura', 'Philip Pullman', '1995-07-01', 'Fantasía', 34.99, NULL),
('Cosmos', 'Carl Sagan', '1980-09-28', 'Ciencia', 14.99, NULL),
('Miles de Millones', 'Autor3', '2018-05-10', 'Divulgación científica', 39.99, NULL),
('La teoría del todo', 'Jane Hawking', '2007-05-01', 'Biografía', 28.99, NULL),
('El gran diseño', 'Stephen Hawking', '2010-09-09', 'Ciencia', 32.99, NULL);


-- CREACION DE LOS PROCEDIMIENTOS ALMACENADOS--

--PROCEDIMIENTO ALMACENADO AGREGAR--

CREATE PROCEDURE LibroAdd
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

--PROCEDIMIENTO ALMACENADO GETALL--

CREATE PROCEDURE LibroGetAll
AS
SELECT

IdLibro,
Titulo,
AñoPublicacion,
Genero,
Precio,
Imagen

FROM Biblioteca