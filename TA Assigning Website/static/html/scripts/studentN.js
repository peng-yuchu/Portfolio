
function openTab(evt, tabName) {
    // Declare all variables
    var i, tabcontent, tablinks;

    document.getElementById("stud_name").innerHTML = localStorage.getItem('user_name');
    document.getElementById("stud_email").innerHTML = localStorage.getItem('user_email');
    document.getElementById("stud_id").innerHTML = localStorage.getItem('user_id');
    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }


    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(tabName).style.display = "block";

}


function Courses() {


    var apiURL = 'http://localhost:5000';

    var courselist;

    var studTemplateHtml;

    var applicantlist;

    var applicantTemplateHtml;

    var cname;

    var pemail = localStorage.getItem('user_email');

    var pid = localStorage.getItem('user_id');

    var pname = localStorage.getItem('user_name');

    var create_course;

    var makeGetRequest = function (url, onSuccess, onFailure) {
        $.ajax({
            type: 'GET',
            url: apiURL + url,
            dataType: "json",
            success: onSuccess,
            error: onFailure
        });
    };

    var makePostRequest = function (url, data, onSuccess, onFailure) {
        $.ajax({
            type: 'POST',
            url: apiURL + url,
            data: JSON.stringify(data),
            contentType: "application/json",
            dataType: "json",
            success: onSuccess,
            error: onFailure
        });
    };


    var insertApplied = function (applied, beginning) {
        // Start with the template, make a new DOM element using jQuery
        var newElem = $(applicantTemplateHtml);
        // Populate the data in the new element
        // Set the "id" attribute 
        // Now fill in the data that we retrieved from the server
        newElem.find('.applied_name').text(applied.course_name);
        // FINISH ME (Task 2): fill-in the rest of the data 
        newElem.find('.applied_professor').text(applied.professor_name);

        newElem.find('.applied_status').text(applied.status);

        if (beginning) {
            applied_list.append(newElem);
        } else {
            applied_list.preend(newElem);
        }
    };


    var displayApplied = function () {
        // Prepare the AJAX handlers for success and failure
        appliedlist.html('');
        var onSuccess = function (data) {
            /* FINISH ME (Task 2): display all professors from API and display in alphabetical orded by lastname */
            var jsondata = data.courses;
            for (var i = 0; i < jsondata.length; i++) {
                insertApplied(jsondata[i], true);
            }
            console.log('Display applicant - Success')
        };
        var onFailure = function () {
            console.error('List applicant - Failed');
        };
        /* FINISH ME (Task 2): make a GET request to get recent professors */
        let requestUrl = '/api/courses?course_name=' + cname + '&professor_name=' + pemail
        console.log(requestUrl);
        makeGetRequest(requestUrl, onSuccess, onFailure);
    };


    var insertCourse = function (course, beginning) {
        // Start with the template, make a new DOM element using jQuery
        var newElem = $(studTemplateHtml);
        // Populate the data in the new element
        // Set the "id" attribute 
        // Now fill in the data that we retrieved from the server
        newElem.find('.course_name').text(course.name);
        // FINISH ME (Task 2): fill-in the rest of the data 
        newElem.find('.lab_section_select').text(course.lab_section)

        if (beginning) {
            courselist.append(newElem);
        } else {
            courselist.preend(newElem);
        }
    };


    var displayCourses = function () {
        // Prepare the AJAX handlers for success and failure
        courselist.html('');
        var onSuccess = function (data) {
            /* FINISH ME (Task 2): display all professors from API and display in alphabetical orded by lastname */
            var jsondata = data.courses;
            for (var i = 0; i < jsondata.length; i++) {
                insertCourse(jsondata[i], true);
            }
            console.log('Display courses - Success')
        };
        var onFailure = function () {
            console.error('List courses - Failed');
        };
        /* FINISH ME (Task 2): make a GET request to get recent professors */
        console.log
        let requestUrl = '/api/courses?professor_name=' + pemail;
        console.log(requestUrl);
        makeGetRequest(requestUrl, onSuccess, onFailure);
    };

    var attachSelectHandler = function (e) {
        // Attach this handler to the 'click' action for elements with class 'select_prof'
        courselist.on('click', '.btn', function (e) {
            // FINISH ME (Task 4): get the id, name, title, school of the selected professor (whose select button is clicked)      

            cname = $(this).parents('article').find('.course_name').text()  //FINISH ME
            console.log(cname);
            displayApplicant();
            openTab(event, 'applicants')
        });
    };


    var attachCreateHandler = function (e) {
        // Attach this handler to the 'click' action for elements with class 'select_prof'
        create_course.on('click', '.submit_form_input', function (e) {
            // FINISH ME (Task 4): get the id, name, title, school of the selected professor (whose select button is clicked)      
            var course = {};

            course.name = create_course.find('.course_teaching_input').val();
            course.professor = pname;
            course.prof_id = pid;
            course.lab_section = create_course.find('.lab_section_input').val();
            course.prof_email = pemail;

            var onSuccess = function (data) {
                // FINISH ME (Task 6): insert professor at the beginning of the professorlist container
                // Hint : call insertProfessor

                insertCourse(course, true);

                console.log("insert course - ok");

                // FINISH ME (Task 6): activate and show the #list tab
            };
            var onFailure = function () {
                //FINISH ME (Task 6): display an alert box to notify that the professor could not be created ; print the errror message in the console. 
                console.error("Failed to submit professor");
            };

            // FINISH ME (Task 6): make a POST request to add the professor
            makePostRequest('/api/new_course', course, onSuccess, onFailure);

        });
    };



    var start = function () {

        courselist = $(".courses_list");
        // Grab the first professor div element, to use as a template
        studTemplateHtml = $(".courses_list .courses_box")[0].outerHTML;
        // Delete everything from .professorlist
        courselist.html('');
        displayCourses();


        applicantlist = $(".applicant_list");
        // Grab the first professor div element, to use as a template
        applicantTemplateHtml = $(".applicant_list .applicant_box")[0].outerHTML;
        // Delete everything from .professorlist
        applicantlist.html('');

        attachSelectHandler();

        create_course = $("form#preference_form");
        attachCreateHandler();


    };

    return {
        start: start
    };


};