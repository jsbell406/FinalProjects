//	 Name . . . : James Bell
//	 Class. . . : CSCI 238 Standards based Mobile Applications
//	 Instructor : Mounika Pokuri
//	 Date . . . : 2/27/2019
//	 File . . . : index.js
//	 Assignment : Task 2
//	 Notes. . . : Task 2 Requirements:                   
//                  1. A narrative description of the application that outlines the purpose, intended use and audience.
//                  2. Use a Web SQL database in some way. Collect and display data from the database.
//                  3. Validate at least some of the data entered by the user.
//                  4. Use at least two of the following: capture image or other media, use geolocation, display google or other map.
//                  5. Comment your code.
(function () {
    "use strict";

    document.addEventListener( 'deviceready', onDeviceReady.bind( this ), false );

    function onDeviceReady() {
        // Handle the Cordova pause and resume events
        document.addEventListener( 'pause', onPause.bind( this ), false );
        document.addEventListener( 'resume', onResume.bind( this ), false );
        
        // TODO: Cordova has been loaded. Perform any initialization that requires Cordova here.

        // verify plugins
        console.log("camera " + navigator.camera);

        //dropDatabase();
    
        buildDatabase();
        MainApp();
    };

    function onPause() {
        // TODO: This application has been suspended. Save application state here.
    };

    function onResume() {
        // TODO: This application has been reactivated. Restore application state here.
    };
})();

function dropDatabase() {
    // -----      slqlite DB         ------
    var db = window.openDatabase("Recipes", "1.0", ",recipes", 1024);

    if (!window.openDatabase) {
        status("Database not supported");
        return;
    }

    db.transaction(function (tx) {
        tx.executeSql("drop table ingredient", null, null, logger);
        tx.executeSql("drop table step", null, null, logger);
        tx.executeSql("drop table recipe", null, null, logger);
    }, logger, success);

    function logger(tx, error) {
        console.log("Error", error);
    }

    function success() {
        console.log("Tables Created ");
    }
}

function buildDatabase() {
  
    // -----      slqlite DB         ------
    var db = window.openDatabase("Recipes", "1.0", ",recipes", 1024);

    if (!window.openDatabase) {
        status("Database not supported");
        return;
    }

    db.transaction(function (tx) {
        // Create Recipe Table
        tx.executeSql("create table if not exists recipe ("
            + "recipeId integer primary key,"
            + "name text,"
            + "image blob"
            + ")", null, null, logger);

        // Create Ingredient Table
        tx.executeSql("create table if not exists ingredient ("
            + "ingredientId integer primary key,"
            + "recipeId integer,"
            + "name text,"
            + "unitOfMeasurement text,"
            + "amount text,"
            + "FOREIGN KEY(recipeId) REFERENCES recipe(recipeId)"
            + ")", null, null, logger);

        // Create Step Table
        tx.executeSql("create table if not exists step ("
            + "stepId integer primary key,"
            + "recipeId integer,"
            + "stepPosition integer,"
            + "stepDescription text,"
            + "FOREIGN KEY(recipeId) REFERENCES recipe(recipeId)"
            + ")", null, null, logger);

    }, logger, success);

    function logger(tx, error) {
        console.log("Error", error);
    }

    function success() {
        console.log("Tables Created ");
    }
}

