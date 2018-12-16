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
<<<<<<< HEAD
=======
=======
>>>>>>> parent of 09b51ac... Revert "Merge branch 'master' of https://github.com/grvx24/CryptologyWebService"
    var init = function () {
        encryptInit();
    }

    return {
        init: init
    }

}

var MD5VisualizationInit = function (config) {

<<<<<<< HEAD
    function removeTable() {
        $('#registersTable th').remove();
        $('#registersTable td').remove();
        $('#registersTable tr').remove();
        $('#registersTable tbody').remove();

        $('#xTable th').remove();
        $('#xTable td').remove();
        $('#xTable tr').remove();
        $('#xTable tbody').remove();

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
=======
>>>>>>> parent of 09b51ac... Revert "Merge branch 'master' of https://github.com/grvx24/CryptologyWebService"
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
                        $('#XTableButton').attr('disabled', false);
                    }
                 
                },
                error: function (response) {
                    console.log(response.responseJSON.message);
                    alert(response.responseJSON.message);
                }
            });
        });

        $('#XTableButton').click(function () {

            var model = {
                message: $('#exampleMessage').val(),
            }

            $.ajax({
                type: 'POST',
                url: config.urls.XtableUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    var $table = $('#xTable');
                    var $tbody = $('<tbody></tbody>');

                    $table.append($tbody);

                    var $tr = $('<tr />');
                    $tr.append($('<td style="background-color:#9dc2f2; font-size:18px;width:50px;" />').html('i'));
                    $tr.append($('<td style="background-color:#9dc2f2; font-size:18px;" />').html('X[i]'));
                    $tbody.append($tr);

                    for (i = 0; i < 16; i++) {
                        var $tr = $('<tr />');
                        $tr.append($('<td style="font-size:18px;"/>').html(i));
                        $tr.append($('<td style="font-size:18px;"/>').html(data[i]));
                        $tbody.append($tr);
                    }

                    $('#XTableButton').attr('disabled', true);
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
            $('#XTableButton').attr('disabled', true);
            if ($("#exampleMessage").val() == "") {
                $('#paddingButton').attr('disabled', true);
            } else {
                $('#paddingButton').attr('disabled', false);
            }
        });
>>>>>>> PM
=======
        });
>>>>>>> parent of 09b51ac... Revert "Merge branch 'master' of https://github.com/grvx24/CryptologyWebService"


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

<<<<<<< HEAD
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
=======
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
>>>>>>> parent of 09b51ac... Revert "Merge branch 'master' of https://github.com/grvx24/CryptologyWebService"
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











