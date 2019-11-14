//  Name . . . : James Bell
//  Class. . . : CSCI-257 Web Services
//  Instructor : Bryon Steinwand
//  Date . . . : 11/17/2018
//  Assignment : Task One
//  File . . . : StudentForm.js
//  Notes. . . : JavaScript File that controls the student form.  Can only add new student, or edit an existing one
(function () {


    // Globalish Variables
    var students;
    var selectedStudent;
    var studentSelectedToUpdate;
    var studentListToUpdate = $("#Students");
    var fieldCats = $("#disciplineForAdd");
    var focusCats = $("#focusForAdd");
    var disciplines;



    // updates student list and discipline drop down menu in add section of form
    updateStudentList();
    updateDisciplineList();
    function updateStudentList() {
        getStudents(populateStudentList);
    }
    function updateDisciplineList() {
        getDisciplines(populateDisciplineSelect);
    }


    // add submit function processAddStudent
    $("#formAddStudent").off("submit");
    $("#formAddStudent").on("submit", processAddStudent);

    
    // gathers information to add new student
    function processAddStudent() {

        // collect data from fields on add new student section of form
        var firstName = $("#FirstNameAdd").val();
        var lastName = $("#LastNameAdd").val();
        var disciplineId = $("#disciplineForAdd option:selected").attr("id").substring(5);

        //alert(disciplineId);

        // new object
        var newStudent = new Object();


        // key value pairs 
        newStudent.FirstName = firstName;
        newStudent.LastName = lastName;

       // alert(newStudent.FirstName);


        // AJAX POST call in JSON to discipline controller
        if (!isNaN(parseInt(disciplineId))) {
            $.ajax({
                contentType: 'application/json',
                url: "/api/discipline/" + disciplineId + "/student",
                type: 'POST',
                data: JSON.stringify(newStudent),
                success: function (result) {
                    clearForm();

                    // updates student list
                    updateStudentList();
                },
                error: (function (xhr) {
                    console.log("error: " + xhr.status);
                    alert("failure: " + xhr.status);
                })
            });


            // stop form
            return false;
        }
    }


    // gets disciplines and calls next function
    function getDisciplines(callBackWhenDone) {
        disciplines = "loading";

       // alert("1");
        // use discipline controller to retrieve all disciplines
        var jqxhr = $.getJSON("/api/discipline/", function (data) {
            console.log("success");
            disciplines = data;
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

    // populates discipline drop down select list
    function populateDisciplineSelect() {
        var fields = [];        

        // clears list
        fieldCats.html("");
        //alert("3");

        // add each discipline, storing its id with the field shown
        $.each(disciplines, function (i, field) {
            if (!fields.includes(field.Field)) {
                fields.push(field.Field);
                var myOption = $("<option></option>");
                myOption.attr("id", "disc_" + field.DisciplineId);
                myOption.html(field.Field);
                fieldCats.append(myOption);
            }

        });
        
    }

    // get all the student from the student controller
    function getStudents(callBackWhenDone) {
       // alert("loading students");
            students = "loading";

           
            var jqxhr = $.getJSON("/api/student/", function (data) {
                console.log("success");
                students = data;
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


    // populates the student unorder list, stores student Id and add a click function to each list item.
    function populateStudentList() {
        //alert("populate");
            studentListToUpdate.html("");
            $.each(students, function (i, item) {
                var myLi = $("<li></li>");
                myLi.attr("id", "stud_" + item.StudentId);

                // add click function selectStudent to each list item
                myLi.on("click", selectStudent);
                myLi.html(item.FirstName + " " + item.LastName);
                studentListToUpdate.append(myLi);
            });
        }

    // fill the edit student section of form with selected student's information
    function selectStudent() {

        // get student's id
        var studId = this.id.substring(5);
        //alert(studId);
        selectedStudent = "none";

        // find the student in students
        $.each(students, function (i, item) {
            if (item.StudentId == studId) {
                selectedStudent = item;
               // alert(selectedStudent.StudentId);
            }
        });

        // fill the edit student portion with the selected students infromation
        if (selectedStudent != "none") {
            $("#FirstName").val(selectedStudent.FirstName);
            $("#LastName").val(selectedStudent.LastName);
            //alert(selectedStudent.Discipline);
            if (selectedStudent.Discipline != null) {
                //alert("not null");
                $("#editDiscipline").val(selectedStudent.Discipline.Field);
            }
            $("#StudentId").val(selectedStudent.StudentId);

            // add submit function processUpdateStudent
            $("#formEditStudent").off("submit");
            $("#formEditStudent").on("submit", processUpdateStudent);

        }
    }

    // updates changes to selected student to database
    function processUpdateStudent() {

        // gather new values in edit student section
        var sFirstName = $("#FirstName").val();
        var sLastName = $("#LastName").val();

        // update selected student with new values
        selectedStudent.FirstName = sFirstName;
        selectedStudent.LastName = sLastName;


        // send selected student to student controller for processing
        $.ajax({
            contentType: 'application/json',
            url: "/api/student/" + selectedStudent.StudentId,
            type: 'PUT',
            data: JSON.stringify(selectedStudent),
            success: function (result) {
                clearForm();
                updateStudentList();
            },
            error: (function (xhr) {
                console.log("error: " + xhr.status);
                alert("failure: " + xhr.status);
            })
        });

        // stop form
        return false;
    }

    // clear all fields
    function clearForm() {
        $("#FirstName").val("");
        $("#LastName").val("");
        $("#editDiscipline").val("");

        $("#FirstNameAdd").val("");
        $("#LastNameAdd").val("");
        $("#disciplineForAdd").val("");
        $("#focusForAdd").val(""); 
    }
}());
    