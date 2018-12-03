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

    var visualInit = function () {

        $('#paddingButton').click(function () {

            var model = {
                message: $('#exampleMessage').val(),
            }
            //not working :(
            //if ($('#exampleMessage').val().length() < 57) {
            //    $('#showRoundsButton').prop('enabled', true);
            //}
           

            $.ajax({
                type: 'POST',
                url: config.urls.paddingUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {
                    $('#messageLength').val(data[0]);
                    $('#paddedBits').val(data[1]);
                    $('#messageLength2').val(data[2]);
                    $('#modResult').val(data[3]);
                    $('#padValue').val(data[4]);
                    $('#padMessageLength').val(data[5]);
                    $('#allPadding').val(data[6]);
                    $('#messageBits').val(data[7]);

                    var message = $('#exampleMessage').val();

                    if (message.length*8 < 448)
                    {
                        $('#showRoundsButton').attr('disabled', false);                 
                    }
                },
                error: function (response) {
                    console.log(response.responseJSON.message);
                    alert(response.responseJSON.message);
                }
            });
        });

        $("#exampleMessage").on('input', function () {
            $('#messageLength').val('');
            $('#paddedBits').val('');
            $('#messageLength2').val('');
            $('#modResult').val('');
            $('#padValue').val('');
            $('#padMessageLength').val('');
            $('#allPadding').val('');
            $('#messageBits').val('');
            $('#showRoundsButton').attr('disabled', true); 
            $('#AA').val('');
            $('#endA').val('');
       });
    }

    var tableInit = function () {

        $('#showRoundsButton').click(function () {

            var model = {
                message: $('#exampleMessage').val(),
            }
      
            $.ajax({
                type: 'POST',
                url: config.urls.tableUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {
                    console.log(data);

                    //for (var i = 0; i < data.length; i++) {
                       

                    //    $('#AA').val(data[data.length - 1]);  
                    //}
                    var iteration = Object(data[63]).A;
                    console.log(iteration);

                    $('#AA').val(data[data.length - 1]);  
                   // $('#AA').val(2344);
                    $('#endA').val(1234);
                },
                error: function (response) {
                    console.log(response);
                }
            });
        })
    }

    //var tableInit = function () {
    //        $("#example").DataTable();

    //        // Premade test data, you can also use your own
    //    var model = {
    //            message: $('#exampleMessage').val(),
    //        }

    //        $("#showRoundsButton").click(function () {
    //            loadData();
    //        });

    //        function loadData() {
    //            $.ajax({
    //                type: 'GET',
    //                url: config.urls.tableUrl,
    //                contentType: "text/plain",
    //                dataType: 'json',
    //                data: JSON.stringify(model),
    //                success: function (data) {
    //                    myJsonData = data;
    //                    populateDataTable(myJsonData);
    //                },
    //                error: function (e) {
    //                    console.log("There was an error with your request...");
    //                    console.log("error: " + JSON.stringify(e));
    //                }
    //            });
    //        }

    //        // populate the data table with JSON data
    //        function populateDataTable(data) {
    //            console.log("populating data table...");
    //            // clear the table before populating it with more data
    //            $("#example").DataTable().clear();
    //            var length = data.length;
    //            for (var i = 1; i < length + 1; i++) {
    //                var customer = data[i];

    //                // You could also use an ajax property on the data table initialization
    //                $('#example').dataTable().fnAddData([
    //                    customer.Iteration,
    //                    customer.A,
    //                    customer.B,
    //                    customer.C,
    //                    customer.D
    //                ]);
    //            }
    //        }
        
    //}



    var init = function () {
        visualInit();
        encryptInit();
        tableInit();
    }

    return {
        init: init
    }

}


var SHA1FunctionInit = function (config) {

    var encryptInit = function () {

        $('#encryptButton').click(function () {

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


var SHA256FunctionInit = function (config) {

    var encryptInit = function () {

        $('#encryptButton').click(function () {

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


var SHA512FunctionInit = function (config) {

    var encryptInit = function () {

        $('#encryptButton').click(function () {

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


var HMACInit = function (config) {
    var encryptInit = function () {

        $('#encryptButton').click(function () {

            var model = {
                message: $('#inputEncrypt').val(),
                key: $('#keyEncrypt').val(),
                hashType: $('#hashSelectTypeEncrypt option:selected').val(),
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

//$(document).ready(function () {
//    $('#table_T').DataTable();
//});



$(document).ready(function () {
    $('#clearButton').click(function () {       
            /*Clear input */
            $('#inputEncrypt').val('');
            /*Clear output using id */
            $('#outputEncrypt').val('');
    });
});

$(document).ready(function () {
    $('#clearButton1').click(function () {
        /*Clear key */
        $('#keyEncrypt').val('');
        /*Clear input */
        $('#inputEncrypt').val('');
        /*Clear output using id */
        $('#outputEncrypt').val('');
    });
});