function MainApp() {

// -----    START   DB Util ----------------------------------------------------------------------------

    var db = window.openDatabase("Recipes", "1.0", ",recipes", 1024);
    if (!window.openDatabase) {
        status("Database not supported");
        return;
    }

    function logger(tx, error) {
        console.log("Error", error);
    }

    function success() {
        console.log("Database connection successful");
    }
// -----    END    DB Util ------------------------------------------------------------------------------



// -----    START   MAIN  -------------------------------------------------------------------------------

    $("#recipeNameError").hide();
    populateUnitOfMeasurementDDL();
    showRecipes();

// -----    END    MAIN  ---------------------------------------------------------------------------------



// -----   START   Step DB Functions ---------------------------------------------------------------------

    function saveStep(step, recipeId) {
        db.transaction(function (tx) {
            tx.executeSql(
                "insert into step (recipeId, stepPosition, stepDescription)"
                + "values (?,?,?)",
                [recipeId, step.stepPosition, step.stepDescription], null, logger);
        }, logger, success);
    }

    function getStepById(stepId,CallBack) {
        db.transaction(function (tx) {
            tx.executeSql("select * from step where stepId = ?", [stepId], function (tx, results) {
                var step = {};
                if (results.rows.length === 1) {
                    var record = results.rows.item(0);
                    step.stepId = record.stepId;
                    step.recipeId = record.recipeId;
                    step.stepPosition = record.stepPosition;
                    step.stepDescription = record.stepDescription;
                    CallBack(step);
                }
            });
        }, logger, success);
    }

    function getSteps(recipeId, CallBack) {
        db.transaction(function (tx) {
            tx.executeSql("select * from step where recipeId = ? order by stepPosition", [recipeId], function (tx, results) {
                var x = 0;
                var steps = [];

                for (x = 0; x < results.rows.length; x++) {
                    var step = {};
                    var record = results.rows.item(x);
                    step.stepId = record.stepId;
                    step.recipeId = recipeId;
                    step.stepPosition = record.stepPosition;
                    step.stepDescription = record.stepDescription;

                    steps.push(step);
                }
                CallBack(steps);
            }, null, logger);
        }, logger, success);
    }

    function updateStep(step, recipeId) {
        db.transaction(function (tx) {
            tx.executeSql(
                "update step set recipeId = ?, stepPosition = ?, stepDescription = ?  where stepId = ?",
                [recipeId, step.stepPosition, step.stepDescription, step.stepId],
                null, logger);
        }, logger, success);
    }

    function deleteStepById(id) {
        db.transaction(function (tx) {
            tx.executeSql("delete from step where stepId = ?", [id], null, null, logger);
        }, logger, success);
    }

// -----   END   Step DB Functions ----------------------------------------------------------------------------------------



// ------ START   Step Utility Functions ----------------------------------------------------------------------------------

    function showSteps(recipeId) {
        getSteps(recipeId, displaySteps);
    }

    function editStep() {
        var stepId = this.id.substring(5);
        getStepById(stepId, fillEditStep);
    }

    function deleteStep() {
        var stepId = this.id.substring(5);
        $("#editStepId").val(stepId);
        getSteps($("#editRecipeId").val(), prepareStepForDelete);
    }

    function prepareStepForDelete(steps) {
        var stepId = $("#editStepId").val();
        var recipeId = $("#editRecipeId").val();
        var oldPosition = 0;
        var stepsToUpdate = [];
        var stepsToDelete = [];
        var stepsToAdd = [];

        // Finds step to delete, then adjusts remaining step's positions.  Hands the results to proccessSteps function
        for (x = 0; x < steps.length; x++) {
            var placeHolderStep = {};
            placeHolderStep = steps[x];
            if (placeHolderStep.stepId == stepId) {
                oldPosition = placeHolderStep.stepPosition;
                stepsToDelete.push(placeHolderStep);
                continue;
            }

            if (oldPosition != 0) {
                placeHolderStep.stepPosition = oldPosition;
                oldPosition++;
                stepsToUpdate.push(placeHolderStep);
            }
        }
        proccessSteps(stepsToAdd, stepsToUpdate, stepsToDelete, recipeId);
    }

    // saves any new steps, updates any modified steps, deletes any steps marked for deletion
    function proccessSteps(stepsToAdd, stepsToUpdate, stepsToDelete, recipeId) {
        if (stepsToAdd.length == 0 & stepsToUpdate.length == 0 & stepsToDelete == 0) {
            return showSteps(recipeId);
        }

        newStep = stepsToAdd.pop();
        if (newStep)
            saveStep(newStep, recipeId);
        oldStep = stepsToUpdate.pop();
        if (oldStep)
            updateStep(oldStep, recipeId);
        delStep = stepsToDelete.pop();
        if (delStep)
            deleteStepById(delStep.stepId);

        proccessSteps(stepsToAdd, stepsToUpdate, stepsToDelete, recipeId);
    }

// ------ END    Step Utility Functions -----------------------------------------------------------------------------------



// ------ START    Step Event handlers -----------------------------------------------------------------------------------

    $("#btnSaveStep").on("click", function () {
        var recipeId = $("#editRecipeId").val();
        getSteps(recipeId, btnSaveStepCallBack);

    });

    $("#saveRecipeStepsPage").on("click", function () {
        $("#recipeName").val("");
        $.mobile.changePage("#homePage");
        showRecipes();
    });

// ------ END    Step Event handlers -----------------------------------------------------------------------------



// ------ START    Step Callback Functions -----------------------------------------------------------------------

    function fillEditStep(step)
    {
        $("#editStepId").val(step.stepId);
        $("#editStepPosition").val(step.stepPosition);
        $("#stepPosition").val(step.stepPosition);
        $("#stepDescription").val(step.stepDescription);
    }

    function displaySteps(steps)
    {
        var displayStepsList = $("#displaySteps");
        displayStepsList.html("");

        // build step list
        $.each(steps, function (i, item) {
            var newListItem = $("<li></li>");
            newListItem.html(item.stepDescription);
            newListItem.attr("id", "step_" + item.stepId);
            newListItem.on("click", editStep);
            newListItem.on("swipeleft", deleteStep);
            displayStepsList.append(newListItem);
        });

        displayStepsList.listview("refresh");
        // set step position to next available position
        $("#stepPosition").val(steps.length + 1);

        // reset values
        $("#stepDescription").val("");
        $("#editStepId").val("");
    }

    function btnSaveStepCallBack(steps)
    {     
        stepsToUpdate = [];
        stepsToAdd = [];
        stepsToDelete = [];
        var x = 0;
        var recipeId = $("#editRecipeId").val();

        // get step information
        var stepPosition = parseInt($("#stepPosition").val());
        var stepDescription = $("#stepDescription").val();

        // step position must be a number
        if (!isNaN(stepPosition)) {
            //valid          
        }
        else {
            //not valid
            $("#stepDescription").val(steps.length + 1);
            return;
        }

        // step description cant be empty
        if (stepDescription.length > 0) {
            // valid
        }
        else {
            // not valid
            return;
        }
        // step position cant be greater than the next available position
        if (stepPosition > steps.length + 1) {
            stepPosition = steps.length + 1;
        }

        var currentStep = {};     
        currentStep.stepPosition = stepPosition;
        currentStep.stepDescription = stepDescription;

        if ($("#editStepId").val() === "") {

            // new step
            if (stepPosition == steps.length + 1)
            {              
                // position is in next spot
                stepsToAdd.push(currentStep);

                // done 
                proccessSteps(stepsToAdd, stepsToUpdate, stepsToDelete, recipeId);
                return;
            }
            else
            {
                // new step but in position thats taken
                stepsToAdd.push(currentStep);
                for (x = currentStep.stepPosition - 1; x < steps.length; x++)
                {
                    placeHolderStep = {};
                    placeHolderStep = steps[x];
                    placeHolderStep.stepPosition = x + 2;

                    stepsToUpdate.push(placeHolderStep);
                }

                // done
                proccessSteps(stepsToAdd, stepsToUpdate, stepsToDelete, recipeId);
                return;
            }
        }
        else {
            // edit step
            currentStep.stepId = $("#editStepId").val();
            stepsToUpdate.push(currentStep);

            if (steps[currentStep.stepPosition - 1].stepId == currentStep.stepId)
            {
                // edit is in the same position
                // done
                proccessSteps(stepsToAdd, stepsToUpdate, stepsToDelete, recipeId);
                return;
            }
            else {
                var oldPosition = $("#editStepPosition").val();

                if ($("#editStepPosition").val() === "")
                {
                    // not a valid step edit do not continue
                    return;
                }

                if (currentStep.stepPosition < oldPosition)
                {
                    // shift position down one
                    for (x = currentStep.stepPosition; x < steps.length; x++) {
                        placeHolderStep = {};
                        placeHolderStep = steps[x - 1];
                        placeHolderStep.stepPosition = x + 1;

                        stepsToUpdate.push(placeHolderStep);
                    }
                    // done
                    proccessSteps(stepsToAdd, stepsToUpdate, stepsToDelete, recipeId);
                    return;
                }
                else
                {
                    // shift positions up one
                    for (x = oldPosition; x < currentStep.stepPosition; x++) {
                        placeHolderStep = {};
                        placeHolderStep = steps[x];
                        placeHolderStep.stepPosition = x;

                        stepsToUpdate.push(placeHolderStep);
                    }
                    // done
                    proccessSteps(stepsToAdd, stepsToUpdate, stepsToDelete, recipeId);
                    return;
                }             
            }
        }
    }

 // ------ END    Step Callback Functions -----------------------------------------------------------------------



// -----   START   Ingredient DB Functions ---------------------------------------------------------------

    function saveIngredient(ingredient, recipeId, CallBack)
    {
        db.transaction(function (tx) {
            tx.executeSql(
                "insert into ingredient (recipeId, name, unitOfMeasurement, amount)"
                + "values (?,?,?,?)",
                [recipeId, ingredient.name, ingredient.unit, ingredient.amount], function (tx, results) {
                    CallBack(recipeId);
                }, null, logger);           
        }, logger, success);
    }

    function getIngredientById(id, CallBackWithIngredient) {
        db.transaction(function (tx) {
            tx.executeSql("select * from ingredient where ingredientId = ?", [id], function (tx, results) {
                var ingredient = new Object();
                if (results.rows.length === 1) {
                    var record = results.rows.item(0);
                    ingredient.ingredientId = record.ingredientId;
                    ingredient.recipeId = record.recipeId;
                    ingredient.name = record.name;
                    ingredient.unit = record.unitOfMeasurement;
                    ingredient.amount = record.amount;

                    CallBackWithIngredient(ingredient);
                }
            }, null, logger);
        }, logger, success);
    }

    function getIngredients(recipeId, CallBack) {
        db.transaction(function (tx) {
            tx.executeSql("select * from ingredient where recipeId = ?", [recipeId], function (tx, results) {
                var x = 0;
                var ingredients = [];

                for (x = 0; x < results.rows.length; x++) {
                    var ingredient = new Object();
                    var record = results.rows.item(x);
                    ingredient.ingredientId = record.ingredientId;
                    ingredient.recipeId = recipeId;
                    ingredient.name = record.name;
                    ingredient.unit = record.unitOfMeasurement;
                    ingredient.amount = record.amount;
                    ingredients.push(ingredient);
                }

                CallBack(ingredients);
            }, null, logger);
        }, logger, success);
    }

    function UpdateIngredient(ingredient, recipeId, CallBack) {
        db.transaction(function (tx) {
            tx.executeSql(
                "update ingredient set recipeId = ?, name = ?, unitOfMeasurement = ?, amount = ? where ingredientId = ?",
                [recipeId, ingredient.name, ingredient.unit, ingredient.amount, ingredient.ingredientId],
                null, logger);
            CallBack(recipeId);
        }, logger, success);
    }

    function DeleteIngredientById(id, recipeId, CallBack) {
        db.transaction(function (tx) {
            tx.executeSql("delete from ingredient where ingredientId = ?", [id],null, null, logger);
            CallBack(recipeId);
        }, logger, success);
    }

// -----   END   Ingredient DB Functions ----------------------------------------------------------------------------------------



// ------ START   Ingredient Utility Functions ----------------------------------------------------------------------------------

    function editIngredient() {
        var ingredientId = this.id.substring(5);
        getIngredientById(ingredientId, populateEditIngredient);
    }

    function deleteIngredient() {
        var recipeId = $("#editRecipeId").val();
        var ingredientId = this.id.substring(5);
        DeleteIngredientById(ingredientId, recipeId, showIngredients);
    }

    function populateUnitOfMeasurementDDL() {
        var unitsOfMeasurement = ["Cup", "Teaspoon", "Tablespoon", "Ounce", "Pound", "Quart", "Small", "Medium", "Large","Pinch"];
        var unitMeasurementList = $("#unitOfMeasurement");

        // build drop down list with unit options
        $.each(unitsOfMeasurement, function (i, item) {
            var newOption = $("<option></option>");
            newOption.val(item);
            newOption.html(item);
            unitMeasurementList.append(newOption);
        });
    }

    function parseAmount(amount) {
        // replace all white space with and empty char
        var noSpaceAmount = amount.replace(/\s/g, "");

        // find the position of the division symbol, returns -1 if not found
        var findDivide = noSpaceAmount.search("/");
        var fraction = "";
        var prefix;

        // if the division symbol is found get the character before and the character after
        if (findDivide > -1) {
            fraction = noSpaceAmount.substring(findDivide - 1, findDivide + 2);
            prefix = noSpaceAmount.substring(0, findDivide - 1);
        }
        else {
            prefix = noSpaceAmount;
        }

        var x;
        var char;
        var wholeNumber = "";

        // build the whole number out of prefix and break when first non number is encountered
        for (x = 0; x < prefix.length; x++) {
            char = prefix.charAt(x);
            if (!isNaN(char)) {
                wholeNumber += char;
            }
            else {
                break;
            }
        }

        // build return string
        // function will parse amount to cover for entries such as 1 and 1/2 cup..... and parse to 1 - 1/2.
        // function can handle any amount of wierd spaces entered by user.
        // function can be modified to give access to prefix and fraction to perform math.
        var returnString;
        if (fraction === "") {
            returnString = wholeNumber;
        }
        else {
            returnString = wholeNumber + " - " + fraction;
        }

        return returnString;
    }

    function isIngredientValid(ingredient) {
        // after amount parsing check to see if amount starts with a number, if it does not start with a number the ingredient is not valid
        isValid = true;
        if (isNaN(parseInt(ingredient.amount.charAt(0)))) {
            isValid = false;
        }

        return isValid;
    }

// ------ END    Ingredient Utility Functions -----------------------------------------------------------------------------------



// ------ START    Ingredient Event handlers -----------------------------------------------------------------------------------

    $("#saveRecipeIngredientPage").on("click", function () {
        $("#recipeName").val("");
        $.mobile.changePage("#homePage");
        showRecipes();
    });

    $("#btnSaveIngredient").on("click", function () {
        var amount = $("#amount").val();
        var unit = $("#unitOfMeasurement").val();
        var name = $("#ingredientName").val();
        var recipeId = $("#editRecipeId").val();
        var parsedAmount = parseAmount(amount);

        var ingredient = new Object();
        ingredient.recipeId = recipeId;
        ingredient.name = name;
        ingredient.unit = unit;
        ingredient.amount = parsedAmount;

        if (!isIngredientValid(ingredient)) {
            return;
        }

        if ($("#editIngredientId").val() === "")
        {
            saveIngredient(ingredient, recipeId, showIngredients);
        }
        else
        {
            ingredient.ingredientId = $("#editIngredientId").val();
            UpdateIngredient(ingredient, recipeId, showIngredients);
        }     
    });

// ------ END    Ingredient Event handlers -----------------------------------------------------------------------------


    
// ------ START    Ingredient Callback Functions -----------------------------------------------------------------------

    function showIngredients(recipeId) {
        $("#amount").val("");
        $("#unitOfMeasurement").val("");
        $("#ingredientName").val("");
        $("#editIngredientId").val("");
        getIngredients(recipeId, displayIngredients);
    }

    function displayIngredients(ingredients) {
        var ingredientList = $("#displayIngredients");
        ingredientList.html("");

        // build list of ingredients
        $.each(ingredients, function (i, item) {
            var newListItem = $("<li></li>");
            newListItem.on("click", editIngredient);
            newListItem.on("swipeleft", deleteIngredient);
            newListItem.attr("id", "ingr_" + item.ingredientId);
            newListItem.html("<a>" + item.amount + " " + item.unit + " " + item.name + "</a>");
            ingredientList.append(newListItem);
        });
        ingredientList.listview("refresh");
        $("#unitOfMeasurement").selectmenu("refresh");
    }

    function populateEditIngredient(ingredient) {
        // set ingredient id in form
        $("#editIngredientId").val(ingredient.ingredientId);

        // set values in form
        $("#amount").val(ingredient.amount);
        $("#unitOfMeasurement").val(ingredient.unit);
        $("#ingredientName").val(ingredient.name);

        $("#unitOfMeasurement").selectmenu("refresh");
    }
   
// ------ END    Ingredient Callback Functions ---------------------------------------------------------------


    
// -----   START   Recipe DB Functions -----------------------------------------------------------------------

    function saveRecipe(recipe, callBack) {
        // Takes a new recipe and saves to DB, also sets the edit ID 
        db.transaction(function (tx) {
            if (recipe.image)
            {
                tx.executeSql(
                    "insert into recipe (name, image)"
                    + "values (?,?)",
                    [recipe.name, recipe.image],
                    null, logger);
            }
            else
            {
                tx.executeSql(
                    "insert into recipe (name)"
                    + "values (?,?)",
                    [recipe.name],
                    null, logger);
            }
           
            tx.executeSql("SELECT * FROM recipe ORDER BY recipeId DESC LIMIT 1", [], function (tx, results) {
                var record = results.rows.item(0); 
                callBack(record.recipeId);
            }, null, logger);           
        }, logger, success);
    }

    function getRecipeById(recipeId, getRecipeCallBack) {
        db.transaction(function (tx) {
            tx.executeSql("select * from recipe where recipeId = ?", [recipeId], function (tx, results) {
                var recipe = {};
                if (results.rows.length === 1) {
                    var record = results.rows.item(0);
                    recipe.name = record.name;
                    recipe.recipeId = record.recipeId;
                    recipe.image = record.image;
                    getRecipeCallBack(recipe);
                }
            });
        }, logger, success);
    }

    function getRecipes(CallBack) {
        db.transaction(function (tx) {
            tx.executeSql("select * from recipe order by name", [], function (tx, results) {
                var recipes = [];
                for (var i = 0; i < results.rows.length; i++) {
                    var record = results.rows.item(i);
                    var recipe = {};
                    recipe.recipeId = record.recipeId;
                    recipe.name = record.name;
                    recipe.image = record.image;
                    recipes.push(recipe);
                }
                CallBack(recipes);
            }, null, logger);
        }, logger, success);
    }

    function updateRecipe(recipe, id, callBack) {
        db.transaction(function (tx) {
            if (recipe.image)
            {
                tx.executeSql(
                    "update recipe set name = ?, image = ? where recipeId = ?",
                    [recipe.name, recipe.image, id],
                    null, logger);
            }
            else
            {
                tx.executeSql(
                    "update recipe set name = ?, where recipeId = ?",
                    [recipe.name, id],
                    null, logger);
            }
           
            callBack(id);
        }, logger, success);
    }

    function deleteRecipeById(id) {
        // deletes recipe
        db.transaction(function (tx) {
            tx.executeSql("delete from recipe where recipeId = ?", [id], null, logger);
            tx.executeSql("delete from ingredient where recipeId = ?", [id], null, logger);
            tx.executeSql("delete from step where recipeId = ?", [id], null, logger);
            showRecipes();
        }, logger, success);
    }
// ------ END    Recipe DB Functions ----------------------------------------------------------------------------------



// ------ START    Recipe Utility Functions ---------------------------------------------------------------------------

    function showRecipes() {
        getRecipes(displayRecipes);
    }

    function editRecipe() {
        var editRecipeId = this.id.substring(5);
        currentRecipeId = editRecipeId;
        getRecipeById(editRecipeId, populateRecipeEdit);
    }

    function deleteRecipe() {
        var id = this.id.substring(5);
        deleteRecipeById(id);
    }

    // Validates the recipe name, name must be valid for user to continue
    function validateRecipeName() {
        recipeName = $("#recipeName").val();
        // recipe name is required
        if (recipeName.length > 0) {
            $("#recipeNameError").hide();
            return true;
        }
        else {
            $("#recipeNameError").show();
            return false;
        }
    }
   
// ------ END    Recipe Utility Functions -----------------------------------------------------------------------------



// ------ START    Recipe Event handlers ------------------------------------------------------------------------------

    $("#btnAddNewRecipe").on("click", function () {
        $("#stepPosition").val("1");
        $("#editRecipeId").val("");
        $.mobile.changePage("#addEditRecipeName");
    });

    $("#btnRecipeIncredientPage").on("click", function () {
        if (validateRecipeName()) {
            saveRecipeToDbAndContinue(btnRecipeIncredientPageCallBack);
        }
    });

    $("#btnRecipeStepPage").on("click", function () {
        if (validateRecipeName()) {
            saveRecipeToDbAndContinue(btnRecipeStepPageCallBack);
        }
    });

    $("#addEditRecipeName").on("swipeleft", function () {
        if (validateRecipeName()) {
            saveRecipeToDbAndContinue(btnRecipeIncredientPageCallBack);
        }
    });
  
// ------ END     Recipe Event handlers --------------------------------------------------------------------



// ------ START    Recipe Callback Functions ---------------------------------------------------------------

    function saveRecipeToDbAndContinue(callBackToPass) {
        recipeName = $("#recipeName").val();
        recipeId = $("#editRecipeId").val();
       
        recipe = {};
        recipe.name = recipeName;
    
        if (recipeId === "") {
            // new recipe
            saveRecipe(recipe, callBackToPass);
        }
        else {
            // update recipe
            updateRecipe(recipe, recipeId, callBackToPass);
        }
    }

    // Builds list of recipes to be displayed to user
    function displayRecipes(recipes) {
        var myRecipeList = $("#lstShowRecipes");
        myRecipeList.html("");
        var image = $("#recipeImage");
        image.removeAttr("src");

        $.each(recipes, function (i, item) {
            var listItem = $("<li></li>");
            listItem.html("<a>" + item.name + "</a>");
            listItem.attr("id", "reci_" + item.recipeId);
            listItem.on("click", editRecipe);
            listItem.on("swipeleft", deleteRecipe);

            myRecipeList.append(listItem);
            myRecipeList.listview("refresh");
        });
    }

    function populateRecipeEdit(recipe) {
        $.mobile.changePage("#addEditRecipeName");
        $("#editRecipeId").val(recipe.recipeId);
        $("#recipeName").val(recipe.name);

        var image = $("#recipeImage");
        image.attr("src", recipe.image);
    }

    function btnRecipeIncredientPageCallBack(recipeId) {
        // set recipeId 
        $("#editRecipeId").val(recipeId);

        // reset ingredient form for new entry
        $("#amount").val("");
        $("#unitOfMeasurement").val("Cup");
        $("#ingredientName").val("");
        $("#editIngredientId").val("");

        // change page and refresh
        $.mobile.changePage("#addEditIngredientPage");
        showIngredients(recipeId);        
    }

    function btnRecipeStepPageCallBack(recipeId) {
        // set recipeId 
        $("#editRecipeId").val(recipeId);

         // change page and refresh
        $.mobile.changePage("#addEditStepsPage");
        showSteps(recipeId);
    }

    function btnTakePhotoCallBack(recipeId) {
        // set recipeId 
        $("#editRecipeId").val(recipeId);
       
    }

// ------ END    Recipe Callback Functions ---------------------------------------------------------------



// ------ START    Recipe Camera -------------------------------------------------------------------------

    $("#btnTakePicture").on("click", function () {
        if (validateRecipeName())
        {
            getPic();
        }  
    });

    function getPic() {
        if (navigator.camera) {
            navigator.camera.getPicture(onPicSuccess, onPicFailure, {
                quality: 50,
                sourceType: Camera.PictureSourceType.CAMERA,
                destinationType: Camera.DestinationType.FILE_URI
            });
        }
        else {
            console.log("no camera");
        }
    }

    function onPicSuccess(imageData) {
        recipeName = $("#recipeName").val();
        recipeId = $("#editRecipeId").val();

        recipe = {};
        recipe.name = recipeName;
        recipe.image = imageData;

        var image = $("#recipeImage");
        image.attr("src", imageData);

        if (recipeId === "") {
            // new recipe
            saveRecipe(recipe, btnTakePhotoCallBack);
        }
        else {
            // update recipe
            updateRecipe(recipe, recipeId, btnTakePhotoCallBack);
        }
    }

    function onPicFailure(errMsg) {
        console.error(errMsg);
    }
}