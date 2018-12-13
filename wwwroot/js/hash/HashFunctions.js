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

<<<<<<< HEAD
=======
    var init = function () {
        encryptInit();
    }

    return {
        init: init
    }

}

var MD5VisualizationInit = function (config) {

    function removeTable() {
        $('#registersTable th').remove();
        $('#registersTable td').remove();
        $('#registersTable tr').remove();
        $('#registersTable tbody').remove();

        $('#registersFinal th').remove();
        $('#registersFinal td').remove();
        $('#registersFinal tr').remove();
        $('#registersFinal tbody').remove();

        $('#messageHash th').remove();
        $('#messageHash td').remove();
        $('#messageHash tr').remove();
        $('#messageHash tbody').remove();
    }
    

>>>>>>> PM
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
<<<<<<< HEAD
       });
    }
=======
            $('#endA_Bin').val('');
            $('#endA_Hex').val('');
            $('#BB').val('');
            $('#endB').val('');
            $('#endB_Bin').val('');
            $('#endB_Hex').val('');
            $('#CC').val('');
            $('#endC').val('');
            $('#endC_Bin').val('');
            $('#endC_Hex').val('');
            $('#DD').val('');
            $('#endD').val('');
            $('#endD_Bin').val('');
            $('#endD_Hex').val('');
            removeTable();
            $('#finalLabel').attr('hidden', true);
            $('#transformations').attr('hidden', true);
        });
>>>>>>> PM

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

<<<<<<< HEAD
                    $('#AA').val(data[data.length - 1]);  
                   // $('#AA').val(2344);
                    $('#endA').val(1234);
