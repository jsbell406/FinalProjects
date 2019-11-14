//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : EmployerForm.js
//  Notes. . . : JavaScript File that controls the employer form.  Can only add new employer, or edit an existing one

(function () {

    // Globalish Variables
    var employers;
    var employerListToUpdate = $("#Employers");
    var selectedEmployer

    // Populates list with current employers
    updateEmployerList();
    function updateEmployerList() {
        getEmployers(populateEmployerList);
    }

    // add form submit button functionality
    $("#formAddEmployer").off("submit");
    $("#formAddEmployer").on("submit", processAddEmployer);


    // get all employers from database
    // once fetch is complete calls next function populateEmployerList()
    function getEmployers(callBackWhenDone) {
        //alert("loading emp");
        employers = "loading";

        var jqxhr = $.getJSON("/api/employer/", function (data) {
            console.log("success");
            employers = data;
            callBackWhenDone();
        })
            .done(function () {
                console.log("second success");
            })
            .fail(function () {
                console.log("error");
            })
            .always(function () {
                console.log("complete");
            });
    }

    // Populates the unordered list on the page of current employers
    function populateEmployerList() {

        // clears list
        employerListToUpdate.html("");

        // for each employer do.....
        $.each(employers, function (i, employer) {
            
            // add new list item
            var myLi = $("<li></li>");

            // add employerId as and attribute, for later identification
            myLi.attr("id", "empl_" + employer.EmployerId);

            // add click function selectEmployer to the new list item
            myLi.on("click", selectEmployer);

            // Insert the company name as the list text
            myLi.html(employer.CompanyName);
           // alert(employer.EmployerId);

            // add the new list item to the unordered list
            employerListToUpdate.append(myLi);
        });
    }


    // Fills the edit employer section of the form with the requested employer
    function selectEmployer() {

    
        selectedEmployer = "none";

        // get the id attribute of the selected list item
        var empId = this.id.substring(5);

        // search the employers list for the employer
        $.each(employers, function (i, item) {
            if (item.EmployerId == empId) {
                selectedEmployer = item;
            }
        });

       // alert(selectedEmployer.EmployerId);


        // Fill form will selected employers information
        if (selectedEmployer != null) {
            $("#OrgName").val(selectedEmployer.CompanyName);
            $("#ConFirstName").val(selectedEmployer.ContactFirstName);
            $("#ConLastName").val(selectedEmployer.ContactLastName);
            $("#ConPhone").val(selectedEmployer.ContactPhone);
            $("#ConEmail").val(selectedEmployer.ContactEmail);
            $("#EmployerId").val(selectedEmployer.EmployerId);


            // add submit button function processUpdateEmployer
            $("#formEditEmployer").off("submit");
            $("#formEditEmployer").on("submit", processUpdateEmployer);
        }
    }


    // Processes changes made to a selected employer
    function processUpdateEmployer() {

        // gather all value in edit employer section of form
        var companyName = $("#OrgName").val();
        var conFirstName = $("#ConFirstName").val();
        var conLastName = $("#ConLastName").val();
        var conPhone = $("#ConPhone").val();
        var conEmail = $("#ConEmail").val();

        // assign values to the selected employer
        selectedEmployer.CompanyName = companyName;
        selectedEmployer.ContactFirstName = conFirstName;
        selectedEmployer.ContactLastName = conLastName;
        selectedEmployer.ContactPhone = conPhone;
        selectedEmployer.ContactEmail = conEmail;

        // ajax PUT call to replace employer object with the new object
        // sends employer object in JSON format to the employer controller 
        $.ajax({
            contentType: 'application/json',
            url: "/api/employer/" + selectedEmployer.EmployerId,
            type: 'PUT',
            data: JSON.stringify(selectedEmployer),
            success: function (result) {
               // alert("Success");
                // clear form
                clearForm()

                // updates employer list
                updateEmployerList();
            },
            error: (function (xhr) {
                console.log("error: " + xhr.status);
                alert("failure: " + xhr.status);
            })
        });

        // stops form process
        return false;
    }

    // collects data in add employer section of form and inserts new employer
    function processAddEmployer() {

        // collects values in add new employer section of form
        var companyName = $("#OrgNameAdd").val();
        var conFirstName = $("#ConFirstNameAdd").val();
        var conLastName = $("#ConLastNameAdd").val();
        var conPhone = $("#ConPhoneAdd").val();
        var conEmail = $("#ConEmailAdd").val();

        // create a new object to store employer information
        var newEmployer = new Object();

        // add key value pairs to new object
        newEmployer.CompanyName = companyName;
        newEmployer.ContactFirstName = conFirstName;
        newEmployer.ContactLastName = conLastName;
        newEmployer.ContactPhone = conPhone;
        newEmployer.ContactEmail = conEmail;

        // ajax call in JSON with new employer object
        $.ajax({
            contentType: 'application/json',
            url: "/api/employer/" + selectedEmployer.EmployerId,
            type: 'POST',
            data: JSON.stringify(newEmployer),
            success: function (result) {
                // alert("Success");
                clearForm()
                // update employer list
                updateEmployerList();
            },
            error: (function (xhr) {
                console.log("error: " + xhr.status);
                alert("failure: " + xhr.status);
            })
        });

        // stop form
        return false;
    }


    // clears all fields on page
    function clearForm() {
        $("#OrgName").val("");
        $("#ConFirstName").val("");
        $("#ConLastName").val("");
        $("#ConPhone").val("");
        $("#ConEmail").val("");
        $("#EmployerId").val("");

        $("#OrgNameAdd").val("");
        $("#ConFirstNameAdd").val("");
        $("#ConLastNameAdd").val("");
        $("#ConPhoneAdd").val("");
        $("#ConEmailAdd").val("");
        $("#EmployerIdAdd").val("");
    }


}());