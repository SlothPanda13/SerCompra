-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema Sercompra
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `Sercompra` ;

-- -----------------------------------------------------
-- Schema Sercompra
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Sercompra` DEFAULT CHARACTER SET utf8 ;
USE `Sercompra` ;

-- -----------------------------------------------------
-- Table `Sercompra`.`Documentacion`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`Documentacion` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`Documentacion` (
  `idDocumentacion` INT NOT NULL AUTO_INCREMENT,
  `RUT` VARCHAR(45) NOT NULL,
  `FotocopiaCedula` VARCHAR(45) NOT NULL COMMENT 'Fotocopia  de  la  Cédula  de  Ciudadanía  o  Extranjería  del  Representante  Legal  o Contratista ',
  `CamaraComercio` VARCHAR(45) NOT NULL COMMENT 'ertificado de cámara y comercio ',
  `EstadosFinancieros` VARCHAR(45) NOT NULL COMMENT 'Estados Financieros del último año cuando (cuando aplique)',
  `LicenciasPermisos` VARCHAR(45) NOT NULL COMMENT 'Llicencias,Decretos,    Resoluciones,    Acuerdos, acreditaciones                                                                                                               o permisos deautoridades de control (según aplique).',
  `OtrosDocumentos` VARCHAR(45) NOT NULL COMMENT 'OTROS  DOCUMENTOS  REQUERIDOS  SEGÚN  ACTIVIDAD  ECONÓMICA    DE  LA EMPRESA',
  PRIMARY KEY (`idDocumentacion`),
  UNIQUE INDEX `idDocumentacion_UNIQUE` (`idDocumentacion` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Sercompra`.`Usuario`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`Usuario` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`Usuario` (
  `idUsuario` INT NOT NULL AUTO_INCREMENT,
  `Rol` VARCHAR(15) NOT NULL,
  `Email` VARCHAR(45) NOT NULL,
  `Contraseña` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idUsuario`),
  UNIQUE INDEX `idUsuario_UNIQUE` (`idUsuario` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Sercompra`.`Proveedor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`Proveedor` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`Proveedor` (
  `idProveedor` INT NOT NULL AUTO_INCREMENT COMMENT 'Numero para identificar al proveedor',
  `NombreProveedor` VARCHAR(40) NOT NULL COMMENT 'NOMBRE DE LA FIRMA CONTRATISTA O PROVEEDOR',
  `Telefono` VARCHAR(15) NOT NULL COMMENT 'Telefono de contacto del proveedor',
  `Direccion` VARCHAR(45) NOT NULL COMMENT 'Direccion donde encontrar al proveedor',
  `NombreRepresentante` VARCHAR(20) NOT NULL COMMENT 'NOMBRE DEL REPRESENTANTE LEGAL CONTRATISTA O PROVEEDOR',
  `CiudadMunicipio` VARCHAR(20) NOT NULL COMMENT 'Ciudad o municipio del proveedor',
  `Notificaciones` MEDIUMTEXT NOT NULL COMMENT 'Notificaciones que le llegan al proveedor',
  `Documentacion_idDocumentacion` INT NOT NULL,
  `Usuario_idUsuario` INT NOT NULL,
  PRIMARY KEY (`idProveedor`, `Documentacion_idDocumentacion`, `Usuario_idUsuario`),
  INDEX `fk_Proveedor_Documentacion1_idx` (`Documentacion_idDocumentacion` ASC) VISIBLE,
  UNIQUE INDEX `idProveedor_UNIQUE` (`idProveedor` ASC) VISIBLE,
  INDEX `fk_Proveedor_Usuario1_idx` (`Usuario_idUsuario` ASC) VISIBLE,
  CONSTRAINT `fk_Proveedor_Documentacion1`
    FOREIGN KEY (`Documentacion_idDocumentacion`)
    REFERENCES `Sercompra`.`Documentacion` (`idDocumentacion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Proveedor_Usuario1`
    FOREIGN KEY (`Usuario_idUsuario`)
    REFERENCES `Sercompra`.`Usuario` (`idUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Sercompra`.`Funcionario_Area_Compras`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`Funcionario_Area_Compras` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`Funcionario_Area_Compras` (
  `idFuncionario_Area_Compras` INT NOT NULL AUTO_INCREMENT,
  `Nombre_Trabajador` VARCHAR(45) NOT NULL,
  `Cargo` VARCHAR(10) NOT NULL,
  `Notificaciones` MEDIUMTEXT NOT NULL,
  `Usuario_idUsuario` INT NOT NULL,
  PRIMARY KEY (`idFuncionario_Area_Compras`, `Usuario_idUsuario`),
  UNIQUE INDEX `idFuncionario_Area_Compras_UNIQUE` (`idFuncionario_Area_Compras` ASC) VISIBLE,
  INDEX `fk_Funcionario_Area_Compras_Usuario1_idx` (`Usuario_idUsuario` ASC) VISIBLE,
  CONSTRAINT `fk_Funcionario_Area_Compras_Usuario1`
    FOREIGN KEY (`Usuario_idUsuario`)
    REFERENCES `Sercompra`.`Usuario` (`idUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Sercompra`.`SolicitudProveedor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`SolicitudProveedor` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`SolicitudProveedor` (
  `idSolicitudProveedor` INT NOT NULL AUTO_INCREMENT COMMENT 'Id de la solicitud',
  `Estado` VARCHAR(10) NOT NULL COMMENT 'Solicitud aceptada, rechazada, en espera',
  `Funcionario_Area_Compras_idFuncionario_Area_Compras` INT NOT NULL,
  `Proveedor_idProveedor` INT NOT NULL,
  `Proveedor_Documentacion_idDocumentacion` INT NOT NULL,
  PRIMARY KEY (`idSolicitudProveedor`, `Funcionario_Area_Compras_idFuncionario_Area_Compras`, `Proveedor_idProveedor`, `Proveedor_Documentacion_idDocumentacion`),
  INDEX `fk_SolicitudProveedor_Funcionario_Area_Compras1_idx` (`Funcionario_Area_Compras_idFuncionario_Area_Compras` ASC) VISIBLE,
  INDEX `fk_SolicitudProveedor_Proveedor1_idx` (`Proveedor_idProveedor` ASC, `Proveedor_Documentacion_idDocumentacion` ASC) VISIBLE,
  UNIQUE INDEX `idSolicitudProveedor_UNIQUE` (`idSolicitudProveedor` ASC) VISIBLE,
  CONSTRAINT `fk_SolicitudProveedor_Funcionario_Area_Compras1`
    FOREIGN KEY (`Funcionario_Area_Compras_idFuncionario_Area_Compras`)
    REFERENCES `Sercompra`.`Funcionario_Area_Compras` (`idFuncionario_Area_Compras`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_SolicitudProveedor_Proveedor1`
    FOREIGN KEY (`Proveedor_idProveedor` , `Proveedor_Documentacion_idDocumentacion`)
    REFERENCES `Sercompra`.`Proveedor` (`idProveedor` , `Documentacion_idDocumentacion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Sercompra`.`Compra`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`Compra` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`Compra` (
  `idCompra` INT NOT NULL AUTO_INCREMENT,
  `TotalPrecio` INT NOT NULL,
  `Calificacion` TINYINT(2) NOT NULL,
  `Funcionario_Area_Compras_idFuncionario_Area_Compras` INT NOT NULL,
  PRIMARY KEY (`idCompra`, `Funcionario_Area_Compras_idFuncionario_Area_Compras`),
  INDEX `fk_Compra_Funcionario_Area_Compras1_idx` (`Funcionario_Area_Compras_idFuncionario_Area_Compras` ASC) VISIBLE,
  UNIQUE INDEX `idCompra_UNIQUE` (`idCompra` ASC) VISIBLE,
  CONSTRAINT `fk_Compra_Funcionario_Area_Compras1`
    FOREIGN KEY (`Funcionario_Area_Compras_idFuncionario_Area_Compras`)
    REFERENCES `Sercompra`.`Funcionario_Area_Compras` (`idFuncionario_Area_Compras`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Sercompra`.`Cotizacion`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`Cotizacion` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`Cotizacion` (
  `idCotizacion` INT NOT NULL AUTO_INCREMENT,
  `Precio` INT NOT NULL,
  `Estado` VARCHAR(10) NOT NULL,
  `Fecha` DATE NOT NULL,
  PRIMARY KEY (`idCotizacion`),
  UNIQUE INDEX `idCotizacion_UNIQUE` (`idCotizacion` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Sercompra`.`BienServicio`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`BienServicio` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`BienServicio` (
  `idBienServicio` INT NOT NULL AUTO_INCREMENT,
  `Categoria` VARCHAR(15) NOT NULL,
  `Descripcion` MEDIUMTEXT NOT NULL,
  `Proveedor_idProveedor` INT NOT NULL,
  PRIMARY KEY (`idBienServicio`, `Proveedor_idProveedor`),
  INDEX `fk_BienServicio_Proveedor1_idx` (`Proveedor_idProveedor` ASC) VISIBLE,
  UNIQUE INDEX `idBienServicio_UNIQUE` (`idBienServicio` ASC) VISIBLE,
  CONSTRAINT `fk_BienServicio_Proveedor1`
    FOREIGN KEY (`Proveedor_idProveedor`)
    REFERENCES `Sercompra`.`Proveedor` (`idProveedor`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Sercompra`.`Cotizacion-BienServicio`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`Cotizacion-BienServicio` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`Cotizacion-BienServicio` (
  `idCotizacion-BienServicio` INT NOT NULL AUTO_INCREMENT,
  `BienServicio_idBienServicio` INT NOT NULL,
  `BienServicio_Proveedor_idProveedor` INT NOT NULL,
  `Cotizacion_idCotizacion` INT NOT NULL,
  PRIMARY KEY (`idCotizacion-BienServicio`, `BienServicio_idBienServicio`, `BienServicio_Proveedor_idProveedor`, `Cotizacion_idCotizacion`),
  INDEX `fk_Cotizacion-BienServicio_BienServicio1_idx` (`BienServicio_idBienServicio` ASC, `BienServicio_Proveedor_idProveedor` ASC) VISIBLE,
  INDEX `fk_Cotizacion-BienServicio_Cotizacion1_idx` (`Cotizacion_idCotizacion` ASC) VISIBLE,
  UNIQUE INDEX `idCotizacion-BienServicio_UNIQUE` (`idCotizacion-BienServicio` ASC) VISIBLE,
  CONSTRAINT `fk_Cotizacion-BienServicio_BienServicio1`
    FOREIGN KEY (`BienServicio_idBienServicio` , `BienServicio_Proveedor_idProveedor`)
    REFERENCES `Sercompra`.`BienServicio` (`idBienServicio` , `Proveedor_idProveedor`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Cotizacion-BienServicio_Cotizacion1`
    FOREIGN KEY (`Cotizacion_idCotizacion`)
    REFERENCES `Sercompra`.`Cotizacion` (`idCotizacion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Sercompra`.`BienServico-Compra`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`BienServico-Compra` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`BienServico-Compra` (
  `idBienServico-Compra` INT NOT NULL AUTO_INCREMENT,
  `Compra_idCompra` INT NOT NULL,
  `Compra_Funcionario_Area_Compras_idFuncionario_Area_Compras` INT NOT NULL,
  `BienServicio_idBienServicio` INT NOT NULL,
  `BienServicio_Proveedor_idProveedor` INT NOT NULL,
  PRIMARY KEY (`idBienServico-Compra`, `Compra_idCompra`, `Compra_Funcionario_Area_Compras_idFuncionario_Area_Compras`, `BienServicio_idBienServicio`, `BienServicio_Proveedor_idProveedor`),
  INDEX `fk_BienServico-Compra_Compra1_idx` (`Compra_idCompra` ASC, `Compra_Funcionario_Area_Compras_idFuncionario_Area_Compras` ASC) VISIBLE,
  INDEX `fk_BienServico-Compra_BienServicio1_idx` (`BienServicio_idBienServicio` ASC, `BienServicio_Proveedor_idProveedor` ASC) VISIBLE,
  UNIQUE INDEX `idBienServico-Compra_UNIQUE` (`idBienServico-Compra` ASC) VISIBLE,
  CONSTRAINT `fk_BienServico-Compra_Compra1`
    FOREIGN KEY (`Compra_idCompra` , `Compra_Funcionario_Area_Compras_idFuncionario_Area_Compras`)
    REFERENCES `Sercompra`.`Compra` (`idCompra` , `Funcionario_Area_Compras_idFuncionario_Area_Compras`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_BienServico-Compra_BienServicio1`
    FOREIGN KEY (`BienServicio_idBienServicio` , `BienServicio_Proveedor_idProveedor`)
    REFERENCES `Sercompra`.`BienServicio` (`idBienServicio` , `Proveedor_idProveedor`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Sercompra`.`Administrador`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Sercompra`.`Administrador` ;

CREATE TABLE IF NOT EXISTS `Sercompra`.`Administrador` (
  `idAdministrador` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NOT NULL,
  `Notificaciones` MEDIUMTEXT NOT NULL,
  `Usuario_idUsuario` INT NOT NULL,
  PRIMARY KEY (`idAdministrador`, `Usuario_idUsuario`),
  UNIQUE INDEX `idAdministrador_UNIQUE` (`idAdministrador` ASC) VISIBLE,
  INDEX `fk_Administrador_Usuario1_idx` (`Usuario_idUsuario` ASC) VISIBLE,
  CONSTRAINT `fk_Administrador_Usuario1`
    FOREIGN KEY (`Usuario_idUsuario`)
    REFERENCES `Sercompra`.`Usuario` (`idUsuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `Sercompra`.`Usuario`
-- -----------------------------------------------------
START TRANSACTION;
USE `Sercompra`;
INSERT INTO `Sercompra`.`Usuario` (`idUsuario`, `Rol`, `Email`, `Contraseña`) VALUES (1, 'Administrador', DEFAULT, DEFAULT);
INSERT INTO `Sercompra`.`Usuario` (`idUsuario`, `Rol`, `Email`, `Contraseña`) VALUES (2, 'Funcionario', DEFAULT, DEFAULT);
INSERT INTO `Sercompra`.`Usuario` (`idUsuario`, `Rol`, `Email`, `Contraseña`) VALUES (3, 'Proveeor', DEFAULT, DEFAULT);

COMMIT;


-- -----------------------------------------------------
-- Data for table `Sercompra`.`Administrador`
-- -----------------------------------------------------
START TRANSACTION;
USE `Sercompra`;
INSERT INTO `Sercompra`.`Administrador` (`idAdministrador`, `Nombre`, `Notificaciones`, `Usuario_idUsuario`) VALUES (00001, 'Julian Lara', DEFAULT, DEFAULT);

COMMIT;