=======
                    rows[64].cells[1].style.fontWeight = "bold";
                    rows[64].cells[2].style.fontWeight = "bold";
                    rows[64].cells[3].style.fontWeight = "bold";
                    rows[64].cells[4].style.fontWeight = "bold";

                    var finalA = rows[64].cells[1].innerHTML;
                    var finalB = rows[64].cells[2].innerHTML;
                    var finalC = rows[64].cells[3].innerHTML;
                    var finalD = rows[64].cells[4].innerHTML;


                    $('#AA').val(finalA);
                    var modValue = Math.pow(2, 32);
                    var endSum = parseInt(1732584193) + parseInt(finalA);
                    var end = (endSum)%(modValue);                  
                    $('#endA').val(end);
                    var end_Bin = end.toString(2);
                    $('#endA_Bin').val(end_Bin);
                    var endA_Hex = parseInt(end_Bin, 2).toString(16);
                    $('#endA_Hex').val(endA_Hex);
                    endA_Hex= endA_Hex.toString();
                    var length = endA_Hex.length;
                    if (length < 8) {
                        for (i = length; i < 8; i++) {
                            endA_Hex = '0'+ endA_Hex;
                        }
                    }

                    $('#BB').val(finalB);
                    modValue = Math.pow(2, 32);
                    endSum = parseInt(4023233417) + parseInt(finalB);
                    end = (endSum) % (modValue);
                    $('#endB').val(end);
                    end_Bin = end.toString(2);
                    $('#endB_Bin').val(end_Bin);
                    var endB_Hex = parseInt(end_Bin, 2).toString(16);
                    $('#endB_Hex').val(endB_Hex);
                    endB_Hex = endB_Hex.toString();
                    var length = endB_Hex.length;
                    if (length < 8) {
                        for (i = length; i < 8; i++) {
                            endB_Hex = '0' + endB_Hex;
                        }
                    }

                    $('#CC').val(finalC);
                    modValue = Math.pow(2, 32);
                    endSum = parseInt(2562383102) + parseInt(finalC);
                    end = (endSum) % (modValue);
                    $('#endC').val(end);
                    end_Bin = end.toString(2);
                    $('#endC_Bin').val(end_Bin);
                    var endC_Hex = parseInt(end_Bin, 2).toString(16);
                    $('#endC_Hex').val(endC_Hex);
                    endC_Hex = endC_Hex.toString();
                    var length = endC_Hex.length;
                    if (length < 8) {
                        for (i = length; i < 8; i++) {
                            endC_Hex = '0' + endC_Hex;
                        }
                    }

                    $('#DD').val(finalD);
                    modValue = Math.pow(2, 32);
                    endSum = parseInt(271733878) + parseInt(finalD);
                    end = (endSum) % (modValue);
                    $('#endD').val(end);
                    end_Bin = end.toString(2);
                    $('#endD_Bin').val(end_Bin);
                    var endD_Hex = parseInt(end_Bin, 2).toString(16);
                    $('#endD_Hex').val(endD_Hex);
                    endD_Hex = endD_Hex.toString();
                    var length = endD_Hex.length;
                    if (length < 8) {
                        for (i = length; i < 8; i++) {
                            endD_Hex = '0' + endD_Hex;
                        }
                    }


                    var $tableFinal = $('#registersFinal');
                    var $tbodyFinal = $('<tbody></tbody>');

                    $tableFinal.append($tbodyFinal);

                    var $trFinal = $('<tr />');
                    $trFinal.append($('<th style="font-size:18px; width:40px;" />').html('A'));;
                    var index=0
                    for (i = 0; i < 4; i++) {                     
                        $trFinal.append($('<td style="font-size:18px; width:40px;"/>').html(endA_Hex[index] + endA_Hex[index + 1]));    
                        index = index + 2;    
                    }
                    $tbodyFinal.append($trFinal);

                    var $trFinal = $('<tr />');
                    $trFinal.append($('<th style="font-size:18px; width:40px;" />').html('B'));;
                    index = 0
                    for (i = 0; i < 4; i++) {
                        $trFinal.append($('<td style="font-size:18px; width:40px;"/>').html(endB_Hex[index] + endB_Hex[index + 1]));
                        index = index + 2;
                    }
                    $tbodyFinal.append($trFinal);

                    var $trFinal = $('<tr />');
                    $trFinal.append($('<th style="font-size:18px; width:40px;" />').html('C'));;
                    index = 0
                    for (i = 0; i < 4; i++) {
                        $trFinal.append($('<td style="font-size:18px; width:40px;"/>').html(endC_Hex[index] + endC_Hex[index + 1]));
                        index = index + 2;
                    }
                    $tbodyFinal.append($trFinal);

                    var $trFinal = $('<tr />');
                    $trFinal.append($('<th style="font-size:18px; width:40px;" />').html('D'));;
                    index = 0
                    for (i = 0; i < 4; i++) {
                        $trFinal.append($('<td style="font-size:18px; width:40px;"/>').html(endD_Hex[index] + endD_Hex[index + 1]));
                        index = index + 2;
                    }
                    $tbodyFinal.append($trFinal);
                    $('#finalLabel').attr('hidden', false);


                    var $tableHash = $('#messageHash');
                    var $tbodyHash = $('<tbody></tbody>');

                    $tableHash.append($tbodyHash);

                    var $trHash = $('<tr />');
                    $trHash.append($('<th style="font-size:25px; border-color:white" />').html('Skrót wiadomośći:'));

                    var hash_A = endA_Hex[6].toString() + endA_Hex[7].toString() + endA_Hex[4].toString() + endA_Hex[5].toString() + endA_Hex[2].toString() + endA_Hex[3].toString() +
                        endA_Hex[0].toString() + endA_Hex[1].toString();
                    var hash_B = endB_Hex[6].toString() + endB_Hex[7].toString() + endB_Hex[4].toString() + endB_Hex[5].toString() + endB_Hex[2].toString() + endB_Hex[3].toString() +
                        endB_Hex[0].toString() + endB_Hex[1].toString();
                    var hash_C = endC_Hex[6].toString() + endC_Hex[7].toString() + endC_Hex[4].toString() + endC_Hex[5].toString() + endC_Hex[2].toString() + endC_Hex[3].toString() +
                        endC_Hex[0].toString() + endC_Hex[1].toString();
                    var hash_D = endD_Hex[6].toString() + endD_Hex[7].toString() + endD_Hex[4].toString() + endD_Hex[5].toString() + endD_Hex[2].toString() + endD_Hex[3].toString() +
                        endD_Hex[0].toString() + endD_Hex[1].toString();
                    var hash = hash_A + hash_B + hash_C + hash_D;
                    $trHash.append($('<td style="font-size:25px; border-color:white"/>').html(hash));
                    $tbodyHash.append($trHash);

                    $('#showRoundsButton').attr('disabled', true);
                    $('#transformations').attr('hidden', false);
>>>>>>> PM
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











