

function Create () {


    var apiURL = 'http://127.0.0.1:5000';//Put the backend url here




    var makePostRequest = function (url, data, onSuccess, onFailure) {
        $.ajax({
            type: 'POST',
            url: apiURL + url,
            data: JSON.stringify(data),
            contectType: "application/json",
            dataType: "json",
            success: onSuccess,
            error: onFailure
        });
    };

    //when "submit" is clicked
    var attachCreateHandler = function (e) {
        create.on('click', '.submit_acc', function (e) {
            var is_empty = 0;

            e.preventDefault();

            var user = {};

            user.username = $('#Email').val();
            user.password = $('#Pass').val();
            user.name = $('#Name').val();
            user.lastname = $('#Last').val();
            user.title = $('#Title').val();
            user.wsuid = $('#Id').val();
            user.phone = $('#Phone').val();
            // user.major = $('Major').val();
            // user.gpa = $('GPA').val();
            // user.grad_m = $('GRAD_month').val();
            // user.grad_y=$('GRAD_year').val();
            // user.serve = $('serve').val();

            if(user.username == "")
            {
                is_empty = 1;
            }
            else if(user.password == "")
            {
                is_empty = 1;
            }
            else if(user.name == "")
            {
                is_empty = 1;
            }
            else if(user.lastname == "")
            {
                is_empty = 1;
            }
            else if(user.title == "")
            {
                is_empty = 1;
            }
            else if(user.wsuid == "")
            {
                is_empty = 1;
            }
            else if(user.phone == "")
            {
                is_empty = 1;
            }


            if(is_empty == 1){
                window.alert("Empty field, You must enter all fields");
            }
            else if(is_empty == 0){
            var onSuccess = function(data) {

                console.log("new user - ok");

                if(user.title == "Professor"){
                    window.open("login.html", "_self");
                } else if(user.title == "Student"){
                    localStorage.setItem('user_email', user.username);
                    window.open("additional.html", "_self"); // changed from login to additional
                }

            };

            var onFailure = function(){
                console.error('Could not create user');
            };

            makePostRequest('/api/newuser',user,onSuccess,onFailure);
        }


        });
    };


    var start = function () {
        //userlist = (".");//but this doens't exist in our html!
        //userTemplateHtml = $(".");
        create = $(".create_account");
        attachCreateHandler();
    };

    return {
        start: start
    };
};
