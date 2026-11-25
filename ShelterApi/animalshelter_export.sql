-- Állatmenhely - teljes adatbázis export (animalshelter)
-- Az importálás létrehozza az adatbázist is.

CREATE DATABASE IF NOT EXISTS `animalshelter` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `animalshelter`;

DROP TABLE IF EXISTS `animals`;
DROP TABLE IF EXISTS `shelters`;

CREATE TABLE `shelters` (
  `Id` char(36) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `City` varchar(40) NOT NULL,
  `Capacity` int NOT NULL,
  `RegTime` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `shelters` (`Id`, `Name`, `City`, `Capacity`, `RegTime`) VALUES
('dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64', 'Négylábú Otthon', 'Miskolc', 30, '2026-04-10 08:46:25'),
('8d32af8e-bca2-5907-997f-05ff3b325fe7', 'Gazdira Vár', 'Debrecen', 25, '2026-08-05 08:04:01'),
('95f1187f-f2c2-54b7-afc1-3dba453580ab', 'Cirmos Menedék', 'Eger', 15, '2026-07-18 11:51:48'),
('a2691474-e139-594f-a782-443415ee7d41', 'Boldog Mancs', 'Szeged', 40, '2026-01-08 15:34:23'),
('c41845d3-62ac-5c31-9b16-80193b82aec0', 'Kis Menedék', 'Salgótarján', 3, '2026-05-25 09:52:06');

CREATE TABLE `animals` (
  `Id` char(36) NOT NULL,
  `Name` varchar(40) NOT NULL,
  `Species` varchar(30) NOT NULL,
  `Age` int NOT NULL,
  `Weight` double NOT NULL,
  `RegTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ShelterId` char(36) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_animals_shelters` (`ShelterId`),
  CONSTRAINT `fk_animals_shelters` FOREIGN KEY (`ShelterId`) REFERENCES `shelters` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO `animals` (`Id`, `Name`, `Species`, `Age`, `Weight`, `RegTime`, `ShelterId`) VALUES
('3f6a1c52-8b7d-4e29-a1f4-6c0d9e7b2a01', 'Bodri',  'Kutya',   4,  18.5,  '2026-11-25 10:00:00', 'dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64'),
('7c2e9b14-0d5a-4f38-8e6b-1a9f3c5d7e02', 'Rex',    'Kutya',   7,  32.1,  '2026-11-25 10:00:01', 'dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64'),
('a94d5e60-2b1c-4d73-b8a9-5e0f6c1d3a03', 'Cirmi',  'Macska',  2,   3.8,  '2026-11-25 10:00:02', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('e1b8c37f-6a29-4c05-9d4e-7f2a8b0c5d04', 'Szürke', 'Nyúl',    1,   1.6,  '2026-11-25 10:00:03', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('52d7f0a9-c3e1-4b68-a05d-9b4c1e6f8a05', 'Mici',   'Macska',  5,   4.2,  '2026-11-25 10:00:04', '95f1187f-f2c2-54b7-afc1-3dba453580ab'),
('0b9e4d21-7f5c-4a83-b6d2-3c8a1f0e9b06', 'Csipsz', 'Hörcsög', 1,   0.12, '2026-11-25 10:00:05', '95f1187f-f2c2-54b7-afc1-3dba453580ab'),
('d6a3c8e5-1e40-4f97-8c2b-4a7d5e3f1c07', 'Füles',  'Szamár', 12, 210,    '2026-11-25 10:00:06', 'a2691474-e139-594f-a782-443415ee7d41'),
('8f1b5a73-9d2e-4c60-a3f8-2e6b0d4c7a08', 'Morzsi', 'Kutya',   3,  11.4,  '2026-11-25 10:00:07', 'c41845d3-62ac-5c31-9b16-80193b82aec0');
