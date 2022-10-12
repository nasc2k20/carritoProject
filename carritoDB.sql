CREATE DATABASE carritoDb;

USE carritoDb;

CREATE TABLE usuario (
    codUsuario INT NOT NULL PRIMARY KEY IDENTITY(1,1) ,
    loginUsuario VARCHAR (50) NOT NULL,
    loginPassword VARCHAR(100) NOT NULL,
    nombreCompleto VARCHAR(150) NOT NULL
);

CREATE TABLE categoria (
    codCategoria INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    nombreCategoria VARCHAR(50) NOT NULL
);

CREATE TABLE fabricante (
    codFabricante INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    nombreFabricante VARCHAR(50) NOT NULL
);
--DROP TABLE producto
CREATE TABLE producto (
    codProducto INT NOT NULL PRIMARY KEY IDENTITY(1,1) ,
    nombreProducto VARCHAR(100) NOT NULL,
    descripcionProd VARCHAR(200) NOT NULL,
    precioProd DECIMAL(12,2) NOT NULL,
    marcaProd VARCHAR(25) NOT NULL,
    unidadesProd INT NOT NULL,
	--fotoProducto VARBINARY,
	fotoProducto VARCHAR(250),
    codCategoria INT NOT NULL,
	codFabricante INT NOT NULL
);

CREATE TABLE pedido (
    codPedido INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    fechaPedido DATE NOT NULL,
    datePedido DATETIME NOT NULL,
    estadoPedido INT NOT NULL,
	loginUsuario VARCHAR (25) NOT NULL,
	codCliente INT NOT NULL,
    codTipoPago INT NOT NULL,
    codDomicilio INT NOT NULL,
	totalPedido DECIMAL(12,2) NOT NULL
);

CREATE TABLE pedido_detalle(
	codPedidoDet INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	cantProd INT NOT NULL,
	precioProd DECIMAL(12,2) NOT NULL,
	subTotalPD DECIMAL (12,2) NOT NULL,
	codProducto INT NOT NULL,
	codPedido INT NOT NULL
);

CREATE TABLE carrito (
    codCarrito INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    codCliente INT NOT NULL,
    codProducto INT NOT NULL,
    cantidadCarrito INT NOT NULL,
    precioProd DECIMAL(12,2) NOT NULL,
    subTotalCarrito DECIMAL(12,2) NOT NULL
);

CREATE TABLE cliente (
	codCliente INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	nombreCliente VARCHAR(50) NOT NULL,
	apellidoCliente VARCHAR(50) NOT NULL,
	duiCliente VARCHAR(10) NOT NULL,
	loginUsuario VARCHAR (50) NOT NULL,
	codUsuario INT NOT NULL,
);

CREATE TABLE domicilio (
    codDomicilio INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    calleDom VARCHAR(100) NOT NULL,
    estadoDom VARCHAR(100) NOT NULL,
    ciudadDom VARCHAR(100) NOT NULL,
    telefonoDom VARCHAR(100) NOT NULL,
    paisDom VARCHAR(100) NOT NULL,
    codCliente INT NOT NULL
);

CREATE TABLE tipo_pago (
    codTipoPago INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    nombreTipoPago VARCHAR(100) NOT NULL
);

