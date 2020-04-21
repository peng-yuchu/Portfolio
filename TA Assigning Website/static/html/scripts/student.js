function openTab(evt, tabName) {
    // Declare all variables
    var i, tabcontent, tablinks;
    document.getElementById("stud_name").innerHTML = localStorage.getItem('user_name') + ' ' +localStorage.getItem('user_lastname');
    document.getElementById("stud_email").innerHTML = localStorage.getItem('user_email');
    document.getElementById("stud_id").innerHTML = localStorage.getItem('user_id');

    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }


    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(tabName).style.display = "block";
}

function Student(){

    var apiURL = 'http://localhost:5000';

    var appliedlist;

    var studTemplateHtml;

    var courselist;

    var courseTemplateHtml;

    var cname;

    var appliedname;

    var appliedlab;

    var clab_section;

    var semail = localStorage.getItem('user_email');

    var sid = localStorage.getItem('user_id');

    var sname = localStorage.getItem('user_name');

    var slastname = localStorage.getItem('user_lastname');

    var sphone = localStorage.getItem('user_phone');

    var smajor = localStorage.getItem("user_major");

    var sgpa = localStorage.getItem("user_gpa");

    var syear = localStorage.getItem("user_year");

    var smonth = localStorage.getItem("user_month");

    var sserved = localStorage.getItem("user_served");

    var search_course;

    var add_info_s;

    var makeGetRequest = function(url, onSuccess, onFailure) {
        $.ajax({
            type: 'GET',
            url: apiURL + url,
            dataType: "json",
            success: onSuccess,
            error: onFailure
        });
    };
    var makePostRequest = function(url, data, onSuccess, onFailure) {
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
    var makeDeleteRequest = function (url, onSuccess) {
        $.ajax({
            type: 'DELETE',
            url: apiURL + url,
            contectType: "application/json",
            dataType: "json",
            success: onSuccess
        });
    };



     var insertCourse = function(course, beginning) {
        // Start with the template, make a new DOM element using jQuery
        var newElem = $(courseTemplateHtml);
        // Populate the data in the new element
        // Set the "id" attribute
        // Now fill in the data that we retrieved from the server
        newElem.find('.course_name').text(course.name);
         // FINISH ME (Task 2): fill-in the rest of the data
        //newElem.find('.lab_section_select').text(course.lab_section)
        newElem.find('.course_prof').text(course.professor);
        newElem.find('.course_section').text(course.lab_section);

        if (beginning) {
            courselist.append(newElem);
        } else {
            courselist.preend(newElem);
        }
    };


     var displayCourses = function() {
        // Prepare the AJAX handlers for success and failure
         courselist.html('');
        var onSuccess = function(data) {
            /* FINISH ME (Task 2): display all professors from API and display in alphabetical orded by lastname */
            var jsondata = data.courses;
            for(var i = 0; i < jsondata.length; i++){
                insertCourse(jsondata[i],true);
            }
            console.log('Display courses - Success')
        };
        var onFailure = function() {
            console.error('List courses - Failed');
        };
        /* FINISH ME (Task 2): make a GET request to get recent professors */
        // need to chnage this part!!
        let requestUrl = '/api/courses'
        // need to chnage this part!!
        console.log(requestUrl);
        makeGetRequest(requestUrl,onSuccess,onFailure);
     };

     // var displaySpecificCourse = function () {
     //     console.log('Display courses Attempt')
     //     // Prepare the AJAX handlers for success and failure
     //     courselist.html('');
     //     var onSuccess = function (data) {
     //         /* FINISH ME (Task 2): display all professors from API and display in alphabetical orded by lastname */
     //         var jsondata = data.courses;
     //         for (var i = 0; i < jsondata.length; i++) {
     //             insertCourse(jsondata[i], true);
     //         }
     //         console.log('Display courses - Success')
     //     };
     //     var onFailure = function () {
     //         console.error('List courses - Failed');
     //     };
     //     /* FINISH ME (Task 2): make a GET request to get recent professors */
     //     // need to chnage this part!!
     //     let requestUrl = '/api/courses?course_name=' + cname + '&lab_section=' + clab_section;
     //     // need to chnage this part!!
     //     console.log(requestUrl);
     //     makeGetRequest(requestUrl, onSuccess, onFailure);
     //     console.log("Made it here");
     // };

     var searchCourse = function(course, beginning) {
        // Start with the template, make a new DOM element using jQuery
        var newElem = $(studTemplateHtml);
        // Populate the data in the new element
        // Set the "id" attribute
        // Now fill in the data that we retrieved from the server
        newElem.find('.course_name').text(course.name);
        // FINISH ME (Task 2): fill-in the rest of the data
        newElem.find('.lab_section_select').text(course.lab_section);

        if (beginning) {
            courselist.append(newElem);
        } else {
            courselist.preend(newElem);
        }
    };

     var insertApplied = function (applicant, beginning) {
         // Start with the template, make a new DOM element using jQuery
         var newElem = $(studTemplateHtml);
         // Populate the data in the new element
         // Set the "id" attribute
         // Now fill in the data that we retrieved from the server
         newElem.find('.applied_name').text(applicant.course);
         // FINISH ME (Task 2): fill-in the rest of the data
         newElem.find('.applied_professor').text(applicant.labsection)

         newElem.find('.applied_status').text(applicant.status)

         if (beginning) {
             appliedlist.append(newElem);
         } else {
             appliedlist.preend(newElem);
         }
     };

     var displayApplied = function() {
        // Prepare the AJAX handlers for success and failure
        appliedlist.html('');
        var onSuccess = function(data) {
            /* FINISH ME (Task 2): display all professors from API and display in alphabetical orded by lastname */
            var jsondata = data.applied;
            for(var i = 0; i < jsondata.length; i++){
                insertApplied(jsondata[i],true);
            }
            console.log('Display applied courses - Success');
        };
        var onFailure = function() {
            console.error('List applied courses - Failed');
        };
        /* FINISH ME (Task 2): make a GET request to get recent professors */
        console.log
        // make changes
        let requestUrl = '/api/applicants?applicant_id='+sid;
        //
        console.log(requestUrl);
        makeGetRequest(requestUrl,onSuccess,onFailure);
    };


      var attachSelectHandler = function(e) {
        // Attach this handler to the 'click' action for elements with class 'select_prof'
        courselist.on('click', '.btn', function(e) {
            // FINISH ME (Task 4): get the id, name, title, school of the selected professor (whose select button is clicked)

            cname = $(this).parents('article').find('.course_name').text()  //FINISH ME
            clab_section = $(this).parents('article').find('.course_section').text()///////////
            console.log(cname);
            console.log(clab_section);
            //displayApplicant();
            //openTab(event,'applicants')
            var onSuccess = function (data) {
                /* FINISH ME (Task 2): display all professors from API and display in alphabetical orded by lastname */
                console.log('apply courses - Success')
                location.reload();
            };
            var onFailure = function () {
                console.error('apply courses - Failed');
            };

            let requestUrl = '/api/apply'
            console.log(requestUrl);
            var user = {};

            user.name = sname
            user.lastname = slastname
            user.course = cname
            user.labsection = clab_section
            user.wsuid = sid
            user.phone = sphone
            user.profname = $(this).parents('article').find('.course_prof').text()
            user.major = smajor
            user.gpa = sgpa
            user.month = smonth
            user.year = syear
            user.served = sserved
            user.email = semail

            makePostRequest(requestUrl, user, onSuccess,onFailure);
        });
        appliedlist.on('click', '.del_btn', function(e){
            e.preventDefault();
            appliedname = $(this).parents('article').find('.applied_name').text()
            appliedlab= $(this).parents('article').find('.applied_professor').text()
            var onSuccess = function(data){
                
                console.log("delete - ok");

                location.reload();
            };

            makeDeleteRequest('/api/remove_applied?coursename='+appliedname+'&appliedlabsection='+appliedlab, onSuccess);

        });


    };


      var attachCreateHandler = function (e) {
        // Attach this handler to the 'click' action for elements with class 'select_prof'
          search_course.on('click', '.submit_form_input', function (e) {
              console.log('Search Course button hit');
            // FINISH ME (Task 4): get the id, name, title, school of the selected professor (whose select button is clicked)
            //var course = {};

            //cname = $(this).parents('article').find('.course_taking_input').text();
            //clab_section = $(this).parents('article').find('.lab_section_input').text();
              cname = search_course.find(".course_taking_input").val();
              clab_section = search_course.find(".lab_section_input").val();

            //course.name = search_course.find('.course_taking_input').val();
            //course.stud_name = sname;
            //course.stud_id = sid;
            //course.lab_section = search_course.find('.lab_section_input').val();
            //course.stud_email = semail;

            //  var onSuccess = function(data) {
            //     // FINISH ME (Task 6): insert professor at the beginning of the professorlist container
            //     // Hint : call insertProfessor
            //
            //      //insertCourse(course,true);
            //      //displaySpecificCourse();
            //
            //     console.log("searched courses - ok");
            //
            //     // FINISH ME (Task 6): activate and show the #list tab
            // };
            // var onFailure = function() {
            //     //FINISH ME (Task 6): display an alert box to notify that the professor could not be created ; print the errror message in the console.
            //     console.error("Failed to display specific course");
            // };
            var onSuccess = function (data) {
                /* FINISH ME (Task 2): display all professors from API and display in alphabetical orded by lastname */
                var jsondata = data.courses;
                for (var i = 0; i < jsondata.length; i++) {
                    insertCourse(jsondata[i], true);
                }
                console.log('search courses - Success')
            };
            var onFailure = function () {
                console.error('search courses - Failed');
            };
            // FINISH ME (Task 6): make a POST request to add the professor
            //makeGetRequest('/api/course_name='+,course,onSuccess,onFailure);
            //displaySpecificCourse();
            console.log('Display courses Attempt')
            // Prepare the AJAX handlers for success and failure
            courselist.html('');

            /* FINISH ME (Task 2): make a GET request to get recent professors */
            // need to chnage this part!!
            let requestUrl = '/api/courses?course_name=' + cname + '&lab_section=' + clab_section;
            // need to chnage this part!!
            console.log(requestUrl);
            makeGetRequest(requestUrl, onSuccess, onFailure);
            console.log("Made it here");
        });
    };

    var attachInfoHandler = function (e) {

        add_info_s.on('click', '.submit_info', function (e) {
            var is_empty = 0;
            console.log("made it inside");
            e.preventDefault();

            var user = {};

            user.major = $('#Major').val();
            user.gpa = $('#GPA').val();
            user.grad_m = $('#GRAD_month').val();
            user.grad_y=$('#GRAD_year').val();
            user.serve = $('#Served').val();
            user.email = semail;

            if(user.major == "")
            {
                is_empty = 1;
            }
            else if(user.gpa == "")
            {
                is_empty = 1;
            }
            else if(user.grad_m == "")
            {
                is_empty = 1;
            }
            else if(user.grad_y == "")
            {
                is_empty = 1;
            }
            else if(user.serve == "")
            {
                is_empty = 1;
            }

            if(is_empty == 1){
                window.alert("Empty field, You must enter all fields");
            }

            else if(is_empty == 0){
            var onSuccess = function(data) {

                console.log("edit info - ok");
                // location.reload();
                //window.open("login.html", "_self");
                openTab(event,"course")
            };

            var onFailure = function(){
                console.error('Could not update user');
            };

            makePostRequest('/api/addinfo',user,onSuccess,onFailure);
        }


        });
    };

    var start = function (){

        appliedlist = $(".applied_list");
        // Grab the first professor div element, to use as a template
        studTemplateHtml = $(".applied_list .applied_box")[0].outerHTML;
        // Delete everything from .professorlist
        appliedlist.html('');


        courselist = $(".course_list");
        // Grab the first professor div element, to use as a template
        courseTemplateHtml = $(".course_list .course_box")[0].outerHTML;
        // Delete everything from .professorlist
        courselist.html('');

        displayCourses();
        displayApplied();
        attachSelectHandler();

        search_course = $("form#courses_form");
        attachCreateHandler();

        add_info_s = $("form#additional_s");
        attachInfoHandler();
    };

    return {
        start: start
    };


};
