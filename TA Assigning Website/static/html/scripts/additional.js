function Additional () {


    var apiURL = 'http://127.0.0.1:5000';//Put the backend url here

    // var semail = localStorage.getItem('user_email');
    //
    // var sid = localStorage.getItem('user_id');
    //
    // var sname = localStorage.getItem('user_name');


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

    var makeDeleteRequest = function (url, onSuccess) {
        $.ajax({
            type: 'DELETE',
            url: apiURL + url,
            contectType: "application/json",
            dataType: "json",
            success: onSuccess
        });
    };


    //when "submit" is clicked
    var attachCreateHandler = function (e) {
        user_email = localStorage.getItem('user_email');

        add_info.on('click', '.cancel_info', function (e) {

            e.preventDefault();


            var onSuccess = function(data) {

                console.log("cancel - ok");

                window.open("login.html", "_self");
            };

            // var onFailure = function(){
            //     console.error('Could not return cancel');
            // };

            makeDeleteRequest('/api/remove?username='+user_email, onSuccess);
        });

        add_info.on('click', '.submit_info', function (e) {
            var is_empty = 0;

            e.preventDefault();

            var user = {};

            user.major = $('#Major').val();
            user.gpa = $('#GPA').val();
            user.grad_m = $('#GRAD_month').val();
            user.grad_y=$('#GRAD_year').val();
            user.serve = $('#Served').val();
            user.email = localStorage.getItem('user_email');

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

                window.open("login.html", "_self");
            };

            var onFailure = function(){
                console.error('Could not update user');
            };

            makePostRequest('/api/addinfo',user,onSuccess,onFailure);
        }


        });
    };


    var start = function () {
        //userlist = (".");//but this doens't exist in our html!
        //userTemplateHtml = $(".");
        add_info = $(".additional");
        attachCreateHandler();
    };

    return {
        start: start
    };
};