/*ALTER TABLE producto DROP CONSTRAINT fk_productoCat;
ALTER TABLE producto DROP CONSTRAINT fk_productoFabr;
ALTER TABLE carrito DROP CONSTRAINT fk_carrProd;
ALTER TABLE pedido_detalle DROP CONSTRAINT fk_pedidoDetProd;
DROP TABLE producto
ALTER TABLE producto DROP COLUMN fotoProducto;
ALTER TABLE producto ALTER COLUMN fotoProducto VARCHAR(250);*/
ALTER TABLE producto ADD CONSTRAINT fk_productoCat FOREIGN KEY (codCategoria) REFERENCES categoria (codCategoria);
ALTER TABLE producto ADD CONSTRAINT fk_productoFabr FOREIGN KEY (codFabricante) REFERENCES fabricante (codFabricante);
ALTER TABLE pedido ADD CONSTRAINT fk_pedidoClie FOREIGN KEY (codCliente) REFERENCES cliente (codCliente);
ALTER TABLE pedido ADD CONSTRAINT fk_pedidoTpago FOREIGN KEY (codTipoPago) REFERENCES tipo_pago (codTipoPago);
ALTER TABLE pedido ADD CONSTRAINT fk_pedidoDomi FOREIGN KEY (codDomicilio) REFERENCES domicilio (codDomicilio);
ALTER TABLE pedido_detalle ADD CONSTRAINT fk_pedidoPeDet FOREIGN KEY (codPedido) REFERENCES pedido (codPedido);
ALTER TABLE cliente ADD CONSTRAINT fk_clienteUsu FOREIGN KEY (codUsuario) REFERENCES usuario (codUsuario);
ALTER TABLE pedido_detalle ADD CONSTRAINT fk_pedidoDetProd FOREIGN KEY (codProducto) REFERENCES producto (codProducto);
ALTER TABLE domicilio ADD CONSTRAINT fk_domCliente FOREIGN KEY (codCliente) REFERENCES cliente (codCliente);
ALTER TABLE carrito ADD CONSTRAINT fk_carrCliente FOREIGN KEY (codCliente) REFERENCES cliente (codCliente);
ALTER TABLE carrito ADD CONSTRAINT fk_carrProd FOREIGN KEY (codProducto) REFERENCES producto (codProducto);


/*PROCEDIMIENTOS ALMACENADOS PARA USUARIOS*/
CREATE PROCEDURE pa_UsuarioLogin
	@loginUsuario VARCHAR (50),
    @loginPassword VARCHAR(100)
AS
BEGIN
	SELECT * FROM usuario WHERE loginUsuario=@loginUsuario AND loginPassword=@loginPassword
END

/*PROCEDIMIENTOS ALMACENADOS DE CATEGORIAS*/
CREATE PROCEDURE pa_CategoriaMostrar
AS
BEGIN
	SELECT * FROM categoria ORDER BY nombreCategoria
END

CREATE PROCEDURE pa_CategoriaMostrarDato
	@codCategoria INT
AS
BEGIN
	SELECT * FROM categoria 
	WHERE codCategoria=@codCategoria 
	ORDER BY nombreCategoria
END

CREATE PROCEDURE pa_CategoriaInsertar
	@nombreCategoria VARCHAR(50)
AS
BEGIN
	INSERT INTO categoria (nombreCategoria) VALUES (@nombreCategoria)
END

CREATE PROCEDURE pa_CategoriaActualizar
	@codCategoria INT,
	@nombreCategoria VARCHAR(50)
AS
BEGIN
	UPDATE categoria 
	SET 
	nombreCategoria=@nombreCategoria 
	WHERE codCategoria=@codCategoria
END
--DROP PROC pa_CategoriaEliminar;
CREATE PROCEDURE pa_CategoriaEliminar
	@codCategoria INT
AS
BEGIN
	IF OBJECT_ID('dbo.fk_productoCat','C') IS NOT NULL
		BEGIN
			PRINT 'No se Puede Eliminar la Categoria por Clave Foranea';
		END
	ELSE
		BEGIN
			DELETE FROM categoria WHERE codCategoria=@codCategoria;
		END
END

EXEC pa_CategoriaEliminar @codCategoria=1;
/*PROCEDIMIENTOS ALMACENADOS DE FABRICANTES*/

CREATE PROCEDURE pa_FabricanteMostrar
AS
BEGIN
	SELECT * FROM fabricante ORDER BY nombreFabricante ASC
END

CREATE PROCEDURE pa_FabricanteInsertar
	@nombreFabricante VARCHAR(50)
AS
BEGIN
	INSERT INTO fabricante(nombreFabricante) VALUES (@nombreFabricante)
END

CREATE PROCEDURE pa_FabricanteActualizar
	@codFabricante INT,
	@nombreFabricante VARCHAR(50)
