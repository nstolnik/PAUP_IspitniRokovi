CREATE DATABASE  IF NOT EXISTS `ir_db` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `ir_db`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: ir_db
-- ------------------------------------------------------
-- Server version	5.7.21-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ispiti`
--

DROP TABLE IF EXISTS `ispiti`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ispiti` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `kolegij_id` int(11) NOT NULL,
  `datum` datetime NOT NULL,
  `ak_godina` varchar(9) COLLATE cp1250_croatian_ci NOT NULL,
  `tip` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=cp1250 COLLATE=cp1250_croatian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ispiti`
--

LOCK TABLES `ispiti` WRITE;
/*!40000 ALTER TABLE `ispiti` DISABLE KEYS */;
INSERT INTO `ispiti` VALUES (1,1,'2018-02-01 09:30:00','2016/2017',0),(3,1,'2018-02-02 10:24:00','2017/2018',0),(5,1,'2018-01-09 16:55:00','2017/2018',1),(7,4,'2018-02-13 16:00:00','2017/2018',0),(9,1,'2018-01-31 14:36:00','2017/2018',1),(10,6,'2018-02-01 15:00:00','2017/2018',0),(11,4,'2018-03-14 14:50:00','2017/2018',1),(14,8,'2018-02-01 15:00:00','2017/2018',0),(15,4,'2018-01-30 22:38:00','2017/2018',0);
/*!40000 ALTER TABLE `ispiti` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `kolegiji`
--

DROP TABLE IF EXISTS `kolegiji`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `kolegiji` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ime` varchar(50) COLLATE cp1250_croatian_ci NOT NULL,
  `korisnik_id` int(11) NOT NULL,
  `studij_id` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=cp1250 COLLATE=cp1250_croatian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kolegiji`
--

LOCK TABLES `kolegiji` WRITE;
/*!40000 ALTER TABLE `kolegiji` DISABLE KEYS */;
INSERT INTO `kolegiji` VALUES (1,'PAUP',9,1),(4,'Vjerojatnost i statistika',3,1),(6,'Tesnitrok2',3,5),(8,'oop',9,1),(9,'xxx',3,1);
/*!40000 ALTER TABLE `kolegiji` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `korisnici`
--

DROP TABLE IF EXISTS `korisnici`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `korisnici` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(20) COLLATE cp1250_croatian_ci NOT NULL,
  `email` varchar(40) COLLATE cp1250_croatian_ci NOT NULL,
  `password` varchar(50) COLLATE cp1250_croatian_ci NOT NULL,
  `ime` varchar(20) COLLATE cp1250_croatian_ci NOT NULL,
  `prezime` varchar(20) COLLATE cp1250_croatian_ci NOT NULL,
  `uloga` tinyint(4) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=cp1250 COLLATE=cp1250_croatian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `korisnici`
--

LOCK TABLES `korisnici` WRITE;
/*!40000 ALTER TABLE `korisnici` DISABLE KEYS */;
INSERT INTO `korisnici` VALUES (1,'testUser','test@test','12345','testIme','testPrezime',1),(3,'korsnik01','korisnik01@mail.hr','korisnik01','Ivan','Ivic',0),(4,'iva123','iva.ivic@mail.hr','iva123','Iva','Ivic',0),(5,'korisnik02','korisnik02@mail.hr','korisnik02','Janko','Janic',0),(6,'korisnik03','korisnik03@mail.hr','korisnik03','Korisnik3','Korisnik3',0),(7,'korisnik04','k@mail.ghr','korisnik04','Sgfg','dgdg',0),(9,'A1','Aaaaaaaa@MEV.HR','12345','A12','A123',1),(10,'test123','test123@mail.hr','test123','Test','Test',0);
/*!40000 ALTER TABLE `korisnici` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `studiji`
--

DROP TABLE IF EXISTS `studiji`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `studiji` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ime` varchar(50) COLLATE cp1250_croatian_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=cp1250 COLLATE=cp1250_croatian_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `studiji`
--

LOCK TABLES `studiji` WRITE;
/*!40000 ALTER TABLE `studiji` DISABLE KEYS */;
INSERT INTO `studiji` VALUES (1,'Računarstvo'),(3,'Proizvodnja'),(5,'IspitniRok');
/*!40000 ALTER TABLE `studiji` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-01-31  1:43:03
