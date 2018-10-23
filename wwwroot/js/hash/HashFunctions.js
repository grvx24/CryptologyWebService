var MD5FunctionInit = function (config) {


    var encryptInit = function() {

        $('#encryptButton').click(function() {

            var model = {
                message: $('#inputEncrypt').val(),
            }

            $.ajax({
                type: 'POST',
                url: config.urls.encryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    $('#outputEncrypt').val(data);
                },
                error: function (response) {
                    console.log(response.responseJSON.message);
                    alert(response.responseJSON.message);
                }
            });
        });
    }
 

    var init = function () {
        encryptInit();
    }

    return {
        init: init
    }

}