AS
BEGIN
	UPDATE fabricante 
	SET 
	nombreFabricante=@nombreFabricante 
	WHERE codFabricante=@codFabricante
END

CREATE PROCEDURE pa_FabricanteEliminar
	@codFabricante INT
AS
BEGIN
	DELETE FROM fabricante WHERE codFabricante=@codFabricante
END


/*PROCEDIMIENTOS ALMACENADOS DE CLIENTES*/
CREATE PROCEDURE pa_ClienteMostrar
AS
BEGIN
	SELECT * FROM cliente a 
	INNER JOIN usuario b ON b.loginUsuario = a.loginUsuario 
	ORDER BY a.nombreCliente ASC
END
CREATE PROCEDURE pa_ClienteInsertar
	@nombreCliente VARCHAR(50),
	@apellidoCliente VARCHAR(50),
	@duiCliente VARCHAR(10),
	@loginUsuario VARCHAR (50),
    @loginPassword VARCHAR(100),
    @nombreCompleto VARCHAR(150)
AS
BEGIN
	INSERT INTO usuario (loginUsuario, loginPassword, nombreCompleto) 
	VALUES (@loginUsuario, @loginPassword, @nombreCompleto)
	INSERT INTO cliente(nombreCliente, apellidoCliente, duiCliente, loginUsuario, codUsuario) 
	VALUES (@nombreCliente, @apellidoCliente, @duiCliente, @loginUsuario, SCOPE_IDENTITY())	
END

CREATE PROCEDURE pa_ClienteActualizar
	@codCliente INT,
	@nombreCliente VARCHAR(50),
	@apellidoCliente VARCHAR(50),
	@duiCliente VARCHAR(10)
AS
BEGIN
	UPDATE cliente 
	SET 
	nombreCliente=@nombreCliente, 
	apellidoCliente=@apellidoCliente, 
	duiCliente=@duiCliente 
	WHERE codCliente=@codCliente
END

CREATE PROCEDURE pa_ClienteEliminar
	@codCliente INT,
	@loginUsuario VARCHAR (50) 
AS
BEGIN
	DELETE FROM cliente WHERE @codCliente=@codCliente
	DELETE FROM usuario WHERE loginUsuario=@loginUsuario
END


/*PROCEDIMIENTOS ALMACENADOS DE DOMICILIOS*/
CREATE PROCEDURE pa_DomicilioMostrar
	@codCliente INT
AS
BEGIN
	SELECT * FROM domicilio 
	WHERE codCliente=@codCliente
	ORDER BY ciudadDom ASC
END

CREATE PROCEDURE pa_DomicilioInsertar
	@calleDom VARCHAR(100),
    @estadoDom VARCHAR(100),
    @ciudadDom VARCHAR(100),
    @telefonoDom VARCHAR(100),
    @paisDom VARCHAR(100),
    @codCliente INT 
AS
BEGIN
	INSERT INTO domicilio (calleDom, estadoDom, ciudadDom, telefonoDom, paisDom, codCliente) 
	VALUES (@calleDom, @estadoDom, @ciudadDom, @telefonoDom, @paisDom, @codCliente)
END

CREATE PROCEDURE pa_DomicilioActualizar
	@codDomicilio INT,
	@calleDom VARCHAR(100),
    @estadoDom VARCHAR(100),
    @ciudadDom VARCHAR(100),
    @telefonoDom VARCHAR(100),
    @paisDom VARCHAR(100)
AS
BEGIN
	UPDATE domicilio 
	SET 
	calleDom=@calleDom, 
	estadoDom=@estadoDom, 
	ciudadDom=@ciudadDom, 
	telefonoDom=@telefonoDom, 
	paisDom=@paisDom
	WHERE codDomicilio=@codDomicilio
END

CREATE PROCEDURE pa_DomicilioEliminar
	@codDomicilio INT
AS
BEGIN
	DELETE FROM domicilio WHERE codDomicilio=@codDomicilio
END


/*PROCEDIMIENTOS ALMACENADOS PARA PRODUCTO*/

CREATE PROCEDURE pa_ProductoMostrar
AS
BEGIN
	SELECT * FROM producto 
	ORDER BY nombreProducto ASC
