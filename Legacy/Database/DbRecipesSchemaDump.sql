-- MySQL dump 10.13  Distrib 8.0.20, for macos10.15 (x86_64)
--
-- Host: localhost    Database: dbRecipes
-- ------------------------------------------------------
-- Server version	8.0.20

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `beverages`
--

DROP TABLE IF EXISTS `beverages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `beverages` (
  `IDBeverage` varchar(64) NOT NULL,
  PRIMARY KEY (`IDBeverage`),
  CONSTRAINT `FK_Beverages_Ingredients` FOREIGN KEY (`IDBeverage`) REFERENCES `ingredients` (`IDIngredient`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `beverageslanguages`
--

DROP TABLE IF EXISTS `beverageslanguages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `beverageslanguages` (
  `IDBeverageLanguage` varchar(64) NOT NULL,
  `IDBeverage` varchar(64) NOT NULL,
  `IDLanguage` int NOT NULL,
  `BeverageInfoLanguage` longtext NOT NULL,
  PRIMARY KEY (`IDBeverageLanguage`),
  KEY `FK_BeveragesLanguages_Beverages` (`IDBeverage`),
  CONSTRAINT `FK_BeveragesLanguages_Beverages` FOREIGN KEY (`IDBeverage`) REFERENCES `beverages` (`IDBeverage`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `beveragesrecipes`
--

DROP TABLE IF EXISTS `beveragesrecipes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `beveragesrecipes` (
  `IDBeverageRecipe` varchar(64) NOT NULL,
  `IDRecipe` varchar(64) NOT NULL,
  `IDBeverage` varchar(64) NOT NULL,
  `IDUserSuggestedBy` varchar(64) NOT NULL,
  `DateSuggestion` datetime NOT NULL,
  `BeverageRecipeAvgRating` double DEFAULT NULL,
  PRIMARY KEY (`IDBeverageRecipe`),
  KEY `FK_BeveragesRecipes_Recipes` (`IDRecipe`),
  KEY `FK_BeveragesRecipes_Beverages` (`IDBeverage`),
  CONSTRAINT `FK_BeveragesRecipes_Beverages` FOREIGN KEY (`IDBeverage`) REFERENCES `beverages` (`IDBeverage`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_BeveragesRecipes_Recipes` FOREIGN KEY (`IDRecipe`) REFERENCES `recipes` (`IDRecipe`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `beveragesrecipesvotes`
--

DROP TABLE IF EXISTS `beveragesrecipesvotes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `beveragesrecipesvotes` (
  `IDBeverageRecipeVote` varchar(64) NOT NULL,
  `IDBeverageRecipe` varchar(64) NOT NULL,
  `IDUser` varchar(64) NOT NULL,
  `RecipeVoteDate` datetime NOT NULL,
  `RecipeVote` int NOT NULL,
  PRIMARY KEY (`IDBeverageRecipeVote`),
  KEY `FK_BeveragesRecipesVotes_BeveragesRecipes` (`IDBeverageRecipe`),
  CONSTRAINT `FK_BeveragesRecipesVotes_BeveragesRecipes` FOREIGN KEY (`IDBeverageRecipe`) REFERENCES `beveragesrecipes` (`IDBeverageRecipe`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `dbrecipesconfigparameters`
--

DROP TABLE IF EXISTS `dbrecipesconfigparameters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dbrecipesconfigparameters` (
  `IDDBRecipeConfigParameter` int NOT NULL,
  `DBRecipeConfigParameterName` varchar(50) NOT NULL,
  `DBRecipeConfigParameterValue` longtext NOT NULL,
  PRIMARY KEY (`IDDBRecipeConfigParameter`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ingredients`
--

DROP TABLE IF EXISTS `ingredients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredients` (
  `IDIngredient` varchar(64) NOT NULL,
  `IDIngredientPreparationRecipe` varchar(64) DEFAULT NULL,
  `IDIngredientImage` varchar(64) DEFAULT NULL,
  `AverageWeightOfOnePiece` double DEFAULT NULL,
  `Kcal100gr` double DEFAULT '0',
  `grProteins` double DEFAULT NULL COMMENT 'http://www.valori-alimenti.com',
  `grFats` double DEFAULT NULL COMMENT 'http://www.valori-alimenti.com',
  `grCarbohydrates` double DEFAULT NULL COMMENT 'http://www.valori-alimenti.com',
  `grAlcohol` double DEFAULT NULL COMMENT 'http://www.valori-alimenti.com',
  `mgCalcium` double DEFAULT NULL,
  `mgSodium` double DEFAULT NULL,
  `mgPhosphorus` double DEFAULT NULL,
  `mgPotassium` double DEFAULT NULL,
  `mgIron` double DEFAULT NULL,
  `mgMagnesium` double DEFAULT NULL,
  `mcgVitaminA` double DEFAULT NULL,
  `mgVitaminB1` double DEFAULT NULL,
  `mgVitaminB2` double DEFAULT NULL,
  `mcgVitaminB9` double DEFAULT NULL,
  `mcgVitaminB12` double DEFAULT NULL,
  `mgVitaminC` double DEFAULT NULL,
  `grSaturatedFat` double DEFAULT NULL,
  `grMonounsaturredFat` double DEFAULT NULL,
  `grPolyunsaturredFat` double DEFAULT NULL,
  `mgCholesterol` double DEFAULT NULL,
  `mgPhytosterols` double DEFAULT NULL,
  `mgOmega3` double DEFAULT NULL,
  `IsForBaby` tinyint(1) DEFAULT NULL,
  `IsMeat` tinyint(1) DEFAULT NULL,
  `IsFish` tinyint(1) DEFAULT NULL,
  `IsVegetarian` tinyint(1) NOT NULL DEFAULT '0',
  `IsVegan` tinyint(1) NOT NULL DEFAULT '0',
  `IsGlutenFree` tinyint(1) NOT NULL DEFAULT '0',
  `IsHotSpicy` tinyint(1) NOT NULL,
  `Checked` tinyint(1) NOT NULL,
  `IngredientCreatedBy` varchar(64) DEFAULT NULL,
  `IngredientCreationDate` datetime DEFAULT NULL,
  `IngredientModifiedByUser` varchar(64) DEFAULT NULL,
  `IngredientLastMod` datetime DEFAULT NULL,
  `IngredientEnabled` tinyint(1) DEFAULT NULL,
  `January` tinyint(1) NOT NULL,
  `February` tinyint(1) NOT NULL,
  `March` tinyint(1) NOT NULL,
  `April` tinyint(1) NOT NULL,
  `May` tinyint(1) NOT NULL,
  `June` tinyint(1) NOT NULL,
  `July` tinyint(1) NOT NULL,
  `August` tinyint(1) NOT NULL,
  `September` tinyint(1) NOT NULL,
  `October` tinyint(1) NOT NULL,
  `November` tinyint(1) NOT NULL,
  `December` tinyint(1) NOT NULL,
  `grDietaryFiber` double DEFAULT NULL,
  `grStarch` double DEFAULT NULL,
  `grSugar` double DEFAULT NULL,
  PRIMARY KEY (`IDIngredient`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ingredientsallowedquantitytypes`
--

DROP TABLE IF EXISTS `ingredientsallowedquantitytypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredientsallowedquantitytypes` (
  `IDIngredientAllowedQuantityType` varchar(64) NOT NULL,
  `IDingredient` varchar(64) DEFAULT NULL,
  `IDIngredientQuantityType` int DEFAULT NULL,
  PRIMARY KEY (`IDIngredientAllowedQuantityType`),
  KEY `FK_IngredientsAllowedQuantityTypes_Ingredients` (`IDingredient`),
  KEY `FK_IngredientsAllowedQuantityTypes_IngredientsQuantityTypes` (`IDIngredientQuantityType`),
  CONSTRAINT `FK_IngredientsAllowedQuantityTypes_Ingredients` FOREIGN KEY (`IDingredient`) REFERENCES `ingredients` (`IDIngredient`),
  CONSTRAINT `FK_IngredientsAllowedQuantityTypes_IngredientsQuantityTypes` FOREIGN KEY (`IDIngredientQuantityType`) REFERENCES `ingredientsquantitytypes` (`IDIngredientQuantityType`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ingredientsalternatives`
--

DROP TABLE IF EXISTS `ingredientsalternatives`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredientsalternatives` (
  `IDIngredientAlternative` varchar(64) NOT NULL,
  `IDIngredientMain` varchar(64) NOT NULL,
  `IDIngredientSlave` varchar(64) NOT NULL,
  `AddedByUser` varchar(64) NOT NULL,
  `AddedOn` datetime NOT NULL,
  `CheckedBy` varchar(64) DEFAULT NULL,
  `CheckedOn` datetime DEFAULT NULL,
  `Checked` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDIngredientAlternative`),
  UNIQUE KEY `IX_Unique_Main-Slave_Ingr` (`IDIngredientMain`,`IDIngredientSlave`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ingredientscategories`
--

DROP TABLE IF EXISTS `ingredientscategories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredientscategories` (
  `IDIngredientCategory` int NOT NULL,
  `IDIngredientCategoryFather` int DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`IDIngredientCategory`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ingredientscategorieslanguages`
--

DROP TABLE IF EXISTS `ingredientscategorieslanguages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredientscategorieslanguages` (
  `IDIngredientCategoryLanguage` int NOT NULL,
  `IDIngredientCategory` int NOT NULL,
  `IDLanguage` int NOT NULL,
  `IngredientCategoryLanguage` varchar(100) NOT NULL,
  `IngredientCategoryLanguageDesc` varchar(250) DEFAULT NULL,
  PRIMARY KEY (`IDIngredientCategoryLanguage`),
  UNIQUE KEY `IX_UNIQ_IDIngredientCategory_IDLanguage` (`IDIngredientCategory`,`IDLanguage`),
  CONSTRAINT `FK_IngredientsCategoriesLanguages_IngredientsCategories` FOREIGN KEY (`IDIngredientCategory`) REFERENCES `ingredientscategories` (`IDIngredientCategory`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ingredientsingredientscategories`
--

DROP TABLE IF EXISTS `ingredientsingredientscategories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredientsingredientscategories` (
  `IDIngredientIngredientCategory` varchar(64) NOT NULL,
  `IDIngredient` varchar(64) NOT NULL,
  `IDIngredientCategory` int NOT NULL,
  `isPrincipalCategory` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDIngredientIngredientCategory`),
  KEY `FK_IngredientsIngredientsCategories_Ingredients` (`IDIngredient`),
  KEY `FK_IngredientsIngredientsCategories_IngredientsCategories` (`IDIngredientCategory`),
  CONSTRAINT `FK_IngredientsIngredientsCategories_Ingredients` FOREIGN KEY (`IDIngredient`) REFERENCES `ingredients` (`IDIngredient`) ON UPDATE CASCADE,
  CONSTRAINT `FK_IngredientsIngredientsCategories_IngredientsCategories` FOREIGN KEY (`IDIngredientCategory`) REFERENCES `ingredientscategories` (`IDIngredientCategory`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ingredientslanguages`
--

DROP TABLE IF EXISTS `ingredientslanguages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredientslanguages` (
  `IDIngredientLanguage` varchar(64) NOT NULL,
  `IDIngredient` varchar(64) NOT NULL,
  `IDLanguage` int NOT NULL,
  `IngredientSingular` varchar(250) NOT NULL,
  `IngredientPlural` varchar(250) DEFAULT NULL,
  `IngredientDescription` longtext,
  `isAutoTranslate` tinyint(1) NOT NULL,
  `GeoIDRegion` int DEFAULT NULL,
  PRIMARY KEY (`IDIngredientLanguage`),
  KEY `IX_IDIngredient-IDLanguage` (`IDIngredient`,`IDLanguage`,`GeoIDRegion`),
  KEY `IX_IngredientSingular` (`IngredientSingular`,`IngredientPlural`),
  KEY `FK_IngredientsLanguages_Languages` (`IDLanguage`),
  CONSTRAINT `FK_IngredientsLanguages_Ingredients` FOREIGN KEY (`IDIngredient`) REFERENCES `ingredients` (`IDIngredient`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_IngredientsLanguages_Languages` FOREIGN KEY (`IDLanguage`) REFERENCES `languages` (`IDLanguage`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ingredientsquantitytypes`
--

DROP TABLE IF EXISTS `ingredientsquantitytypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredientsquantitytypes` (
  `IDIngredientQuantityType` int NOT NULL,
  `isWeight` tinyint(1) NOT NULL DEFAULT '0',
  `isLiquid` tinyint(1) NOT NULL DEFAULT '0',
  `isPiece` tinyint(1) NOT NULL DEFAULT '0',
  `isStandardQuantityType` tinyint(1) NOT NULL DEFAULT '0',
  `NoStdAvgWeight` double DEFAULT NULL,
  `EnabledToUser` tinyint(1) NOT NULL,
  `ShowInIngredientList` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDIngredientQuantityType`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ingredientsquantitytypeslanguages`
--

DROP TABLE IF EXISTS `ingredientsquantitytypeslanguages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingredientsquantitytypeslanguages` (
  `IDIngredientQuantityTypeLanguage` int NOT NULL,
  `IDIngredientQuantityType` int NOT NULL,
  `IDLanguage` int NOT NULL,
  `IngredientQuantityTypeSingular` varchar(250) NOT NULL,
  `IngredientQuantityTypePlural` varchar(250) DEFAULT NULL,
  `ConvertionRatio` double NOT NULL DEFAULT '1',
  `IngredientQuantityTypeX1000Singular` varchar(50) DEFAULT NULL,
  `IngredientQuantityTypeX1000Plural` varchar(50) DEFAULT NULL,
  `IngredientQuantityTypeWordsShowBefore` varchar(50) DEFAULT NULL,
  `IngredientQuantityTypeWordsShowAfter` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IDIngredientQuantityTypeLanguage`),
  KEY `FK_IngredientsQuantityTypesLanguages_IngredientsQuantityTypes` (`IDIngredientQuantityType`),
  CONSTRAINT `FK_IngredientsQuantityTypesLanguages_IngredientsQuantityTypes` FOREIGN KEY (`IDIngredientQuantityType`) REFERENCES `ingredientsquantitytypes` (`IDIngredientQuantityType`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `kitchentools`
--

DROP TABLE IF EXISTS `kitchentools`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kitchentools` (
  `IDKitchenTool` varchar(64) NOT NULL,
  `isDish` tinyint(1) NOT NULL,
  `isGlass` tinyint(1) NOT NULL,
  `isTool` tinyint(1) NOT NULL,
  `isDecoration` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDKitchenTool`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `kitchentoolslanguages`
--

DROP TABLE IF EXISTS `kitchentoolslanguages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kitchentoolslanguages` (
  `IDKitchenToolLanguage` varchar(64) NOT NULL,
  `IDKitchenTool` varchar(64) NOT NULL,
  `IDLanguage` int NOT NULL,
  `KitchenToolSingular` varchar(150) NOT NULL,
  `KitchenToolPlural` varchar(150) NOT NULL,
  `KitchenTooldescription` varchar(550) DEFAULT NULL,
  `isAutoTranslate` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDKitchenToolLanguage`),
  KEY `FK_KitchenToolsLanguages_KitchenTools` (`IDKitchenTool`),
  CONSTRAINT `FK_KitchenToolsLanguages_KitchenTools` FOREIGN KEY (`IDKitchenTool`) REFERENCES `kitchentools` (`IDKitchenTool`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `languages`
--

DROP TABLE IF EXISTS `languages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `languages` (
  `IDLanguage` int NOT NULL,
  `Language` varchar(250) NOT NULL,
  `LanguageCode` varchar(50) NOT NULL,
  `Enabled` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDLanguage`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `lastseenobjects`
--

DROP TABLE IF EXISTS `lastseenobjects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lastseenobjects` (
  `IDLastSeenObject` varchar(64) NOT NULL,
  `IDUser` varchar(64) NOT NULL,
  `IDObjectType` int NOT NULL,
  `IDObject` varchar(64) NOT NULL,
  `SeenDateTime` datetime NOT NULL,
  `SearchKeyWords` varchar(150) DEFAULT NULL,
  `OtherInfo` longtext,
  PRIMARY KEY (`IDLastSeenObject`),
  KEY `IX_IDUser` (`IDUser`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `quantitynotstd`
--

DROP TABLE IF EXISTS `quantitynotstd`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quantitynotstd` (
  `IDQuantityNotStd` int NOT NULL,
  `PercentageFactor` double NOT NULL,
  `EnabledToUser` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDQuantityNotStd`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `quantitynotstdlanguage`
--

DROP TABLE IF EXISTS `quantitynotstdlanguage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quantitynotstdlanguage` (
  `IDQuantityNotStdLanguage` int NOT NULL,
  `IDQuantityNotStd` int NOT NULL,
  `IDLanguage` int NOT NULL,
  `QuantityNotStdSingular` varchar(150) DEFAULT NULL,
  `QuantityNotStdPlural` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`IDQuantityNotStdLanguage`),
  KEY `FK_QuantityNotStdLanguage_QuantityNotStd` (`IDQuantityNotStd`),
  CONSTRAINT `FK_QuantityNotStdLanguage_QuantityNotStd` FOREIGN KEY (`IDQuantityNotStd`) REFERENCES `quantitynotstd` (`IDQuantityNotStd`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `quantitytypesallowedquantitynotstd`
--

DROP TABLE IF EXISTS `quantitytypesallowedquantitynotstd`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quantitytypesallowedquantitynotstd` (
  `IDQuantityTypeAllowedQuantityNotStd` int NOT NULL,
  `IDIngredientQuantityType` int NOT NULL,
  `IDQuantityNotStd` int NOT NULL,
  PRIMARY KEY (`IDQuantityTypeAllowedQuantityNotStd`),
  UNIQUE KEY `IX_UniqueIDIngrQtaType_IDQtaNotStd` (`IDIngredientQuantityType`,`IDQuantityNotStd`),
  KEY `FK_QuantityTypesAllowedQuantityNotStd_QuantityNotStd` (`IDQuantityNotStd`),
  CONSTRAINT `FK_QuantityTypesAllowedQuantityNotStd_IngredientsQuantityTypes` FOREIGN KEY (`IDIngredientQuantityType`) REFERENCES `ingredientsquantitytypes` (`IDIngredientQuantityType`),
  CONSTRAINT `FK_QuantityTypesAllowedQuantityNotStd_QuantityNotStd` FOREIGN KEY (`IDQuantityNotStd`) REFERENCES `quantitynotstd` (`IDQuantityNotStd`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipeproperties`
--

DROP TABLE IF EXISTS `recipeproperties`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipeproperties` (
  `IDRecipeProperty` int NOT NULL,
  `IDRecipePropertyType` int NOT NULL,
  `OrderPosition` int NOT NULL,
  `Enabled` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDRecipeProperty`),
  KEY `FK_RecipeProperties_RecipePropertiesTypes` (`IDRecipePropertyType`),
  CONSTRAINT `FK_RecipeProperties_RecipePropertiesTypes` FOREIGN KEY (`IDRecipePropertyType`) REFERENCES `recipepropertiestypes` (`IDRecipePropertyType`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipepropertieslanguages`
--

DROP TABLE IF EXISTS `recipepropertieslanguages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipepropertieslanguages` (
  `IDRecipePropertyLanguage` int NOT NULL,
  `IDRecipeProperty` int NOT NULL,
  `IDLanguage` int NOT NULL,
  `RecipeProperty` varchar(100) NOT NULL,
  `RecipePropertyToolTip` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`IDRecipePropertyLanguage`),
  UNIQUE KEY `IX_Unique_IDRecipeProperty_IDLanguage` (`IDRecipeProperty`,`IDLanguage`),
  CONSTRAINT `FK_RecipePropertiesLanguages_RecipeProperties` FOREIGN KEY (`IDRecipeProperty`) REFERENCES `recipeproperties` (`IDRecipeProperty`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipepropertiestypes`
--

DROP TABLE IF EXISTS `recipepropertiestypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipepropertiestypes` (
  `IDRecipePropertyType` int NOT NULL,
  `isDishType` tinyint(1) NOT NULL COMMENT 'Primo, Secondo, Antipasto',
  `isCookingType` tinyint(1) NOT NULL COMMENT 'Cotto al Forno, Bollito',
  `isColorType` tinyint(1) NOT NULL COMMENT 'General color of the dish presentation',
  `isEatType` tinyint(1) NOT NULL COMMENT 'Es: Finger Food, glass food',
  `isUseType` tinyint(1) NOT NULL COMMENT 'Es. For PicNic, for barbecue, for special event',
  `isPeriodType` tinyint(1) NOT NULL COMMENT 'Cook in winter, cook for chrismas',
  `OrderPosition` int NOT NULL,
  `Enabled` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDRecipePropertyType`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipepropertiestypeslanguages`
--

DROP TABLE IF EXISTS `recipepropertiestypeslanguages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipepropertiestypeslanguages` (
  `IDRecipePropertyTypeLanguage` int NOT NULL,
  `IDRecipePropertyType` int NOT NULL,
  `IDLanguage` int NOT NULL,
  `RecipePropertyType` varchar(100) NOT NULL,
  `RecipePropertyTypeToolTip` varchar(150) DEFAULT NULL,
  PRIMARY KEY (`IDRecipePropertyTypeLanguage`),
  UNIQUE KEY `IX_Unique_IDRecipePropertyType_IDLanguage` (`IDRecipePropertyType`,`IDLanguage`),
  CONSTRAINT `FK_RecipePropertiesTypesLanguages_RecipePropertiesTypes` FOREIGN KEY (`IDRecipePropertyType`) REFERENCES `recipepropertiestypes` (`IDRecipePropertyType`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipes`
--

DROP TABLE IF EXISTS `recipes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipes` (
  `IDRecipe` varchar(64) NOT NULL,
  `IDRecipeFather` varchar(64) DEFAULT NULL,
  `IDOwner` varchar(64) DEFAULT NULL,
  `NumberOfPerson` int DEFAULT NULL,
  `PreparationTimeMinute` int DEFAULT NULL,
  `CookingTimeMinute` int DEFAULT NULL,
  `RecipeDifficulties` int DEFAULT NULL,
  `IDRecipeImage` varchar(64) DEFAULT NULL,
  `IDRecipeVideo` varchar(64) DEFAULT NULL,
  `IDCity` int DEFAULT NULL,
  `CreationDate` datetime DEFAULT NULL,
  `LastUpdate` datetime DEFAULT NULL,
  `UpdatedByUser` varchar(64) DEFAULT NULL,
  `RecipeConsulted` int NOT NULL DEFAULT '0',
  `RecipeAvgRating` double NOT NULL DEFAULT '0',
  `isStarterRecipe` tinyint(1) NOT NULL,
  `DeletedOn` datetime DEFAULT NULL,
  `BaseRecipe` tinyint(1) NOT NULL COMMENT 'A recipe like pizza base, cream, maionese, etc.',
  `RecipeEnabled` tinyint(1) NOT NULL,
  `Checked` tinyint(1) DEFAULT NULL,
  `RecipeCompletePerc` int DEFAULT NULL,
  `RecipePortionKcal` double DEFAULT NULL,
  `RecipePortionProteins` double DEFAULT NULL,
  `RecipePortionFats` double DEFAULT NULL,
  `RecipePortionCarbohydrates` double DEFAULT NULL,
  `RecipePortionQta` double DEFAULT NULL,
  `Vegetarian` tinyint(1) DEFAULT NULL,
  `Vegan` tinyint(1) DEFAULT NULL,
  `GlutenFree` tinyint(1) DEFAULT NULL,
  `HotSpicy` tinyint(1) DEFAULT NULL,
  `RecipePortionAlcohol` double DEFAULT NULL,
  PRIMARY KEY (`IDRecipe`),
  KEY `IX_RecipeOwner` (`IDOwner`,`IDRecipe`,`CreationDate`),
  KEY `IX_RecipeFileds_ForFilter` (`RecipeEnabled`,`IDRecipeFather`,`RecipePortionKcal`,`IDRecipe`,`PreparationTimeMinute`,`CookingTimeMinute`,`RecipeAvgRating`,`Vegetarian`,`Vegan`,`GlutenFree`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipesbooksrecipes`
--

DROP TABLE IF EXISTS `recipesbooksrecipes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipesbooksrecipes` (
  `IDRecipeBookRecipe` varchar(64) NOT NULL,
  `IDUser` varchar(64) NOT NULL,
  `IDRecipe` varchar(64) NOT NULL,
  `RecipeAddedOn` datetime NOT NULL,
  `RecipeOrder` int DEFAULT NULL,
  PRIMARY KEY (`IDRecipeBookRecipe`),
  UNIQUE KEY `IX_IDRecipe-IDUser_Unique` (`IDUser`,`IDRecipe`),
  KEY `IX_IDUser` (`IDUser`,`IDRecipe`,`RecipeAddedOn`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipesingredients`
--

DROP TABLE IF EXISTS `recipesingredients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipesingredients` (
  `IDRecipeIngredient` varchar(64) NOT NULL,
  `IDRecipe` varchar(64) NOT NULL,
  `IDIngredient` varchar(64) NOT NULL,
  `IsPrincipalIngredient` tinyint(1) NOT NULL DEFAULT '0',
  `QuantityNotStd` varchar(150) DEFAULT NULL,
  `IDQuantityNotStd` int DEFAULT NULL,
  `Quantity` double DEFAULT NULL,
  `IDQuantityType` int NOT NULL,
  `QuantityNotSpecified` tinyint(1) DEFAULT '0',
  `RecipeIngredientGroupNumber` tinyint unsigned NOT NULL,
  `IDRecipeIngredientAlternative` varchar(64) DEFAULT NULL,
  `IngredientRelevance` int DEFAULT NULL,
  PRIMARY KEY (`IDRecipeIngredient`),
  UNIQUE KEY `RecipeIngredientUnique` (`IDRecipe`,`IDIngredient`,`RecipeIngredientGroupNumber`),
  KEY `FK_RecipesIngredients_Ingredients` (`IDIngredient`),
  KEY `FK_RecipesIngredients_QuantityNotStd` (`IDQuantityNotStd`),
  KEY `FK_RecipesIngredients_IngredientsQuantityTypes` (`IDQuantityType`),
  CONSTRAINT `FK_RecipesIngredients_Ingredients` FOREIGN KEY (`IDIngredient`) REFERENCES `ingredients` (`IDIngredient`) ON UPDATE CASCADE,
  CONSTRAINT `FK_RecipesIngredients_IngredientsQuantityTypes` FOREIGN KEY (`IDQuantityType`) REFERENCES `ingredientsquantitytypes` (`IDIngredientQuantityType`) ON UPDATE CASCADE,
  CONSTRAINT `FK_RecipesIngredients_QuantityNotStd` FOREIGN KEY (`IDQuantityNotStd`) REFERENCES `quantitynotstd` (`IDQuantityNotStd`),
  CONSTRAINT `FK_RecipesIngredients_Recipes1` FOREIGN KEY (`IDRecipe`) REFERENCES `recipes` (`IDRecipe`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipesingredientslanguages`
--

DROP TABLE IF EXISTS `recipesingredientslanguages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipesingredientslanguages` (
  `IDRecipeIngredientLanguage` varchar(64) NOT NULL,
  `IDRecipeIngredient` varchar(64) NOT NULL,
  `IDLanguage` int NOT NULL,
  `RecipeIngredientNote` varchar(250) DEFAULT NULL,
  `RecipeIngredientGroupName` varchar(150) DEFAULT NULL,
  `isAutoTranslate` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDRecipeIngredientLanguage`),
  UNIQUE KEY `IX_IDRecipeIngredient_IDLang_Unique` (`IDRecipeIngredient`,`IDLanguage`),
  CONSTRAINT `FK_RecipesIngredientsLanguages_RecipesIngredients` FOREIGN KEY (`IDRecipeIngredient`) REFERENCES `recipesingredients` (`IDRecipeIngredient`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipeskitchentools`
--

DROP TABLE IF EXISTS `recipeskitchentools`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipeskitchentools` (
  `IDRecipeKitchenTool` varchar(64) NOT NULL,
  `IDRecipe` varchar(64) NOT NULL,
  `IDKitchenTool` varchar(64) NOT NULL,
  `isNeedful` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDRecipeKitchenTool`),
  KEY `FK_RecipesKitchenTools_KitchenTools` (`IDKitchenTool`),
  KEY `FK_RecipesKitchenTools_Recipes` (`IDRecipe`),
  CONSTRAINT `FK_RecipesKitchenTools_KitchenTools` FOREIGN KEY (`IDKitchenTool`) REFERENCES `kitchentools` (`IDKitchenTool`) ON UPDATE CASCADE,
  CONSTRAINT `FK_RecipesKitchenTools_Recipes` FOREIGN KEY (`IDRecipe`) REFERENCES `recipes` (`IDRecipe`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipeslanguages`
--

DROP TABLE IF EXISTS `recipeslanguages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipeslanguages` (
  `IDRecipeLanguage` varchar(64) NOT NULL,
  `IDRecipe` varchar(64) NOT NULL,
  `IDLanguage` int NOT NULL,
  `RecipeName` varchar(250) NOT NULL,
  `RecipeLanguageAutoTranslate` tinyint(1) NOT NULL,
  `RecipeHistory` longtext,
  `RecipeHistoryDate` datetime(6) DEFAULT NULL,
  `RecipeNote` longtext,
  `RecipeSuggestion` longtext,
  `RecipeDisabled` tinyint(1) NOT NULL DEFAULT '0',
  `GeoIDRegion` int DEFAULT NULL,
  `RecipeLanguageTags` longtext,
  PRIMARY KEY (`IDRecipeLanguage`),
  KEY `IX_IDRecipe_IDLanguage` (`IDRecipe`,`IDLanguage`,`GeoIDRegion`),
  KEY `IX_RecipeName` (`RecipeName`),
  KEY `FK_RecipesLanguages_Languages` (`IDLanguage`),
  CONSTRAINT `FK_RecipesLanguages_Languages` FOREIGN KEY (`IDLanguage`) REFERENCES `languages` (`IDLanguage`) ON UPDATE CASCADE,
  CONSTRAINT `FK_RecipesLanguages_Recipes` FOREIGN KEY (`IDRecipe`) REFERENCES `recipes` (`IDRecipe`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipeslanguagestags`
--

DROP TABLE IF EXISTS `recipeslanguagestags`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipeslanguagestags` (
  `IDRecipeLanguageTag` int NOT NULL,
  `IDRecipeTag` int NOT NULL,
  `IDLanguage` int NOT NULL,
  `RecipeLanguageTag` varchar(50) NOT NULL,
  `RecipeLanguageSimilarTags` varchar(500) DEFAULT NULL,
  `RecipeLanguageReletadTags` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`IDRecipeLanguageTag`),
  KEY `FK_RecipesLanguagesTags_RecipesTags` (`IDRecipeTag`),
  CONSTRAINT `FK_RecipesLanguagesTags_RecipesTags` FOREIGN KEY (`IDRecipeTag`) REFERENCES `recipestags` (`IDRecipeTag`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipespresentations`
--

DROP TABLE IF EXISTS `recipespresentations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipespresentations` (
  `IDRecipePresentation` varchar(64) NOT NULL,
  `IDRecipe` varchar(64) NOT NULL,
  `IDUser` varchar(64) NOT NULL,
  `IDRecipePresentationPhoto` varchar(64) NOT NULL,
  `RecipePresentationAddedOn` datetime NOT NULL,
  `ReciapePresentationDeletedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`IDRecipePresentation`),
  KEY `FK_RecipesPresentations_Recipes` (`IDRecipe`),
  CONSTRAINT `FK_RecipesPresentations_Recipes` FOREIGN KEY (`IDRecipe`) REFERENCES `recipes` (`IDRecipe`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipespresentationskitchentools`
--

DROP TABLE IF EXISTS `recipespresentationskitchentools`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipespresentationskitchentools` (
  `IDRecipePresentationKitchenTool` varchar(64) NOT NULL,
  `IDRecipePresentation` varchar(64) NOT NULL,
  `IDKitchenTool` varchar(64) NOT NULL,
  PRIMARY KEY (`IDRecipePresentationKitchenTool`),
  KEY `FK_RecipesPresentationsKitchenTools_RecipesPresentations` (`IDRecipePresentation`),
  KEY `FK_RecipesPresentationsKitchenTools_KitchenTools` (`IDKitchenTool`),
  CONSTRAINT `FK_RecipesPresentationsKitchenTools_KitchenTools` FOREIGN KEY (`IDKitchenTool`) REFERENCES `kitchentools` (`IDKitchenTool`),
  CONSTRAINT `FK_RecipesPresentationsKitchenTools_RecipesPresentations` FOREIGN KEY (`IDRecipePresentation`) REFERENCES `recipespresentations` (`IDRecipePresentation`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipespresentationslanguages`
--

DROP TABLE IF EXISTS `recipespresentationslanguages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipespresentationslanguages` (
  `IDRecipePresentationLanguage` varchar(64) NOT NULL,
  `IDRecipePresentation` varchar(64) NOT NULL,
  `IDLanguage` int NOT NULL,
  `RecipePresentationNote` longtext NOT NULL,
  `isAutoTranslate` tinyint(1) NOT NULL,
  PRIMARY KEY (`IDRecipePresentationLanguage`),
  KEY `FK_RecipesPresentationsLanguages_RecipesPresentations` (`IDRecipePresentation`),
  CONSTRAINT `FK_RecipesPresentationsLanguages_RecipesPresentations` FOREIGN KEY (`IDRecipePresentation`) REFERENCES `recipespresentations` (`IDRecipePresentation`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipespropertiesvalues`
--

DROP TABLE IF EXISTS `recipespropertiesvalues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipespropertiesvalues` (
  `IDRecipePropertyValue` varchar(64) NOT NULL,
  `IDRecipe` varchar(64) NOT NULL,
  `IDRecipeProperty` int NOT NULL,
  `Value` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`IDRecipePropertyValue`),
  KEY `IX_IDRecipeProperty_Value` (`IDRecipeProperty`,`Value`,`IDRecipe`),
  KEY `IX_IDRecipe` (`IDRecipe`),
  CONSTRAINT `FK_RecipesPropertiesValues_RecipeProperties` FOREIGN KEY (`IDRecipeProperty`) REFERENCES `recipeproperties` (`IDRecipeProperty`) ON UPDATE CASCADE,
  CONSTRAINT `FK_RecipesPropertiesValues_Recipes1` FOREIGN KEY (`IDRecipe`) REFERENCES `recipes` (`IDRecipe`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipessteps`
--

DROP TABLE IF EXISTS `recipessteps`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipessteps` (
  `IDRecipeStep` varchar(64) NOT NULL,
  `IDRecipeLanguage` varchar(64) NOT NULL,
  `StepGroup` varchar(150) DEFAULT NULL COMMENT 'Group logicaly the step of recipe',
  `StepNumber` int NOT NULL,
  `RecipeStep` longtext NOT NULL,
  `StepTimeMinute` int DEFAULT NULL,
  `IDRecipeStepImage` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`IDRecipeStep`),
  KEY `IX_IDRecipeLanguage` (`IDRecipeLanguage`),
  CONSTRAINT `FK_RecipesSteps_RecipesLanguages` FOREIGN KEY (`IDRecipeLanguage`) REFERENCES `recipeslanguages` (`IDRecipeLanguage`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipestags`
--

DROP TABLE IF EXISTS `recipestags`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipestags` (
  `IDRecipeTag` int NOT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `MinThreshold` int DEFAULT NULL,
  `MaxThreshold` int DEFAULT NULL,
  PRIMARY KEY (`IDRecipeTag`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `recipesvotes`
--

DROP TABLE IF EXISTS `recipesvotes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recipesvotes` (
  `IDRecipeVote` varchar(64) NOT NULL,
  `IDRecipe` varchar(64) NOT NULL,
  `IDUser` varchar(64) NOT NULL,
  `RecipeVoteDate` datetime NOT NULL,
  `RecipeVote` double NOT NULL,
  PRIMARY KEY (`IDRecipeVote`),
  UNIQUE KEY `IX_IDRecipe-IDUser` (`IDRecipe`,`IDUser`),
  KEY `IX_IDRecipe` (`IDRecipe`),
  CONSTRAINT `FK_RecipesVotes_Recipes` FOREIGN KEY (`IDRecipe`) REFERENCES `recipes` (`IDRecipe`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping routines for database 'dbRecipes'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-21 18:27:09
