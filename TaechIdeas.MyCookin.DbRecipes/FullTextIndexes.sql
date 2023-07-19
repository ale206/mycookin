CREATE FULLTEXT INDEX ON [dbo].[vIndexedIngredientLang_Es]
    ([IngredientSingular] LANGUAGE 3082, [IngredientPlural] LANGUAGE 3082)
    KEY INDEX [IX_IDIngredientLanguage]
    ON [IngredientCatalog];


GO
CREATE FULLTEXT INDEX ON [dbo].[vIndexedIngredientLang_It]
    ([IngredientSingular] LANGUAGE 1040, [IngredientPlural] LANGUAGE 1040)
    KEY INDEX [IX_IDIngredientLanguage]
    ON [IngredientCatalog];


GO
CREATE FULLTEXT INDEX ON [dbo].[RecipesLanguages]
    ([RecipeName] LANGUAGE 1040 STATISTICAL_SEMANTICS, [RecipeLanguageTags] LANGUAGE 1040 STATISTICAL_SEMANTICS)
    KEY INDEX [PK_RecipesLanguages]
    ON [RecipeStepsCatalog];


GO
CREATE FULLTEXT INDEX ON [dbo].[vIndexedRecipesLang_It]
    ([RecipeName] LANGUAGE 1040 STATISTICAL_SEMANTICS)
    KEY INDEX [IX_vIndexedRecipesLang_It]
    ON [RecipeStepsCatalog];


GO
CREATE FULLTEXT INDEX ON [dbo].[vIndexedRecipesLang_Es]
    ([RecipeName] LANGUAGE 3082 STATISTICAL_SEMANTICS)
    KEY INDEX [IX_vIndexedRecipesLang_Es]
    ON [RecipeIndexing];


GO
CREATE FULLTEXT INDEX ON [dbo].[vIndexedRecipesLang_En]
    ([RecipeName] LANGUAGE 1033 STATISTICAL_SEMANTICS)
    KEY INDEX [IX_vIndexedRecipesLang_En]
    ON [RecipeIndexing];


GO
CREATE FULLTEXT INDEX ON [dbo].[vRecipeProperty]
    ([RecipeProperty] LANGUAGE 1033, [RecipePropertyToolTip] LANGUAGE 1033)
    KEY INDEX [IX_IDRecipePropertyLanguage]
    ON [RecipeStepsCatalog];


GO
CREATE FULLTEXT INDEX ON [dbo].[RecipesSteps]
    ([RecipeStep] LANGUAGE 1040)
    KEY INDEX [PK_RecipesSteps]
    ON [RecipeStepsCatalog];


GO
CREATE FULLTEXT INDEX ON [dbo].[vIndexedIngredientLang_En]
    ([IngredientSingular] LANGUAGE 1033, [IngredientPlural] LANGUAGE 1033)
    KEY INDEX [IX_IDIngredientLanguage]
    ON [IngredientCatalog];


GO
CREATE FULLTEXT INDEX ON [dbo].[vAllRecipesNames_It]
    ([RecipeName] LANGUAGE 1033)
    KEY INDEX [IX_vAllRecipesNames_It]
    ON [RecipeCatalog_It_v1];


GO
CREATE FULLTEXT INDEX ON [dbo].[vAllRecipesNames_Es]
    ([RecipeName] LANGUAGE 1033)
    KEY INDEX [IX_vAllRecipesNames_Es]
    ON [RecipeCatalog_Es_v1];


GO
CREATE FULLTEXT INDEX ON [dbo].[vAllRecipesNames_En]
    ([RecipeName] LANGUAGE 1033)
    KEY INDEX [IX_vAllRecipesNames_En]
    ON [RecipeCatalog_En_v1];

