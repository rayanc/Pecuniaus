-- phpMyAdmin SQL Dump
-- version 4.1.8
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Oct 18, 2014 at 06:52 AM
-- Server version: 5.5.36-cll-lve
-- PHP Version: 5.4.23

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `Pecuniaus`
--

DELIMITER $$
--
-- Procedures
--
$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Stand-in structure for view `avz_vw_merchantSearchResults`
--
CREATE TABLE IF NOT EXISTS `avz_vw_merchantSearchResults` (
`merchantName` varchar(200)
,`legalName` varchar(200)
,`BusinessName` varchar(200)
,`RNCNumber` varchar(11)
,`assignedSalesRep` varchar(101)
,`ownerName` varchar(201)
,`salesRepId` bigint(20)
,`taskName` varchar(50)
,`taskTypeId` int(11)
,`workFlowName` varchar(50)
,`WorkflowId` smallint(6)
,`contractid` bigint(20)
,`assigneduserId` bigint(20)
,`merchantId` bigint(20)
,`taskStatusId` int(11)
,`tasKStatus` varchar(50)
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `avz_vw_merchantTempSearchResults`
--
CREATE TABLE IF NOT EXISTS `avz_vw_merchantTempSearchResults` (
`merchantName` varchar(200)
,`legalName` varchar(200)
,`businessName` varchar(200)
,`RNCNumber` varchar(11)
,`assignedSalesRep` varchar(101)
,`ownerName` char(0)
,`salesRepId` varchar(200)
,`taskName` varchar(15)
,`taskTypeId` varchar(1)
,`workFlowName` varchar(50)
,`WorkflowId` smallint(6)
,`contractid` bigint(20)
,`assigneduserId` bigint(20)
,`merchantId` bigint(20)
,`taskStatusId` int(1)
,`tasKStatus` varchar(8)
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `avz_vw_merchantTempdetails`
--
CREATE TABLE IF NOT EXISTS `avz_vw_merchantTempdetails` (
`merchantName` varchar(200)
,`legalName` varchar(200)
,`businessName` varchar(200)
,`businessStartDate` date
,`BusinessTypeId` int(11)
,`businessUrl` char(0)
,`RNCNumber` varchar(11)
,`assignedSalesRep` varchar(101)
,`ownerName` char(0)
,`salesRepId` varchar(200)
,`taskName` varchar(50)
,`workFlowName` varchar(50)
,`WorkflowId` smallint(6)
,`contractid` bigint(20)
,`assigneduserId` bigint(20)
,`assignedDate` datetime
,`merchantId` bigint(20)
,`industryTypeId` int(11)
,`SSN` char(0)
,`phoneNumber` char(0)
,`TypeofBusinessentity` varchar(100)
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `avz_vw_merchantdetails`
--
CREATE TABLE IF NOT EXISTS `avz_vw_merchantdetails` (
`merchantName` varchar(200)
,`legalName` varchar(200)
,`businessName` varchar(200)
,`businessStartDate` date
,`BusinessTypeId` int(11)
,`businessUrl` varchar(500)
,`RNCNumber` varchar(11)
,`assignedSalesRep` varchar(101)
,`ownerName` varchar(201)
,`salesRepId` bigint(20)
,`taskName` varchar(50)
,`workFlowName` varchar(50)
,`WorkflowId` smallint(6)
,`contractid` bigint(20)
,`assigneduserId` bigint(20)
,`assignedDate` datetime
,`merchantId` bigint(20)
,`industryTypeId` int(11)
,`SSN` varchar(50)
,`phoneNumber` varchar(45)
,`TypeofBusinessentity` varchar(100)
,`address` text
);
-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_addresstype`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_addresstype` (
  `AddressTypeId` smallint(6) NOT NULL,
  `AddressType` varchar(50) DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InnsertDate` datetime NOT NULL,
  PRIMARY KEY (`AddressTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `lkp_tb_addresstype`
--

INSERT INTO `lkp_tb_addresstype` (`AddressTypeId`, `AddressType`, `IsActive`, `InsertUserId`, `InnsertDate`) VALUES
(1, 'Physical Address', 1, 1, '2014-09-16 00:00:00'),
(2, 'Owner Address', 1, 1, '2014-09-16 00:00:00'),
(3, 'Contact Address', 1, 1, '2014-09-16 00:00:00'),
(4, 'Collector Address', 1, 1, '2014-10-12 00:00:00'),
(5, 'Legal Company Address', 1, 1, '2014-10-12 00:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_banknames`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_banknames` (
  `BankId` int(11) NOT NULL AUTO_INCREMENT,
  `BankName` varchar(500) DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL,
  `City` varchar(100) DEFAULT NULL,
  `Country` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`BankId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `lkp_tb_banknames`
--

INSERT INTO `lkp_tb_banknames` (`BankId`, `BankName`, `InsertDate`, `City`, `Country`) VALUES
(1, 'New Orleans Bank', NULL, 'Montreal', 'USA');

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_declinereasons`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_declinereasons` (
  `DeclineReasonId` bigint(20) NOT NULL,
  `workFlowId` smallint(6) NOT NULL,
  `ReasonDescription` longtext NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`DeclineReasonId`),
  KEY `FK_tb_DeclineReasons_tb_Workflows` (`workFlowId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `lkp_tb_declinereasons`
--

INSERT INTO `lkp_tb_declinereasons` (`DeclineReasonId`, `workFlowId`, `ReasonDescription`, `IsActive`) VALUES
(15001, 1, 'Unacceptable Credit Score', 1),
(15002, 1, 'Merchant doesn’t fulfill Gross Yearly Sales requirement', 1),
(15003, 1, 'Merchant doesn’t have authorized Credit Card Processor', 1),
(15004, 1, 'Merchant doesn’t fulfill minimum CC transactions requirement', 1),
(15005, 1, 'CC sales have gone down', 1),
(15006, 1, 'Less than 5 months accepting CC payments', 1),
(15007, 1, 'CC volumes don’t fulfill requirements', 1),
(15008, 1, 'The person who signed the form is not an authorized person', 1),
(15009, 1, 'The Merchant has already been created by another Rep', 1),
(15010, 1, 'Merchant stopped processing.', 1),
(15011, 1, 'The client doesn’t want a loan at this time', 1),
(15012, 1, 'The client declines because of the Administrative Expenses', 1),
(15013, 1, 'The client declines because of the price', 1),
(15014, 1, 'The client declines because of the Retention Percentage', 1),
(15015, 1, 'The client wants a bigger loan', 1),
(15016, 1, 'The client wants a higher repayment time', 1),
(15017, 1, 'The client is unreachable / refuses to return calls', 1),
(15018, 1, 'The client refuses to present required documents', 1),
(15019, 1, 'Merchant has different financing option', 1);

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_documenttypes`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_documenttypes` (
  `DocumentTypeId` int(11) NOT NULL,
  `Description` varchar(200) NOT NULL,
  `WorkFlowId` smallint(6) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `Modifydate` datetime DEFAULT NULL,
  `isRequired` bit(1) DEFAULT NULL,
  PRIMARY KEY (`DocumentTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `lkp_tb_documenttypes`
--

INSERT INTO `lkp_tb_documenttypes` (`DocumentTypeId`, `Description`, `WorkFlowId`, `IsActive`, `InsertUserId`, `InsertDate`, `ModifyUserId`, `Modifydate`, `isRequired`) VALUES
(1, 'Contract Application', 1, 1, 1, '2014-09-16 00:00:00', NULL, NULL, b'1'),
(2, 'Legal Documents of the Company ', 2, 1, 1, '2014-09-16 00:00:00', NULL, NULL, b'1'),
(3, 'Commercial Name Verification Screenshot ', 2, 1, 1, '2014-09-16 00:00:00', NULL, NULL, b'1'),
(4, 'RNC Screenshot', 2, 1, 1, '2014-09-16 00:00:00', NULL, NULL, b'1'),
(5, 'ID or Passport', 2, 1, 1, '2014-09-16 00:00:00', NULL, NULL, b'1'),
(6, 'Lease Contract or Land Title', 2, 1, 1, '2014-09-16 00:00:00', NULL, NULL, b'1'),
(7, 'Null Check', 2, 1, 1, '2014-09-16 00:00:00', NULL, NULL, b'1'),
(8, 'Bank Statements', 2, 1, 1, '2014-09-16 00:00:00', NULL, NULL, b'1');

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_entitytypes`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_entitytypes` (
  `EntitytypeID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(70) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  PRIMARY KEY (`EntitytypeID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=11008 ;

--
-- Dumping data for table `lkp_tb_entitytypes`
--

INSERT INTO `lkp_tb_entitytypes` (`EntitytypeID`, `Name`, `Description`, `IsActive`, `InsertUserID`, `InsertDate`) VALUES
(11001, 'Persona Fisica', NULL, b'1', 1, '2014-09-16'),
(11002, 'Sociedad en Nombre Colectivo', NULL, b'1', 1, '2014-09-16'),
(11003, 'Sociedad en Comandita Simple', NULL, b'1', 1, '2014-09-16'),
(11004, 'Sociedad en Comandita por Acciones', NULL, b'1', 1, '2014-09-16'),
(11005, 'Sociedad de Responsabilidad Limitada (S.R.L.)', NULL, b'1', 1, '2014-09-16'),
(11006, 'Empresa Individual de Responsabilidad Limitada (E.I.R.L.)', NULL, b'1', 1, '2014-09-16'),
(11007, 'Sociedad Anónima (S.A.)', NULL, b'1', 1, '2014-09-16');

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_expencetypes`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_expencetypes` (
  `ExpenseTypeId` int(11) NOT NULL,
  `Description` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`ExpenseTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `lkp_tb_expencetypes`
--

INSERT INTO `lkp_tb_expencetypes` (`ExpenseTypeId`, `Description`, `IsActive`) VALUES
(12001, 'Admin Misc', 1);

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_expenses`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_expenses` (
  `ExpenceId` int(11) NOT NULL,
  `ExpenseTypeId` int(11) NOT NULL,
  `ExpenseAmount` double NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertDate` date NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `ModifyDate` date DEFAULT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ContractTypeID` int(11) NOT NULL,
  `MinimumFundingAmount` double NOT NULL,
  `MaximumFundingAmount` double NOT NULL,
  PRIMARY KEY (`ExpenceId`),
  KEY `FK_tb_Expenses_Gen_tb_ExpenceTypes` (`ExpenseTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `lkp_tb_expenses`
--

INSERT INTO `lkp_tb_expenses` (`ExpenceId`, `ExpenseTypeId`, `ExpenseAmount`, `IsActive`, `InsertDate`, `InsertUserId`, `ModifyDate`, `ModifyUserId`, `ContractTypeID`, `MinimumFundingAmount`, `MaximumFundingAmount`) VALUES
(12101, 12001, 4000, 1, '2014-10-09', 1, NULL, NULL, 13100, 0, 500000),
(12102, 12001, 4000, 1, '2014-10-09', 1, NULL, NULL, 13200, 0, 500000),
(12103, 12001, 8000, 1, '2014-10-09', 1, NULL, NULL, 13100, 500001, 999999.99),
(12104, 12001, 8000, 1, '2014-10-09', 1, NULL, NULL, 13200, 500001, 999999.99),
(12105, 12001, 12000, 1, '2014-10-09', 1, NULL, NULL, 13100, 999999.99, 1e18),
(12106, 12001, 12000, 1, '2014-10-09', 1, NULL, NULL, 13200, 999999.99, 1e18);

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_financeactivitylist`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_financeactivitylist` (
  `activityid` int(11) NOT NULL AUTO_INCREMENT,
  `activityname` varchar(100) NOT NULL,
  `description` varchar(100) DEFAULT NULL,
  `isactive` bit(1) NOT NULL,
  PRIMARY KEY (`activityid`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=8 ;

--
-- Dumping data for table `lkp_tb_financeactivitylist`
--

INSERT INTO `lkp_tb_financeactivitylist` (`activityid`, `activityname`, `description`, `isactive`) VALUES
(1, 'Regular Payments', 'Regular Payments', b'1'),
(2, 'Processor Adjustments', 'Processor Adjustments', b'1'),
(3, 'Cash/Check/Transference Payments', 'Cash/Check/Transference Payments', b'1'),
(4, 'Refund', 'Refund', b'1'),
(5, 'Renewal Repurchase', 'Renewal Repurchase', b'1'),
(6, 'Credit Transference', 'Credit Transference', b'1'),
(7, 'Cash Processing', 'Cash Processing', b'1');

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_industrytypegroups`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_industrytypegroups` (
  `GroupId` int(11) NOT NULL AUTO_INCREMENT,
  `GroupName` varchar(45) NOT NULL,
  `decription` varchar(45) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`GroupId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `lkp_tb_industrytypegroups`
--

INSERT INTO `lkp_tb_industrytypegroups` (`GroupId`, `GroupName`, `decription`, `IsActive`) VALUES
(1, 'Commodities & Services', 'Commodities & Services', b'1'),
(2, 'Travel', 'Travel', b'1'),
(3, 'Restricted', 'Restricted', b'1'),
(4, 'Restricted & Prohibited', 'Restricted & Prohibited', b'1');

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_industrytypes`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_industrytypes` (
  `IndustryTypeId` int(11) NOT NULL AUTO_INCREMENT,
  `IAllowable` char(1) DEFAULT NULL,
  `IndustryTypeCode` varchar(45) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Description` varchar(45) DEFAULT NULL,
  `IsActive` varchar(45) NOT NULL,
  `Group` int(11) NOT NULL,
  `StatGuideLineType` int(11) DEFAULT NULL,
  PRIMARY KEY (`IndustryTypeId`),
  KEY `FK_industrytypeallowable_stateguidelinetype_lkp_tb_types_idx` (`StatGuideLineType`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=14741 ;

--
-- Dumping data for table `lkp_tb_industrytypes`
--

INSERT INTO `lkp_tb_industrytypes` (`IndustryTypeId`, `IAllowable`, `IndustryTypeCode`, `Name`, `Description`, `IsActive`, `Group`, `StatGuideLineType`) VALUES
(14001, 'A', '0742', 'VETERINARY SERVICES', 'VETERINARY SERVICES', '1', 1, 170003),
(14002, 'A', '0763', 'AGRICULTURAL CO-OPERATIVE', 'AGRICULTURAL CO-OPERATIVE', '1', 1, 170001),
(14003, 'A', '0780', 'LANDSCAPE/HORTICULTURAL SER', 'LANDSCAPE/HORTICULTURAL SER', '1', 1, 170001),
(14004, 'A', '1520', 'GENERAL CONTRACTORS - RESIDENTIAL & COMMERCIA', 'GENERAL CONTRACTORS - RESIDENTIAL & COMMERCIA', '1', 1, 170001),
(14005, 'A', '1711', 'HEATING, PLUMBING, AIR CONDICTIONING CONTRACT', 'HEATING, PLUMBING, AIR CONDICTIONING CONTRACT', '1', 1, 170001),
(14006, 'A', '1731', 'ELECTRICAL CONTRACTORS', 'ELECTRICAL CONTRACTORS', '1', 1, 170001),
(14007, 'A', '1740', 'MASONRY/TILE/PLASTER/INSUL', 'MASONRY/TILE/PLASTER/INSUL', '1', 1, 170001),
(14008, 'A', '1750', 'CARPENTRY', 'CARPENTRY', '1', 1, 170001),
(14009, 'A', '1761', 'ROOFING/SIDING/SHEET METAL', 'ROOFING/SIDING/SHEET METAL', '1', 1, 170001),
(14010, 'A', '1771', 'CONTRACTORS - CONCRETE', 'CONTRACTORS - CONCRETE', '1', 1, 170001),
(14011, 'A', '1799', 'SPEC IAL TRADE CONTRACTORS - DEFAULT', 'SPEC IAL TRADE CONTRACTORS - DEFAULT', '1', 1, 170001),
(14012, 'A', '2741', 'MISC PUBLISHING & PRINTING', 'MISC PUBLISHING & PRINTING', '1', 1, 170001),
(14013, 'A', '2791', 'TYPESETTING, PLATEMAKING &RELATED', 'TYPESETTING, PLATEMAKING &RELATED', '1', 1, 170001),
(14014, 'A', '2842', 'SPECIALTY CLEANING/POLISHING/SANITATION', 'SPECIALTY CLEANING/POLISHING/SANITATION', '1', 1, 170001),
(14015, 'A', '3000', 'UNITED', 'UNITED', '1', 2, 170001),
(14016, 'A', '3001', 'AMERICAN', 'AMERICAN', '1', 2, 170001),
(14017, 'A', '3002', 'PAN AM', 'PAN AM', '1', 2, 170001),
(14018, 'A', '3003', 'EASTERN', 'EASTERN', '1', 2, 170001),
(14019, 'A', '3004', 'TWA', 'TWA', '1', 2, 170001),
(14020, 'A', '3005', 'BRITISH AIR', 'BRITISH AIR', '1', 2, 170001),
(14021, 'A', '3006', 'JAL', 'JAL', '1', 2, 170001),
(14022, 'A', '3007', 'AIR FRANCE', 'AIR FRANCE', '1', 2, 170001),
(14023, 'A', '3008', 'LUFTHANSA', 'LUFTHANSA', '1', 2, 170001),
(14024, 'A', '3009', 'AIR CANADA', 'AIR CANADA', '1', 2, 170001),
(14025, 'A', '3010', 'KLM', 'KLM', '1', 2, 170001),
(14026, 'A', '3011', 'AEROFLOT', 'AEROFLOT', '1', 2, 170001),
(14027, 'A', '3012', 'QANTAS', 'QANTAS', '1', 2, 170001),
(14028, 'A', '3013', 'ALITALIA', 'ALITALIA', '1', 2, 170001),
(14029, 'A', '3014', 'SAUDIA AIR', 'SAUDIA AIR', '1', 2, 170001),
(14030, 'A', '3015', 'SWISSAIR', 'SWISSAIR', '1', 2, 170001),
(14031, 'A', '3016', 'SAS', 'SAS', '1', 2, 170001),
(14032, 'A', '3017', 'SAA', 'SAA', '1', 2, 170001),
(14033, 'A', '3018', 'VARIG AIR', 'VARIG AIR', '1', 2, 170001),
(14034, 'A', '3020', 'AIR-INDIA', 'AIR-INDIA', '1', 2, 170001),
(14035, 'A', '3021', 'AIR ALGERIE', 'AIR ALGERIE', '1', 2, 170001),
(14036, 'A', '3022', 'PHILIPP AIR', 'PHILIPP AIR', '1', 2, 170001),
(14037, 'A', '3023', 'MEXICANA', 'MEXICANA', '1', 2, 170001),
(14038, 'A', '3024', 'PAKISTAN', 'PAKISTAN', '1', 2, 170001),
(14039, 'A', '3025', 'AIR NZ', 'AIR NZ', '1', 2, 170001),
(14040, 'A', '3027', 'UTA/IA', 'UTA/IA', '1', 2, 170001),
(14041, 'A', '3028', 'AIR MALTA', 'AIR MALTA', '1', 2, 170001),
(14042, 'A', '3029', 'SABENA', 'SABENA', '1', 2, 170001),
(14043, 'A', '3030', 'AERO ARGENT', 'AERO ARGENT', '1', 2, 170001),
(14044, 'A', '3031', 'OLYMPIC', 'OLYMPIC', '1', 2, 170001),
(14045, 'A', '3032', 'EL AL', 'EL AL', '1', 2, 170001),
(14046, 'A', '3033', 'ANSETT AIR', 'ANSETT AIR', '1', 2, 170001),
(14047, 'A', '3034', 'AUSTRALIAN', 'AUSTRALIAN', '1', 2, 170001),
(14048, 'A', '3035', 'TAP AIR', 'TAP AIR', '1', 2, 170001),
(14049, 'A', '3036', 'VASP (BRAZIL)', 'VASP (BRAZIL)', '1', 2, 170001),
(14050, 'A', '3037', 'EGYPTAIR', 'EGYPTAIR', '1', 2, 170001),
(14051, 'A', '3038', 'KUWAIT AIR', 'KUWAIT AIR', '1', 2, 170001),
(14052, 'A', '3039', 'AVIANCA', 'AVIANCA', '1', 2, 170001),
(14053, 'A', '3040', 'GULF AIR (BAHRAIN)', 'GULF AIR (BAHRAIN)', '1', 2, 170001),
(14054, 'A', '3041', 'BALKAN', 'BALKAN', '1', 2, 170001),
(14055, 'A', '3042', 'FINNAIR', 'FINNAIR', '1', 2, 170001),
(14056, 'A', '3043', 'AER LINGUS', 'AER LINGUS', '1', 2, 170001),
(14057, 'A', '3045', 'NIGERIA AIR', 'NIGERIA AIR', '1', 2, 170001),
(14058, 'A', '3046', 'CRUZEIRO', 'CRUZEIRO', '1', 2, 170001),
(14059, 'A', '3047', 'THY (TURKEY)', 'THY (TURKEY)', '1', 2, 170001),
(14060, 'A', '3048', 'AIR MAROC', 'AIR MAROC', '1', 2, 170001),
(14061, 'A', '3049', 'TUNIS AIR', 'TUNIS AIR', '1', 2, 170001),
(14062, 'A', '3050', 'ICELANDAIR', 'ICELANDAIR', '1', 2, 170001),
(14063, 'A', '3051', 'AUSTRIAN AIR', 'AUSTRIAN AIR', '1', 2, 170001),
(14064, 'A', '3052', 'LANCHILE', 'LANCHILE', '1', 2, 170001),
(14065, 'A', '3053', 'AVIACO (SPAIN)', 'AVIACO (SPAIN)', '1', 2, 170001),
(14066, 'A', '3054', 'LADECO (CHILE)', 'LADECO (CHILE)', '1', 2, 170001),
(14067, 'A', '3055', 'LAB (BOLIVA)', 'LAB (BOLIVA)', '1', 2, 170001),
(14068, 'A', '3056', 'QUEBECAIRE', 'QUEBECAIRE', '1', 2, 170001),
(14069, 'A', '3057', 'E/W AIRLINE', 'E/W AIRLINE', '1', 2, 170001),
(14070, 'A', '3058', 'DELTA', 'DELTA', '1', 2, 170001),
(14071, 'A', '3060', 'NORTHWEST', 'NORTHWEST', '1', 2, 170001),
(14072, 'A', '3061', 'CONTINENTAL', 'CONTINENTAL', '1', 2, 170001),
(14073, 'A', '3063', 'USAIR', 'USAIR', '1', 2, 170001),
(14074, 'A', '3064', 'BRANIFF', 'BRANIFF', '1', 2, 170001),
(14075, 'A', '3065', 'AIRINTER', 'AIRINTER', '1', 2, 170001),
(14076, 'A', '3066', 'SOUTHWEST', 'SOUTHWEST', '1', 2, 170001),
(14077, 'A', '3069', 'AIR EUROPE', 'AIR EUROPE', '1', 2, 170001),
(14078, 'A', '3071', 'AIR BC', 'AIR BC', '1', 2, 170001),
(14079, 'A', '3075', 'SINGAPORE AIR', 'SINGAPORE AIR', '1', 2, 170001),
(14080, 'A', '3076', 'AEROMEXICO', 'AEROMEXICO', '1', 2, 170001),
(14081, 'A', '3077', 'THAI AIRWAYS', 'THAI AIRWAYS', '1', 2, 170001),
(14082, 'A', '3078', 'CHINA AIRLINES', 'CHINA AIRLINES', '1', 2, 170001),
(14083, 'A', '3079', 'AMTRAK', 'AMTRAK', '1', 2, 170001),
(14084, 'A', '3081', 'NORDAIR', 'NORDAIR', '1', 2, 170001),
(14085, 'A', '3082', 'KOREAN AIR', 'KOREAN AIR', '1', 2, 170001),
(14086, 'A', '3083', 'AIR AFRIQUE', 'AIR AFRIQUE', '1', 2, 170001),
(14087, 'A', '3084', 'EVA AIRWAYS', 'EVA AIRWAYS', '1', 2, 170001),
(14088, 'A', '3085', 'MIDWEST EXPRESS', 'MIDWEST EXPRESS', '1', 2, 170001),
(14089, 'A', '3086', 'CARNIVAL AIRLINES', 'CARNIVAL AIRLINES', '1', 2, 170001),
(14090, 'A', '3087', 'METRO AIR', 'METRO AIR', '1', 2, 170001),
(14091, 'A', '3088', 'CROATIA AIRLINES', 'CROATIA AIRLINES', '1', 2, 170001),
(14092, 'A', '3090', 'RIO SUL', 'RIO SUL', '1', 2, 170001),
(14093, 'A', '3092', 'MIDWAY AIR', 'MIDWAY AIR', '1', 2, 170001),
(14094, 'A', '3094', 'ZAMBIA AIR', 'ZAMBIA AIR', '1', 2, 170001),
(14095, 'A', '3095', 'WARDAIR', 'WARDAIR', '1', 2, 170001),
(14096, 'A', '3096', 'AIR ZIMBABWE', 'AIR ZIMBABWE', '1', 2, 170001),
(14097, 'A', '3097', 'AIRNSWALES', 'AIRNSWALES', '1', 2, 170001),
(14098, 'A', '3098', 'AIRNIUGINI', 'AIRNIUGINI', '1', 2, 170001),
(14099, 'A', '3099', 'CATHAYPACAIR', 'CATHAYPACAIR', '1', 2, 170001),
(14100, 'A', '3100', 'MALAY AIR', 'MALAY AIR', '1', 2, 170001),
(14101, 'A', '3102', 'IBERIA', 'IBERIA', '1', 2, 170001),
(14102, 'A', '3103', 'GARUDA AIR', 'GARUDA AIR', '1', 2, 170001),
(14103, 'A', '3106', 'BRAATHENS', 'BRAATHENS', '1', 2, 170001),
(14104, 'A', '3110', 'WINGSAIR', 'WINGSAIR', '1', 2, 170001),
(14105, 'A', '3111', 'BR. MIDLAND', 'BR. MIDLAND', '1', 2, 170001),
(14106, 'A', '3112', 'WINDWARD IS', 'WINDWARD IS', '1', 2, 170001),
(14107, 'A', '3115', 'WESTAIRCOMM', 'WESTAIRCOMM', '1', 2, 170001),
(14108, 'A', '3117', 'VIASA', 'VIASA', '1', 2, 170001),
(14109, 'A', '3118', 'VALLEY AIR', 'VALLEY AIR', '1', 2, 170001),
(14110, 'A', '3125', 'TAN AIRLINE', 'TAN AIRLINE', '1', 2, 170001),
(14111, 'A', '3126', 'TALAIR', 'TALAIR', '1', 2, 170001),
(14112, 'A', '3127', 'TACA INTL', 'TACA INTL', '1', 2, 170001),
(14113, 'A', '3129', 'SURINAM AIR', 'SURINAM AIR', '1', 2, 170001),
(14114, 'A', '3130', 'SUNWORLD', 'SUNWORLD', '1', 2, 170001),
(14115, 'A', '3132', 'SUNBIRD AIR', 'SUNBIRD AIR', '1', 2, 170001),
(14116, 'A', '3133', 'SUNBELT AIR', 'SUNBELT AIR', '1', 2, 170001),
(14117, 'A', '3135', 'SUDAN AIR', 'SUDAN AIR', '1', 2, 170001),
(14118, 'A', '3136', 'SKYWESTAIR', 'SKYWESTAIR', '1', 2, 170001),
(14119, 'A', '3137', 'SINGLETON', 'SINGLETON', '1', 2, 170001),
(14120, 'A', '3138', 'SIMMONS AIR', 'SIMMONS AIR', '1', 2, 170001),
(14121, 'A', '3143', 'SCENIC AIR', 'SCENIC AIR', '1', 2, 170001),
(14122, 'A', '3144', 'VIRGIN ATLANTIC', 'VIRGIN ATLANTIC', '1', 2, 170001),
(14123, 'A', '3145', 'SAN JUAN AIR', 'SAN JUAN AIR', '1', 2, 170001),
(14124, 'A', '3146', 'LUXAIR', 'LUXAIR', '1', 2, 170001),
(14125, 'A', '3148', 'ROYALE AIR', 'ROYALE AIR', '1', 2, 170001),
(14126, 'A', '3151', 'AIR ZAIRE', 'AIR ZAIRE', '1', 2, 170001),
(14127, 'A', '3154', 'PRINCEVILLE', 'PRINCEVILLE', '1', 2, 170001),
(14128, 'A', '3156', 'PRECISION', 'PRECISION', '1', 2, 170001),
(14129, 'A', '3159', 'PBA', 'PBA', '1', 2, 170001),
(14130, 'A', '3161', 'ALL NIPPON', 'ALL NIPPON', '1', 2, 170001),
(14131, 'A', '3164', 'NORONTAIR', 'NORONTAIR', '1', 2, 170001),
(14132, 'A', '3165', 'NY HELI', 'NY HELI', '1', 2, 170001),
(14133, 'A', '3167', 'NENGLANDAIR', 'NENGLANDAIR', '1', 2, 170001),
(14134, 'A', '3170', 'MT COOK', 'MT COOK', '1', 2, 170001),
(14135, 'A', '3171', 'CANADIAN', 'CANADIAN', '1', 2, 170001),
(14136, 'A', '3172', 'NATIONAIR', 'NATIONAIR', '1', 2, 170001),
(14137, 'A', '3174', 'MIDSTATEAIR', 'MIDSTATEAIR', '1', 2, 170001),
(14138, 'A', '3175', 'MIDEASTAIR', 'MIDEASTAIR', '1', 2, 170001),
(14139, 'A', '3176', 'METROFLTAIR', 'METROFLTAIR', '1', 2, 170001),
(14140, 'A', '3177', 'MESABAAVIAT', 'MESABAAVIAT', '1', 2, 170001),
(14141, 'A', '3178', 'MESA AIR', 'MESA AIR', '1', 2, 170001),
(14142, 'A', '3180', 'MALL AIRWAYS', 'MALL AIRWAYS', '1', 2, 170001),
(14143, 'A', '3181', 'MALEV', 'MALEV', '1', 2, 170001),
(14144, 'A', '3182', 'LOT (POLAND)', 'LOT (POLAND)', '1', 2, 170001),
(14145, 'A', '3183', 'LI AIRLINES', 'LI AIRLINES', '1', 2, 170001),
(14146, 'A', '3184', 'LIAT', 'LIAT', '1', 2, 170001),
(14147, 'A', '3185', 'LAV (VENEZUELA)', 'LAV (VENEZUELA)', '1', 2, 170001),
(14148, 'A', '3186', 'LAP (PARAGUAY)', 'LAP (PARAGUAY)', '1', 2, 170001),
(14149, 'A', '3187', 'LACSA', 'LACSA', '1', 2, 170001),
(14150, 'A', '3190', 'JUGOSLAVAIR', 'JUGOSLAVAIR', '1', 2, 170001),
(14151, 'A', '3191', 'ISLANDAIR', 'ISLANDAIR', '1', 2, 170001),
(14152, 'A', '3192', 'IRAN NATL', 'IRAN NATL', '1', 2, 170001),
(14153, 'A', '3193', 'INDIAN AIR', 'INDIAN AIR', '1', 2, 170001),
(14154, 'A', '3195', 'HOLIDAY AIR', 'HOLIDAY AIR', '1', 2, 170001),
(14155, 'A', '3196', 'HAWAIIANAIR', 'HAWAIIANAIR', '1', 2, 170001),
(14156, 'A', '3197', 'HAVASUAIR', 'HAVASUAIR', '1', 2, 170001),
(14157, 'A', '3198', 'HARBORAIR', 'HARBORAIR', '1', 2, 170001),
(14158, 'A', '3199', 'SAM', 'SAM', '1', 2, 170001),
(14159, 'A', '3200', 'GUYANA AIR', 'GUYANA AIR', '1', 2, 170001),
(14160, 'A', '3203', 'GOLDPACAIR', 'GOLDPACAIR', '1', 2, 170001),
(14161, 'A', '3204', 'FREEDOM AIR', 'FREEDOM AIR', '1', 2, 170001),
(14162, 'A', '3206', 'FIRST AIR', 'FIRST AIR', '1', 2, 170001),
(14163, 'A', '3211', 'SAN', 'SAN', '1', 2, 170001),
(14164, 'A', '3212', 'DOMINICA', 'DOMINICA', '1', 2, 170001),
(14165, 'A', '3213', 'DIRECT AIR', 'DIRECT AIR', '1', 2, 170001),
(14166, 'A', '3215', 'DANAIRSERV', 'DANAIRSERV', '1', 2, 170001),
(14167, 'A', '3216', 'CUMBERLNDAIR', 'CUMBERLNDAIR', '1', 2, 170001),
(14168, 'A', '3217', 'CSA', 'CSA', '1', 2, 170001),
(14169, 'A', '3218', 'CROWN AIR', 'CROWN AIR', '1', 2, 170001),
(14170, 'A', '3219', 'COPA', 'COPA', '1', 2, 170001),
(14171, 'A', '3220', 'COMP FAU AIR', 'COMP FAU AIR', '1', 2, 170001),
(14172, 'A', '3221', 'TAME', 'TAME', '1', 2, 170001),
(14173, 'A', '3222', 'COMMAND AIR', 'COMMAND AIR', '1', 2, 170001),
(14174, 'A', '3223', 'COMAIR', 'COMAIR', '1', 2, 170001),
(14175, 'A', '3226', 'CHRISTMANAIR', 'CHRISTMANAIR', '1', 2, 170001),
(14176, 'A', '3228', 'CAYMANAIR', 'CAYMANAIR', '1', 2, 170001),
(14177, 'A', '3229', 'SAETA', 'SAETA', '1', 2, 170001),
(14178, 'A', '3231', 'SAHSA', 'SAHSA', '1', 2, 170001),
(14179, 'A', '3233', 'CAPITOL AIR', 'CAPITOL AIR', '1', 2, 170001),
(14180, 'A', '3234', 'BWIA', 'BWIA', '1', 2, 170001),
(14181, 'A', '3235', 'BROCKWAYAIR', 'BROCKWAYAIR', '1', 2, 170001),
(14182, 'A', '3236', 'BIGSKYAIR', 'BIGSKYAIR', '1', 2, 170001),
(14183, 'A', '3238', 'BEMIDJI AV', 'BEMIDJI AV', '1', 2, 170001),
(14184, 'A', '3239', 'BARHARBORAIR', 'BARHARBORAIR', '1', 2, 170001),
(14185, 'A', '3240', 'BAHAMASAIR', 'BAHAMASAIR', '1', 2, 170001),
(14186, 'A', '3241', 'AVIATECA', 'AVIATECA', '1', 2, 170001),
(14187, 'A', '3242', 'AVENSA', 'AVENSA', '1', 2, 170001),
(14188, 'A', '3243', 'AUSTAIRSERV', 'AUSTAIRSERV', '1', 2, 170001),
(14189, 'A', '3245', 'ATLANTISAIR', 'ATLANTISAIR', '1', 2, 170001),
(14190, 'A', '3246', 'ALTANTIC SE', 'ALTANTIC SE', '1', 2, 170001),
(14191, 'A', '3247', 'ATLANTICAIR', 'ATLANTICAIR', '1', 2, 170001),
(14192, 'A', '3248', 'ASPEN AIR', 'ASPEN AIR', '1', 2, 170001),
(14193, 'A', '3251', 'ALOHA AIR', 'ALOHA AIR', '1', 2, 170001),
(14194, 'A', '3252', 'ALM', 'ALM', '1', 2, 170001),
(14195, 'A', '3253', 'AMERICA WEST', 'AMERICA WEST', '1', 2, 170001),
(14196, 'A', '3254', 'TRUMP AIR', 'TRUMP AIR', '1', 2, 170001),
(14197, 'A', '3256', 'ALASKA AIR', 'ALASKA AIR', '1', 2, 170001),
(14198, 'A', '3259', 'AMERICAN TRANS AIR', 'AMERICAN TRANS AIR', '1', 2, 170001),
(14199, 'A', '3260', 'AIRSUNSHINE', 'AIRSUNSHINE', '1', 2, 170001),
(14200, 'A', '3261', 'AIR CHINA', 'AIR CHINA', '1', 2, 170001),
(14201, 'A', '3262', 'RENO AIR', 'RENO AIR', '1', 2, 170001),
(14202, 'A', '3263', 'ASC AIRLINE', 'ASC AIRLINE', '1', 2, 170001),
(14203, 'A', '3266', 'AIR SEYCH', 'AIR SEYCH', '1', 2, 170001),
(14204, 'A', '3267', 'AIR PANAMA', 'AIR PANAMA', '1', 2, 170001),
(14205, 'A', '3268', 'AIR PACIFIC', 'AIR PACIFIC', '1', 2, 170001),
(14206, 'A', '3275', 'AIRNEVADA', 'AIRNEVADA', '1', 2, 170001),
(14207, 'A', '3276', 'AIR MIDWST', 'AIR MIDWST', '1', 2, 170001),
(14208, 'A', '3277', 'AIR MADAG', 'AIR MADAG', '1', 2, 170001),
(14209, 'A', '3279', 'AIR LA', 'AIR LA', '1', 2, 170001),
(14210, 'A', '3280', 'AIR JAMAICA', 'AIR JAMAICA', '1', 2, 170001),
(14211, 'A', '3282', 'AIRDJIBOUTI', 'AIRDJIBOUTI', '1', 2, 170001),
(14212, 'A', '3284', 'AERVIRGINIS', 'AERVIRGINIS', '1', 2, 170001),
(14213, 'A', '3285', 'AEROPERU', 'AEROPERU', '1', 2, 170001),
(14214, 'A', '3286', 'AERONICARAG', 'AERONICARAG', '1', 2, 170001),
(14215, 'A', '3287', 'AEROCOACHAV', 'AEROCOACHAV', '1', 2, 170001),
(14216, 'A', '3291', 'ARIANAAFGAN', 'ARIANAAFGAN', '1', 2, 170001),
(14217, 'A', '3292', 'CYPRUS AIR', 'CYPRUS AIR', '1', 2, 170001),
(14218, 'A', '3293', 'ECUATORIANA', 'ECUATORIANA', '1', 2, 170001),
(14219, 'A', '3294', 'ETHIOPIAN', 'ETHIOPIAN', '1', 2, 170001),
(14220, 'A', '3295', 'KENYAAIR', 'KENYAAIR', '1', 2, 170001),
(14221, 'A', '3297', 'TAROM', 'TAROM', '1', 2, 170001),
(14222, 'A', '3298', 'AIR MAURITIUS', 'AIR MAURITIUS', '1', 2, 170001),
(14223, 'A', '3299', 'WILDEROES''S FLYVESELSKAP', 'WILDEROES''S FLYVESELSKAP', '1', 2, 170001),
(14224, 'A', '3351', 'AFFILIATED AUTO RENTAL', 'AFFILIATED AUTO RENTAL', '1', 2, 170001),
(14225, 'A', '3352', 'AMERICAN INTERNATIONAL', 'AMERICAN INTERNATIONAL', '1', 2, 170001),
(14226, 'A', '3353', 'BROOKS RENT-A-CAR', 'BROOKS RENT-A-CAR', '1', 2, 170001),
(14227, 'A', '3354', 'ACTION AUTO RENTAL', 'ACTION AUTO RENTAL', '1', 2, 170001),
(14228, 'A', '3357', 'HERTZ RENT-A-CAR', 'HERTZ RENT-A-CAR', '1', 2, 170001),
(14229, 'A', '3359', 'PAYLESS CAR RENTAL', 'PAYLESS CAR RENTAL', '1', 2, 170001),
(14230, 'A', '3360', 'SNAPPY CAR RENTAL', 'SNAPPY CAR RENTAL', '1', 2, 170001),
(14231, 'A', '3361', 'AIRWAYS', 'AIRWAYS', '1', 2, 170001),
(14232, 'A', '3362', 'ALTRA AUTO RENTAL', 'ALTRA AUTO RENTAL', '1', 2, 170001),
(14233, 'A', '3364', 'AGENCY RENT-A-CAR', 'AGENCY RENT-A-CAR', '1', 2, 170001),
(14234, 'A', '3366', 'BUDGET RENT-A-CAR', 'BUDGET RENT-A-CAR', '1', 2, 170001),
(14235, 'A', '3368', 'HOLIDAY RENT-A-CAR', 'HOLIDAY RENT-A-CAR', '1', 2, 170001),
(14236, 'A', '3370', 'RENT-A-WRECK', 'RENT-A-WRECK', '1', 2, 170001),
(14237, 'A', '3374', 'SEARS AUTO RENTALS', 'SEARS AUTO RENTALS', '1', 2, 170001),
(14238, 'A', '3376', 'AJAX RENT-A-CAR', 'AJAX RENT-A-CAR', '1', 2, 170001),
(14239, 'A', '3380', 'INTER AMERICAN CAR', 'INTER AMERICAN CAR', '1', 2, 170001),
(14240, 'A', '3381', 'EUROP CAR', 'EUROP CAR', '1', 2, 170001),
(14241, 'A', '3385', 'TROPICAL RENT-A-CAR', 'TROPICAL RENT-A-CAR', '1', 2, 170001),
(14242, 'A', '3386', 'SHOWCASE RENTAL CARS', 'SHOWCASE RENTAL CARS', '1', 2, 170001),
(14243, 'A', '3387', 'ALAMO RENT-A-CAR', 'ALAMO RENT-A-CAR', '1', 2, 170001),
(14244, 'A', '3388', 'AUTO EUROPE', 'AUTO EUROPE', '1', 2, 170001),
(14245, 'A', '3389', 'AVIS RENT-A-CAR', 'AVIS RENT-A-CAR', '1', 2, 170001),
(14246, 'A', '3390', 'DOLLAR RENT-A-CAR', 'DOLLAR RENT-A-CAR', '1', 2, 170001),
(14247, 'A', '3391', 'EUROPE BY CAR', 'EUROPE BY CAR', '1', 2, 170001),
(14248, 'A', '3393', 'NATIONAL CAR RENTAL', 'NATIONAL CAR RENTAL', '1', 2, 170001),
(14249, 'A', '3394', 'KEMWELL GROUP', 'KEMWELL GROUP', '1', 2, 170001),
(14250, 'A', '3395', 'THRIFY RENT-A-CAR', 'THRIFY RENT-A-CAR', '1', 2, 170001),
(14251, 'A', '3396', 'TILDEN RENT-A-CAR', 'TILDEN RENT-A-CAR', '1', 2, 170001),
(14252, 'A', '3398', 'ECONO-CAR RENT-A-CAR', 'ECONO-CAR RENT-A-CAR', '1', 2, 170001),
(14253, 'A', '3399', 'AMEREX RENT-A-CAR', 'AMEREX RENT-A-CAR', '1', 2, 170001),
(14254, 'A', '3400', 'AUTO HOST CAR RENTALS', 'AUTO HOST CAR RENTALS', '1', 2, 170001),
(14255, 'A', '3405', 'ENTERPRISE RENT-A-CAR', 'ENTERPRISE RENT-A-CAR', '1', 2, 170001),
(14256, 'A', '3409', 'GENERAL RENT-A-CAR', 'GENERAL RENT-A-CAR', '1', 2, 170001),
(14257, 'A', '3411', 'ATLANTA RENT-A-CAR', 'ATLANTA RENT-A-CAR', '1', 2, 170001),
(14258, 'A', '3412', 'A-1 RENT-A-CAR', 'A-1 RENT-A-CAR', '1', 2, 170001),
(14259, 'A', '3414', 'GODFREY NATIONAL', 'GODFREY NATIONAL', '1', 2, 170001),
(14260, 'A', '3419', 'ALPHA RENT-A-CAR', 'ALPHA RENT-A-CAR', '1', 2, 170001),
(14261, 'A', '3420', 'ANSA INTERNATIONAL', 'ANSA INTERNATIONAL', '1', 2, 170001),
(14262, 'A', '3421', 'ALLSTATE', 'ALLSTATE', '1', 2, 170001),
(14263, 'A', '3423', 'AVCAR', 'AVCAR', '1', 2, 170001),
(14264, 'A', '3425', 'AUTOMATE', 'AUTOMATE', '1', 2, 170001),
(14265, 'A', '3427', 'AVON RENT-A-CAR', 'AVON RENT-A-CAR', '1', 2, 170001),
(14266, 'A', '3428', 'CAREY RENT-A-CAR', 'CAREY RENT-A-CAR', '1', 2, 170001),
(14267, 'A', '3429', 'INSURANCE RENT-A-CAR', 'INSURANCE RENT-A-CAR', '1', 2, 170001),
(14268, 'A', '3430', 'MAJOR RENT-A-CAR', 'MAJOR RENT-A-CAR', '1', 2, 170001),
(14269, 'A', '3431', 'REPLACEMENT RENT-A-CAR', 'REPLACEMENT RENT-A-CAR', '1', 2, 170001),
(14270, 'A', '3432', 'RESERVE RENT-A-CAR', 'RESERVE RENT-A-CAR', '1', 2, 170001),
(14271, 'A', '3433', 'UGLY DUCKLING RENT-A-CAR', 'UGLY DUCKLING RENT-A-CAR', '1', 2, 170001),
(14272, 'A', '3434', 'USA RENT-A-CAR', 'USA RENT-A-CAR', '1', 2, 170001),
(14273, 'A', '3435', 'VALUE RENT-A-CAR', 'VALUE RENT-A-CAR', '1', 2, 170001),
(14274, 'A', '3436', 'AUTOHANSA RENT-A-CAR', 'AUTOHANSA RENT-A-CAR', '1', 2, 170001),
(14275, 'A', '3437', 'CITE RENT-A-CAR', 'CITE RENT-A-CAR', '1', 2, 170001),
(14276, 'A', '3438', 'INTERENT', 'INTERENT', '1', 2, 170001),
(14277, 'A', '3439', 'MILLEVILLE', 'MILLEVILLE', '1', 2, 170001),
(14278, 'A', '3440', 'VIA ROUTE', 'VIA ROUTE', '1', 2, 170001),
(14279, 'A', '3501', 'HOLIDAY INN', 'HOLIDAY INN', '1', 2, 170001),
(14280, 'A', '3502', 'BEST WESTERN', 'BEST WESTERN', '1', 2, 170001),
(14281, 'A', '3503', 'SHERATON', 'SHERATON', '1', 2, 170001),
(14282, 'A', '3504', 'HILTON', 'HILTON', '1', 2, 170001),
(14283, 'A', '3505', 'FORTE HOTELS', 'FORTE HOTELS', '1', 2, 170001),
(14284, 'A', '3506', 'GOLDEN TULIP', 'GOLDEN TULIP', '1', 2, 170001),
(14285, 'A', '3507', 'FRIENDSHIP INN', 'FRIENDSHIP INN', '1', 2, 170001),
(14286, 'A', '3508', 'QUALITY INN', 'QUALITY INN', '1', 2, 170001),
(14287, 'A', '3509', 'MARRIOTT', 'MARRIOTT', '1', 2, 170001),
(14288, 'A', '3510', 'DAYS INN', 'DAYS INN', '1', 2, 170001),
(14289, 'A', '3511', 'ARABELLA HOTELS', 'ARABELLA HOTELS', '1', 2, 170001),
(14290, 'A', '3512', 'INTER-CONTINENTAL', 'INTER-CONTINENTAL', '1', 2, 170001),
(14291, 'A', '3513', 'WESTIN HOTELS', 'WESTIN HOTELS', '1', 2, 170001),
(14292, 'A', '3515', 'RODEWAY INN', 'RODEWAY INN', '1', 2, 170001),
(14293, 'A', '3516', 'LA QUINTA MOTOR INN', 'LA QUINTA MOTOR INN', '1', 2, 170001),
(14294, 'A', '3517', 'AMERICANA HOTELS', 'AMERICANA HOTELS', '1', 2, 170001),
(14295, 'A', '3518', 'SOL HOTELS', 'SOL HOTELS', '1', 2, 170001),
(14296, 'A', '3519', 'PULLMAN INTERNATIONAL HOTELS', 'PULLMAN INTERNATIONAL HOTELS', '1', 2, 170001),
(14297, 'A', '3520', 'MERIDIEN', 'MERIDIEN', '1', 2, 170001),
(14298, 'A', '3521', 'CREST HOTELS', 'CREST HOTELS', '1', 2, 170001),
(14299, 'A', '3522', 'TOKYO HOTELS', 'TOKYO HOTELS', '1', 2, 170001),
(14300, 'A', '3523', 'PENINSULA HOTELS', 'PENINSULA HOTELS', '1', 2, 170001),
(14301, 'A', '3524', 'WELCOMGROUP', 'WELCOMGROUP', '1', 2, 170001),
(14302, 'A', '3525', 'DUNFEY HOTEL', 'DUNFEY HOTEL', '1', 2, 170001),
(14303, 'A', '3526', 'PRINCE HOTEL', 'PRINCE HOTEL', '1', 2, 170001),
(14304, 'A', '3527', 'DOWNTOWNER-PASSPORT', 'DOWNTOWNER-PASSPORT', '1', 2, 170001),
(14305, 'A', '3528', 'RED LION INNS/HOTELS', 'RED LION INNS/HOTELS', '1', 2, 170001),
(14306, 'A', '3529', 'CP (CANADIAN PACIFIC) HOTELS', 'CP (CANADIAN PACIFIC) HOTELS', '1', 2, 170001),
(14307, 'A', '3530', 'STOUFFER', 'STOUFFER', '1', 2, 170001),
(14308, 'A', '3531', 'ASTIR HOTELS', 'ASTIR HOTELS', '1', 2, 170001),
(14309, 'A', '3532', 'SUN ROUTE HOTELS', 'SUN ROUTE HOTELS', '1', 2, 170001),
(14310, 'A', '3533', 'HOTEL IBIS', 'HOTEL IBIS', '1', 2, 170001),
(14311, 'A', '3534', 'SOUTHERN PACIFIC', 'SOUTHERN PACIFIC', '1', 2, 170001),
(14312, 'A', '3535', 'HILTON INTERNATIONAL', 'HILTON INTERNATIONAL', '1', 2, 170001),
(14313, 'A', '3536', 'AMFAC HOTEL', 'AMFAC HOTEL', '1', 2, 170001),
(14314, 'A', '3537', 'ANA HOTELS', 'ANA HOTELS', '1', 2, 170001),
(14315, 'A', '3538', 'CONCORDE HOTELS', 'CONCORDE HOTELS', '1', 2, 170001),
(14316, 'A', '3539', 'FUJITA HOTEL', 'FUJITA HOTEL', '1', 2, 170001),
(14317, 'A', '3540', 'IBEROTEL', 'IBEROTEL', '1', 2, 170001),
(14318, 'A', '3541', 'HOTEL OKURA', 'HOTEL OKURA', '1', 2, 170001),
(14319, 'A', '3542', 'ROYAL HOTELS', 'ROYAL HOTELS', '1', 2, 170001),
(14320, 'A', '3543', 'FOUR SEASONS', 'FOUR SEASONS', '1', 2, 170001),
(14321, 'A', '3544', 'CIGAHOTELS', 'CIGAHOTELS', '1', 2, 170001),
(14322, 'A', '3545', 'SHANGRI-LA INTERNATIONAL', 'SHANGRI-LA INTERNATIONAL', '1', 2, 170001),
(14323, 'A', '3546', 'SIAM LODGE HOTELS', 'SIAM LODGE HOTELS', '1', 2, 170001),
(14324, 'A', '3548', 'HOTELES MELIA', 'HOTELES MELIA', '1', 2, 170001),
(14325, 'A', '3549', 'AUBERGE DES GOVERNEURS', 'AUBERGE DES GOVERNEURS', '1', 2, 170001),
(14326, 'A', '3550', 'REGAL 8 INNS', 'REGAL 8 INNS', '1', 2, 170001),
(14327, 'A', '3551', 'AUBERGE WANDLYN INN', 'AUBERGE WANDLYN INN', '1', 2, 170001),
(14328, 'A', '3552', 'COAST HOTELS', 'COAST HOTELS', '1', 2, 170001),
(14329, 'A', '3555', 'THE INN GROUP', 'THE INN GROUP', '1', 2, 170001),
(14330, 'A', '3558', 'JOLLY HOTELS', 'JOLLY HOTELS', '1', 2, 170001),
(14331, 'A', '3559', 'THISTLE HOTELS', 'THISTLE HOTELS', '1', 2, 170001),
(14332, 'A', '3561', 'HAAG HOTELS', 'HAAG HOTELS', '1', 2, 170001),
(14333, 'A', '3562', 'COMFORT INN', 'COMFORT INN', '1', 2, 170001),
(14334, 'A', '3563', 'JOURNEY''S END MOTELS', 'JOURNEY''S END MOTELS', '1', 2, 170001),
(14335, 'A', '3564', 'KEDDY''S HOTELS', 'KEDDY''S HOTELS', '1', 2, 170001),
(14336, 'A', '3565', 'RELAX INNS', 'RELAX INNS', '1', 2, 170001),
(14337, 'A', '3568', 'LADBROKE HOTELS', 'LADBROKE HOTELS', '1', 2, 170001),
(14338, 'A', '3570', 'FORUM HOTELS', 'FORUM HOTELS', '1', 2, 170001),
(14339, 'A', '3572', 'MIYAKO HOTEL', 'MIYAKO HOTEL', '1', 2, 170001),
(14340, 'A', '3573', 'SANDMAN HOTELS', 'SANDMAN HOTELS', '1', 2, 170001),
(14341, 'A', '3574', 'VENTURE INN', 'VENTURE INN', '1', 2, 170001),
(14342, 'A', '3575', 'VAGABOND HOTELS', 'VAGABOND HOTELS', '1', 2, 170001),
(14343, 'A', '3577', 'MANDARIAN ORIENTAL HOTEL', 'MANDARIAN ORIENTAL HOTEL', '1', 2, 170001),
(14344, 'A', '3579', 'HOTEL MERCURE', 'HOTEL MERCURE', '1', 2, 170001),
(14345, 'A', '3581', 'DELTA HOTEL', 'DELTA HOTEL', '1', 2, 170001),
(14346, 'A', '3582', 'LUCIEN BARRIERE', 'LUCIEN BARRIERE', '1', 2, 170001),
(14347, 'A', '3583', 'SAS HOTELS', 'SAS HOTELS', '1', 2, 170001),
(14348, 'A', '3584', 'PRINCESS HOTELS INTERNAT''L', 'PRINCESS HOTELS INTERNAT''L', '1', 2, 170001),
(14349, 'A', '3585', 'HUNGAR HOTELS', 'HUNGAR HOTELS', '1', 2, 170001),
(14350, 'A', '3586', 'SOKOS HOTELS', 'SOKOS HOTELS', '1', 2, 170001),
(14351, 'A', '3587', 'DORAL HOTEL', 'DORAL HOTEL', '1', 2, 170001),
(14352, 'A', '3588', 'HELMSLEY HOTEL', 'HELMSLEY HOTEL', '1', 2, 170001),
(14353, 'A', '3590', 'FAIRMONT HOTEL', 'FAIRMONT HOTEL', '1', 2, 170001),
(14354, 'A', '3591', 'SONESTA HOTEL', 'SONESTA HOTEL', '1', 2, 170001),
(14355, 'A', '3592', 'OMNI HOTEL', 'OMNI HOTEL', '1', 2, 170001),
(14356, 'A', '3593', 'CUNARD HOTELS', 'CUNARD HOTELS', '1', 2, 170001),
(14357, 'A', '3595', 'HOSPITALITY INNS', 'HOSPITALITY INNS', '1', 2, 170001),
(14358, 'A', '3598', 'REGENT INTERNATIONAL HOTELS', 'REGENT INTERNATIONAL HOTELS', '1', 2, 170001),
(14359, 'A', '3599', 'PANNONIA HOTELS', 'PANNONIA HOTELS', '1', 2, 170001),
(14360, 'A', '3603', 'NOAH''S HOTEL (MELBOURNE)', 'NOAH''S HOTEL (MELBOURNE)', '1', 2, 170001),
(14361, 'A', '3612', 'MOVENPICK', 'MOVENPICK', '1', 2, 170001),
(14362, 'A', '3615', 'TRAVELODGE', 'TRAVELODGE', '1', 2, 170001),
(14363, 'A', '3620', 'TELFORD INTERNATIONAL', 'TELFORD INTERNATIONAL', '1', 2, 170001),
(14364, 'A', '3622', 'MERLIN HOTEL (PERTH)', 'MERLIN HOTEL (PERTH)', '1', 2, 170001),
(14365, 'A', '3623', 'DORINT HOTELS', 'DORINT HOTELS', '1', 2, 170001),
(14366, 'A', '3624', 'DOYLE HOTEL', 'DOYLE HOTEL', '1', 2, 170001),
(14367, 'A', '3625', 'HOTEL UNIVERSALE', 'HOTEL UNIVERSALE', '1', 2, 170001),
(14368, 'A', '3628', 'YORK-HANOVER HOTELS', 'YORK-HANOVER HOTELS', '1', 2, 170001),
(14369, 'A', '3629', 'DAN HOTELS', 'DAN HOTELS', '1', 2, 170001),
(14370, 'A', '3631', 'ATAHOTELS', 'ATAHOTELS', '1', 2, 170001),
(14371, 'A', '3632', 'STARHOTELS', 'STARHOTELS', '1', 2, 170001),
(14372, 'A', '3633', 'RANK HOTELS', 'RANK HOTELS', '1', 2, 170001),
(14373, 'A', '3634', 'SWISSOTEL', 'SWISSOTEL', '1', 2, 170001),
(14374, 'A', '3635', 'RESO HOTELS', 'RESO HOTELS', '1', 2, 170001),
(14375, 'A', '3636', 'SAROVA HOTELS', 'SAROVA HOTELS', '1', 2, 170001),
(14376, 'A', '3637', 'RAMADA INN', 'RAMADA INN', '1', 2, 170001),
(14377, 'A', '3638', 'HOWARD JOHNSON', 'HOWARD JOHNSON', '1', 2, 170001),
(14378, 'A', '3639', 'MOUNT CHARLOTTE THISTLE', 'MOUNT CHARLOTTE THISTLE', '1', 2, 170001),
(14379, 'A', '3640', 'HYATT', 'HYATT', '1', 2, 170001),
(14380, 'A', '3641', 'SOFITEL HOTELS', 'SOFITEL HOTELS', '1', 2, 170001),
(14381, 'A', '3642', 'NOVOTEL', 'NOVOTEL', '1', 2, 170001),
(14382, 'A', '3643', 'STEIGENBERGER', 'STEIGENBERGER', '1', 2, 170001),
(14383, 'A', '3644', 'ECONOLODGE', 'ECONOLODGE', '1', 2, 170001),
(14384, 'A', '3645', 'QUEENS MOAT HOUSES', 'QUEENS MOAT HOUSES', '1', 2, 170001),
(14385, 'A', '3646', 'SWALLOW HOTELS', 'SWALLOW HOTELS', '1', 2, 170001),
(14386, 'A', '3647', 'HUSA HOTELS', 'HUSA HOTELS', '1', 2, 170001),
(14387, 'A', '3648', 'DE VERE HOTELS', 'DE VERE HOTELS', '1', 2, 170001),
(14388, 'A', '3649', 'RADISSON HOTEL', 'RADISSON HOTEL', '1', 2, 170001),
(14389, 'A', '3650', 'RED ROOF INN', 'RED ROOF INN', '1', 2, 170001),
(14390, 'A', '3651', 'IMPERIAL LONDON HOTELS', 'IMPERIAL LONDON HOTELS', '1', 2, 170001),
(14391, 'A', '3652', 'EMBASSY HOTELS', 'EMBASSY HOTELS', '1', 2, 170001),
(14392, 'A', '3653', 'PENTA HOTELS', 'PENTA HOTELS', '1', 2, 170001),
(14393, 'A', '3654', 'LOEWS HOTEL', 'LOEWS HOTEL', '1', 2, 170001),
(14394, 'A', '3655', 'SCANDIC HOTELS', 'SCANDIC HOTELS', '1', 2, 170001),
(14395, 'A', '3656', 'SARA HOTELS', 'SARA HOTELS', '1', 2, 170001),
(14396, 'A', '3657', 'OBEROI HOTELS', 'OBEROI HOTELS', '1', 2, 170001),
(14397, 'A', '3658', 'OTANI HOTEL', 'OTANI HOTEL', '1', 2, 170001),
(14398, 'A', '3659', 'TAJ HOTELS INTERNAT''L', 'TAJ HOTELS INTERNAT''L', '1', 2, 170001),
(14399, 'A', '3660', 'KNIGHTS INN', 'KNIGHTS INN', '1', 2, 170001),
(14400, 'A', '3661', 'METROPOLE HOTELS', 'METROPOLE HOTELS', '1', 2, 170001),
(14401, 'A', '3662', 'VENICE-SIMPLON ORIENT EXPR.', 'VENICE-SIMPLON ORIENT EXPR.', '1', 2, 170001),
(14402, 'A', '3663', 'HOTELES EL PRESIDENTE', 'HOTELES EL PRESIDENTE', '1', 2, 170001),
(14403, 'A', '3664', 'FLAG INN', 'FLAG INN', '1', 2, 170001),
(14404, 'A', '3665', 'HAMPTON INN', 'HAMPTON INN', '1', 2, 170001),
(14405, 'A', '3666', 'STAKIS HOTELS', 'STAKIS HOTELS', '1', 2, 170001),
(14406, 'A', '3667', 'INTOURIST', 'INTOURIST', '1', 2, 170001),
(14407, 'A', '3668', 'MARATIM', 'MARATIM', '1', 2, 170001),
(14408, 'A', '3669', 'ZIMBABWE SUN HOTELS', 'ZIMBABWE SUN HOTELS', '1', 2, 170001),
(14409, 'A', '3670', 'ARCADE', 'ARCADE', '1', 2, 170001),
(14410, 'A', '3671', 'ARCTIA', 'ARCTIA', '1', 2, 170001),
(14411, 'A', '3672', 'CAMPANILE', 'CAMPANILE', '1', 2, 170001),
(14412, 'A', '3673', 'IBUSZ HOTELS', 'IBUSZ HOTELS', '1', 2, 170001),
(14413, 'A', '3674', 'RANTASIPI HOTELS', 'RANTASIPI HOTELS', '1', 2, 170001),
(14414, 'A', '3675', 'INTERHOTEL CEDOC', 'INTERHOTEL CEDOC', '1', 2, 170001),
(14415, 'A', '3676', 'ORBIS HOTELS', 'ORBIS HOTELS', '1', 2, 170001),
(14416, 'A', '3677', 'CLIMAT DE FRANCE', 'CLIMAT DE FRANCE', '1', 2, 170001),
(14417, 'A', '3678', 'CUMULUS HOTELS', 'CUMULUS HOTELS', '1', 2, 170001),
(14418, 'A', '3679', 'DANUBIUS HOTELS', 'DANUBIUS HOTELS', '1', 2, 170001),
(14419, 'A', '3680', 'HOTEIS OTHAN', 'HOTEIS OTHAN', '1', 2, 170001),
(14420, 'A', '3681', 'ADAMS MARK', 'ADAMS MARK', '1', 2, 170001),
(14421, 'A', '3682', 'ALLSTAR INN', 'ALLSTAR INN', '1', 2, 170001),
(14422, 'A', '3683', 'BRADBURY SUITES', 'BRADBURY SUITES', '1', 2, 170001),
(14423, 'A', '3684', 'BUDGET HOST INNS', 'BUDGET HOST INNS', '1', 2, 170001),
(14424, 'A', '3685', 'BUDGETEL', 'BUDGETEL', '1', 2, 170001),
(14425, 'A', '3686', 'SUISSE CHALET', 'SUISSE CHALET', '1', 2, 170001),
(14426, 'A', '3687', 'CLARION HOTEL', 'CLARION HOTEL', '1', 2, 170001),
(14427, 'A', '3688', 'COMPRI HOTEL', 'COMPRI HOTEL', '1', 2, 170001),
(14428, 'A', '3689', 'CONSORT', 'CONSORT', '1', 2, 170001),
(14429, 'A', '3690', 'COURTYARD BY MARRIOTT', 'COURTYARD BY MARRIOTT', '1', 2, 170001),
(14430, 'A', '3691', 'DILLON INN', 'DILLON INN', '1', 2, 170001),
(14431, 'A', '3692', 'DOUBLETREE HOTEL', 'DOUBLETREE HOTEL', '1', 2, 170001),
(14432, 'A', '3693', 'DRURY INN', 'DRURY INN', '1', 2, 170001),
(14433, 'A', '3694', 'ECONOMY INNS OF AMERICA', 'ECONOMY INNS OF AMERICA', '1', 2, 170001),
(14434, 'A', '3695', 'EMBASSY SUITES', 'EMBASSY SUITES', '1', 2, 170001),
(14435, 'A', '3696', 'EXEL INN', 'EXEL INN', '1', 2, 170001),
(14436, 'A', '3697', 'FAIRFIELD HOTEL', 'FAIRFIELD HOTEL', '1', 2, 170001),
(14437, 'A', '3698', 'HARLEY HOTEL', 'HARLEY HOTEL', '1', 2, 170001),
(14438, 'A', '3699', 'MIDWAY MOTOR LODGE', 'MIDWAY MOTOR LODGE', '1', 2, 170001),
(14439, 'A', '3700', 'MOTEL 6', 'MOTEL 6', '1', 2, 170001),
(14440, 'A', '3701', 'GUEST QUARTERS', 'GUEST QUARTERS', '1', 2, 170001),
(14441, 'A', '3702', 'THE REGISTRY HOTELS', 'THE REGISTRY HOTELS', '1', 2, 170001),
(14442, 'A', '3703', 'RESIDENCE INN', 'RESIDENCE INN', '1', 2, 170001),
(14443, 'A', '3704', 'ROYCE HOTEL', 'ROYCE HOTEL', '1', 2, 170001),
(14444, 'A', '3705', 'SANDMAN INN', 'SANDMAN INN', '1', 2, 170001),
(14445, 'A', '3706', 'SHILO INN', 'SHILO INN', '1', 2, 170001),
(14446, 'A', '3707', 'SHONEY''S INN', 'SHONEY''S INN', '1', 2, 170001),
(14447, 'A', '3708', 'SIXPENCE INN', 'SIXPENCE INN', '1', 2, 170001),
(14448, 'A', '3709', 'SUPER 8 MOTEL', 'SUPER 8 MOTEL', '1', 2, 170001),
(14449, 'A', '3710', 'THE RITZ CARLTON', 'THE RITZ CARLTON', '1', 2, 170001),
(14450, 'A', '3711', 'FLAG INNS (AUSTRALIA)', 'FLAG INNS (AUSTRALIA)', '1', 2, 170001),
(14451, 'A', '3712', 'GOLDEN CHAIN', 'GOLDEN CHAIN', '1', 2, 170001),
(14452, 'A', '3713', 'QUALITY PACIFIC HOTEL', 'QUALITY PACIFIC HOTEL', '1', 2, 170001),
(14453, 'A', '3714', 'FOUR SEASONS (AUSTRALIA)', 'FOUR SEASONS (AUSTRALIA)', '1', 2, 170001),
(14454, 'A', '3715', 'FAIRFIELD INN', 'FAIRFIELD INN', '1', 2, 170001),
(14455, 'A', '3716', 'CARLTON HOTELS', 'CARLTON HOTELS', '1', 2, 170001),
(14456, 'A', '3717', 'CITY LODGE HOTELS', 'CITY LODGE HOTELS', '1', 2, 170001),
(14457, 'A', '3718', 'KAROS HOTELS', 'KAROS HOTELS', '1', 2, 170001),
(14458, 'A', '3719', 'PROTEA HOTELS', 'PROTEA HOTELS', '1', 2, 170001),
(14459, 'A', '3720', 'SOUTHERN SUN HOTELS', 'SOUTHERN SUN HOTELS', '1', 2, 170001),
(14460, 'A', '3721', 'HILTON CONRAD', 'HILTON CONRAD', '1', 2, 170001),
(14461, 'A', '3722', 'WYNDHAM HOTELS', 'WYNDHAM HOTELS', '1', 2, 170001),
(14462, 'A', '3723', 'RICA HOTELS', 'RICA HOTELS', '1', 2, 170001),
(14463, 'A', '3724', 'INTER NOR HOTELS', 'INTER NOR HOTELS', '1', 2, 170001),
(14464, 'A', '4011', 'RAILROADS', 'RAILROADS', '1', 2, 170001),
(14465, 'A', '4111', 'LOCAL COMMUTER TRANSPORT', 'LOCAL COMMUTER TRANSPORT', '1', 2, 170001),
(14466, 'A', '4112', 'PASSENGER RAILWAYS', 'PASSENGER RAILWAYS', '1', 2, 170001),
(14467, 'A', '4119', 'AMBULANCE SERVICE', 'AMBULANCE SERVICE', '1', 1, 170001),
(14468, 'A', '4121', 'TAXICABS/LIMOUSINES', 'TAXICABS/LIMOUSINES', '1', 2, 170001),
(14469, 'A', '4131', 'BUS LINES/CHARTER/TOUR', 'BUS LINES/CHARTER/TOUR', '1', 2, 170001),
(14470, 'A', '4214', 'MOTOR FREIGHT CARRIERS', 'MOTOR FREIGHT CARRIERS', '1', 1, 170001),
(14471, 'A', '4215', 'COURIER SERVICES', 'COURIER SERVICES', '1', 1, 170001),
(14472, 'A', '4225', 'PUBLIC WAREHOUSING', 'PUBLIC WAREHOUSING', '1', 1, 170001),
(14473, 'R', '4411', 'STEAMSHIP/CRUISE LINES', 'STEAMSHIP/CRUISE LINES', '1', 4, 170002),
(14474, 'A', '4457', 'BOAT RENTALS & LEASES', 'BOAT RENTALS & LEASES', '1', 1, 170001),
(14475, 'A', '4468', 'MARINAS', 'MARINAS', '1', 1, 170001),
(14476, 'A', '4511', 'OTHER AIRLINES', 'OTHER AIRLINES', '1', 2, 170001),
(14477, 'A', '4582', 'AIRPORTS/FIELDS/TERMINALS', 'AIRPORTS/FIELDS/TERMINALS', '1', 2, 170001),
(14478, 'A', '4722', 'TRAVEL AGENCIES', 'TRAVEL AGENCIES', '1', 2, 170001),
(14479, 'A', '4723', 'TUI TRAVEL AGENCY', 'TUI TRAVEL AGENCY', '1', 2, 170001),
(14480, 'A', '4761', 'TRAVEL SERVICES/MAIL/PHONE', 'TRAVEL SERVICES/MAIL/PHONE', '1', 2, 170001),
(14481, 'A', '4784', 'TOLL AND BRIDGE FEES', 'TOLL AND BRIDGE FEES', '1', 2, 170001),
(14482, 'A', '4789', 'TRANSPORTATION SVCS - DEFAULT', 'TRANSPORTATION SVCS - DEFAULT', '1', 2, 170001),
(14483, 'A', '4812', 'PHONE SERV/EQUIP NON-UTIL', 'PHONE SERV/EQUIP NON-UTIL', '1', 1, 170001),
(14484, 'A', '4814', 'PHONE SERV/EQUIP UTILITY', 'PHONE SERV/EQUIP UTILITY', '1', 1, 170001),
(14485, 'A', '4815', 'VISAPHONE', 'VISAPHONE', '1', 1, 170001),
(14486, 'A', '4816', 'COMPUTER NETWORK/INFO SVCS', 'COMPUTER NETWORK/INFO SVCS', '1', 1, 170001),
(14487, 'R', '4821', 'TELEGRAPH SERVICES', 'TELEGRAPH SERVICES', '1', 4, 170002),
(14488, 'P', '4829', 'WIRE TRANSFER - MONEY ORDER', 'WIRE TRANSFER - MONEY ORDER', '1', 4, 170003),
(14489, 'R', '4899', 'CABLE/PAY TV SERVICES', 'CABLE/PAY TV SERVICES', '1', 3, 170002),
(14490, 'R', '4900', 'UTILITIES/ELEC/GAS/H2O/SANI', 'UTILITIES/ELEC/GAS/H2O/SANI', '1', 3, 170002),
(14491, 'A', '5013', 'MOTOR VEHICLE SUPPLY/NEW PARTS', 'MOTOR VEHICLE SUPPLY/NEW PARTS', '1', 1, 170001),
(14492, 'A', '5021', 'COMMERCIAL FURNITURE', 'COMMERCIAL FURNITURE', '1', 1, 170001),
(14493, 'A', '5039', 'CONSTRUCTION MATERIALS - DEF', 'CONSTRUCTION MATERIALS - DEF', '1', 1, 170001),
(14494, 'A', '5044', 'PHOTOGRAPHIC, PHOTOCOPY EQUIP. & SUPPLIES', 'PHOTOGRAPHIC, PHOTOCOPY EQUIP. & SUPPLIES', '1', 1, 170001),
(14495, 'A', '5045', 'COMPUTERS/PERIPHERALS/SOFTWARE', 'COMPUTERS/PERIPHERALS/SOFTWARE', '1', 1, 170001),
(14496, 'A', '5046', 'COMMERCIAL EQUIPMENT - DEFAULT', 'COMMERCIAL EQUIPMENT - DEFAULT', '1', 1, 170001),
(14497, 'R', '5047', 'LAB/MED/HOSPITAL EQUIPMENT', 'LAB/MED/HOSPITAL EQUIPMENT', '1', 3, 170002),
(14498, 'A', '5051', 'METAL SERVICE CENTERS', 'METAL SERVICE CENTERS', '1', 1, 170001),
(14499, 'A', '5065', 'ELECTRICAL PARTS/EQUIPMENT', 'ELECTRICAL PARTS/EQUIPMENT', '1', 1, 170001),
(14500, 'A', '5072', 'HARDWARE EQUIPMENT/SUPPLIES', 'HARDWARE EQUIPMENT/SUPPLIES', '1', 1, 170001),
(14501, 'A', '5074', 'PLUMBING/HEATING EQUIPMENT', 'PLUMBING/HEATING EQUIPMENT', '1', 1, 170001),
(14502, 'A', '5085', 'INDUSTRIAL SUPPLIES - DEF', 'INDUSTRIAL SUPPLIES - DEF', '1', 1, 170001),
(14503, 'R', '5094', 'PRECIOUS STONES/METALS/JEWELRY', 'PRECIOUS STONES/METALS/JEWELRY', '1', 3, 170002),
(14504, 'A', '5099', 'DURABLE GOODS - DEFAULT', 'DURABLE GOODS - DEFAULT', '1', 1, 170001),
(14505, 'A', '5111', 'STATIONERY/OFFICE SUPPLIES', 'STATIONERY/OFFICE SUPPLIES', '1', 1, 170001),
(14506, 'R', '5122', 'DRUGS/DRUGGISTS SUNDRIES', 'DRUGS/DRUGGISTS SUNDRIES', '1', 3, 170002),
(14507, 'R', '5131', 'PIECE GOODS/NOTIONS/DRY GOODS', 'PIECE GOODS/NOTIONS/DRY GOODS', '1', 3, 170002),
(14508, 'A', '5137', 'UNIFORMS & COMMERCIAL CLOTHING', 'UNIFORMS & COMMERCIAL CLOTHING', '1', 1, 170001),
(14509, 'A', '5139', 'COMMERCIAL FOOTWEAR', 'COMMERCIAL FOOTWEAR', '1', 1, 170001),
(14510, 'A', '5169', 'CHEMICALS/ALLIED PRODS - DEF', 'CHEMICALS/ALLIED PRODS - DEF', '1', 1, 170001),
(14511, 'A', '5172', 'PETROLEM AND PETROLEM PRODUCTS', 'PETROLEM AND PETROLEM PRODUCTS', '1', 1, 170001),
(14512, 'A', '5192', 'BOOKS/PERIODICALS/NEWSPAPERS', 'BOOKS/PERIODICALS/NEWSPAPERS', '1', 1, 170001),
(14513, '*', '5193', 'FLORIST SUPPLIES/NURSERY STOCK', 'FLORIST SUPPLIES/NURSERY STOCK', '1', 3, 170004),
(14514, 'A', '5198', 'PAINT', 'PAINT', '1', 1, 170001),
(14515, 'A', '5199', 'NON-DURABLE GOODS - DEFAULT', 'NON-DURABLE GOODS - DEFAULT', '1', 1, 170001),
(14516, 'A', '5200', 'HOME SUPPLY WAREHOUSE STORES', 'HOME SUPPLY WAREHOUSE STORES', '1', 1, 170001),
(14517, 'A', '5211', 'LUMBER/BUILD. SUPPLY STORES', 'LUMBER/BUILD. SUPPLY STORES', '1', 1, 170001),
(14518, 'A', '5231', 'GLASS/PAINT/WALLPAPER STORE', 'GLASS/PAINT/WALLPAPER STORE', '1', 1, 170001),
(14519, 'A', '5251', 'HARDWARE STORES', 'HARDWARE STORES', '1', 1, 170001),
(14520, 'A', '5261', 'LAWN/GARDEN SUPPLY/NURSERY', 'LAWN/GARDEN SUPPLY/NURSERY', '1', 1, 170001),
(14521, 'A', '5271', 'MOBILE HOME DEALERS', 'MOBILE HOME DEALERS', '1', 1, 170001),
(14522, 'A', '5300', 'WHOLESALE CLUBS', 'WHOLESALE CLUBS', '1', 1, 170001),
(14523, 'R', '5309', 'DUTY FREE STORES', 'DUTY FREE STORES', '1', 4, 170002),
(14524, 'A', '5310', 'DISCOUNT STORES', 'DISCOUNT STORES', '1', 1, 170001),
(14525, 'R', '5311', 'DEPARTMENT STORES', 'DEPARTMENT STORES', '1', 3, 170002),
(14526, 'R', '5331', 'VARIETY STORES', 'VARIETY STORES', '1', 3, 170002),
(14527, 'A', '5399', 'MISC GEN MERCHANDISE - DEF', 'MISC GEN MERCHANDISE - DEF', '1', 1, 170001),
(14528, 'R', '5411', 'GROCERY STORES', 'GROCERY STORES', '1', 3, 170002),
(14529, 'R', '5422', 'FREEZER/MEAT LOCKERS', 'FREEZER/MEAT LOCKERS', '1', 3, 170002),
(14530, 'R', '5441', 'CANDY/NUT/CONFECTION STORE', 'CANDY/NUT/CONFECTION STORE', '1', 3, 170002),
(14531, 'R', '5451', 'DAIRY PRODUCT STORES', 'DAIRY PRODUCT STORES', '1', 3, 170002),
(14532, 'R', '5462', 'BAKERIES', 'BAKERIES', '1', 3, 170002),
(14533, 'R', '5499', 'MISC FOOD STORES - DEFAULT', 'MISC FOOD STORES - DEFAULT', '1', 3, 170002),
(14534, 'A', '5511', 'AUTOMOBILE DEALERS AND LEASING', 'AUTOMOBILE DEALERS AND LEASING', '1', 1, 170001),
(14535, 'A', '5521', 'AUTO DEALERS USED ONLY', 'AUTO DEALERS USED ONLY', '1', 1, 170001),
(14536, 'A', '5531', 'AUTO/HOME SUPPLY STORES', 'AUTO/HOME SUPPLY STORES', '1', 1, 170001),
(14537, 'A', '5532', 'AUTOMOTIVE TIRE STORES', 'AUTOMOTIVE TIRE STORES', '1', 1, 170001),
(14538, 'A', '5533', 'AUTOMOTIVE PARTS STORES', 'AUTOMOTIVE PARTS STORES', '1', 1, 170001),
(14539, 'A', '5541', 'SERVICE STATIONS', 'SERVICE STATIONS', '1', 1, 170001),
(14540, 'A', '5542', 'AUTOMATED FUEL DISPENSERS', 'AUTOMATED FUEL DISPENSERS', '1', 1, 170001),
(14541, 'A', '5551', 'BOAT DEALERS', 'BOAT DEALERS', '1', 1, 170001),
(14542, 'A', '5561', 'TRAILER CAMPER DEALER', 'TRAILER CAMPER DEALER', '1', 1, 170001),
(14543, 'A', '5571', 'MOTORCYCLE DEALERS', 'MOTORCYCLE DEALERS', '1', 1, 170001),
(14544, 'A', '5592', 'MOTOR HOME DEALERS', 'MOTOR HOME DEALERS', '1', 1, 170001),
(14545, 'R', '5598', 'SNOWMOBILE DEALERS', 'SNOWMOBILE DEALERS', '1', 4, 170002),
(14546, 'A', '5599', 'MISC AUTO DEALERS - DEFAULT', 'MISC AUTO DEALERS - DEFAULT', '1', 1, 170001),
(14547, 'R', '5611', 'MEN/BOYS CLOTHING/ACC STORES', 'MEN/BOYS CLOTHING/ACC STORES', '1', 4, 170002),
(14548, 'R', '5621', 'WOMENS READY TO WEAR STORES', 'WOMENS READY TO WEAR STORES', '1', 4, 170002),
(14549, 'R', '5631', 'WOMENS ACCESS/SPECIALTY', 'WOMENS ACCESS/SPECIALTY', '1', 4, 170002),
(14550, 'R', '5641', 'CHILDREN/INFANTS WEAR STORE', 'CHILDREN/INFANTS WEAR STORE', '1', 4, 170002),
(14551, 'R', '5651', 'FAMILY CLOTHING STORES', 'FAMILY CLOTHING STORES', '1', 4, 170002),
(14552, 'R', '5655', 'SPORTS/RIDING APPAREL STORE', 'SPORTS/RIDING APPAREL STORE', '1', 4, 170002),
(14553, 'A', '5661', 'SHOE STORES', 'SHOE STORES', '1', 4, 170001),
(14554, 'R', '5681', 'FURRIERS AND FUR SHOPS', 'FURRIERS AND FUR SHOPS', '1', 4, 170002),
(14555, 'R', '5691', 'MENS/WOMENS CLOTHING STORES', 'MENS/WOMENS CLOTHING STORES', '1', 4, 170002),
(14556, 'R', '5697', 'TAILOR/SEAMSTRESS/ALTERS', 'TAILOR/SEAMSTRESS/ALTERS', '1', 4, 170002),
(14557, 'R', '5698', 'WIG AND TOUPEE STORES', 'WIG AND TOUPEE STORES', '1', 4, 170002),
(14558, 'R', '5699', 'MISC APPAREL/ACCESS SHOPS', 'MISC APPAREL/ACCESS SHOPS', '1', 4, 170002),
(14559, 'A', '5712', 'FURNITURE/EQUIP STORES', 'FURNITURE/EQUIP STORES', '1', 1, 170001),
(14560, 'A', '5713', 'FLOOR COVERING STORES', 'FLOOR COVERING STORES', '1', 1, 170001),
(14561, 'R', '5714', 'DRAPERY & UPHOLSTERY STORES', 'DRAPERY & UPHOLSTERY STORES', '1', 3, 170002),
(14562, 'R', '5718', 'FIREPLACES & ACCESSORIES', 'FIREPLACES & ACCESSORIES', '1', 3, 170002),
(14563, 'R', '5719', 'MISC HOME FURNISHING', 'MISC HOME FURNISHING', '1', 3, 170002),
(14564, 'R', '5722', 'HOUSEHOLD APPLIANCE STORES', 'HOUSEHOLD APPLIANCE STORES', '1', 3, 170002),
(14565, 'A', '5732', 'ELECTRONICS SALES', 'ELECTRONICS SALES', '1', 1, 170001),
(14566, 'R', '5733', 'MUSIC STORES/PIANOS', 'MUSIC STORES/PIANOS', '1', 4, 170002),
(14567, 'A', '5734', 'COMPUTER SOFTWARE STORES', 'COMPUTER SOFTWARE STORES', '1', 1, 170001),
(14568, 'R', '5735', 'RECORD SHOPS', 'RECORD SHOPS', '1', 3, 170002),
(14569, 'R', '5811', 'CATERERS', 'CATERERS', '1', 3, 170002),
(14570, 'R', '5812', 'OTHER RESTAURANTS', 'OTHER RESTAURANTS', '1', 3, 170002),
(14571, 'R', '5813', 'BARS/TAVERNS/LOUNGES/DISCOS', 'BARS/TAVERNS/LOUNGES/DISCOS', '1', 3, 170002),
(14572, 'R', '5814', 'FAST FOOD RESTAURANTS', 'FAST FOOD RESTAURANTS', '1', 3, 170002),
(14573, 'R', '5912', 'DRUG STORES & PHARMACIES', 'DRUG STORES & PHARMACIES', '1', 3, 170002),
(14574, 'R', '5921', 'PKG STORES/BEER/WINE/LIQUOR', 'PKG STORES/BEER/WINE/LIQUOR', '1', 3, 170002),
(14575, 'R', '5931', 'USED MERCHANDISE STORES', 'USED MERCHANDISE STORES', '1', 3, 170002),
(14576, 'R', '5932', 'ANTIQUE SHOPS', 'ANTIQUE SHOPS', '1', 3, 170002),
(14577, 'R', '5933', 'PAWN SHOPS', 'PAWN SHOPS', '1', 3, 170002),
(14578, 'A', '5935', 'WRECKING SALVAGE YARDS', 'WRECKING SALVAGE YARDS', '1', 1, 170001),
(14579, 'R', '5937', 'ANTIQUE REPRODUCTIONS', 'ANTIQUE REPRODUCTIONS', '1', 4, 170002),
(14580, 'A', '5940', 'BICYCLE SHOPS/SALES/SERVICE', 'BICYCLE SHOPS/SALES/SERVICE', '1', 1, 170001),
(14581, 'R', '5941', 'SPORTING GOODS STORES', 'SPORTING GOODS STORES', '1', 4, 170002),
(14582, 'A', '5942', 'BOOK STORES', 'BOOK STORES', '1', 1, 170001),
(14583, 'A', '5943', 'STATIONERY STORES', 'STATIONERY STORES', '1', 1, 170001),
(14584, 'R', '5944', 'JEWELRY STORES', 'JEWELRY STORES', '1', 3, 170002),
(14585, 'R', '5945', 'HOBBY', 'HOBBY', '1', 3, 170002),
(14586, 'A', '5946', 'CAMERA & PHOTO SUPPLY STORE', 'CAMERA & PHOTO SUPPLY STORE', '1', 1, 170001),
(14587, 'R', '5947', 'GIFT', 'GIFT', '1', 3, 170002),
(14588, 'R', '5948', 'LUGGAGE/LEATHER STORES', 'LUGGAGE/LEATHER STORES', '1', 3, 170002),
(14589, 'R', '5949', 'FABRIC STORES', 'FABRIC STORES', '1', 3, 170002),
(14590, 'R', '5950', 'GLASSWARE/CRYSTAL STORES', 'GLASSWARE/CRYSTAL STORES', '1', 3, 170002),
(14591, 'R', '5960', 'DIRECT MARKETING INSURANCE SVC', 'DIRECT MARKETING INSURANCE SVC', '1', 3, 170002),
(14592, 'R', '5962', 'DIRECT MKTG-TRAVEL RELATED ARR', 'DIRECT MKTG-TRAVEL RELATED ARR', '1', 3, 170002),
(14593, 'R', '5963', 'DIRECT SELL/DOOR-TO-DOOR', 'DIRECT SELL/DOOR-TO-DOOR', '1', 3, 170002),
(14594, 'A', '5964', 'CATALOG MERCHANT', 'CATALOG MERCHANT', '1', 1, 170001),
(14595, 'A', '5965', 'COMBINATION CATALOG & RETAIL', 'COMBINATION CATALOG & RETAIL', '1', 1, 170001),
(14596, 'R', '5966', 'OUTBOUND TELEMARKETING MERCHNT', 'OUTBOUND TELEMARKETING MERCHNT', '1', 3, 170002),
(14597, 'R', '5967', 'INBOUND TELESERVICES MERCHANT', 'INBOUND TELESERVICES MERCHANT', '1', 3, 170002),
(14598, 'A', '5968', 'CONTINUITY/SUBSCRIPTION MERCHT', 'CONTINUITY/SUBSCRIPTION MERCHT', '1', 1, 170001),
(14599, 'A', '5969', 'DIRECT MARKETERS - OTHER', 'DIRECT MARKETERS - OTHER', '1', 3, 170001),
(14600, 'R', '5970', 'ARTIST/CRAFT SHOPS', 'ARTIST/CRAFT SHOPS', '1', 3, 170002),
(14601, 'R', '5971', 'ART DEALERS & GALLERIES', 'ART DEALERS & GALLERIES', '1', 3, 170002),
(14602, 'R', '5972', 'STAMP & COIN STORES', 'STAMP & COIN STORES', '1', 3, 170002),
(14603, 'R', '5973', 'RELIGIOUS GOODS STORES', 'RELIGIOUS GOODS STORES', '1', 3, 170002),
(14604, 'A', '5974', 'RUBBER STAMP STORES', 'RUBBER STAMP STORES', '1', 1, 170001),
(14605, 'R', '5975', 'HEARING AID/SALES/SERVICE', 'HEARING AID/SALES/SERVICE', '1', 4, 170002),
(14606, 'R', '5976', 'ORTHOPEDIC GOODS', 'ORTHOPEDIC GOODS', '1', 4, 170002),
(14607, 'R', '5977', 'COSMETIC STORES', 'COSMETIC STORES', '1', 4, 170002),
(14608, 'A', '5978', 'TYPEWRITER/SALES/SERVICE', 'TYPEWRITER/SALES/SERVICE', '1', 1, 170001),
(14609, 'A', '5983', 'FUEL DEALERS', 'FUEL DEALERS', '1', 4, 170001),
(14610, '*', '5992', 'FLORISTS', 'FLORISTS', '1', 4, 170004),
(14611, 'R', '5993', 'CIGAR STORES/STANDS', 'CIGAR STORES/STANDS', '1', 4, 170002),
(14612, 'A', '5994', 'NEWS DEALERS/NEWSSTANDS', 'NEWS DEALERS/NEWSSTANDS', '1', 4, 170001),
(14613, 'A', '5995', 'PET STORES/FOOD & SUPPLY', 'PET STORES/FOOD & SUPPLY', '1', 1, 170001),
(14614, 'R', '5996', 'SWIMMING POOLS/SALES/SERV', 'SWIMMING POOLS/SALES/SERV', '1', 4, 170002),
(14615, 'R', '5997', 'ELEC RAZOR STORES/SALE/SERV', 'ELEC RAZOR STORES/SALE/SERV', '1', 4, 170002),
(14616, 'A', '5998', 'TENT AND AWNING SHOPS', 'TENT AND AWNING SHOPS', '1', 1, 170001),
(14617, 'A', '5999', 'MISC SPECIALITY RETAIL', 'MISC SPECIALITY RETAIL', '1', 3, 170001),
(14618, 'P', '6010', 'FINANCIAL INST/MANUAL CASH', 'FINANCIAL INST/MANUAL CASH', '1', 4, 170003),
(14619, 'P', '6011', 'FINANCIAL INST/AUTO CASH', 'FINANCIAL INST/AUTO CASH', '1', 4, 170003),
(14620, 'P', '6012', 'FINANCIAL INST/MERCHANDISE', 'FINANCIAL INST/MERCHANDISE', '1', 4, 170003),
(14621, 'P', '6051', 'NON-FIN INST/FC/MO/TC/STAMPS', 'NON-FIN INST/FC/MO/TC/STAMPS', '1', 4, 170003),
(14622, 'R', '6211', 'SECURITY BROKERS/DEALERS', 'SECURITY BROKERS/DEALERS', '1', 4, 170002),
(14623, 'R', '6300', 'INSURANCE SALES/UNDERWRITE', 'INSURANCE SALES/UNDERWRITE', '1', 4, 170002),
(14624, 'R', '6381', 'INSURANCE PREMIUMS', 'INSURANCE PREMIUMS', '1', 4, 170002),
(14625, 'R', '6399', 'INSURANCE - DEFAULT', 'INSURANCE - DEFAULT', '1', 4, 170002),
(14626, 'R', '6611', 'OVERPAYMENTS', 'OVERPAYMENTS', '1', 4, 170002),
(14627, 'R', '6760', 'SAVINGS BONDS', 'SAVINGS BONDS', '1', 4, 170002),
(14628, 'A', '7011', 'HOTELS/MOTELS/RESORTS', 'HOTELS/MOTELS/RESORTS', '1', 2, 170001),
(14629, 'R', '7012', 'TIMESHARES', 'TIMESHARES', '1', 4, 170002),
(14630, 'R', '7032', 'SPORT/RECREATIONAL CAMPS', 'SPORT/RECREATIONAL CAMPS', '1', 4, 170002),
(14631, 'R', '7033', 'TRAILER PARKS/CAMP SITES', 'TRAILER PARKS/CAMP SITES', '1', 4, 170002),
(14632, 'A', '7210', 'LAUNDRY/CLEANING/GARMENT SV', 'LAUNDRY/CLEANING/GARMENT SV', '1', 1, 170001),
(14633, 'R', '7211', 'LAUNDRIES-FAMILY/COMMERCIAL', 'LAUNDRIES-FAMILY/COMMERCIAL', '1', 1, 170002),
(14634, 'A', '7216', 'DRY CLEANERS', 'DRY CLEANERS', '1', 1, 170001),
(14635, 'A', '7217', 'CARPET/UPHOLSTERY CLEANING', 'CARPET/UPHOLSTERY CLEANING', '1', 1, 170001),
(14636, 'A', '7221', 'PHOTO STUDIOS', 'PHOTO STUDIOS', '1', 1, 170001),
(14637, 'R', '7230', 'BARBER/BEAUTY SHOPS', 'BARBER/BEAUTY SHOPS', '1', 4, 170002),
(14638, 'R', '7251', 'SHOE REPAIR/SHINE/HAT CLEAN', 'SHOE REPAIR/SHINE/HAT CLEAN', '1', 4, 170002),
(14639, 'R', '7261', 'FUNERAL SERVICE/CREMATORIES', 'FUNERAL SERVICE/CREMATORIES', '1', 4, 170002),
(14640, 'R', '7273', 'DATING & ESCORT SERVICES', 'DATING & ESCORT SERVICES', '1', 4, 170002),
(14641, 'R', '7276', 'TAX PREPARATION SERVICE', 'TAX PREPARATION SERVICE', '1', 4, 170002),
(14642, 'R', '7277', 'COUNSELING SERVICE - ALL', 'COUNSELING SERVICE - ALL', '1', 4, 170002),
(14643, 'R', '7278', 'BUYING/SHOPPING SERVICES', 'BUYING/SHOPPING SERVICES', '1', 4, 170002),
(14644, 'R', '7280', 'HOSPITAL PATIENTS FUNDS', 'HOSPITAL PATIENTS FUNDS', '1', 4, 170002),
(14645, 'R', '7295', 'BABYSITTING SERVICES', 'BABYSITTING SERVICES', '1', 4, 170002),
(14646, 'R', '7296', 'CLOTHING/RENT/COSTUME/UNIFO', 'CLOTHING/RENT/COSTUME/UNIFO', '1', 4, 170002),
(14647, 'R', '7297', 'MASSAGE PARLORS', 'MASSAGE PARLORS', '1', 4, 170002),
(14648, 'R', '7298', 'HEALTH & BEAUTY SPAS', 'HEALTH & BEAUTY SPAS', '1', 4, 170002),
(14649, 'A', '7299', 'MISC PERSONAL SERV - DEF', 'MISC PERSONAL SERV - DEF', '1', 4, 170001),
(14650, 'R', '7311', 'ADVERTISING SERVICES', 'ADVERTISING SERVICES', '1', 3, 170002),
(14651, 'R', '7321', 'CONSUMER CR REPORTING AGEN', 'CONSUMER CR REPORTING AGEN', '1', 3, 170002),
(14652, 'A', '7332', 'BLUEPRINT/PHOTOCOPY SERVICE', 'BLUEPRINT/PHOTOCOPY SERVICE', '1', 1, 170001),
(14653, 'A', '7333', 'COMMERCIAL PHOTO/ART/GRAPH', 'COMMERCIAL PHOTO/ART/GRAPH', '1', 1, 170001),
(14654, 'A', '7338', 'QUICK COPY/REPRO SERVICES', 'QUICK COPY/REPRO SERVICES', '1', 1, 170001),
(14655, 'A', '7339', 'STENOGRAPHIC SERVICES', 'STENOGRAPHIC SERVICES', '1', 1, 170001),
(14656, 'A', '7341', 'WINDOW CLEANING SERVICES', 'WINDOW CLEANING SERVICES', '1', 1, 170001),
(14657, 'A', '7342', 'DISINFECT/EXTERMINATE SERV', 'DISINFECT/EXTERMINATE SERV', '1', 1, 170001),
(14658, 'A', '7349', 'CLEAN/MAINT/JANITORAL SERV', 'CLEAN/MAINT/JANITORAL SERV', '1', 1, 170001),
(14659, 'A', '7361', 'EMPLOYMENT/TEMP HELP AGEN', 'EMPLOYMENT/TEMP HELP AGEN', '1', 1, 170001),
(14660, 'A', '7372', 'COMPUTER PROGRAM/SYS DESIGN', 'COMPUTER PROGRAM/SYS DESIGN', '1', 1, 170001),
(14661, 'A', '7375', 'INFORMATION RETRIEVAL SERVICES', 'INFORMATION RETRIEVAL SERVICES', '1', 1, 170001),
(14662, 'A', '7379', 'COMPUTER MAINT/SVCS - DEF', 'COMPUTER MAINT/SVCS - DEF', '1', 1, 170001),
(14663, 'A', '7392', 'MGMT/CONSULT/PUBLIC REL SER', 'MGMT/CONSULT/PUBLIC REL SER', '1', 1, 170001),
(14664, 'R', '7393', 'DETECTIVE/PROTECTIVE AGEN', 'DETECTIVE/PROTECTIVE AGEN', '1', 4, 170002),
(14665, 'A', '7394', 'EQUIP/FURN RENT/LEASE SERV', 'EQUIP/FURN RENT/LEASE SERV', '1', 1, 170001),
(14666, 'A', '7395', 'PHOTOFINISH LABS/DEV', 'PHOTOFINISH LABS/DEV', '1', 1, 170001),
(14667, 'A', '7399', 'BUSINESS SERVICES - DEFAULT', 'BUSINESS SERVICES - DEFAULT', '1', 3, 170001);
INSERT INTO `lkp_tb_industrytypes` (`IndustryTypeId`, `IAllowable`, `IndustryTypeCode`, `Name`, `Description`, `IsActive`, `Group`, `StatGuideLineType`) VALUES
(14668, 'A', '7512', 'OTHER CAR RENTALS', 'OTHER CAR RENTALS', '1', 1, 170001),
(14669, 'A', '7513', 'TRUCK/TRAILER RENTALS', 'TRUCK/TRAILER RENTALS', '1', 1, 170001),
(14670, 'A', '7519', 'MOTOR HOME/RV RENTALS', 'MOTOR HOME/RV RENTALS', '1', 1, 170001),
(14671, 'A', '7523', 'AUTO PARKING LOTS/GARAGES', 'AUTO PARKING LOTS/GARAGES', '1', 2, 170001),
(14672, 'A', '7524', 'PARKING LOTS (EPS)', 'PARKING LOTS (EPS)', '1', 2, 170001),
(14673, 'A', '7531', 'AUTO BODY REPAIR SHOPS', 'AUTO BODY REPAIR SHOPS', '1', 1, 170001),
(14674, 'A', '7534', 'TIRE RETREAD/REPAIR SHOPS', 'TIRE RETREAD/REPAIR SHOPS', '1', 1, 170001),
(14675, 'A', '7535', 'AUTO PAINT SHOPS', 'AUTO PAINT SHOPS', '1', 1, 170001),
(14676, 'A', '7538', 'AUTO SERVICE SHOPS/NON DEALER', 'AUTO SERVICE SHOPS/NON DEALER', '1', 1, 170001),
(14677, 'A', '7542', 'CAR WASHES', 'CAR WASHES', '1', 1, 170001),
(14678, 'A', '7549', 'TOWING SERVICES', 'TOWING SERVICES', '1', 1, 170001),
(14679, 'A', '7622', 'RADIO/TV/STEREO REPAIR SHOP', 'RADIO/TV/STEREO REPAIR SHOP', '1', 1, 170001),
(14680, 'A', '7623', 'AIR COND/REFRIG REPAIR SHOP', 'AIR COND/REFRIG REPAIR SHOP', '1', 1, 170001),
(14681, 'A', '7629', 'SMALL APPLIANCE REPAIR DEF', 'SMALL APPLIANCE REPAIR DEF', '1', 1, 170001),
(14682, 'R', '7631', 'WATCH/CLOCK/JEWELRY REPAIR', 'WATCH/CLOCK/JEWELRY REPAIR', '1', 3, 170002),
(14683, 'R', '7641', 'REUPHOLSTERY/REFINISH', 'REUPHOLSTERY/REFINISH', '1', 3, 170002),
(14684, 'A', '7692', 'WELDING', 'WELDING', '1', 1, 170001),
(14685, 'A', '7699', 'MISC REPAIR SERVICES', 'MISC REPAIR SERVICES', '1', 1, 170001),
(14686, 'A', '7829', 'FILMS/VIDEO PRODUCTION/DIST', 'FILMS/VIDEO PRODUCTION/DIST', '1', 1, 170001),
(14687, 'R', '7832', 'MOTION PICTURE THEATRES', 'MOTION PICTURE THEATRES', '1', 4, 170002),
(14688, 'R', '7833', 'MOTION PICTURE THEATRES (EPS)', 'MOTION PICTURE THEATRES (EPS)', '1', 4, 170002),
(14689, 'R', '7841', 'VIDEO TAPE RENTAL STORES', 'VIDEO TAPE RENTAL STORES', '1', 4, 170002),
(14690, 'R', '7911', 'DANCE HALLS/STUDIOS/SCHOOLS', 'DANCE HALLS/STUDIOS/SCHOOLS', '1', 4, 170002),
(14691, 'R', '7922', 'THEATRICAL PRODUCERS', 'THEATRICAL PRODUCERS', '1', 4, 170002),
(14692, 'R', '7929', 'BANDS/ORCHESTRAS/ENTERTAIN', 'BANDS/ORCHESTRAS/ENTERTAIN', '1', 4, 170002),
(14693, 'R', '7932', 'BILLIARD/POOL ESTABLISHMENT', 'BILLIARD/POOL ESTABLISHMENT', '1', 4, 170002),
(14694, 'R', '7933', 'BOWLING ALLEYS', 'BOWLING ALLEYS', '1', 4, 170002),
(14695, 'R', '7941', 'COMMERCIAL/PRO SPORTS', 'COMMERCIAL/PRO SPORTS', '1', 4, 170002),
(14696, 'R', '7991', 'TOURIST ATTRACTIONS AND XHBT', 'TOURIST ATTRACTIONS AND XHBT', '1', 4, 170002),
(14697, 'R', '7992', 'PUBLIC GOLF COURSES', 'PUBLIC GOLF COURSES', '1', 4, 170002),
(14698, 'R', '7993', 'VIDEO AMUSEMENT GAME SUPPLY', 'VIDEO AMUSEMENT GAME SUPPLY', '1', 4, 170002),
(14699, 'R', '7994', 'VIDEO GAME ARCADES/ESTABLISH', 'VIDEO GAME ARCADES/ESTABLISH', '1', 4, 170002),
(14700, 'R', '7995', 'BETTING/TRACK/CASINO/LOTTO', 'BETTING/TRACK/CASINO/LOTTO', '1', 4, 170002),
(14701, 'R', '7996', 'AMUSEMENT PARKS/CIRCUS', 'AMUSEMENT PARKS/CIRCUS', '1', 4, 170002),
(14702, 'R', '7997', 'MEMBER CLUBS/SPORT/REC/GOLF', 'MEMBER CLUBS/SPORT/REC/GOLF', '1', 4, 170002),
(14703, 'R', '7998', 'AQUARIUMS/SEAQUARIUMS', 'AQUARIUMS/SEAQUARIUMS', '1', 4, 170002),
(14704, 'R', '7999', 'AMUSEMENT/REC SERV - DEF', 'AMUSEMENT/REC SERV - DEF', '1', 4, 170002),
(14705, '^', '8011', 'DOCTORS', 'DOCTORS', '1', 3, 170005),
(14706, 'R', '8021', 'DENTISTS/ORTHODONTISIS', 'DENTISTS/ORTHODONTISIS', '1', 3, 170002),
(14707, 'R', '8031', 'OSTEOPATHS', 'OSTEOPATHS', '1', 3, 170002),
(14708, '^', '8041', 'CHIROPRACTORS', 'CHIROPRACTORS', '1', 3, 170005),
(14709, '^', '8042', 'OPTOMETRISTS/OPHTHALMOLOGISTS', 'OPTOMETRISTS/OPHTHALMOLOGISTS', '1', 3, 170005),
(14710, 'R', '8043', 'OPTICIANS', 'OPTICIANS', '1', 3, 170002),
(14711, 'R', '8044', 'OPTICAL GOODS & GLASSES', 'OPTICAL GOODS & GLASSES', '1', 3, 170002),
(14712, '^', '8049', 'CHIROPODISTS/PODIATRISTS', 'CHIROPODISTS/PODIATRISTS', '1', 3, 170005),
(14713, 'R', '8050', 'NURSING/PERSONAL CARE FAC', 'NURSING/PERSONAL CARE FAC', '1', 3, 170002),
(14714, '^', '8062', 'HOSPITALS', 'HOSPITALS', '1', 3, 170005),
(14715, 'R', '8071', 'MEDICAL/DENTAL LABS', 'MEDICAL/DENTAL LABS', '1', 3, 170002),
(14716, 'R', '8099', 'MED/HEALTH SERVICES - DEF', 'MED/HEALTH SERVICES - DEF', '1', 3, 170002),
(14717, 'R', '8111', 'LEGAL SERVICES ATTORNEYS', 'LEGAL SERVICES ATTORNEYS', '1', 3, 170002),
(14718, 'R', '8211', 'ELEMENTARY/SECONDARY SCHOOL', 'ELEMENTARY/SECONDARY SCHOOL', '1', 3, 170002),
(14719, 'A', '8220', 'COLLEGES/UNIV/JC/PROFESSION', 'COLLEGES/UNIV/JC/PROFESSION', '1', 3, 170001),
(14720, 'R', '8241', 'CORRESPONDENCE SCHOOLS', 'CORRESPONDENCE SCHOOLS', '1', 3, 170002),
(14721, 'R', '8244', 'BUSINESS/SECRETARIAL SCHOOL', 'BUSINESS/SECRETARIAL SCHOOL', '1', 3, 170002),
(14722, 'R', '8249', 'VOCATIONAL/TRADE SCHOOLS', 'VOCATIONAL/TRADE SCHOOLS', '1', 3, 170002),
(14723, 'A', '8299', 'SCHOOLS - DEFAULT', 'SCHOOLS - DEFAULT', '1', 3, 170001),
(14724, 'R', '8351', 'CHILD CARE SERVICES', 'CHILD CARE SERVICES', '1', 3, 170002),
(14725, 'A', '8398', 'CHARITABLE/SOC SERVICE ORGS', 'CHARITABLE/SOC SERVICE ORGS', '1', 3, 170001),
(14726, 'A', '8641', 'CIVIC/SOCIAL/FRATERNAL ASSC', 'CIVIC/SOCIAL/FRATERNAL ASSC', '1', 3, 170001),
(14727, 'R', '8651', 'POLITICAL ORGANIZATIONS', 'POLITICAL ORGANIZATIONS', '1', 3, 170002),
(14728, 'R', '8661', 'RELIGIOUS ORGANIZATIONS', 'RELIGIOUS ORGANIZATIONS', '1', 3, 170002),
(14729, 'R', '8675', 'AUTO ASSOCIATIONS', 'AUTO ASSOCIATIONS', '1', 3, 170002),
(14730, 'A', '8699', 'MEMBER ORGANIZATIONS - DEF', 'MEMBER ORGANIZATIONS - DEF', '1', 1, 170001),
(14731, 'R', '8734', 'TESTING LABS (NON-MEDICAL)', 'TESTING LABS (NON-MEDICAL)', '1', 3, 170002),
(14732, 'R', '8911', 'ARCHITECTURAL/ENG/SURVEY', 'ARCHITECTURAL/ENG/SURVEY', '1', 3, 170002),
(14733, 'R', '8931', 'ACCOUNTANTS/AUDITORS/BOOKPR', 'ACCOUNTANTS/AUDITORS/BOOKPR', '1', 3, 170002),
(14734, 'R', '8999', 'PROFESSIONAL SERVICES - DEF', 'PROFESSIONAL SERVICES - DEF', '1', 3, 170002),
(14735, 'A', '9211', 'COURT COSTS/ALIMONY/SUPPORT', 'COURT COSTS/ALIMONY/SUPPORT', '1', 3, 170001),
(14736, 'R', '9222', 'FINES', 'FINES', '1', 3, 170002),
(14737, 'R', '9223', 'BAIL AND BOND PAYMENTS', 'BAIL AND BOND PAYMENTS', '1', 3, 170002),
(14738, 'R', '9311', 'TAX PAYMENTS', 'TAX PAYMENTS', '1', 3, 170002),
(14739, 'R', '9399', 'GOV''T SERV - DEFAULT', 'GOV''T SERV - DEFAULT', '1', 3, 170002),
(14740, 'A', '9402', 'POSTAGE STAMPS', 'POSTAGE STAMPS', '1', 1, 170001);

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_languages`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_languages` (
  `LanguageID` int(11) NOT NULL,
  `LanguageName` varchar(100) NOT NULL,
  `CharSet` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`LanguageID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `lkp_tb_languages`
--

INSERT INTO `lkp_tb_languages` (`LanguageID`, `LanguageName`, `CharSet`, `IsActive`, `InsertDate`, `InsertUserID`, `ModifyUserID`, `ModifyDate`) VALUES
(1, 'English', 'ANSI', 1, '2014-08-31 00:00:00', 1, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_modules`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_modules` (
  `moduleid` int(11) NOT NULL AUTO_INCREMENT,
  `modulename` varchar(200) DEFAULT NULL,
  `parentmodule` varchar(200) DEFAULT NULL,
  `isactive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`moduleid`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `lkp_tb_modules`
--

INSERT INTO `lkp_tb_modules` (`moduleid`, `modulename`, `parentmodule`, `isactive`) VALUES
(1, 'User Management', NULL, b'1'),
(2, 'Add User', '1', b'1'),
(3, 'Add Group', '1', b'1'),
(4, 'Add Group Permissions', '1', b'1');

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_notetypes`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_notetypes` (
  `NoteTypeId` int(11) NOT NULL,
  `Description` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`NoteTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `lkp_tb_notetypes`
--

INSERT INTO `lkp_tb_notetypes` (`NoteTypeId`, `Description`, `IsActive`, `InsertUserId`, `InsertDate`, `ModifyUserId`, `ModifyDate`) VALUES
(500001, 'Prequal Notes', 1, 1, '2014-09-16 00:00:00', NULL, NULL),
(510001, 'Contract Notes', 1, 1, '2014-09-16 00:00:00', NULL, NULL),
(520001, 'Renewal Notes', 1, 1, '2014-09-16 00:00:00', NULL, NULL),
(530001, 'General Notes', 1, 1, '2014-09-16 00:00:00', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_processorlist`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_processorlist` (
  `ProcessorId` smallint(6) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`ProcessorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `lkp_tb_processorlist`
--

INSERT INTO `lkp_tb_processorlist` (`ProcessorId`, `Name`, `IsActive`) VALUES
(10001, 'CardNet', 1),
(10002, 'VisaNet', 1);

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_province`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_province` (
  `StateID` int(11) NOT NULL AUTO_INCREMENT,
  `CountryID` int(11) DEFAULT NULL,
  `State` varchar(250) DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`StateID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=33 ;

--
-- Dumping data for table `lkp_tb_province`
--

INSERT INTO `lkp_tb_province` (`StateID`, `CountryID`, `State`, `IsActive`) VALUES
(1, 1, 'Azua', b'1'),
(2, 1, 'Bahoruco', b'1'),
(3, 1, 'Barahona', b'1'),
(4, 1, 'Dajabón', b'1'),
(5, 1, 'Duarte', b'1'),
(6, 1, 'Elías Piña', b'1'),
(7, 1, 'El Seibo', b'1'),
(8, 1, 'Espaillat', b'1'),
(9, 1, 'Hato Mayor', b'1'),
(10, 1, 'Independencia', b'1'),
(11, 1, 'La Altagracia', b'1'),
(12, 1, 'La Romana', b'1'),
(13, 1, 'La Vega', b'1'),
(14, 1, 'María Trinidad Sánchez', b'1'),
(15, 1, 'Monseñor Nouel', b'1'),
(16, 1, 'Montecristi', b'1'),
(17, 1, 'Monte Plata', b'1'),
(18, 1, 'Pedernales', b'1'),
(19, 1, 'Peravia', b'1'),
(20, 1, 'Puerto Plata', b'1'),
(21, 1, 'Hermanas Mirabal', b'1'),
(22, 1, 'Samaná', b'1'),
(23, 1, 'Sánchez Ramírez', b'1'),
(24, 1, 'San Cristóbal', b'1'),
(25, 1, 'San José de Ocoa', b'1'),
(26, 1, 'San Juan', b'1'),
(27, 1, 'San Pedro de Macorís', b'1'),
(28, 1, 'Santiago', b'1'),
(29, 1, 'Santiago Rodríguez', b'1'),
(30, 1, 'Santo Domingo', b'1'),
(31, 1, 'Valverde', b'1'),
(32, 1, 'Distrito Nacional', b'1');

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_statuses`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_statuses` (
  `StatusId` int(11) NOT NULL,
  `StatusTypeId` varchar(10) NOT NULL,
  `StatusName` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`StatusId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `lkp_tb_statuses`
--

INSERT INTO `lkp_tb_statuses` (`StatusId`, `StatusTypeId`, `StatusName`, `IsActive`) VALUES
(10001, 'MRC', 'Inactive', 1),
(10002, 'MRC', 'Contract being paid on time', 1),
(10003, 'MRC', 'Contract being paid faster than expected', 1),
(10004, 'MRC', 'Contract being paid slower than expected', 1),
(10005, 'MRC', 'Investigation', 1),
(10006, 'MRC', 'Collection', 1),
(20001, 'CNT', 'Prequalification', 1),
(20002, 'CNT', 'Document Gathering', 1),
(20003, 'CNT', 'Processing Documents', 1),
(20004, 'CNT', 'Review', 1),
(20005, 'CNT', 'Contarct Approval', 1),
(20006, 'CNT', 'Contract being Signed', 1),
(20007, 'CNT', 'Funded', 1),
(20008, 'CNT', 'Contracts Being Signed', 1),
(20009, 'CNT', 'Written Off', 1),
(20101, 'CNT', 'In Renewal Proccess', 1),
(20102, 'CNT', 'In Review', 1),
(20103, 'CNT', 'Renewal Waiting for Approval', 1),
(20104, 'CNT', 'Contracts Being Signed', 1),
(22001, 'TSK', 'Unassigned', 1),
(22002, 'TSK', 'Assigned', 1),
(22003, 'TSK', 'CompleteByUser', 1),
(22004, 'TSK', 'Complete', 1),
(180001, 'COL', 'New collection', 1),
(180002, 'COL', 'In Investigation', 1),
(180003, 'COL', 'Payment Agreement', 1),
(180004, 'COL', 'Collections – External', 1);

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_types`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_types` (
  `TypeId` int(11) NOT NULL,
  `Usage` varchar(5) NOT NULL,
  `Description` varchar(45) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`TypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `lkp_tb_types`
--

INSERT INTO `lkp_tb_types` (`TypeId`, `Usage`, `Description`, `IsActive`) VALUES
(13100, 'CNT', 'New Contracts', b'1'),
(13200, 'CNT', 'Renewal Contracts', b'1'),
(170001, 'MCC', 'Allowable', b'1'),
(170002, 'MCC', 'Restricted until Approved ', b'1'),
(170003, 'MCC', 'Prohibited by Contract', b'1'),
(170004, 'MCC', 'Restricted until Approved Open for DMSFACMAIN', b'1'),
(170005, 'MCC', 'Restricted until Approval Open for DMSGENCOUN', b'1');

-- --------------------------------------------------------

--
-- Table structure for table `lkp_tb_zipcode`
--

CREATE TABLE IF NOT EXISTS `lkp_tb_zipcode` (
  `ZipCodeId` int(11) NOT NULL,
  `Zicode` mediumint(9) DEFAULT NULL,
  `CountryId` int(11) DEFAULT NULL,
  `StateId` int(11) NOT NULL,
  `CityId` int(11) DEFAULT NULL,
  `TimeZone` int(11) DEFAULT NULL,
  `InsertUserId` bigint(20) DEFAULT NULL,
  `InsertDat` datetime DEFAULT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  PRIMARY KEY (`ZipCodeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_PrequalUsers`
--

CREATE TABLE IF NOT EXISTS `tb_PrequalUsers` (
  `UserID` bigint(20) DEFAULT NULL,
  `TaskTypeID` int(11) DEFAULT NULL,
  `AssignedDate` datetime DEFAULT NULL,
  `WorkFlowID` int(11) DEFAULT NULL,
  `InsertUserId` bigint(20) DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `tb_activitytype`
--

CREATE TABLE IF NOT EXISTS `tb_activitytype` (
  `ActivityTypeId` int(11) NOT NULL,
  `ActivityName` varchar(100) NOT NULL,
  `Description` longtext NOT NULL,
  `IsActive` bigint(20) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `modifyUserId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ActivityTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tb_activitytype`
--

INSERT INTO `tb_activitytype` (`ActivityTypeId`, `ActivityName`, `Description`, `IsActive`, `InsertUserId`, `InsertDate`, `ModifyDate`, `modifyUserId`) VALUES
(10001, 'Mail Sent', 'mail sent', 1, 0, '0000-00-00 00:00:00', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_addresses`
--

CREATE TABLE IF NOT EXISTS `tb_addresses` (
  `AddressId` bigint(20) NOT NULL AUTO_INCREMENT,
  `AddressTypeId` smallint(6) NOT NULL,
  `Address1` varchar(200) NOT NULL,
  `Address2` varchar(200) DEFAULT NULL,
  `Country` varchar(50) NOT NULL,
  `City` varchar(50) NOT NULL,
  `State` varchar(50) NOT NULL,
  `ZipCodeId` int(11) NOT NULL,
  `Phone1` varchar(12) DEFAULT NULL,
  `Phone2` varchar(12) DEFAULT NULL,
  `Phone3` varchar(12) DEFAULT NULL,
  `email1` varchar(50) DEFAULT NULL,
  `email2` varchar(50) DEFAULT NULL,
  `email3` varchar(50) DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `FaxNum` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`AddressId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `tb_addresses`
--

INSERT INTO `tb_addresses` (`AddressId`, `AddressTypeId`, `Address1`, `Address2`, `Country`, `City`, `State`, `ZipCodeId`, `Phone1`, `Phone2`, `Phone3`, `email1`, `email2`, `email3`, `InsertUserId`, `InsertDate`, `ModifyUserId`, `ModifyDate`, `FaxNum`) VALUES
(1, 1, 'CHD', 'UT', 'India', 'UT', 'Azua', 0, NULL, NULL, NULL, NULL, NULL, NULL, 0, '0000-00-00 00:00:00', NULL, NULL, NULL),
(2, 1, 'Chandigarh', 'UT', 'USA', 'UT', 'Azua', 0, '23232323', '2323232', '232323', NULL, NULL, NULL, 0, '0000-00-00 00:00:00', NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_affiliations`
--

CREATE TABLE IF NOT EXISTS `tb_affiliations` (
  `affiliationid` int(11) NOT NULL AUTO_INCREMENT,
  `merchantid` bigint(20) NOT NULL,
  `ownerid` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`affiliationid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_assignedcollections`
--

CREATE TABLE IF NOT EXISTS `tb_assignedcollections` (
  `AssignmentID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CollectorID` int(11) NOT NULL,
  `CollectorTypeID` smallint(6) NOT NULL,
  `OwnedBalance` double DEFAULT NULL,
  `AssignedByID` bigint(20) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`AssignmentID`),
  KEY `FK_tb_AssignedCollections_tb_Collectors` (`CollectorID`),
  KEY `FK_tb_AssignedCollections_tb_Users` (`AssignedByID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `tb_assignedcollections`
--

INSERT INTO `tb_assignedcollections` (`AssignmentID`, `CollectorID`, `CollectorTypeID`, `OwnedBalance`, `AssignedByID`, `InsertUserID`, `InsertDate`, `ModifyUserID`, `ModifyDate`) VALUES
(1, 1, 1, 1000, 2, 1, '2014-10-12 00:00:00', NULL, NULL),
(2, 2, 2, 1002, 3, 1, '2014-10-12 00:00:00', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_bankdetails`
--

CREATE TABLE IF NOT EXISTS `tb_bankdetails` (
  `BankDetailId` int(11) NOT NULL AUTO_INCREMENT,
  `BankId` int(11) DEFAULT NULL,
  `AccountNumber` varchar(100) DEFAULT NULL,
  `BankCode` varchar(100) DEFAULT NULL,
  `AccountName` varchar(200) DEFAULT NULL,
  `MerchantId` bigint(20) DEFAULT NULL,
  `ContractId` bigint(20) DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`BankDetailId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=7 ;

--
-- Dumping data for table `tb_bankdetails`
--

INSERT INTO `tb_bankdetails` (`BankDetailId`, `BankId`, `AccountNumber`, `BankCode`, `AccountName`, `MerchantId`, `ContractId`, `InsertDate`, `ModifyDate`) VALUES
(1, 7, '8', '8', 'Helllo', 5, 5, '2014-09-19 12:42:58', '0000-00-00 00:00:00'),
(2, 2, '45643', 'ROUTE456', 'John', 1, 2, '2014-09-19 12:42:58', '0000-00-00 00:00:00'),
(3, 2, '8784164', 'Check758747', 'Cross', 2, 2, NULL, NULL),
(5, 1, '11111111', NULL, '0', 25, 0, NULL, NULL),
(6, 1, '11111111', NULL, '0', 26, 0, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_collectionactivities`
--

CREATE TABLE IF NOT EXISTS `tb_collectionactivities` (
  `ActivityID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CollectionID` bigint(20) NOT NULL,
  `CollectionTypeID` smallint(6) NOT NULL,
  `ActivityTypeID` int(11) NOT NULL,
  `CollectionComments` varchar(100) DEFAULT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ActivityID`),
  KEY `FK_tb_CollectionActivities_tb_CollectionActivityList` (`ActivityTypeID`),
  KEY `FK_tb_CollectionActivities_tb_Collections` (`CollectionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_collectionactivitylist`
--

CREATE TABLE IF NOT EXISTS `tb_collectionactivitylist` (
  `ActivityTypeID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ActivityTypeID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=10 ;

--
-- Dumping data for table `tb_collectionactivitylist`
--

INSERT INTO `tb_collectionactivitylist` (`ActivityTypeID`, `Name`, `IsActive`, `InsertDate`, `InsertUserID`, `ModifyUserID`, `ModifyDate`) VALUES
(1, 'Contacted by telephone', 1, '2014-10-15 00:00:00', 1, NULL, NULL),
(2, 'Contacted by email', 1, '2014-10-15 00:00:00', 1, NULL, NULL),
(3, 'Investigative visit', 1, '2014-10-15 00:00:00', 1, NULL, NULL),
(4, 'Letter', 1, '2014-10-15 00:00:00', 1, NULL, NULL),
(5, 'Legal warning', 1, '2014-10-15 00:00:00', 1, NULL, NULL),
(6, 'Contacted landlord', 1, '2014-10-15 00:00:00', 1, NULL, NULL),
(7, 'Notify Payment', 1, '2014-10-15 00:00:00', 1, NULL, NULL),
(8, 'Payment Agreement', 1, '2014-10-15 00:00:00', 1, NULL, NULL),
(9, 'Other', 1, '2014-10-15 00:00:00', 1, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_collectioncontracts`
--

CREATE TABLE IF NOT EXISTS `tb_collectioncontracts` (
  `MerchantID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `OwnedAmount` double NOT NULL,
  `RepaymentAmount` double NOT NULL,
  `ExpectedTurn` double NOT NULL,
  `RealTurn` double NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` bigint(20) DEFAULT NULL,
  KEY `FK_tb_CollectionContracts_tb_Contracts` (`ContractID`),
  KEY `FK_tb_CollectionContracts_tb_Merchants` (`MerchantID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_collections`
--

CREATE TABLE IF NOT EXISTS `tb_collections` (
  `CollectionID` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `CollectionTypeID` smallint(6) NOT NULL,
  `StartDate` datetime NOT NULL,
  `AssignedDate` datetime NOT NULL,
  `AssignedUserID` bigint(20) DEFAULT NULL,
  `AssignedByUserID` bigint(20) DEFAULT NULL,
  `ClosedDate` datetime DEFAULT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `Comments` varchar(100) DEFAULT NULL,
  `StatusID` int(11) NOT NULL,
  `DefaultedDate` datetime NOT NULL,
  `SendLetterFlag` tinyint(1) NOT NULL,
  `CompanyID` smallint(6) DEFAULT NULL,
  `IsSpecialCase` tinyint(1) NOT NULL,
  PRIMARY KEY (`CollectionID`),
  KEY `FK_tb_Collections_tb_Contracts` (`ContractID`),
  KEY `FK_tb_Collections_tb_Merchants` (`MerchantID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `tb_collections`
--

INSERT INTO `tb_collections` (`CollectionID`, `MerchantID`, `ContractID`, `CollectionTypeID`, `StartDate`, `AssignedDate`, `AssignedUserID`, `AssignedByUserID`, `ClosedDate`, `InsertUserID`, `InsertDate`, `ModifyUserID`, `ModifyDate`, `Comments`, `StatusID`, `DefaultedDate`, `SendLetterFlag`, `CompanyID`, `IsSpecialCase`) VALUES
(1, 19, 11, 0, '2014-10-12 00:00:00', '2014-10-12 00:00:00', 9, 3, NULL, 1, '2014-10-12 00:00:00', NULL, NULL, NULL, 180001, '2014-10-12 00:00:00', 0, 1, 0),
(2, 20, 12, 0, '2014-10-04 00:00:00', '2014-10-04 00:00:00', 10, 2, NULL, 1, '2014-10-12 00:00:00', NULL, NULL, NULL, 180002, '2014-10-12 00:00:00', 0, 1, 0),
(3, 21, 11, 0, '2014-10-12 00:00:00', '2014-10-12 00:00:00', 9, 3, NULL, 1, '2014-10-12 00:00:00', NULL, NULL, NULL, 180003, '2014-10-12 00:00:00', 0, 1, 0),
(4, 22, 12, 0, '2014-10-04 00:00:00', '2014-10-04 00:00:00', 10, 2, NULL, 1, '2014-10-12 00:00:00', NULL, NULL, NULL, 180004, '2014-10-12 00:00:00', 0, 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `tb_collectors`
--

CREATE TABLE IF NOT EXISTS `tb_collectors` (
  `CollectorID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` bigint(20) NOT NULL,
  `CompanyID` smallint(6) NOT NULL,
  `ContactID` bigint(20) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDaet` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  `CollectorType` int(11) DEFAULT NULL,
  `CollectorCompanyName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`CollectorID`),
  KEY `FK_tb_Collectors_tb_Users` (`UserID`),
  KEY `FK_tb_Collectors_tb_Contacts` (`ContactID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `tb_collectors`
--

INSERT INTO `tb_collectors` (`CollectorID`, `UserID`, `CompanyID`, `ContactID`, `InsertUserID`, `InsertDaet`, `ModifyUserID`, `ModifyDate`, `CollectorType`, `CollectorCompanyName`) VALUES
(1, 9, 1, 44, 1, '2014-10-12', NULL, NULL, NULL, NULL),
(2, 10, 1, 45, 1, '2014-10-12', NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_collectoruploadeddocuments`
--

CREATE TABLE IF NOT EXISTS `tb_collectoruploadeddocuments` (
  `CollectionDOCID` bigint(20) NOT NULL AUTO_INCREMENT,
  `CollectionID` bigint(20) NOT NULL,
  `CollectorID` int(11) NOT NULL,
  `DocumentTypeID` int(11) NOT NULL,
  `UploadDate` datetime NOT NULL,
  `DateAssignedToCollector` datetime NOT NULL,
  `InsetUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`CollectionDOCID`),
  KEY `FK_tb_CollectorUploadedDocuments_Gen_tb_DocumentTypes` (`DocumentTypeID`),
  KEY `FK_tb_CollectorUploadedDocuments_tb_Collections` (`CollectionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_contacts`
--

CREATE TABLE IF NOT EXISTS `tb_contacts` (
  `ContactId` bigint(20) NOT NULL AUTO_INCREMENT,
  `ContactTypeId` int(11) NOT NULL,
  `JobTitle` varchar(50) NOT NULL,
  `FirstName` varchar(100) NOT NULL,
  `MiddleName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) NOT NULL,
  `AddressId1` bigint(20) DEFAULT NULL,
  `DateOfbirth` date NOT NULL,
  `SSN` varchar(50) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `Authorized` bit(1) DEFAULT b'0',
  `PassportNbr` varchar(100) DEFAULT NULL,
  `HomePhone` varchar(45) DEFAULT NULL,
  `CellPhone` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ContactId`),
  KEY `FK_ContractType_ContactID_idx` (`ContactTypeId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=40 ;

--
-- Dumping data for table `tb_contacts`
--

INSERT INTO `tb_contacts` (`ContactId`, `ContactTypeId`, `JobTitle`, `FirstName`, `MiddleName`, `LastName`, `AddressId1`, `DateOfbirth`, `SSN`, `MerchantId`, `Authorized`, `PassportNbr`, `HomePhone`, `CellPhone`) VALUES
(1, 1, 'OWNER', 'John', 'M', 'Santino', 1, '1956-02-23', '123123123', 1, b'1', '111222111', '123456789', '41221123123'),
(2, 2, 'SalesManager', 'Sales', NULL, 'Rep', 1, '1999-06-23', '123123123', 0, b'0', '111222111', '123456789', '3453463345'),
(3, 1, 'Sales Rep', 'F Name', 'M Name', 'L Name', 1, '1981-09-05', '123456789', 0, b'0', NULL, '123456789', '34534535434'),
(4, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', '123456789', 1, b'0', NULL, NULL, NULL),
(5, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(6, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', '123456789', 1, b'0', NULL, NULL, NULL),
(7, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', '123456789', 1, b'0', NULL, NULL, NULL),
(8, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(9, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(10, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', '123456789', 1, b'0', NULL, NULL, NULL),
(11, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(12, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(13, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', '123456789', 1, b'0', NULL, NULL, NULL),
(14, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(15, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(16, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', '123456789', 1, b'0', NULL, NULL, NULL),
(17, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(18, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(19, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', '123456789', 1, b'0', NULL, NULL, NULL),
(20, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(21, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(22, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', '123456789', 1, b'0', NULL, NULL, NULL),
(23, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(24, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', 'sample string 5', 1, b'0', NULL, NULL, NULL),
(25, 1, '', ' 1', '', ' 2', 1, '0000-00-00', ' 9', 1, b'0', NULL, NULL, NULL),
(26, 1, '', 'sample string 1', '', 'sample string 2', 1, '0000-00-00', 'sample string 9', 0, b'0', NULL, NULL, NULL),
(27, 1, '', 'sample string 1', '', 'sample string 2', 1, '0000-00-00', 'sample string 9', 0, b'0', NULL, NULL, NULL),
(28, 1, '', 'sample string 1', '', 'sample string 2', 1, '0000-00-00', 'sample string 9', 0, b'0', NULL, NULL, NULL),
(29, 3, 'Collection Agent', 'Collector', NULL, 'ONE', 1, '2014-10-12', '123123123', 0, b'0', NULL, '1231231231234', NULL),
(34, 1, '', 'Test Owner', '', 'Test Owner Last Name', 1, '0000-00-00', '65656565', 25, b'0', NULL, NULL, NULL),
(35, 1, '', 'Owner  First', '', 'Owner Last', 1, '0000-00-00', '65656565', 26, b'0', NULL, NULL, NULL),
(36, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', '0', 1, b'0', NULL, NULL, NULL),
(37, 1, '', 'sample string 1', '', 'sample string 2', 1, '2014-01-01', '0', 1, b'0', NULL, NULL, NULL),
(38, 1, '', 'sample string 1', '', 'sample string 2', 0, '2014-01-01', '0', 1, b'0', NULL, NULL, NULL),
(39, 1, '', 'sample string 1', '', 'sample string 2', 0, '0000-00-00', '0', 10, b'0', NULL, '23423423', '234234234');

-- --------------------------------------------------------

--
-- Table structure for table `tb_contracts`
--

CREATE TABLE IF NOT EXISTS `tb_contracts` (
  `ContractId` bigint(20) NOT NULL AUTO_INCREMENT,
  `ContractNumber` varchar(50) NOT NULL,
  `CreationDate` datetime NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `StatusId` int(11) NOT NULL,
  `currencyId` int(11) NOT NULL,
  `LoanedAmount` double DEFAULT NULL,
  `OwnedAmount` double DEFAULT NULL,
  `PaidAmount` double DEFAULT NULL,
  `Turn` int(11) DEFAULT NULL,
  `fundingDate` datetime DEFAULT NULL,
  `LastPaymentDate` datetime DEFAULT NULL,
  `StatusModifiedDate` datetime DEFAULT NULL,
  `LoanAppliedDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `ContractTypeId` int(11) NOT NULL,
  `OwnedBalanceZeroDate` datetime DEFAULT NULL,
  `LoanedBalanceZeroDate` datetime DEFAULT NULL,
  `WriteOffBalanceZeroDate` datetime DEFAULT NULL,
  `WrittenOffReasonId` int(11) DEFAULT NULL,
  `DafaultedDate` datetime DEFAULT NULL,
  `BuyRate` double DEFAULT NULL,
  `DailyPayment` double DEFAULT NULL,
  `AdjustedTurn` int(11) DEFAULT NULL,
  `RenewedID` bigint(20) DEFAULT NULL,
  `RenewalID` bigint(20) DEFAULT NULL,
  `RenewalContractID` bigint(20) DEFAULT NULL,
  `IsSpecialCase` tinyint(1) DEFAULT NULL,
  `DeclineReasonID` bigint(20) DEFAULT NULL,
  `DeclineReasonComment` varchar(200) DEFAULT NULL,
  `SalesRepID` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ContractId`),
  KEY `FK_tb_Contracts_Gen_tb_Statuses` (`StatusId`),
  KEY `FK_tb_Contracts_tb_Merchants` (`MerchantId`),
  KEY `FK_tb_Contracts_DeclineReasons_idx` (`DeclineReasonID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=16 ;

--
-- Dumping data for table `tb_contracts`
--

INSERT INTO `tb_contracts` (`ContractId`, `ContractNumber`, `CreationDate`, `MerchantId`, `StatusId`, `currencyId`, `LoanedAmount`, `OwnedAmount`, `PaidAmount`, `Turn`, `fundingDate`, `LastPaymentDate`, `StatusModifiedDate`, `LoanAppliedDate`, `InsertUserId`, `InsertDate`, `ModifyUserId`, `ModifyDate`, `ContractTypeId`, `OwnedBalanceZeroDate`, `LoanedBalanceZeroDate`, `WriteOffBalanceZeroDate`, `WrittenOffReasonId`, `DafaultedDate`, `BuyRate`, `DailyPayment`, `AdjustedTurn`, `RenewedID`, `RenewalID`, `RenewalContractID`, `IsSpecialCase`, `DeclineReasonID`, `DeclineReasonComment`, `SalesRepID`) VALUES
(1, 'CNT00001', '2014-08-31 00:00:00', 1, 20001, 1, 600001, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(2, 'CNT00002', '2014-08-31 00:00:00', 18, 20001, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(3, 'CNT00003', '2014-08-31 00:00:00', 1, 20001, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(4, 'CNT00004', '2014-08-31 00:00:00', 4, 20001, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(5, 'CNT00005', '2014-08-31 00:00:00', 1, 20001, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(6, 'CNT00006', '2014-08-31 00:00:00', 1, 20001, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(7, 'CNT00007', '2014-08-31 00:00:00', 1, 20001, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(8, 'CNT00008', '2014-08-31 00:00:00', 1, 20001, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(9, 'CNT00009', '2014-08-31 00:00:00', 1, 20001, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(10, 'CNT00010', '2014-08-31 00:00:00', 1, 20001, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(11, 'CNT00011', '2014-10-12 00:00:00', 1, 20007, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(12, 'CNT00012', '2014-10-12 00:00:00', 1, 20007, 1, 1000, 1500, NULL, 6, NULL, NULL, NULL, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', NULL, NULL, 13100, NULL, NULL, NULL, NULL, NULL, NULL, 10, 5, NULL, NULL, NULL, NULL, NULL, NULL, 5),
(13, 'CNT13', '2014-10-14 12:49:24', 1, 20001, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, '2014-10-14 12:49:24', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(14, 'CNT14', '2014-10-14 12:51:25', 1, 20001, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, '2014-10-14 12:51:25', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(15, 'CNT15', '2014-10-14 12:53:01', 1, 20001, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, '2014-10-14 12:53:01', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_creditcardactivity`
--

CREATE TABLE IF NOT EXISTS `tb_creditcardactivity` (
  `CreditCardActivityId` bigint(20) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `ProcessorId` int(11) NOT NULL,
  `ProcessorMerchantId` bigint(20) DEFAULT NULL,
  `AcctivityTypeId` int(11) DEFAULT NULL,
  `CurrencyId` int(11) DEFAULT NULL,
  `TotalAmount` double NOT NULL,
  `TotalTickets` int(11) NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `ProcessedDate` datetime DEFAULT NULL,
  `IndustryId` int(11) DEFAULT NULL,
  `MerchantStatusId` int(11) DEFAULT NULL,
  `AuthorizedOwnerId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`CreditCardActivityId`),
  KEY `FK_tb_CreditCardActivity_tb_Merchants` (`MerchantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tb_creditcardactivity`
--

INSERT INTO `tb_creditcardactivity` (`CreditCardActivityId`, `MerchantId`, `ProcessorId`, `ProcessorMerchantId`, `AcctivityTypeId`, `CurrencyId`, `TotalAmount`, `TotalTickets`, `StartDate`, `EndDate`, `ProcessedDate`, `IndustryId`, `MerchantStatusId`, `AuthorizedOwnerId`) VALUES
(0, 1, 1, NULL, NULL, NULL, 4.1, 10, '2014-10-05 00:00:00', '2014-10-05 00:00:00', NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_creditcardprofiles`
--

CREATE TABLE IF NOT EXISTS `tb_creditcardprofiles` (
  `CreditCardProfileId` bigint(20) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `ProcessorId` bigint(20) NOT NULL,
  PRIMARY KEY (`CreditCardProfileId`),
  KEY `FK_tb_CreditCardProfiles_tb_Merchants` (`MerchantId`),
  KEY `FK_tb_CreditCardProfiles_tb_Processor` (`ProcessorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_creditreports`
--

CREATE TABLE IF NOT EXISTS `tb_creditreports` (
  `CreditReportID` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `Image` longblob NOT NULL,
  `CreditScore` int(11) NOT NULL,
  `NumberofCreditCards` smallint(6) NOT NULL,
  `CreditAvailable` int(11) NOT NULL,
  `TimeofReport` datetime NOT NULL,
  `OtherCredits` tinyint(1) NOT NULL,
  `FirstName` varchar(100) DEFAULT NULL,
  `Name` varchar(200) DEFAULT NULL,
  `MiddleName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) DEFAULT NULL,
  `Passport` varchar(50) DEFAULT NULL,
  `Occupation` varchar(50) DEFAULT NULL,
  `Nationality` varchar(50) DEFAULT NULL,
  `RawData` longblob,
  `Errors` varchar(200) DEFAULT NULL,
  `RiskCategory` varchar(50) DEFAULT NULL,
  `ProbabilityOfDefault` double DEFAULT NULL,
  `MonthEvaualted` date DEFAULT NULL,
  PRIMARY KEY (`CreditReportID`),
  KEY `FK_tb_CreditReports_tb_Merchants` (`MerchantID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_declinereasons`
--

CREATE TABLE IF NOT EXISTS `tb_declinereasons` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `contractId` bigint(20) DEFAULT NULL,
  `workflowid` bigint(20) DEFAULT NULL,
  `declineNotes` varchar(200) DEFAULT NULL,
  `declinereasonid` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_documents`
--

CREATE TABLE IF NOT EXISTS `tb_documents` (
  `DocumentId` bigint(20) NOT NULL AUTO_INCREMENT,
  `DocumentTypeId` int(11) NOT NULL,
  `FileName` varchar(1000) DEFAULT NULL,
  `FileDetails` varchar(5000) DEFAULT NULL,
  `MERCHANTID` bigint(20) NOT NULL,
  `UploaduserId` bigint(20) NOT NULL,
  `UploadDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `ContractId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`DocumentId`),
  KEY `FK__tb_Docume__Docum__0D7A0286` (`DocumentTypeId`),
  KEY `FK_Merchant_tb_documents_MID` (`MERCHANTID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=16 ;

--
-- Dumping data for table `tb_documents`
--

INSERT INTO `tb_documents` (`DocumentId`, `DocumentTypeId`, `FileName`, `FileDetails`, `MERCHANTID`, `UploaduserId`, `UploadDate`, `ModifyUserId`, `ModifyDate`, `ContractId`) VALUES
(6, 2, 'doc_62_10_1000.JPG', 'image/jpeg', 10, 1, '2014-10-06 08:10:03', 1, '2014-10-15 10:02:53', 1000),
(7, 3, 'doc_73_10_1000.jpg', 'image/jpeg', 10, 1, '2014-10-06 08:15:33', 1, '2014-10-15 22:31:37', 1000),
(8, 4, 'doc_84_1000_1000.jpg', 'image/jpeg', 10, 1, '2014-10-06 07:35:08', 1, '2014-10-09 22:51:15', 1000),
(9, 5, NULL, NULL, 10, 0, '0000-00-00 00:00:00', NULL, NULL, 1000),
(10, 6, NULL, NULL, 10, 0, '0000-00-00 00:00:00', NULL, NULL, 1000),
(11, 7, NULL, NULL, 10, 0, '0000-00-00 00:00:00', NULL, NULL, 1000),
(12, 8, NULL, NULL, 10, 0, '0000-00-00 00:00:00', NULL, NULL, 1000),
(13, 1, 'feature', 'file', 10, 2, '2014-10-13 07:32:20', NULL, NULL, 1000),
(15, 1, 'doc_01_1000_1000.png', 'image/png', 1000, 0, '0000-00-00 00:00:00', 1, '2014-10-16 11:25:59', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_emailgroup`
--

CREATE TABLE IF NOT EXISTS `tb_emailgroup` (
  `GroupId` int(11) NOT NULL,
  `GroupName` varchar(50) NOT NULL,
  `IsActive` bigint(20) NOT NULL,
  PRIMARY KEY (`GroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_emailgroupmembers`
--

CREATE TABLE IF NOT EXISTS `tb_emailgroupmembers` (
  `GroupMemberId` bigint(20) NOT NULL,
  `GroupId` int(11) NOT NULL,
  `MemberEmail` varchar(1000) NOT NULL,
  `IsActive` bigint(20) NOT NULL,
  PRIMARY KEY (`GroupMemberId`),
  KEY `FK_tb_EmailGroupMembers_tb_EmailGroup` (`GroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_grouprights`
--

CREATE TABLE IF NOT EXISTS `tb_grouprights` (
  `GroupRightID` bigint(20) NOT NULL AUTO_INCREMENT,
  `GroupID` int(11) NOT NULL,
  `ModuleID` int(11) NOT NULL,
  `Read` tinyint(1) NOT NULL,
  `Write` tinyint(1) NOT NULL,
  `Edit` tinyint(1) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  PRIMARY KEY (`GroupRightID`),
  KEY `FK_tb_GroupRights_tb_Groups` (`GroupID`),
  KEY `FK_tb_GroupRights_tb_Users` (`InsertUserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_groups`
--

CREATE TABLE IF NOT EXISTS `tb_groups` (
  `GroupID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupName` varchar(100) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  `isdefault` bit(1) DEFAULT b'0',
  PRIMARY KEY (`GroupID`),
  KEY `FK_tb_Groups_tb_Users` (`InsertUserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_merchantactivities`
--

CREATE TABLE IF NOT EXISTS `tb_merchantactivities` (
  `ActivityId` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantId` bigint(20) NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `ActivityTypeId` int(11) NOT NULL,
  PRIMARY KEY (`ActivityId`),
  KEY `FK_tb_MerchantActivities_tb_Merchants` (`MerchantId`),
  KEY `FK_tb_MerchantActivitietypeId_tb_ActivityType` (`ActivityTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_merchantbankstatements`
--

CREATE TABLE IF NOT EXISTS `tb_merchantbankstatements` (
  `BankInfoId` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantId` bigint(20) NOT NULL,
  `StatementId` bigint(20) NOT NULL,
  `Month` smallint(6) NOT NULL,
  `Year` smallint(6) NOT NULL,
  `amount` double NOT NULL,
  PRIMARY KEY (`BankInfoId`),
  KEY `FK_tb_MerchantBankStatements_tb_Merchants` (`MerchantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_merchantlandlords`
--

CREATE TABLE IF NOT EXISTS `tb_merchantlandlords` (
  `LandlordId` int(11) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `BuildingName` varchar(100) NOT NULL,
  `ContactId` bigint(20) NOT NULL,
  PRIMARY KEY (`LandlordId`),
  KEY `FK_tb_MerchantLandlords_tb_Contacts` (`ContactId`),
  KEY `FK_tb_MerchantLandlords_tb_Merchants` (`MerchantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_merchants`
--

CREATE TABLE IF NOT EXISTS `tb_merchants` (
  `MerchantId` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyId` smallint(6) NOT NULL,
  `LegalName` varchar(200) NOT NULL,
  `BusinessName` varchar(200) NOT NULL,
  `WorkflowId` smallint(6) NOT NULL,
  `AddressId` bigint(20) DEFAULT NULL,
  `AddressId2` bigint(20) DEFAULT NULL,
  `StatusId` int(11) NOT NULL,
  `StatusModifyDate` datetime DEFAULT NULL,
  `SalesRepId` bigint(20) NOT NULL,
  `BusinessStartDate` date NOT NULL,
  `IndustryTypeId` int(11) NOT NULL,
  `BusinessTypeId` int(11) NOT NULL,
  `LastActivityDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `OwnerId` bigint(20) NOT NULL,
  `BankInfoId` bigint(20) NOT NULL,
  `CNetProcessorId` varchar(20) DEFAULT NULL,
  `VNetProcessoIdd` varchar(200) DEFAULT NULL,
  `BusinessWebSite` varchar(500) DEFAULT NULL,
  `RentFlag` tinyint(1) DEFAULT NULL,
  `LandlordName` varchar(100) DEFAULT NULL,
  `RentedAmount` double DEFAULT NULL,
  `currencyId` int(11) DEFAULT NULL,
  `RNCNumber` varchar(11) NOT NULL,
  `TelePhone1` varchar(12) DEFAULT NULL,
  `TelePhone2` varchar(12) DEFAULT NULL,
  `accountId` varchar(200) DEFAULT NULL,
  `GrossAnnualSales` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`MerchantId`),
  KEY `FK_tb_Merchants_Gen_tb_Addresses` (`AddressId`),
  KEY `FK_tb_Merchants_Gen_tb_Statuses` (`StatusId`),
  KEY `FK_tb_Merchants_tb_SalesRep` (`SalesRepId`),
  KEY `FK_BusinessTypeID_lkp_tb_EntityTypes_idx` (`BusinessTypeId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=27 ;

--
-- Dumping data for table `tb_merchants`
--

INSERT INTO `tb_merchants` (`MerchantId`, `CompanyId`, `LegalName`, `BusinessName`, `WorkflowId`, `AddressId`, `AddressId2`, `StatusId`, `StatusModifyDate`, `SalesRepId`, `BusinessStartDate`, `IndustryTypeId`, `BusinessTypeId`, `LastActivityDate`, `InsertUserId`, `InsertDate`, `ModifyUserId`, `ModifyDate`, `OwnerId`, `BankInfoId`, `CNetProcessorId`, `VNetProcessoIdd`, `BusinessWebSite`, `RentFlag`, `LandlordName`, `RentedAmount`, `currencyId`, `RNCNumber`, `TelePhone1`, `TelePhone2`, `accountId`, `GrossAnnualSales`) VALUES
(1, 1, 'sample string 5', 'sample string 4', 1, 50498657, NULL, 10001, NULL, 5, '2014-01-01', 14007, 11004, NULL, 1, '2014-10-01 00:00:00', NULL, NULL, 1, 1, '1009', '2010', 'http://sampledomain.com', 12, NULL, 1300.1, 1, '1122255566', '1234569870', NULL, NULL, '14'),
(3, 1, 'Grupo Corripio', 'Grupo Corripio', 1, 50498657, NULL, 10001, NULL, 5, '2001-02-02', 14007, 11002, NULL, 1, '2014-10-01 00:00:00', NULL, NULL, 2, 1, '1112245511', NULL, NULL, 0, NULL, 0, 1, '11100244', NULL, NULL, NULL, NULL),
(4, 1, 'Banco Intercontinental', 'Banco Intercontinental', 1, 50498657, NULL, 10002, NULL, 6, '2010-05-06', 14007, 11003, NULL, 1, '2014-10-04 00:00:00', NULL, NULL, 1, 0, '212121333', NULL, NULL, 1, 'Zarkhov ', 1000, 1, '2222233311', '12121212', NULL, NULL, NULL),
(5, 1, 'Banco León', 'Banco León', 1, 50498657, NULL, 10003, NULL, 7, '2011-01-01', 14007, 11004, NULL, 1, '2014-10-04 00:00:00', NULL, NULL, 1, 0, '2223322114', NULL, NULL, 1, 'Cheng Li', 1120, 1, '121212', NULL, NULL, NULL, NULL),
(6, 1, 'Quesada Cigars', 'Quesada Cigars', 1, 50498657, NULL, 10004, NULL, 8, '2012-02-02', 14007, 11006, NULL, 1, '2014-10-04 00:00:00', NULL, NULL, 1, 0, '22251112', NULL, NULL, 0, NULL, 0, 1, '222233366', '121215142', NULL, NULL, NULL),
(8, 5, 'sample string 3', 'sample string 2', 1, 50498657, NULL, 20002, '2014-10-09 03:43:39', 5, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-09 03:43:39', NULL, NULL, 1, 0, '', '', NULL, NULL, NULL, NULL, NULL, '1111111', '', '', NULL, NULL),
(9, 5, 'sample string 3', 'sample string 2', 1, 50498657, NULL, 20002, '2014-10-09 04:12:25', 5, '2014-10-09', 14007, 11004, NULL, 1, '2014-10-09 04:12:25', NULL, NULL, 1, 0, '', '', NULL, NULL, NULL, NULL, NULL, '1111111', '', '', NULL, NULL),
(10, 5, '0', 'sample string 2444', 1, 2, NULL, 20002, '2014-10-09 05:46:05', 5, '2001-01-01', 14007, 11004, NULL, 1, '2014-10-09 05:46:05', NULL, NULL, 28, 0, '0', '0', '0', 0, NULL, 0, NULL, 'sample stri', '2323232323', '23232323', NULL, '0'),
(11, 5, 'sample string 33', 'sample string 22', 1, 50498657, NULL, 20002, '2014-10-09 05:55:19', 5, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-09 05:55:19', NULL, NULL, 1, 0, '', '', NULL, NULL, NULL, NULL, NULL, 'sample stri', '', '', NULL, NULL),
(18, 5, 'RJ Legalname', 'RJ Businessname', 1, 2, 2, 20002, '2014-10-11 07:16:50', 0, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-11 07:16:50', NULL, NULL, 0, 0, '', '', NULL, NULL, NULL, NULL, NULL, '45745154', '123456', '', NULL, NULL),
(19, 1, 'MRC_Collection_2', 'BN Collection', 1, 50498657, NULL, 10006, '2014-10-09 03:43:39', 5, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-09 03:43:39', NULL, NULL, 1, 0, '', '', NULL, NULL, NULL, NULL, NULL, 'sample stri', '', '', NULL, NULL),
(20, 1, 'MRC_Collection_1', 'BN Collection 1', 1, 50498657, NULL, 10006, '2014-10-09 04:12:25', 5, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-09 04:12:25', NULL, NULL, 1, 0, '', '', NULL, NULL, NULL, NULL, NULL, 'sample stri', '', '', NULL, NULL),
(21, 1, 'MRC_Collection_3', 'BN Collection3', 1, 50498657, NULL, 10006, '2014-10-09 03:43:39', 5, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-09 03:43:39', NULL, NULL, 1, 0, '', '', NULL, NULL, NULL, NULL, NULL, 'sample stri', '', '', NULL, NULL),
(22, 1, 'MRC_Collection_4', 'BN Collection 4', 1, 50498657, NULL, 10006, '2014-10-09 04:12:25', 5, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-09 04:12:25', NULL, NULL, 1, 0, '', '', NULL, NULL, NULL, NULL, NULL, 'sample stri', '', '', NULL, NULL),
(23, 5, 'RJ Legalname', 'RJ Businessname', 1, 50498657, NULL, 20002, '2014-10-11 23:10:49', 0, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-11 23:10:49', NULL, NULL, 0, 0, '', '', NULL, NULL, NULL, NULL, NULL, '45745154', '', '', NULL, NULL),
(24, 5, 'RJ Legalname', 'RJ Businessname', 1, 50498657, NULL, 20002, '2014-10-11 23:14:37', 0, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-11 23:14:37', NULL, NULL, 0, 0, '', '', NULL, NULL, NULL, NULL, NULL, '45745154', '', '', NULL, NULL),
(25, 5, 'Test MM', 'Test MM', 1, 1, NULL, 20002, '2014-10-13 07:54:08', 5, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-13 07:54:08', NULL, NULL, 24, 0, '0', '0', '0', 0, NULL, 33333, NULL, '5522', '', '', NULL, '4444'),
(26, 5, 'Test MM', 'Test MM', 1, NULL, NULL, 20002, '2014-10-14 11:14:02', 5, '2014-10-09', 14007, 11001, NULL, 1, '2014-10-14 11:14:02', NULL, NULL, 0, 0, '0', '0', '0', 0, NULL, 33333, NULL, '5522', '', '', NULL, '4444');

-- --------------------------------------------------------

--
-- Table structure for table `tb_merchanttradereference`
--

CREATE TABLE IF NOT EXISTS `tb_merchanttradereference` (
  `TradeReferenceId` bigint(20) NOT NULL AUTO_INCREMENT,
  `TradeReferenceName` varchar(200) NOT NULL,
  `TelePhone` varchar(12) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `MerchantId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`TradeReferenceId`),
  KEY `FK_MerchantID_tb_Merchants` (`MerchantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_notes`
--

CREATE TABLE IF NOT EXISTS `tb_notes` (
  `NoteId` bigint(20) NOT NULL AUTO_INCREMENT,
  `NoteTypeId` int(11) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `ContractId` bigint(20) NOT NULL,
  `Note` longtext NOT NULL,
  `WorkFlowId` smallint(6) NOT NULL,
  `ScreenName` varchar(50) NOT NULL,
  PRIMARY KEY (`NoteId`),
  KEY `FK_tb_Notes_Gen_tb_NoteTypes` (`NoteTypeId`),
  KEY `FK_tb_Notes_tb_Contracts` (`ContractId`),
  KEY `FK_tb_Notes_tb_Merchants` (`MerchantId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `tb_notes`
--

INSERT INTO `tb_notes` (`NoteId`, `NoteTypeId`, `MerchantId`, `ContractId`, `Note`, `WorkFlowId`, `ScreenName`) VALUES
(1, 500001, 1, 0, 'sample string 2', 1, 'sample string 3'),
(2, 510001, 1, 0, 'xxx', 0, 'xxx'),
(3, 510001, 1, 0, 'xxx', 0, 'AAA');

-- --------------------------------------------------------

--
-- Table structure for table `tb_offers`
--

CREATE TABLE IF NOT EXISTS `tb_offers` (
  `OfferId` bigint(20) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `ContractId` bigint(20) NOT NULL,
  `Turn` int(11) NOT NULL,
  `LoanAmount` double NOT NULL,
  `OwnedAmount` double NOT NULL,
  `Proportion` double NOT NULL,
  `Retention` double NOT NULL,
  `OfferCreationDate` datetime NOT NULL,
  `OfferAcceptanceDate` datetime NOT NULL,
  `CreatedUserId` bigint(20) NOT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ExpirationDate` datetime NOT NULL,
  PRIMARY KEY (`OfferId`),
  KEY `FK_tb_Offers_tb_Contracts` (`ContractId`),
  KEY `FK_tb_Offers_tb_Merchants` (`MerchantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_owners`
--

CREATE TABLE IF NOT EXISTS `tb_owners` (
  `OwnerId` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantId` bigint(20) NOT NULL,
  `BusinessStartDate` datetime NOT NULL,
  `RentFlag` tinyint(1) NOT NULL,
  `RentAmount` double NOT NULL,
  `MortgageAmount` double DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUser` datetime NOT NULL,
  `ModifyDate` datetime NOT NULL,
  `ContactId` bigint(20) NOT NULL,
  `dateBecameOwner` date DEFAULT NULL,
  `IsAuthorized` bit(1) DEFAULT b'0',
  PRIMARY KEY (`OwnerId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=29 ;

--
-- Dumping data for table `tb_owners`
--

INSERT INTO `tb_owners` (`OwnerId`, `MerchantId`, `BusinessStartDate`, `RentFlag`, `RentAmount`, `MortgageAmount`, `InsertUserId`, `InsertDate`, `ModifyUser`, `ModifyDate`, `ContactId`, `dateBecameOwner`, `IsAuthorized`) VALUES
(1, 10, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:10:46', '0000-00-00 00:00:00', '2014-10-06 12:10:46', 4, NULL, b'1'),
(2, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:14:26', '0000-00-00 00:00:00', '2014-10-06 12:14:26', 7, NULL, b'1'),
(3, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:14:26', '0000-00-00 00:00:00', '2014-10-06 12:14:26', 8, NULL, b'1'),
(4, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:14:26', '0000-00-00 00:00:00', '2014-10-06 12:14:26', 9, NULL, b'1'),
(5, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:16:46', '0000-00-00 00:00:00', '2014-10-06 12:16:46', 10, NULL, b'1'),
(6, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:16:46', '0000-00-00 00:00:00', '2014-10-06 12:16:46', 11, NULL, b'1'),
(7, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:16:46', '0000-00-00 00:00:00', '2014-10-06 12:16:46', 12, NULL, b'1'),
(8, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:17:02', '0000-00-00 00:00:00', '2014-10-06 12:17:02', 13, NULL, b'1'),
(9, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:17:02', '0000-00-00 00:00:00', '2014-10-06 12:17:02', 14, NULL, b'1'),
(10, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:17:02', '0000-00-00 00:00:00', '2014-10-06 12:17:02', 15, NULL, b'1'),
(11, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:17:34', '0000-00-00 00:00:00', '2014-10-06 12:17:34', 16, NULL, b'1'),
(12, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:17:34', '0000-00-00 00:00:00', '2014-10-06 12:17:34', 17, NULL, b'1'),
(13, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:17:34', '0000-00-00 00:00:00', '2014-10-06 12:17:34', 18, NULL, b'1'),
(14, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:19:07', '0000-00-00 00:00:00', '2014-10-06 12:19:07', 19, NULL, b'1'),
(15, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:19:07', '0000-00-00 00:00:00', '2014-10-06 12:19:07', 20, NULL, b'1'),
(16, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:19:07', '0000-00-00 00:00:00', '2014-10-06 12:19:07', 21, NULL, b'1'),
(17, 1, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:19:17', '0000-00-00 00:00:00', '2014-10-06 12:19:17', 22, NULL, b'1'),
(18, 4, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:19:17', '0000-00-00 00:00:00', '2014-10-06 12:19:17', 23, NULL, b'1'),
(19, 4, '2014-01-01 00:00:00', 12, 1300.1, NULL, 1, '2014-10-06 12:19:17', '0000-00-00 00:00:00', '2014-10-06 12:19:17', 24, NULL, b'1'),
(20, 4, '0000-00-00 00:00:00', 1, 1200.1, NULL, 1, '2014-10-06 12:30:45', '0000-00-00 00:00:00', '2014-10-06 12:30:45', 25, NULL, b'1'),
(21, 0, '0000-00-00 00:00:00', 11, 12.1, NULL, 1, '2014-10-10 11:58:50', '0000-00-00 00:00:00', '2014-10-10 11:58:50', 26, NULL, b'0'),
(22, 0, '0000-00-00 00:00:00', 11, 12.1, NULL, 1, '2014-10-10 11:58:50', '0000-00-00 00:00:00', '2014-10-10 11:58:50', 27, NULL, b'0'),
(23, 0, '0000-00-00 00:00:00', 11, 12.1, NULL, 1, '2014-10-10 11:58:50', '0000-00-00 00:00:00', '2014-10-10 11:58:50', 28, NULL, b'0'),
(24, 25, '0000-00-00 00:00:00', 0, 33333, NULL, 1, '2014-10-13 11:39:43', '0000-00-00 00:00:00', '2014-10-14 06:59:56', 34, NULL, b'0'),
(28, 26, '0000-00-00 00:00:00', 0, 33333, NULL, 1, '2014-10-14 11:16:46', '0000-00-00 00:00:00', '2014-10-14 11:42:27', 35, NULL, b'0');

-- --------------------------------------------------------

--
-- Table structure for table `tb_processor`
--

CREATE TABLE IF NOT EXISTS `tb_processor` (
  `ProcessorId` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantId` bigint(20) NOT NULL,
  `ProcessorTypeId` smallint(6) DEFAULT NULL,
  `firstProcessedDate` date DEFAULT NULL,
  `processorNumber` varchar(45) NOT NULL,
  PRIMARY KEY (`ProcessorId`),
  KEY `FK_tb_Processor_Gen_tb_ProcessorList` (`ProcessorTypeId`),
  KEY `FK_tb_Processor_tb_Merchants` (`MerchantId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=787878788 ;

--
-- Dumping data for table `tb_processor`
--

INSERT INTO `tb_processor` (`ProcessorId`, `MerchantId`, `ProcessorTypeId`, `firstProcessedDate`, `processorNumber`) VALUES
(10001, 10, 10001, NULL, ''),
(10331, 18, 10002, NULL, ''),
(54545333, 18, 10001, NULL, ''),
(54545444, 25, 10001, NULL, ''),
(787878787, 25, 10002, NULL, '');

-- --------------------------------------------------------

--
-- Table structure for table `tb_processoractivities`
--

CREATE TABLE IF NOT EXISTS `tb_processoractivities` (
  `ActivityId` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantId` bigint(20) NOT NULL,
  `ContractId` bigint(20) DEFAULT NULL,
  `ProcessorId` smallint(6) NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `TotalAmount` double NOT NULL,
  `AppliedAmount` double NOT NULL,
  `BalanceAmount` double NOT NULL,
  `PaidAmount` double NOT NULL,
  `ActivityTypeId` int(11) DEFAULT NULL,
  `ProcessedDate` datetime DEFAULT NULL,
  `Notes` varchar(2000) DEFAULT NULL,
  `InsertUserId` bigint(20) DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`ActivityId`),
  KEY `FK_tb_ProcessorActivities_Gen_tb_ProcessorList` (`ProcessorId`),
  KEY `FK_tb_ProcessorActivities_tb_ActivityType` (`ActivityTypeId`),
  KEY `FK_tb_ProcessorActivities_tb_Merchants` (`MerchantId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_recentlyvisited`
--

CREATE TABLE IF NOT EXISTS `tb_recentlyvisited` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `merchantId` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `dateVisited` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=16 ;

--
-- Dumping data for table `tb_recentlyvisited`
--

INSERT INTO `tb_recentlyvisited` (`id`, `merchantId`, `userId`, `dateVisited`) VALUES
(1, 58, 1, '2014-10-10 03:19:34'),
(2, 10, 1, '2014-10-10 03:46:18'),
(3, 1, 1, '2014-10-11 02:04:59'),
(4, 1, 1, '2014-10-11 02:07:17'),
(5, 10, 1, '2014-10-11 02:07:43'),
(6, 12, 1, '2014-10-11 02:08:45'),
(7, 12, 1, '2014-10-11 02:19:05'),
(8, 12, 1, '2014-10-11 02:19:26'),
(9, 12, 1, '2014-10-11 07:21:09'),
(10, 12, 1, '2014-10-15 01:16:18'),
(11, 12, 1, '2014-10-15 08:58:12'),
(12, 12, 1, '2014-10-15 09:59:38'),
(13, 12, 1, '2014-10-16 02:35:54'),
(14, 12, 1, '2014-10-16 02:35:56'),
(15, 12, 1, '2014-10-16 02:55:45');

-- --------------------------------------------------------

--
-- Table structure for table `tb_renewals`
--

CREATE TABLE IF NOT EXISTS `tb_renewals` (
  `RenewalID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `MerchantID` bigint(20) NOT NULL,
  `RenewedContractID` bigint(20) NOT NULL,
  `SalesRepID` bigint(20) NOT NULL,
  `RenewalAssignedUSerID` bigint(20) NOT NULL,
  `StatusID` int(11) DEFAULT NULL,
  `DeclineReasonID` int(11) DEFAULT NULL,
  `DeclineReasonComment` varchar(200) DEFAULT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  PRIMARY KEY (`RenewalID`),
  KEY `FK_tb_Renewals_tb_Contracts` (`ContractID`),
  KEY `FK_tb_Renewals_tb_Contracts1` (`RenewedContractID`),
  KEY `FK_tb_Renewals_tb_Merchants` (`MerchantID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_renewalslist`
--

CREATE TABLE IF NOT EXISTS `tb_renewalslist` (
  `RenewalID` bigint(20) NOT NULL AUTO_INCREMENT,
  `ContractID` bigint(20) NOT NULL,
  `MerchantID` bigint(20) DEFAULT NULL,
  `StatusID` int(11) NOT NULL,
  `RenewalEligibleDate` date NOT NULL,
  `AssignedUserID` bigint(20) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  `CompanyID` smallint(6) NOT NULL,
  `PostponedTilldate` date DEFAULT NULL,
  PRIMARY KEY (`RenewalID`),
  KEY `FK_tb_RenewalsList_Gen_tb_Statuses` (`StatusID`),
  KEY `FK_tb_RenewalsList_tb_Contracts` (`ContractID`),
  KEY `FK_tb_RenewalsList_tb_Merchants` (`MerchantID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `tb_renewalslist`
--

INSERT INTO `tb_renewalslist` (`RenewalID`, `ContractID`, `MerchantID`, `StatusID`, `RenewalEligibleDate`, `AssignedUserID`, `InsertUserID`, `InsertDate`, `ModifyUserID`, `ModifyDate`, `CompanyID`, `PostponedTilldate`) VALUES
(3, 1, 1, 20101, '0000-00-00', 0, 0, '0000-00-00', NULL, NULL, 5, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_renewalsrequireddocs`
--

CREATE TABLE IF NOT EXISTS `tb_renewalsrequireddocs` (
  `MerchantID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `RequiredDocTypeID` int(11) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  KEY `FK_tb_RenewalsRequiredDocs_Gen_tb_DocumentTypes` (`RequiredDocTypeID`),
  KEY `FK_tb_RenewalsRequiredDocs_tb_Contracts` (`ContractID`),
  KEY `FK_tb_RenewalsRequiredDocs_tb_Merchants` (`MerchantID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_repaymentplan`
--

CREATE TABLE IF NOT EXISTS `tb_repaymentplan` (
  `RepaymentPlanID` bigint(20) NOT NULL AUTO_INCREMENT,
  `MerchantID` bigint(20) NOT NULL,
  `ContractID` bigint(20) NOT NULL,
  `CompanyID` smallint(6) NOT NULL,
  `DueDate` datetime NOT NULL,
  `Amount` double NOT NULL,
  `CollectedAmount` double NOT NULL,
  `CollectionDate` datetime NOT NULL,
  `CollectionFrequency` smallint(6) NOT NULL,
  `StatusID` int(11) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  PRIMARY KEY (`RepaymentPlanID`),
  KEY `FK_tb_RepaymentPlan_tb_Contracts` (`ContractID`),
  KEY `FK_tb_RepaymentPlan_tb_Merchants` (`MerchantID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_salesrep`
--

CREATE TABLE IF NOT EXISTS `tb_salesrep` (
  `SalesRepId` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserId` bigint(20) NOT NULL,
  `CompanyId` smallint(6) NOT NULL,
  `Insertdate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) DEFAULT NULL,
  `UpdateUserId` bigint(20) DEFAULT NULL,
  `Modifieddate` datetime DEFAULT NULL,
  `IsManager` tinyint(1) DEFAULT NULL,
  `AddressId` bigint(20) DEFAULT NULL,
  `Contactid` bigint(20) NOT NULL,
  `Commission` double NOT NULL,
  `FirstFundingDate` datetime DEFAULT NULL,
  PRIMARY KEY (`SalesRepId`),
  KEY `FK_tb_SalesRep_tb_SalesRep_Contact` (`Contactid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `tb_salesrep`
--

INSERT INTO `tb_salesrep` (`SalesRepId`, `UserId`, `CompanyId`, `Insertdate`, `InsertUserId`, `UpdateUserId`, `Modifieddate`, `IsManager`, `AddressId`, `Contactid`, `Commission`, `FirstFundingDate`) VALUES
(5, 3, 1, '2014-08-31 00:00:00', 1, NULL, NULL, 0, 4, 1, 0.25, '2014-08-31 00:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `tb_salesrep_contact`
--

CREATE TABLE IF NOT EXISTS `tb_salesrep_contact` (
  `ContactId` bigint(20) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL,
  `DateOfBirth` date NOT NULL,
  `SSN` varchar(9) DEFAULT NULL,
  `Jobtitle` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ContactId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=2 ;

--
-- Dumping data for table `tb_salesrep_contact`
--

INSERT INTO `tb_salesrep_contact` (`ContactId`, `FirstName`, `MiddleName`, `LastName`, `DateOfBirth`, `SSN`, `Jobtitle`) VALUES
(1, 'Sales', NULL, 'Manager', '1989-08-31', '133133123', 'Manager');

-- --------------------------------------------------------

--
-- Table structure for table `tb_snooze`
--

CREATE TABLE IF NOT EXISTS `tb_snooze` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `contractId` bigint(20) DEFAULT NULL,
  `snoozetilldate` datetime DEFAULT NULL,
  `percentpaid` double DEFAULT NULL,
  `insertuserid` bigint(20) DEFAULT NULL,
  `insertdate` date DEFAULT NULL,
  `modifyuserid` bigint(20) DEFAULT NULL,
  `modifydate` date DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_tasks`
--

CREATE TABLE IF NOT EXISTS `tb_tasks` (
  `TaskId` bigint(20) NOT NULL AUTO_INCREMENT,
  `TaskTypeId` int(11) NOT NULL,
  `StatusId` int(11) NOT NULL,
  `Priority` int(11) NOT NULL,
  `StartDate` datetime NOT NULL,
  `EndDatte` datetime NOT NULL,
  `MerchantId` bigint(20) DEFAULT NULL,
  `ContractId` bigint(20) DEFAULT NULL,
  `AssignedUserId` bigint(20) DEFAULT NULL,
  `AssignedDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) NOT NULL,
  `ModifyDate` datetime NOT NULL,
  `WorkflowId` smallint(6) NOT NULL,
  `AffiliationFlag` tinyint(1) NOT NULL,
  `MerchantIDTMP` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`TaskId`),
  KEY `FK_tb_Tasks_tb_Merchants` (`MerchantId`),
  KEY `FK_tb_Tasks_tb_Workflows` (`WorkflowId`),
  KEY `FK_tb_Tasks_TMP_tb_Merchants` (`MerchantIDTMP`),
  KEY `FK_tb_TaskTypes_TaskTypeId` (`TaskTypeId`),
  KEY `FK_tb_Tasks_Gen_tb_Statuses_idx` (`StatusId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=63 ;

--
-- Dumping data for table `tb_tasks`
--

INSERT INTO `tb_tasks` (`TaskId`, `TaskTypeId`, `StatusId`, `Priority`, `StartDate`, `EndDatte`, `MerchantId`, `ContractId`, `AssignedUserId`, `AssignedDate`, `InsertUserId`, `InsertDate`, `ModifyUserId`, `ModifyDate`, `WorkflowId`, `AffiliationFlag`, `MerchantIDTMP`) VALUES
(12, 1, 22002, 0, '2014-08-31 00:00:00', '0000-00-00 00:00:00', 1, 1, NULL, '2014-10-09 05:55:19', 1, '2014-08-31 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 1),
(13, 2, 22002, 0, '2014-08-31 00:00:00', '0000-00-00 00:00:00', 1, 1, 2, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', 1, '2014-08-31 00:00:00', 1, 0, 1),
(16, 3, 22002, 0, '2014-10-01 00:00:00', '0000-00-00 00:00:00', 3, 2, NULL, '2014-10-09 05:55:19', 1, '2014-10-01 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, NULL),
(17, 4, 22002, 0, '2014-10-01 00:00:00', '0000-00-00 00:00:00', 2, 3, NULL, '2014-10-09 05:55:19', 1, '2014-10-01 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, NULL),
(18, 5, 22002, 0, '2014-10-01 00:00:00', '0000-00-00 00:00:00', 4, 4, NULL, '2014-10-09 05:55:19', 1, '2014-10-01 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, NULL),
(19, 6, 22002, 0, '2014-10-01 00:00:00', '0000-00-00 00:00:00', 5, 5, NULL, '2014-10-09 05:55:19', 1, '2014-10-01 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, NULL),
(20, 7, 22002, 0, '2014-10-01 00:00:00', '0000-00-00 00:00:00', 6, 6, NULL, '2014-10-09 05:55:19', 1, '2014-10-01 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, NULL),
(21, 1, 22002, 1, '2014-10-01 07:12:20', '0000-00-00 00:00:00', 10, NULL, NULL, '2014-10-09 05:55:19', 1, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 27),
(22, 1, 22002, 1, '2014-10-01 07:13:25', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 28),
(23, 1, 22002, 1, '2014-10-01 07:13:27', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 29),
(24, 1, 22002, 1, '2014-10-01 07:13:29', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 30),
(25, 1, 22002, 1, '2014-10-01 07:13:31', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 31),
(26, 1, 22002, 1, '2014-10-01 07:13:34', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 32),
(27, 1, 22002, 1, '2014-10-01 07:13:36', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 33),
(28, 1, 22002, 1, '2014-10-01 07:13:38', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 34),
(29, 1, 22002, 1, '2014-10-01 07:13:40', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 35),
(30, 1, 22002, 1, '2014-10-01 07:13:42', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 36),
(31, 1, 22002, 1, '2014-10-01 07:13:44', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 37),
(32, 1, 22002, 1, '2014-10-01 07:13:46', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 38),
(33, 1, 22002, 1, '2014-10-01 07:13:48', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 39),
(34, 1, 22002, 1, '2014-10-01 07:13:50', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 40),
(35, 1, 22002, 1, '2014-10-01 07:13:53', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 41),
(36, 1, 22002, 1, '2014-10-01 07:13:55', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 42),
(37, 1, 22002, 1, '2014-10-01 07:13:57', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 43),
(38, 1, 22002, 1, '2014-10-01 07:13:59', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 44),
(39, 1, 22002, 1, '2014-10-01 07:14:01', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 45),
(40, 1, 22002, 1, '2014-10-01 07:14:03', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 46),
(41, 1, 22002, 1, '2014-10-01 07:14:06', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 47),
(42, 1, 22002, 1, '2014-10-01 07:14:08', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 48),
(43, 1, 22002, 1, '2014-10-01 07:14:10', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 0, 49),
(46, 11, 22002, 1, '2014-10-08 06:56:54', '0000-00-00 00:00:00', NULL, NULL, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 2, 1, NULL),
(51, 11, 22004, 1, '2014-10-09 02:39:51', '0000-00-00 00:00:00', 5, 5, NULL, NULL, 0, '0000-00-00 00:00:00', 0, '0000-00-00 00:00:00', 2, 1, NULL),
(52, 12, 22002, 1, '2014-10-09 03:12:22', '0000-00-00 00:00:00', 5, 5, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 2, 1, NULL),
(53, 2, 22002, 1, '2014-10-09 03:43:39', '0000-00-00 00:00:00', 56, 0, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 1, NULL),
(54, 2, 22002, 1, '2014-10-09 04:12:25', '0000-00-00 00:00:00', 56, 0, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 1, NULL),
(55, 2, 22002, 1, '2014-10-09 05:46:05', '0000-00-00 00:00:00', 56, 0, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 1, NULL),
(56, 2, 22002, 1, '2014-10-09 05:55:19', '0000-00-00 00:00:00', 57, 0, NULL, '2014-10-09 05:55:19', 0, '0000-00-00 00:00:00', 2, '2014-10-09 05:55:19', 1, 1, NULL),
(57, 2, 22002, 1, '2014-10-11 07:16:50', '0000-00-00 00:00:00', 70, 0, NULL, '2014-10-11 07:16:50', 0, '0000-00-00 00:00:00', 2, '2014-10-11 07:16:50', 1, 1, NULL),
(58, 2, 22002, 1, '2014-10-11 23:10:49', '0000-00-00 00:00:00', 72, 0, NULL, '2014-10-11 23:10:49', 0, '0000-00-00 00:00:00', 2, '2014-10-11 23:10:49', 1, 1, NULL),
(59, 2, 22002, 1, '2014-10-11 23:14:37', '0000-00-00 00:00:00', 72, 0, NULL, '2014-10-11 23:14:37', 0, '0000-00-00 00:00:00', 2, '2014-10-11 23:14:37', 1, 1, NULL),
(60, 2, 22002, 1, '2014-10-13 07:54:08', '0000-00-00 00:00:00', 80, 0, NULL, '2014-10-13 07:54:08', 0, '0000-00-00 00:00:00', 2, '2014-10-13 07:54:08', 1, 1, NULL),
(61, 2, 22002, 1, '2014-10-14 11:14:02', '0000-00-00 00:00:00', 81, 0, NULL, '2014-10-14 11:14:03', 0, '0000-00-00 00:00:00', 2, '2014-10-14 11:14:03', 1, 1, NULL),
(62, 12, 22001, 1, '2014-10-16 07:27:58', '0000-00-00 00:00:00', 5, 5, NULL, NULL, 0, '0000-00-00 00:00:00', 0, '0000-00-00 00:00:00', 2, 1, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_tasktypes`
--

CREATE TABLE IF NOT EXISTS `tb_tasktypes` (
  `WorkflowId` smallint(6) NOT NULL,
  `TaskTypeId` int(11) NOT NULL AUTO_INCREMENT,
  `TaskName` varchar(50) NOT NULL,
  `SPName` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsSkipped` tinyint(1) DEFAULT NULL,
  `Sequence` smallint(6) DEFAULT NULL,
  PRIMARY KEY (`TaskTypeId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=27 ;

--
-- Dumping data for table `tb_tasktypes`
--

INSERT INTO `tb_tasktypes` (`WorkflowId`, `TaskTypeId`, `TaskName`, `SPName`, `IsActive`, `IsSkipped`, `Sequence`) VALUES
(1, 1, 'Merchant Review', 'sp', 1, 0, 1),
(1, 2, 'Scan Document', 'sp', 1, 0, 2),
(1, 3, 'Data Entry', 'sp', 1, 0, 3),
(1, 4, 'Monthly Credit Card Volumes', 'sp', 1, 0, 4),
(1, 5, 'Merchant Evaluation', 'sp', 1, 0, 5),
(1, 6, 'Offer Creation', 'sp', 1, 0, 6),
(1, 7, 'Offer Acceptance', 'sp', 1, 0, 7),
(2, 8, 'CW  - Scan Document', 'sp', 1, 0, 1),
(2, 9, 'CW - Data Entry', 'sp', 1, 0, 2),
(2, 10, 'CW - Verification Call', 'sp', 1, 0, 3),
(2, 11, 'CW - Bank Information Verification', 'sp', 1, 0, 4),
(2, 12, 'CW - Corporate Documents Verification', 'sp', 1, 0, 5),
(2, 13, 'CW - Commercial Name Verification', 'sp', 1, 0, 6),
(2, 14, 'CW - Landlord Verification', 'sp', 1, 0, 7),
(2, 15, 'CW - Review', 'sp', 1, 0, 8),
(2, 16, 'CW - Contract', 'sp', 1, 0, 9),
(2, 17, 'CW - Funding', 'sp', 1, 0, 10),
(2, 18, 'CW - Final Validation', 'sp', 1, 0, 11),
(3, 19, 'RW - List of merchants', 'sp', 1, 0, 1),
(3, 20, 'RW - Merchant Evaluation', 'sp', 1, 0, 2),
(3, 21, 'RW - Document Verification', 'sp', 1, 0, 3),
(3, 22, 'RW - Offer Creation', 'sp', 1, 0, 4),
(3, 23, 'RW - Renewal Review', 'sp', 1, 0, 5),
(3, 24, 'RW - Contract', 'sp', 1, 0, 6),
(3, 25, 'RW - Funding', 'sp', 1, 0, 7),
(3, 26, 'RW - Final Validation', 'sp', 1, 0, 8);

-- --------------------------------------------------------

--
-- Table structure for table `tb_usergroups`
--

CREATE TABLE IF NOT EXISTS `tb_usergroups` (
  `UserGroupID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` bigint(20) NOT NULL,
  `GroupID` int(11) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `InsertDate` date NOT NULL,
  `ModifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  PRIMARY KEY (`UserGroupID`),
  KEY `FK_tb_UserGroups_tb_Groups` (`GroupID`),
  KEY `FK_tb_UserGroups_tb_Users` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `tb_users`
--

CREATE TABLE IF NOT EXISTS `tb_users` (
  `ID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserID` varchar(100) NOT NULL,
  `UserName` varchar(200) NOT NULL,
  `Password` varchar(200) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `IsReset` tinyint(1) NOT NULL,
  `IsLocked` tinyint(1) NOT NULL,
  `IsLogggedIN` tinyint(1) NOT NULL,
  `InsertDate` date NOT NULL,
  `InsertUserID` bigint(20) NOT NULL,
  `modifyUserID` bigint(20) DEFAULT NULL,
  `ModifyDate` date DEFAULT NULL,
  `LanguageID` int(11) DEFAULT NULL,
  `contactid` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_tb_Users_Gen_tb_Languages` (`LanguageID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=11 ;

--
-- Dumping data for table `tb_users`
--

INSERT INTO `tb_users` (`ID`, `UserID`, `UserName`, `Password`, `IsActive`, `IsReset`, `IsLocked`, `IsLogggedIN`, `InsertDate`, `InsertUserID`, `modifyUserID`, `ModifyDate`, `LanguageID`, `contactid`) VALUES
(2, 'admin', 'System Admin', '124556', 1, 0, 0, 0, '2014-08-31', 1, NULL, NULL, 1, NULL),
(3, 'Salesrep', 'sales rep', '1245', 1, 0, 0, 0, '2014-08-31', 1, NULL, NULL, 1, NULL),
(4, 'user1', 'User', '125212', 1, 0, 0, 0, '2014-08-31', 1, NULL, NULL, 1, NULL),
(5, 'user2', 'User', '125212', 1, 0, 0, 0, '2014-08-31', 1, NULL, NULL, 1, NULL),
(6, 'user3', 'User', '125212', 1, 0, 0, 0, '2014-08-31', 1, NULL, NULL, 1, NULL),
(7, 'user4', 'User', '125212', 1, 0, 0, 0, '2014-08-31', 1, NULL, NULL, 1, NULL),
(8, 'user5', 'User', '125212', 1, 0, 0, 0, '2014-08-31', 1, NULL, NULL, 1, NULL),
(9, 'colagent', 'colagent1', '123123', 1, 0, 0, 0, '2014-10-12', 1, NULL, NULL, 1, NULL),
(10, 'coalagent', 'coalagent1', '12312312', 1, 0, 0, 0, '2014-10-12', 1, NULL, NULL, 1, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_verificationanswers`
--

CREATE TABLE IF NOT EXISTS `tb_verificationanswers` (
  `AnswerId` bigint(20) NOT NULL AUTO_INCREMENT,
  `QuestionId` bigint(20) DEFAULT NULL,
  `AnswerText` varchar(5000) DEFAULT NULL,
  `MerchantId` bigint(20) DEFAULT NULL,
  `ContractId` bigint(20) DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL,
  PRIMARY KEY (`AnswerId`),
  KEY `QuestionID_idx` (`QuestionId`),
  KEY `ContractId_idx` (`ContractId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=31 ;

--
-- Dumping data for table `tb_verificationanswers`
--

INSERT INTO `tb_verificationanswers` (`AnswerId`, `QuestionId`, `AnswerText`, `MerchantId`, `ContractId`, `InsertDate`) VALUES
(14, 1, 'aaa', 10, 10, NULL),
(15, 2, 'bbb', 10, 10, NULL),
(16, 3, 'ABC', 10, 10, NULL),
(17, 4, '0', 10, 10, NULL),
(18, 5, 'sALES', 10, 10, NULL),
(19, 6, 'cITY', 10, 10, NULL),
(20, 7, 'Business Name', 10, 10, NULL),
(21, 8, 'First Car', 10, 10, NULL),
(22, 9, 'Legal Name', 10, 10, NULL),
(23, 10, 'SSN', 10, 10, NULL),
(28, 11, 'Y', 10, 10, NULL),
(29, 12, '0', 10, 10, NULL),
(30, 13, 'Y', 10, 10, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_verificationquestions`
--

CREATE TABLE IF NOT EXISTS `tb_verificationquestions` (
  `QuestionId` bigint(20) NOT NULL AUTO_INCREMENT,
  `Entity` varchar(100) DEFAULT NULL,
  `Description` varchar(5000) DEFAULT NULL,
  `QuestionType` varchar(100) DEFAULT NULL,
  `isActive` bit(1) DEFAULT NULL,
  `InsertDate` datetime DEFAULT NULL,
  PRIMARY KEY (`QuestionId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=14 ;

--
-- Dumping data for table `tb_verificationquestions`
--

INSERT INTO `tb_verificationquestions` (`QuestionId`, `Entity`, `Description`, `QuestionType`, `isActive`, `InsertDate`) VALUES
(1, 'LandLord', 'Please specify your legal name?', 'text', b'1', '2014-09-09 18:14:37'),
(2, 'Landlord', 'Please specify annual Sales?', 'text', b'1', '2014-09-09 18:14:37'),
(3, 'Contract', 'What is your first car color?', 'text', b'1', '2014-09-09 18:14:37'),
(4, 'Landlord', 'Are you ready for loan?', 'dropdown', b'1', '2014-09-09 18:14:37'),
(5, 'Contract', 'Please specify Annual Sales', 'text', b'1', NULL),
(6, 'Contract', 'What City you born in?', 'text', b'1', NULL),
(7, 'Contract', 'Please specify your Business name', 'text', b'1', NULL),
(8, 'Contract', 'What is color of your First Car?', 'text', b'1', NULL),
(9, 'Contract', 'Please specify you Legal name', 'text', b'1', NULL),
(10, 'Contract', 'Please specify Social Security Number', 'text', b'1', NULL),
(11, 'Contract', 'DropDown Test', 'dropdown', b'1', NULL),
(12, 'Contract', 'Checkbox Test', 'checkbox', b'1', NULL),
(13, 'Contract', 'Radio Button Test', 'radio', b'1', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tb_welcomecallanswers`
--

CREATE TABLE IF NOT EXISTS `tb_welcomecallanswers` (
  `MerchantId` bigint(20) NOT NULL,
  `QuestionId` int(11) NOT NULL,
  `Answer` varchar(200) NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  PRIMARY KEY (`MerchantId`),
  KEY `FK_tb_WelComeCallAnswers_tb_WelComeCallQuestions` (`QuestionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_welcomecallquestions`
--

CREATE TABLE IF NOT EXISTS `tb_welcomecallquestions` (
  `QuestionId` int(11) NOT NULL,
  `QuestionDetail` longtext NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) NOT NULL,
  `ModifyDate` datetime NOT NULL,
  PRIMARY KEY (`QuestionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `tb_workflows`
--

CREATE TABLE IF NOT EXISTS `tb_workflows` (
  `WorkFlowId` smallint(6) NOT NULL AUTO_INCREMENT,
  `WorkFlowName` varchar(50) NOT NULL,
  `IsActive` char(10) NOT NULL,
  PRIMARY KEY (`WorkFlowId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `tb_workflows`
--

INSERT INTO `tb_workflows` (`WorkFlowId`, `WorkFlowName`, `IsActive`) VALUES
(1, 'Prequal Workflow', '1'),
(2, 'Contract Workflow', '1'),
(3, 'Renewals Workflow', '1');

-- --------------------------------------------------------

--
-- Table structure for table `tmp_tb_contacts`
--

CREATE TABLE IF NOT EXISTS `tmp_tb_contacts` (
  `ContactId_TMP` bigint(20) NOT NULL AUTO_INCREMENT,
  `ContactTypeId` int(11) DEFAULT NULL,
  `JobTitle` varchar(50) DEFAULT NULL,
  `FirstName` varchar(100) NOT NULL,
  `MiddleName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) NOT NULL,
  `AddressId1` bigint(20) DEFAULT NULL,
  `DateOfbirth` date DEFAULT NULL,
  `SSN` varchar(50) DEFAULT NULL,
  `MerchantId` bigint(20) DEFAULT NULL,
  `IsOwner` bit(1) DEFAULT b'0',
  `IsProcessed` bit(1) DEFAULT b'0',
  `HomePhone` varchar(45) DEFAULT NULL,
  `CellPhone` varchar(45) DEFAULT NULL,
  `sfid` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ContactId_TMP`),
  KEY `FK_TMP_tb_Contacts_TMP_tb_Merchants` (`MerchantId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=26 ;

--
-- Dumping data for table `tmp_tb_contacts`
--

INSERT INTO `tmp_tb_contacts` (`ContactId_TMP`, `ContactTypeId`, `JobTitle`, `FirstName`, `MiddleName`, `LastName`, `AddressId1`, `DateOfbirth`, `SSN`, `MerchantId`, `IsOwner`, `IsProcessed`, `HomePhone`, `CellPhone`, `sfid`) VALUES
(3, 0, 'Sales Rep', 'Harry', NULL, 'Sharma', NULL, '0000-00-00', '123456789', 27, b'1', b'0', '123456789', '234567899', NULL),
(4, 0, 'Sales Rep', 'Narry', NULL, 'Sharma', NULL, '0000-00-00', '234567890', 28, b'1', b'0', '123456789', '234567899', NULL),
(5, 0, 'Sales Rep', 'Vikas', NULL, 'Sharma', NULL, '0000-00-00', '345678900', 29, b'1', b'0', '123456789', '234567899', NULL),
(6, 0, 'Sales Rep', 'Bhavneet', NULL, 'Kumar', NULL, '0000-00-00', '456789000', 30, b'1', b'0', '123456789', '234567899', NULL),
(7, 0, 'Sales Rep', 'Parry', NULL, 'Sharma', NULL, '0000-00-00', '127334343', 31, b'1', b'0', '123456789', '234567899', NULL),
(8, 0, 'Sales Rep', 'Larry', NULL, 'Sharma', NULL, '0000-00-00', '127334343', 32, b'1', b'0', '123456789', '234567899', NULL),
(9, 0, 'Sales Rep', 'Owarry', NULL, 'Sharma', NULL, '0000-00-00', '127334343', 33, b'1', b'0', '123456789', '234567899', NULL),
(10, 0, 'Sales Rep', 'OJarry', NULL, 'Sharma', NULL, '0000-00-00', '127334343', 34, b'1', b'0', '123456789', '234567899', NULL),
(11, 0, 'Sales Rep', 'Edna', NULL, 'Frank', NULL, '0000-00-00', '127334343', 35, b'1', b'0', '123456789', '234567899', NULL),
(12, 0, 'Sales Rep', 'Ashley', NULL, 'James', NULL, '0000-00-00', '127334343', 36, b'1', b'0', '123456789', '234567899', NULL),
(13, 0, 'Sales Rep', 'Liz', NULL, 'D''Cruz', NULL, '0000-00-00', '127334343', 37, b'1', b'0', '123456789', '234567899', NULL),
(14, 0, 'Sales Rep', 'Sean', NULL, 'Forbes', NULL, '0000-00-00', '127334343', 38, b'1', b'0', '123456789', '234567899', NULL),
(15, 0, 'Sales Rep', 'Jack', NULL, 'Rogers', NULL, '0000-00-00', '127334343', 39, b'1', b'0', '123456789', '234567899', NULL),
(16, 0, 'Sales Rep', 'Pat', NULL, 'Stumuller', NULL, '0000-00-00', '127334343', 40, b'1', b'0', '123456789', '234567899', NULL),
(17, 0, 'Sales Rep', 'Andy', NULL, 'Young', NULL, '0000-00-00', '127334343', 41, b'1', b'0', '123456789', '234567899', NULL),
(18, 0, 'Sales Rep', 'John', NULL, 'Bond', NULL, '0000-00-00', '127334343', 42, b'1', b'0', '123456789', '234567899', NULL),
(19, NULL, 'Sales Rep', 'Josh', NULL, 'Davis', NULL, '0000-00-00', '127334343', 43, b'1', b'0', '123456789', '234567899', NULL),
(20, NULL, 'Sales Rep', 'Jane', NULL, 'Grey', NULL, '0000-00-00', '127334343', 44, b'1', b'0', '123456789', '234567899', NULL),
(21, NULL, 'Sales Rep', 'Lauren', NULL, 'Boyle', NULL, '0000-00-00', '127334343', 45, b'1', b'0', '123456789', '234567899', NULL),
(22, NULL, 'Sales Rep', 'Jake', NULL, 'Llorrac', NULL, '0000-00-00', '127334343', 46, b'1', b'0', '123456789', '234567899', NULL),
(23, NULL, 'Sales Rep', 'OMarry', NULL, 'Sharma', NULL, '0000-00-00', '127334343', 47, b'1', b'0', '123456789', '234567899', NULL),
(24, NULL, 'Sales Rep', 'OTarry', NULL, 'Sharma', NULL, '0000-00-00', '127334343', 48, b'1', b'0', '123456789', '234567899', NULL),
(25, NULL, 'Sales Rep', 'OKarry', NULL, 'Sharma', NULL, '0000-00-00', '127334343', 49, b'1', b'0', '123456789', '234567899', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tmp_tb_merchants`
--

CREATE TABLE IF NOT EXISTS `tmp_tb_merchants` (
  `MerchantId_TMP` bigint(20) NOT NULL AUTO_INCREMENT,
  `CompanyId` smallint(6) DEFAULT NULL,
  `LegalName` varchar(200) NOT NULL,
  `BusinessName` varchar(200) NOT NULL,
  `SalesRepId` varchar(200) DEFAULT NULL,
  `BusinessStartDate` date DEFAULT NULL,
  `IndustryTypeId` int(11) DEFAULT NULL,
  `BusinessTypeId` int(11) DEFAULT NULL,
  `LastActivityDate` datetime DEFAULT NULL,
  `InsertUserId` bigint(20) NOT NULL,
  `InsertDate` datetime NOT NULL,
  `ModifyUserId` bigint(20) DEFAULT NULL,
  `ModifyDate` datetime DEFAULT NULL,
  `CNetProcessorNbr` varchar(20) DEFAULT NULL,
  `VNetProcessorNbr` varchar(20) DEFAULT NULL,
  `RNCNumber` varchar(11) DEFAULT NULL,
  `TelePhone1` varchar(20) DEFAULT NULL,
  `TelePhone2` varchar(20) DEFAULT NULL,
  `IsProcessed` tinyint(1) DEFAULT NULL,
  `OtherProcessorNbr` varchar(20) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `Province` varchar(50) DEFAULT NULL,
  `Country` varchar(50) DEFAULT NULL,
  `Comm_Reference` varchar(200) DEFAULT NULL,
  `Comm_Reference1` varchar(200) DEFAULT NULL,
  `Comm_Ref_Telephone` varchar(12) DEFAULT NULL,
  `Comm_Ref1_Telephone` varchar(12) DEFAULT NULL,
  `businessType` varchar(200) DEFAULT NULL,
  `industrytype` varchar(200) DEFAULT NULL,
  `accountId` varchar(200) DEFAULT NULL,
  `addressline1` varchar(200) DEFAULT NULL,
  `addressline2` varchar(200) DEFAULT NULL,
  `salesrepName` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`MerchantId_TMP`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=93 ;

--
-- Dumping data for table `tmp_tb_merchants`
--

INSERT INTO `tmp_tb_merchants` (`MerchantId_TMP`, `CompanyId`, `LegalName`, `BusinessName`, `SalesRepId`, `BusinessStartDate`, `IndustryTypeId`, `BusinessTypeId`, `LastActivityDate`, `InsertUserId`, `InsertDate`, `ModifyUserId`, `ModifyDate`, `CNetProcessorNbr`, `VNetProcessorNbr`, `RNCNumber`, `TelePhone1`, `TelePhone2`, `IsProcessed`, `OtherProcessorNbr`, `City`, `Province`, `Country`, `Comm_Reference`, `Comm_Reference1`, `Comm_Ref_Telephone`, `Comm_Ref1_Telephone`, `businessType`, `industrytype`, `accountId`, `addressline1`, `addressline2`, `salesrepName`) VALUES
(27, 5, 'Viku', 'businessName', '5', '2013-09-01', 1, 1, NULL, 10001, '2014-10-01 07:12:20', NULL, NULL, '12349', '123456789', '8757456', '9501024684', '', NULL, NULL, 'Panchkula', '', 'India', NULL, NULL, NULL, NULL, '', '', '00190000014cwoeAAA', NULL, NULL, NULL),
(28, 5, 'Tiky', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:25', NULL, NULL, '', '', '243234243', '950125858', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011xejlAAA', NULL, NULL, NULL),
(29, 5, 'Likke', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:27', NULL, NULL, '', '', '234324', '950125858', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000014cwqHAAQ', NULL, NULL, NULL),
(30, 5, 'asdas', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:29', NULL, NULL, '111', '1111', '1112244', '(842) 795-0073', '99988899', NULL, NULL, 'Chandigarh', 'Chandigarh', 'India', NULL, NULL, NULL, NULL, '', 'Industry Types', '00190000012YnukAAC', NULL, NULL, NULL),
(31, 5, 'Jerk', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:31', NULL, NULL, '', '', '234234', '950125858', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000012VTAJAA4', NULL, NULL, NULL),
(32, 5, 'Lark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:34', NULL, NULL, '', '', '534534', '950125858', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000012VTKdAAO', NULL, NULL, NULL),
(33, 5, 'Park', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:36', NULL, NULL, '', '', '345345', '950125858', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000012VT9BAAW', NULL, NULL, NULL),
(34, 5, 'Nark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:38', NULL, NULL, '', '', '345345', '950125858', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000012VOJIAA4', NULL, NULL, NULL),
(35, 5, 'Kark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:40', NULL, NULL, '', '', '3445', '(650) 867-3450', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENSAA2', NULL, NULL, NULL),
(36, 5, 'Qark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:42', NULL, NULL, '', '', '4554', '+44 191 4956203', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENTAA2', NULL, NULL, NULL),
(37, 5, 'Wark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:44', NULL, NULL, '', '', '3454', '(650) 450-8810', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENUAA2', NULL, NULL, NULL),
(38, 5, 'Eark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:46', NULL, NULL, '', '', '36635', '(512) 757-6000', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENVAA2', NULL, NULL, NULL),
(39, 5, 'Rark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:48', NULL, NULL, '', '', '234234', '(336) 222-7000', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENWAA2', NULL, NULL, NULL),
(40, 5, 'Yark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:50', NULL, NULL, '', '', '356456', '(014) 427-4427', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENXAA2', NULL, NULL, NULL),
(41, 5, 'Uark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:53', NULL, NULL, '', '', '23424', '(785) 241-6200', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENYAA2', NULL, NULL, NULL),
(42, 5, 'Iark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:55', NULL, NULL, '', '', '56456', '(312) 596-1000', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENZAA2', NULL, NULL, NULL),
(43, 5, 'Oark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:57', NULL, NULL, '', '', '546456', '(503) 421-7800', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENaAAM', NULL, NULL, NULL),
(44, 5, 'Park', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:13:59', NULL, NULL, '', '', '3434', '(520) 773-9050', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENbAAM', NULL, NULL, NULL),
(45, 5, 'Aark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:14:01', NULL, NULL, '', '', '4564', '(212) 842-5500', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENcAAM', NULL, NULL, NULL),
(46, 5, 'Sark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:14:03', NULL, NULL, '', '', '4566', '(415) 901-7000', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000011mENdAAM', NULL, NULL, NULL),
(47, 5, 'Dark', 'businessName', '5', '2013-09-02', 1, 1, NULL, 1, '2014-10-01 07:14:06', NULL, NULL, '111111111', '111111111111', '232323', '', '', NULL, NULL, 'Panchkula', 'Haryana', 'India', NULL, NULL, NULL, NULL, '', 'Industry Types', '00190000015X4SdAAK', NULL, NULL, NULL),
(48, 5, 'Fark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:14:08', NULL, NULL, '', '', '232323', '', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000015X4PFAA0', NULL, NULL, NULL),
(49, 5, 'Gark', 'businessName', '5', '2013-09-01', 1, 1, NULL, 1, '2014-10-01 07:14:10', NULL, NULL, '', '', '232323', '', '', NULL, NULL, '', '', '', NULL, NULL, NULL, NULL, '', '', '00190000015X4LSAA0', NULL, NULL, NULL),
(50, 5, 'Likke', 'businessName', '0', '2013-09-01', 11001, 0, NULL, 0, '2014-10-06 10:34:31', NULL, NULL, NULL, NULL, '234324', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(51, 5, 'Tiky', 'businessName', '0', '2013-09-01', 11001, 0, NULL, 0, '2014-10-06 10:45:04', NULL, NULL, NULL, NULL, '243234243', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(52, 5, 'Tiky', 'businessName', '0', '2013-09-01', 11001, 0, NULL, 0, '2014-10-06 10:45:17', NULL, NULL, NULL, NULL, '243234243', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(53, 5, 'sample string 3', 'sample string 2', '5', '2014-10-09', 10, 1, NULL, 0, '2014-10-09 02:59:28', NULL, NULL, NULL, NULL, 'sample stri', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(54, 5, 'sample string 3', 'sample string 2', '5', '2014-10-09', 10, 1, NULL, 0, '2014-10-09 03:01:11', NULL, NULL, NULL, NULL, 'sample stri', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(55, 5, 'sample string 3', 'sample string 2', '5', '2014-10-09', 10, 1, NULL, 0, '2014-10-09 03:16:20', 0, '2014-10-09 03:36:16', NULL, NULL, 'sample stri', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(56, 5, 'sample string 3', 'sample string 2', '5', '2014-10-09', 10, 11001, NULL, 0, '2014-10-09 03:42:15', 0, '2014-10-09 05:46:05', NULL, NULL, 'sample stri', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(57, 5, 'sample string 33', 'sample string 22', '5', '2014-10-09', 10, 11001, NULL, 0, '2014-10-09 05:53:36', 0, '2014-10-09 05:55:19', NULL, NULL, 'sample stri', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(58, 5, 'sample string 332', 'sample string 222', '5', '2014-10-09', 10, 11001, NULL, 0, '2014-10-10 00:01:31', NULL, NULL, NULL, NULL, 'sample stri', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(59, 5, 'sample string 5332', 'sample string 2252', '5', '2014-10-09', 10, 11001, NULL, 0, '2014-10-10 05:34:29', NULL, NULL, NULL, NULL, 'sample stri', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(60, 5, 'sample string 53321', 'sample string 22512', '5', '2014-10-09', 10, 11001, NULL, 0, '2014-10-10 05:38:31', NULL, NULL, NULL, NULL, 'sample stri', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(61, 5, 'Viku', 'businessName', '0', '2013-09-01', 0, 0, NULL, 0, '2014-10-10 08:34:14', NULL, NULL, NULL, NULL, '8757456', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(62, 5, 'Viku', 'businessName', '0', '2013-09-01', 0, 0, NULL, 0, '2014-10-10 08:38:13', NULL, NULL, NULL, NULL, '8757456', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(63, 5, 'Viku', 'businessName', '0', '2013-09-01', 0, 0, NULL, 0, '2014-10-10 08:51:30', NULL, NULL, NULL, NULL, '8757456', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(65, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 10001, 1, NULL, 10001, '2014-10-11 00:00:00', NULL, NULL, NULL, NULL, '45745154', '123456789', '987654321', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(68, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 14001, 0, NULL, 0, '2014-10-11 05:53:10', 0, '2014-10-11 06:00:29', NULL, NULL, '45745154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(69, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-11 06:44:45', NULL, NULL, NULL, NULL, '45745154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(70, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-11 07:15:51', 0, '2014-10-11 07:16:50', NULL, NULL, '45745154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(71, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-11 22:36:31', NULL, NULL, NULL, NULL, '45745154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(72, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-11 23:09:44', 0, '2014-10-11 23:14:37', NULL, NULL, '45745154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(73, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-11 23:51:04', NULL, NULL, NULL, NULL, '45745154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(74, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-11 23:55:23', NULL, NULL, NULL, NULL, '45745154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(75, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-12 00:17:02', NULL, NULL, NULL, NULL, '45745154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(76, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-12 00:19:26', NULL, NULL, NULL, NULL, '45745154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(77, 5, 'RJ Legalname', 'RJ Businessname', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-12 00:21:49', NULL, NULL, NULL, NULL, '45745154', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(78, 5, '', '', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-12 21:20:02', NULL, NULL, NULL, NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(79, 5, '', '', '0', '2013-09-01', 14001, 11001, NULL, 0, '2014-10-12 21:20:36', NULL, NULL, NULL, NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(80, 5, 'Test MM', 'Test MM', '5', '2010-10-09', 10, 11001, NULL, 0, '2014-10-13 07:53:34', 0, '2014-10-13 07:54:08', NULL, NULL, '5552', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(81, 5, 'sample string 3', 'sample string 2', '5', '2014-10-09', 10, 11001, NULL, 0, '2014-10-14 11:13:48', 0, '2014-10-14 11:14:02', NULL, NULL, 'sample stri', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(86, 5, 'TJs ', 'TJs ', '5', '2001-10-15', 10, 11001, NULL, 18, '2014-10-15 03:46:40', NULL, NULL, NULL, NULL, '1122334455', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(87, 5, '', '', '0', '0001-01-01', 14004, 11001, NULL, 0, '2014-10-15 09:32:10', NULL, NULL, NULL, NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(88, 5, '', '', '0', '0001-01-01', 14001, 11001, NULL, 0, '2014-10-15 09:52:25', NULL, NULL, NULL, NULL, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(89, 5, 'legal', 'business', '0', '2014-10-14', 14001, 11001, NULL, 0, '2014-10-15 10:14:26', NULL, NULL, NULL, NULL, '123456', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(90, 5, 'Enter text..', 'Enter text..', '0', '2014-10-24', 14001, 11001, NULL, 0, '2014-10-15 10:25:40', NULL, NULL, NULL, NULL, '8757456', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(91, 5, 'HarishTM', 'HarishTM', '0', '2004-10-04', 14001, 11001, NULL, 0, '2014-10-15 10:46:07', NULL, NULL, NULL, NULL, '232423', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL),
(92, 5, 'HarishTM', 'HarishTM', '0', '2004-10-04', 14001, 11001, NULL, 0, '2014-10-15 10:47:11', NULL, NULL, NULL, NULL, '232423', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `tmp_tb_notes`
--

CREATE TABLE IF NOT EXISTS `tmp_tb_notes` (
  `NoteId` bigint(20) NOT NULL AUTO_INCREMENT,
  `NoteTypeId` int(11) NOT NULL,
  `MerchantId` bigint(20) NOT NULL,
  `Note` longtext NOT NULL,
  `ScreenName` varchar(50) NOT NULL,
  PRIMARY KEY (`NoteId`),
  KEY `FK_tb_Notes_Gen_tb_NoteTypes` (`NoteTypeId`),
  KEY `FK_tb_Notes_tb_Merchants` (`MerchantId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=13 ;

--
-- Dumping data for table `tmp_tb_notes`
--

INSERT INTO `tmp_tb_notes` (`NoteId`, `NoteTypeId`, `MerchantId`, `Note`, `ScreenName`) VALUES
(2, 500001, 1, 'sample string 2', 'Merchant Review'),
(3, 500001, 1, 'Test Note', 'Test Screen Name'),
(10, 500001, 1, 'Designed Test Note', 'AAAA'),
(11, 500001, 1, 'My Merchant Notes', 'AAA'),
(12, 530001, 1, 'Designed Test Note', 'AAA');

-- --------------------------------------------------------

--
-- Structure for view `avz_vw_merchantSearchResults`
--
DROP TABLE IF EXISTS `avz_vw_merchantSearchResults`;

CREATE ALGORITHM=UNDEFINED DEFINER=`avz_access`@`14.98.%.%` SQL SECURITY DEFINER VIEW `avz_vw_merchantSearchResults` AS select `mrc`.`LegalName` AS `merchantName`,`mrc`.`LegalName` AS `legalName`,`mrc`.`BusinessName` AS `BusinessName`,`mrc`.`RNCNumber` AS `RNCNumber`,concat(`repsal`.`FirstName`,' ',`repsal`.`LastName`) AS `assignedSalesRep`,concat(`owncont`.`FirstName`,' ',`owncont`.`LastName`) AS `ownerName`,`mrc`.`SalesRepId` AS `salesRepId`,`tsktyp`.`TaskName` AS `taskName`,`tsktyp`.`TaskTypeId` AS `taskTypeId`,`wrk`.`WorkFlowName` AS `workFlowName`,`tsk`.`WorkflowId` AS `WorkflowId`,`tsk`.`ContractId` AS `contractid`,`tsk`.`AssignedUserId` AS `assigneduserId`,`mrc`.`MerchantId` AS `merchantId`,`tskst`.`StatusId` AS `taskStatusId`,`tskst`.`StatusName` AS `tasKStatus` from (((((((((`tb_merchants` `mrc` left join `tb_salesrep` `sal` on((`mrc`.`SalesRepId` = `sal`.`SalesRepId`))) left join `tb_salesrep_contact` `repsal` on((`sal`.`Contactid` = `repsal`.`ContactId`))) left join `tb_tasks` `tsk` on((`tsk`.`MerchantId` = `mrc`.`MerchantId`))) left join `tb_tasktypes` `tsktyp` on((`tsk`.`TaskTypeId` = `tsktyp`.`TaskTypeId`))) left join `tb_workflows` `wrk` on((`tsk`.`WorkflowId` = `wrk`.`WorkFlowId`))) left join `tb_owners` `own` on((`own`.`OwnerId` = `mrc`.`OwnerId`))) left join `lkp_tb_entitytypes` `ent` on((`ent`.`EntitytypeID` = `mrc`.`BusinessTypeId`))) left join `lkp_tb_statuses` `tskst` on((`tsk`.`StatusId` = `tskst`.`StatusId`))) left join `tb_contacts` `owncont` on((`owncont`.`ContactId` = `own`.`ContactId`)));

-- --------------------------------------------------------

--
-- Structure for view `avz_vw_merchantTempSearchResults`
--
DROP TABLE IF EXISTS `avz_vw_merchantTempSearchResults`;

CREATE ALGORITHM=UNDEFINED DEFINER=`avz_access`@`14.98.%.%` SQL SECURITY DEFINER VIEW `avz_vw_merchantTempSearchResults` AS select `mrc`.`LegalName` AS `merchantName`,`mrc`.`LegalName` AS `legalName`,`mrc`.`BusinessName` AS `businessName`,`mrc`.`RNCNumber` AS `RNCNumber`,concat(`repsal`.`FirstName`,' ',`repsal`.`LastName`) AS `assignedSalesRep`,'' AS `ownerName`,`mrc`.`SalesRepId` AS `salesRepId`,'Merchant Review' AS `taskName`,'0' AS `taskTypeId`,`wrk`.`WorkFlowName` AS `workFlowName`,`tsk`.`WorkflowId` AS `WorkflowId`,`tsk`.`ContractId` AS `contractid`,`tsk`.`AssignedUserId` AS `assigneduserId`,`mrc`.`MerchantId_TMP` AS `merchantId`,0 AS `taskStatusId`,'Assigned' AS `tasKStatus` from ((((((`tmp_tb_merchants` `mrc` left join `tb_salesrep` `sal` on((`mrc`.`SalesRepId` = `sal`.`SalesRepId`))) left join `tb_salesrep_contact` `repsal` on((`sal`.`Contactid` = `repsal`.`ContactId`))) left join `tb_tasks` `tsk` on((`tsk`.`MerchantId` = `mrc`.`MerchantId_TMP`))) left join `tb_tasktypes` `tsktyp` on((`tsk`.`TaskTypeId` = `tsktyp`.`TaskTypeId`))) left join `lkp_tb_entitytypes` `ent` on((`ent`.`EntitytypeID` = `mrc`.`BusinessTypeId`))) left join `tb_workflows` `wrk` on((`tsk`.`WorkflowId` = `wrk`.`WorkFlowId`)));

-- --------------------------------------------------------

--
-- Structure for view `avz_vw_merchantTempdetails`
--
DROP TABLE IF EXISTS `avz_vw_merchantTempdetails`;

CREATE ALGORITHM=UNDEFINED DEFINER=`avz_access`@`14.98.%.%` SQL SECURITY DEFINER VIEW `avz_vw_merchantTempdetails` AS select `mrc`.`LegalName` AS `merchantName`,`mrc`.`LegalName` AS `legalName`,`mrc`.`BusinessName` AS `businessName`,`mrc`.`BusinessStartDate` AS `businessStartDate`,`mrc`.`BusinessTypeId` AS `BusinessTypeId`,'' AS `businessUrl`,`mrc`.`RNCNumber` AS `RNCNumber`,concat(`repsal`.`FirstName`,' ',`repsal`.`LastName`) AS `assignedSalesRep`,'' AS `ownerName`,`mrc`.`SalesRepId` AS `salesRepId`,`tsktyp`.`TaskName` AS `taskName`,`wrk`.`WorkFlowName` AS `workFlowName`,`tsk`.`WorkflowId` AS `WorkflowId`,`tsk`.`ContractId` AS `contractid`,`tsk`.`AssignedUserId` AS `assigneduserId`,`tsk`.`AssignedDate` AS `assignedDate`,`mrc`.`MerchantId_TMP` AS `merchantId`,`mrc`.`IndustryTypeId` AS `industryTypeId`,'' AS `SSN`,'' AS `phoneNumber`,`ent`.`Name` AS `TypeofBusinessentity` from ((((((`tmp_tb_merchants` `mrc` join `tb_salesrep` `sal` on((`mrc`.`SalesRepId` = `sal`.`SalesRepId`))) join `tb_salesrep_contact` `repsal` on((`sal`.`Contactid` = `repsal`.`ContactId`))) left join `tb_tasks` `tsk` on((`tsk`.`MerchantId` = `mrc`.`MerchantId_TMP`))) left join `tb_tasktypes` `tsktyp` on((`tsk`.`TaskTypeId` = `tsktyp`.`TaskTypeId`))) left join `lkp_tb_entitytypes` `ent` on((`ent`.`EntitytypeID` = `mrc`.`BusinessTypeId`))) left join `tb_workflows` `wrk` on((`tsk`.`WorkflowId` = `wrk`.`WorkFlowId`)));

-- --------------------------------------------------------

--
-- Structure for view `avz_vw_merchantdetails`
--
DROP TABLE IF EXISTS `avz_vw_merchantdetails`;

CREATE ALGORITHM=UNDEFINED DEFINER=`avz_access`@`14.98.%.%` SQL SECURITY DEFINER VIEW `avz_vw_merchantdetails` AS select `mrc`.`LegalName` AS `merchantName`,`mrc`.`LegalName` AS `legalName`,`mrc`.`BusinessName` AS `businessName`,`mrc`.`BusinessStartDate` AS `businessStartDate`,`mrc`.`BusinessTypeId` AS `BusinessTypeId`,`mrc`.`BusinessWebSite` AS `businessUrl`,`mrc`.`RNCNumber` AS `RNCNumber`,concat(`repsal`.`FirstName`,' ',`repsal`.`LastName`) AS `assignedSalesRep`,concat(`owncont`.`FirstName`,' ',`owncont`.`LastName`) AS `ownerName`,`mrc`.`SalesRepId` AS `salesRepId`,`tsktyp`.`TaskName` AS `taskName`,`wrk`.`WorkFlowName` AS `workFlowName`,`tsk`.`WorkflowId` AS `WorkflowId`,`tsk`.`ContractId` AS `contractid`,`tsk`.`AssignedUserId` AS `assigneduserId`,`tsk`.`AssignedDate` AS `assignedDate`,`mrc`.`MerchantId` AS `merchantId`,`mrc`.`IndustryTypeId` AS `industryTypeId`,`owncont`.`SSN` AS `SSN`,`owncont`.`CellPhone` AS `phoneNumber`,`ent`.`Name` AS `TypeofBusinessentity`,concat(`add`.`Address1`,' ',`add`.`Address2`,' ',`add`.`State`,' ',`add`.`City`,' ',`add`.`Country`) AS `address` from (((((((((`tb_merchants` `mrc` join `tb_salesrep` `sal` on((`mrc`.`SalesRepId` = `sal`.`SalesRepId`))) join `tb_salesrep_contact` `repsal` on((`sal`.`Contactid` = `repsal`.`ContactId`))) left join `tb_tasks` `tsk` on((`tsk`.`MerchantId` = `mrc`.`MerchantId`))) left join `tb_tasktypes` `tsktyp` on((`tsk`.`TaskTypeId` = `tsktyp`.`TaskTypeId`))) left join `tb_workflows` `wrk` on((`tsk`.`WorkflowId` = `wrk`.`WorkFlowId`))) left join `tb_owners` `own` on((`own`.`OwnerId` = `mrc`.`OwnerId`))) left join `lkp_tb_entitytypes` `ent` on((`ent`.`EntitytypeID` = `mrc`.`BusinessTypeId`))) left join `tb_contacts` `owncont` on((`owncont`.`ContactId` = `own`.`ContactId`))) left join `tb_addresses` `add` on((`add`.`AddressId` = `owncont`.`AddressId1`)));

--
-- Constraints for dumped tables
--

--
-- Constraints for table `lkp_tb_declinereasons`
--
ALTER TABLE `lkp_tb_declinereasons`
  ADD CONSTRAINT `FK_tb_DeclineReasons_tb_Workflows` FOREIGN KEY (`workFlowId`) REFERENCES `tb_workflows` (`WorkFlowId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `lkp_tb_expenses`
--
ALTER TABLE `lkp_tb_expenses`
  ADD CONSTRAINT `FK_tb_Expenses_Gen_tb_ExpenceTypes` FOREIGN KEY (`ExpenseTypeId`) REFERENCES `lkp_tb_expencetypes` (`ExpenseTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `lkp_tb_industrytypes`
--
ALTER TABLE `lkp_tb_industrytypes`
  ADD CONSTRAINT `FK_industrytypeallowable_stateguidelinetype_lkp_tb_types` FOREIGN KEY (`StatGuideLineType`) REFERENCES `lkp_tb_types` (`TypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_collectoruploadeddocuments`
--
ALTER TABLE `tb_collectoruploadeddocuments`
  ADD CONSTRAINT `FK_tb_CollectorUploadedDocuments_Gen_tb_DocumentTypes` FOREIGN KEY (`DocumentTypeID`) REFERENCES `lkp_tb_documenttypes` (`DocumentTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_contracts`
--
ALTER TABLE `tb_contracts`
  ADD CONSTRAINT `FK_tb_Contracts_DeclineReasons` FOREIGN KEY (`DeclineReasonID`) REFERENCES `lkp_tb_declinereasons` (`DeclineReasonId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_tb_Contracts_Gen_tb_Statuses` FOREIGN KEY (`StatusId`) REFERENCES `lkp_tb_statuses` (`StatusId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_documents`
--
ALTER TABLE `tb_documents`
  ADD CONSTRAINT `FK__tb_Docume__Docum__0D7A0286` FOREIGN KEY (`DocumentTypeId`) REFERENCES `lkp_tb_documenttypes` (`DocumentTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_emailgroupmembers`
--
ALTER TABLE `tb_emailgroupmembers`
  ADD CONSTRAINT `FK_tb_EmailGroupMembers_tb_EmailGroup` FOREIGN KEY (`GroupId`) REFERENCES `tb_emailgroup` (`GroupId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_grouprights`
--
ALTER TABLE `tb_grouprights`
  ADD CONSTRAINT `FK_tb_GroupRights_tb_Groups` FOREIGN KEY (`GroupID`) REFERENCES `tb_groups` (`GroupID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_tb_GroupRights_tb_Users` FOREIGN KEY (`InsertUserID`) REFERENCES `tb_users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_groups`
--
ALTER TABLE `tb_groups`
  ADD CONSTRAINT `FK_tb_Groups_tb_Users` FOREIGN KEY (`InsertUserID`) REFERENCES `tb_users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_merchantactivities`
--
ALTER TABLE `tb_merchantactivities`
  ADD CONSTRAINT `FK_tb_MerchantActivitietypeId_tb_ActivityType` FOREIGN KEY (`ActivityTypeId`) REFERENCES `tb_activitytype` (`ActivityTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_merchants`
--
ALTER TABLE `tb_merchants`
  ADD CONSTRAINT `FK_tb_Merchants_Gen_tb_Statuses` FOREIGN KEY (`StatusId`) REFERENCES `lkp_tb_statuses` (`StatusId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_merchanttradereference`
--
ALTER TABLE `tb_merchanttradereference`
  ADD CONSTRAINT `FK_Merchant_tb_merchanttradereference_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_notes`
--
ALTER TABLE `tb_notes`
  ADD CONSTRAINT `FK_tb_Notes_Gen_tb_NoteTypes` FOREIGN KEY (`NoteTypeId`) REFERENCES `lkp_tb_notetypes` (`NoteTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_offers`
--
ALTER TABLE `tb_offers`
  ADD CONSTRAINT `FK_Merchant_tb_offers_MID` FOREIGN KEY (`MerchantId`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_tb_Offers_tb_Contracts` FOREIGN KEY (`ContractId`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_processor`
--
ALTER TABLE `tb_processor`
  ADD CONSTRAINT `FK_tb_Processor_Gen_tb_ProcessorList` FOREIGN KEY (`ProcessorTypeId`) REFERENCES `lkp_tb_processorlist` (`ProcessorId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_processoractivities`
--
ALTER TABLE `tb_processoractivities`
  ADD CONSTRAINT `FK_tb_ProcessorActivities_Gen_tb_ProcessorList` FOREIGN KEY (`ProcessorId`) REFERENCES `lkp_tb_processorlist` (`ProcessorId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_tb_ProcessorActivities_tb_ActivityType` FOREIGN KEY (`ActivityTypeId`) REFERENCES `tb_activitytype` (`ActivityTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_renewals`
--
ALTER TABLE `tb_renewals`
  ADD CONSTRAINT `FK_Merchant_tb_renewals_MID` FOREIGN KEY (`MerchantID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_tb_Renewals_tb_Contracts` FOREIGN KEY (`ContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_tb_Renewals_tb_Contracts1` FOREIGN KEY (`RenewedContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_tb_Renewals_tb_RenewalsList` FOREIGN KEY (`RenewalID`) REFERENCES `tb_renewalslist` (`RenewalID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_renewalslist`
--
ALTER TABLE `tb_renewalslist`
  ADD CONSTRAINT `FK_tb_RenewalsList_Gen_tb_Statuses` FOREIGN KEY (`StatusID`) REFERENCES `lkp_tb_statuses` (`StatusId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_renewalsrequireddocs`
--
ALTER TABLE `tb_renewalsrequireddocs`
  ADD CONSTRAINT `FK_tb_RenewalsRequiredDocs_Gen_tb_DocumentTypes` FOREIGN KEY (`RequiredDocTypeID`) REFERENCES `lkp_tb_documenttypes` (`DocumentTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_repaymentplan`
--
ALTER TABLE `tb_repaymentplan`
  ADD CONSTRAINT `FK_Merchant_tb_repaymentplan_MID` FOREIGN KEY (`MerchantID`) REFERENCES `tb_merchants` (`MerchantId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_tb_RepaymentPlan_tb_Contracts` FOREIGN KEY (`ContractID`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_tasks`
--
ALTER TABLE `tb_tasks`
  ADD CONSTRAINT `FK_tasktypeid_tasks_tb_taskstypes` FOREIGN KEY (`TaskTypeId`) REFERENCES `tb_tasktypes` (`TaskTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_tb_Tasks_Gen_tb_Statuses` FOREIGN KEY (`StatusId`) REFERENCES `lkp_tb_statuses` (`StatusId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_tb_Tasks_tb_Workflows` FOREIGN KEY (`WorkflowId`) REFERENCES `tb_workflows` (`WorkFlowId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_users`
--
ALTER TABLE `tb_users`
  ADD CONSTRAINT `FK_tb_Users_Gen_tb_Languages` FOREIGN KEY (`LanguageID`) REFERENCES `lkp_tb_languages` (`LanguageID`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_verificationanswers`
--
ALTER TABLE `tb_verificationanswers`
  ADD CONSTRAINT `tb_verificationAnswers_FK_tb_contracts_ContractId` FOREIGN KEY (`ContractId`) REFERENCES `tb_contracts` (`ContractId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `tb_verificationAnswers_FK_tb_verificationquestions_QuestionId` FOREIGN KEY (`QuestionId`) REFERENCES `tb_verificationquestions` (`QuestionId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tb_welcomecallanswers`
--
ALTER TABLE `tb_welcomecallanswers`
  ADD CONSTRAINT `FK_tb_WelComeCallAnswers_tb_WelComeCallQuestions` FOREIGN KEY (`QuestionId`) REFERENCES `tb_welcomecallquestions` (`QuestionId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tmp_tb_notes`
--
ALTER TABLE `tmp_tb_notes`
  ADD CONSTRAINT `FK_tmp_tb_Notes_Gen_tb_NoteTypes` FOREIGN KEY (`NoteTypeId`) REFERENCES `lkp_tb_notetypes` (`NoteTypeId`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
