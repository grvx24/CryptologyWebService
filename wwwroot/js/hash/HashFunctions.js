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

var MD5VisualizationInit = function (config) {

    var visualInit = function () {

        $('#paddingButton').click(function () {

            var model = {
                message: $('#exampleMessage').val(),
            }

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

                    if (message.length * 8 < 448) {
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
                    //console.log(data);

                    var $table = $('#registersTable');
                    var $tbody = $('<tbody></tbody>');

                    $table.append($tbody);

                    var $tr = $('<tr />');
                    $tr.append($('<td style="background-color:#9dc2f2; font-size:18px;" />').html('Iteracja'));
                    $tr.append($('<td style="background-color:#9dc2f2; font-size:18px;" />').html('Rejestr A'));
                    $tr.append($('<td style="background-color:#9dc2f2; font-size:18px;" />').html('Rejestr B'));
                    $tr.append($('<td style="background-color:#9dc2f2; font-size:18px;" />').html('Rejestr C'));
                    $tr.append($('<td style="background-color:#9dc2f2; font-size:18px;" />').html('Rejestr D'));
                    $tbody.append($tr);

                    for (i = 0; i < 64; i++) {
                        var $tr = $('<tr />');
                        $tr.append($('<td style="font-size:18px;"/>').html(data[i].iteration));
                        $tr.append($('<td style="font-size:18px;"/>').html(data[i].a));
                        $tr.append($('<td style="font-size:18px;"/>').html(data[i].b));
                        $tr.append($('<td style="font-size:18px;"/>').html(data[i].c));
                        $tr.append($('<td style="font-size:18px;"/>').html(data[i].d));
                        $tbody.append($tr);
                    }

                    var changeColor = document.getElementById("registersTable");
                    var rows = changeColor.getElementsByTagName("tr");
                    var registerChanged = 1;
                    for (var z = 1; z < 65; z++) {
                        rows[z].cells[registerChanged].style.color = '#33b5e5';
                        registerChanged++;
                        if (registerChanged > 4) {
                            registerChanged = 1;
                        }
                    }

                    $('#AA').val(rows[64].cells[1].innerHTML);
                    var modValue = Math.pow(2, 32);
                    var endSum = parseInt(1732584193) + parseInt(rows[64].cells[1].innerHTML);
                    var end = (endSum)%(modValue);                  
                    $('#endA').val(end);

                    $('#BB').val(rows[64].cells[2].innerHTML);
                    var modValue = Math.pow(2, 32);
                    var endSum = parseInt(4023233417) + parseInt(rows[64].cells[2].innerHTML);
                    var end = (endSum) % (modValue);
                    $('#endB').val(end);
                   
                    $('#CC').val(rows[64].cells[3].innerHTML);
                    var modValue = Math.pow(2, 32);
                    console.log(modValue);
                    var endSum = parseInt(2562383102) + parseInt(rows[64].cells[3].innerHTML);
                    console.log(endSum);
                    var end = (endSum) % (modValue);
                    $('#endC').val(end);

                    $('#DD').val(rows[64].cells[4].innerHTML);
                    var modValue = Math.pow(2, 32);
                    console.log(modValue);
                    var endSum = parseInt(271733878) + parseInt(rows[64].cells[4].innerHTML);
                    console.log(endSum);
                    var end = (endSum) % (modValue);
                    $('#endD').val(end);
                },
                error: function (response) {
                    console.log(response);
                }
            });
        })

    }

    var init = function () {
        visualInit();
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