END

/*EXEC pa_ProductoMostrar
DROP PROCEDURE pa_ProductoInsertar*/
CREATE PROCEDURE pa_ProductoInsertar
	@nombreProducto VARCHAR(100),
    @descripcionProd VARCHAR(200),
    @precioProd DECIMAL(12,2),
    @marcaProd VARCHAR(25),
    @unidadesProd INT,
	--@fotoProducto VARBINARY,
	@fotoProducto VARCHAR(250),
    @codCategoria INT,
	@codFabricante INT 
AS
BEGIN
	INSERT INTO producto (nombreProducto, descripcionProd, precioProd, marcaProd, unidadesProd, fotoProducto, codCategoria, codFabricante) 
	VALUES (@nombreProducto, @descripcionProd, @precioProd, @marcaProd, @unidadesProd, @fotoProducto, @codCategoria, @codFabricante)
END

CREATE PROCEDURE pa_ProductoActualizar
	@codProducto INT,
	@nombreProducto VARCHAR(100),
    @descripcionProd VARCHAR(200),
    @precioProd DECIMAL(12,2),
    @marcaProd VARCHAR(25),
    @unidadesProd INT,
	@fotoProducto VARCHAR(100),
    @codCategoria INT,
	@codFabricante INT 
AS
BEGIN
	UPDATE producto 
	SET 
	nombreProducto=@nombreProducto, 
	descripcionProd=@descripcionProd, 
	precioProd=@precioProd, 
	marcaProd=@marcaProd,
	unidadesProd=@unidadesProd, 
	fotoProducto=@fotoProducto, 
	codCategoria=@codCategoria, 
	codFabricante=@codFabricante
	WHERE codProducto=@codProducto
END

CREATE PROCEDURE pa_ProductoEliminar
	@codProducto INT
AS
BEGIN
	DELETE FROM producto WHERE codProducto=@codProducto
END


INSERT INTO producto VALUES ('MOUSE','Some quick example text to build on the card title and make up the bulk of the cards content.','6.5','GRINGGGGGGA',10,'FOTO',1,1);
INSERT INTO producto VALUES ('LINTERNA','Use border utilities to change just the border-color of a card. Note that you can put .text-{color} classes on the parent .card or a subset of the card’s contents as shown below.','16.5','GRINGGGGGGA',10,'FOTO',1,1);
INSERT INTO producto VALUES ('MOTO','This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.','26.5','GRINGGGGGGA',10,'FOTO',1,1);
INSERT INTO producto VALUES ('CARRO','Cards include a few options for working with images. Choose from appending “image caps” at either end of a card, overlaying images with card content, or simply embedding the image in a card.','12.5','GRINGGGGGGA',10,'FOTO',1,1);
INSERT INTO producto VALUES ('COMPUTADORA','Turn an image into a card background and overlay your card’s text. Depending on the image, you may or may not need additional styles or utilities.','18.5','GRINGGGGGGA',10,'FOTO',1,1);
INSERT INTO producto VALUES ('CELULAR','Mix and match multiple content types to create the card you need, or throw everything in there. Shown below are image styles, blocks, text styles, and a list group—all wrapped in a fixed-width card.','9.75','GRINGGGGGGA',10,'FOTO',1,1);
INSERT INTO producto VALUES ('AUTO','Mix and match multiple content types to create the card you need, or throw everything in there. Shown below are image styles, blocks, text styles, and a list group—all wrapped in a fixed-width card.','12.75','GRINGGGGGGA',10,'FOTO',1,1);
INSERT INTO producto VALUES ('IMPRESORA','Cards assume no specific width to start, so they’ll be 100% wide unless otherwise stated. You can change this as needed with custom CSS, grid classes, grid Sass mixins, or utilities.','21.5','GRINGGGGGGA',10,'FOTO',1,1);
INSERT INTO producto VALUES ('TELEVISOR','Similar to headers and footers, cards can include top and bottom “image caps”—images at the top or bottom of a card.','40.5','GRINGGGGGGA',10,'FOTO',1,1);