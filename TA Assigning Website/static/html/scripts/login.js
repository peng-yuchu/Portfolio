// JavaScript source code

function Login() {


    var apiURL = 'http://localhost:5000/';//Put the backend url here

    //var userlist;

    //var userTemplateHtml;
    /*
    UserName: SakireA
    Password: pswd123
    Name: Sakire
    Lastname: Lnam
    ID: 1
    Phone: 1234567890

    */

    //globals for reciveing from form and comparing to list
    var usernameForm;
    var passwordForm;

    var login;

    var makeGetRequest = function(url, onSuccess, onFailure) {
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
            url: apiUrl + url,
            data: JSON.stringify(data),
            contectType: "application/json",
            dataType: "json",
            success: onSuccess,
            error: onFailure
        });
    };

    /*//this is only needed fotr create
    var insertUser = function (user, beginning) {
        console.log('Creating a new user');

        var newElem = $(userTemplateHtml);

        newElem.find('.username').text(user.username);
        newElem.find('.password').text(user.password);
        newElem.find('.name').text(user.name);
        newElem.find('.lastname').text(user.lastname);
        newElem.find('.id').text(user.id);
        newElem.find('.phone').text(user.name);
        if (beginning) {
            userlist.append(newElem);
        } else {
            userlist.prepend(newElem);
        }
    };
    */

    var checkAgainstLogins = function () {

        var onSuccess = function (data) {
            var valid = 0;
            var jsondata = data.users;
            for (var i = 0; i < jsondata.length; i++) {
                console.log(jsondata[i].title);
                if (jsondata[i].username == usernameForm && jsondata[i].password == passwordForm ) { // CHANGE THIS TO CHECK USERNAME PWD
                    //do login stuff here or break then do it
                    console.log(jsondata[i].title);
                    if (jsondata[i].title == "Student") {
                        //open the student homepage
                        localStorage.setItem('user_email', usernameForm);
                        localStorage.setItem('user_name', jsondata[i].name);
                        localStorage.setItem('user_lastname',jsondata[i].lastname)
                        localStorage.setItem('user_id', jsondata[i].wsuid);
                        localStorage.setItem('user_phone', jsondata[i].phone)
                        localStorage.setItem('user_major', jsondata[i].major)
                        localStorage.setItem('user_gpa', jsondata[i].gpa)
                        localStorage.setItem('user_month', jsondata[i].month)
                        localStorage.setItem('user_year', jsondata[i].year)
                        localStorage.setItem('user_served', jsondata[i].served)
                        window.open("student.html", "_self");//PUT THE STUDENT PAGE URL HERE
                        valid = 1;
                    } else if (jsondata[i].title == "Professor") {
                        //open the teacher homepage
                        localStorage.setItem('user_email', usernameForm);
                        localStorage.setItem('user_name', jsondata[i].name);
                        localStorage.setItem('user_lastname',jsondata[i].lastname)
                        localStorage.setItem('user_id', jsondata[i].wsuid);

                        window.open("professor.html", "_self");//PUT THE TEACHER PAGE URL HERE
                        valid= 1;
                    }
                }
            }
            if(valid == 0){
                window.alert("Invalid Username or Password");
            }

        }

        var onFailure = function() {
            console.error('Could not check against login');
        };
        let requestUrl = "api/login?user_name=" + usernameForm; //extended backend url to list of all users
        console.log(requestUrl);
        makeGetRequest(requestUrl, onSuccess, onFailure);
    };

    //when "submit" is clicked
    var attachLoginHandler = function (e) {
        login.on('click', '.submit_login', function (e) {
            console.log('Attempting login');
            e.preventDefault();
            usernameForm = $('#user').val();
            //console.log(usernameForm);
            passwordForm = $('#pass').val();
            //console.log(passwordform);
            console.log(usernameForm);
            console.log(passwordForm);
            //Now that we have the submitted username and password attempt, we want to... check against the list
            checkAgainstLogins();
        });
    };


    var start = function () {
        //userlist = (".");//but this doens't exist in our html!
        //userTemplateHtml = $(".");
        login = $(".login");
        attachLoginHandler();
    };

    return {
        start: start
    };
};
