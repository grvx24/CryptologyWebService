//#region Caesar cipher
var CaesarCipherInit = function (config) {


    var encryptInit = function() {

        $('#encryptButton').click(function() {

            var model = {
                message: $('#inputEncrypt').val(),
                key: $('#keyEncrypt').val(),
                alphabetType: $('#alphabetEncrypt').val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.encryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function(data) {

                    $('#outputEncrypt').val(data);
                },
                error: function(response) {
                    alert(response.responseJSON.message);
                }
            });
        });
    }
    var decryptInit = function() {
            $('#decryptButton').click(function () {

                var model = {
                    message: $('#inputDecrypt').val(),
                    key: $('#keyDecrypt').val(),
                    alphabetType: $('#alphabetDecrypt').val()
                }

                $.ajax({
                    type: 'POST',
                    url: config.urls.decryptUrl,
                    dataType: 'json',
                    contentType: "application/json",
                    data: JSON.stringify(model),
                    success: function (data) {

                        $('#outputDecrypt').val(data);
                    },
                    error: function (response) {
                        alert(response.responseJSON.message);
                    }
                });

            });
        }

    var init = function() {
        encryptInit();
        decryptInit();
    }

    return {
        init: init
    }

}

var CaesarVisualizationInit = function (config) {

    var visualizationInit = function () {

        $('#startButton').click(function () {

            var model = {
                message: $('#inputCipher').val(),
                key: $('#keyVisualization').val(),
                alphabetType: $('#alphabetVisualization').val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.visualizationUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    $('#alphabetsTable th').remove();
                    $('#alphabetsTable td').remove(); 
                    $('#alphabetsTable tr').remove();
                    $('#alphabetsTable tbody').remove();
                    $('#outputCipher').val("");
                    var alphabetArray = [];
                    var alphabet = data[0];
                    for (i = 0; i < alphabet.length; i++) {
                        alphabetArray[i] = alphabet[i];
                    }
                    var cipherArray = [];
                    var cipher = data[1];
                    for (i = 0; i < cipher.length; i++) {
                        cipherArray[i] = cipher[i];
                    }

                    var $table = $('#alphabetsTable');
                    var $tbody = $('<tbody></tbody>');

                    $table.append($tbody);
                   
                    var $tr = $('<tr />');
                    $tr.append($('<th />').html('Alfabet'));
                    $.each(alphabetArray, function (ignored, alfabet) {
                        $tr.append($('<td />').html(alfabet));
                    });
                    $tbody.append($tr);

                    $tr = $('<tr />');
                    $tr.append($('<th />').html('Szyfr'));
                    $.each(cipherArray, function (ignored, szyfr) {
                        $tr.append($('<td />').html(szyfr));
                    });
                    $tbody.append($tr);
                    
                    
                    if ($('#inputCipher').val() != "") {
                        $('#nextButton').attr('disabled', false);
                        $('#startButton').attr('disabled', true);
                    }
                  
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });
        });


        $('#nextButton').click(function () {

            var model = {
                message: $('#inputCipher').val(),
                key: $('#keyVisualization').val(),
                alphabetType: $('#alphabetVisualization').val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.visualizationUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    var input = data[3];
                    var output = data[2];
                    var alphabet = data[0];
                   
                    var currentOutput = $('#outputCipher').val();

                    var letterData = output[currentOutput.length];
                    var newdata = $('#outputCipher').val() + letterData;
                    $('#outputCipher').val(newdata);

                    if (currentOutput.length + 1 == input.length) {
                        $('#nextButton').attr('disabled', true);
                        $('#startButton').attr('disabled', false);
                    }


                    var cellValue = input[currentOutput.length];
                    console.log(cellValue);

       
                    var table = document.getElementById("alphabetsTable");
                    var rows = table.getElementsByTagName("tr");
                    var cellsLength = rows[0].cells.length;

                    for (var z = 1; z < cellsLength; z++) {
                            rows[0].cells[z].style.backgroundColor = 'white';
                            rows[1].cells[z].style.backgroundColor = 'white';
                        }

                   
                    for (var z = 1; z < cellsLength; z++) {
                        if (rows[0].cells[z].innerHTML == cellValue)
                        {
                            rows[0].cells[z].style.backgroundColor = '#33b5e5';
                            rows[1].cells[z].style.backgroundColor = '#33b5e5';
                        }
                       
                    }

              

                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });
        });



        $("#alphabetVisualization").on('change',
            function () {
                $('#outputCipher').val("");
                $('#startButton').attr('disabled', false);
                $('#alphabetsTable th').remove();
                $('#alphabetsTable td').remove(); 
                $('#alphabetsTable tr').remove();
                $('#alphabetsTable tbody').remove();
                $('#nextButton').attr('disabled', true);
            });

        $("#keyVisualization").on('change',
            function () {
                $('#outputCipher').val("");
                $('#startButton').attr('disabled', false);
                $('#alphabetsTable th').remove(); 
                $('#alphabetsTable td').remove(); 
                $('#alphabetsTable tr').remove();
                $('#alphabetsTable tbody').remove();
                $('#nextButton').attr('disabled', true);
            });

        $("#inputCipher").on('input', function () {
                $('#outputCipher').val("");
                $('#startButton').attr('disabled', false);
                $('#nextButton').attr('disabled', true);
            });

    }
  
    var init = function () {
       visualizationInit();
    }

    return {
        init: init
    }

}
//#endregion

//#region Affine cipher
var AffineCipherInit = function (config) {

    var encryptInit = function () {

        $('#encryptButton').click(function () {

            var model = {
                message: $('#inputEncrypt').val(),
                keyA: $('#keyEncryptA option:selected').val(),
                keyB: $('#keyEncryptB option:selected').val(),
                alphabetType: $('#alphabetEncrypt option:selected').val()
            }

            console.log(model);

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
    var decryptInit = function () {
        $('#decryptButton').click(function () {

            var model = {
                message: $('#inputDecrypt').val(),
                keyA: $('#keyDecryptA option:selected').val(),
                keyB: $('#keyDecryptB option:selected').val(),
                alphabetType: $('#alphabetDecrypt option:selected').val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.decryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    $('#outputDecrypt').val(data);
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });

        });
    }

    var init = function () {
        encryptInit();
        decryptInit();
    }

    return {
        init: init
    }
}
//#endregion

//#region Beacon cipher
var BaconCipherInit = function (config) {

    var encryptInit = function () {

        $('#encryptButton').click(function () {

            var model = {
                message: $('#inputEncrypt').val()
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
    var decryptInit = function () {
        $('#decryptButton').click(function () {

            var model = {
                message: $('#inputDecrypt').val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.decryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    $('#outputDecrypt').val(data);
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });

        });
    }

    var init = function () {
        encryptInit();
        decryptInit();
    }

    return {
        init: init
    }
}

var BaconVisualizationInit = function (config) {

    function clearHighlightsCell()
    {
        $('#part-1 tr td').each(function () {
            $(this).parent().css('background-color', 'white');
        });
        $('#part-2 tr td').each(function () {
            $(this).parent().css('background-color', 'white');
        });
        $('#part-3 tr td').each(function () {
            $(this).parent().css('background-color', 'white');
        });
        $('#part-4 tr td').each(function () {
            $(this).parent().css('background-color', 'white');
        });
        $('#part-5 tr td').each(function () {
            $(this).parent().css('background-color', 'white');
        });
        $('#part-6 tr td').each(function () {
            $(this).parent().css('background-color', 'white');
        }); 
    }

    var visualizationInit = function () {

        $('#nextButton').click(function () {

            var model = {
                message: $('#inputCipher').val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.encryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {
                    var inputLength = ($('#inputCipher').val()).length;
                    var outputLength = ($('#outputCipher').val()).length;  

                    var input = $('#inputCipher').val();
                    var withoutSpaces='';
                    for (i = 0; i < inputLength; i++) {
                        if (input[i] != ' ') {
                            withoutSpaces = withoutSpaces+ input[i];
                        }
                    }
                    withoutSpaces = withoutSpaces.toUpperCase();                 
                    inputLength = withoutSpaces.length;
                   
                    clearHighlightsCell();

                        var letterData = data[outputLength] + data[outputLength+1] + data[outputLength+2] + data[outputLength+3] + data[outputLength+4];
                        var newdata = $('#outputCipher').val() + letterData;
                    $('#outputCipher').val(newdata);

                    if (outputLength+5 == inputLength*5) {
                        $('#nextButton').attr('disabled', true);
                        $('#startButton').attr('disabled', false);
                    }

                    var cellNumber = (outputLength) / 5;
                    var cellValue = withoutSpaces[cellNumber];
 
                    $('#part-1 tr td').each(function () {
                        if ($(this).text() == cellValue) {
                            $(this).parent().css('background-color', '#33b5e5');
                        }
                    }); 
                    $('#part-2 tr td').each(function () {
                        if ($(this).text() == cellValue) {
                            $(this).parent().css('background-color', '#33b5e5');
                        }
                    }); 
                    $('#part-3 tr td').each(function () {
                        if ($(this).text() == cellValue) {
                            $(this).parent().css('background-color', '#33b5e5');
                        }
                    }); 
                    $('#part-4 tr td').each(function () {
                        if ($(this).text() == cellValue) {
                            $(this).parent().css('background-color', '#33b5e5');
                        }
                    }); 
                    $('#part-5 tr td').each(function () {
                        if ($(this).text() == cellValue) {
                            $(this).parent().css('background-color', '#33b5e5');
                        }
                    }); 
                    $('#part-6 tr td').each(function () {
                        if ($(this).text() == cellValue) {
                            $(this).parent().css('background-color', '#33b5e5');
                        }
                    }); 
                   
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });
        });
 
        $('#startButton').click(function () {
            $('#outputCipher').val('');
            if ($('#inputCipher').val() == '') {
                $('#nextButton').attr('disabled', true);
            } else {
                $('#nextButton').attr('disabled', false);
            }        
            $('#startButton').attr('disabled', true);
            clearHighlightsCell();
        });

        $("#inputCipher").on('input', function () {
                $('#nextButton').attr('disabled', true);
                $('#startButton').attr('disabled', false);
                clearHighlightsCell();
                $('#outputCipher').val('');
        });
    }

    var init = function () {
        visualizationInit();
    }

    return {
        init: init
    }
}

//#endregion

//#region ColumnarTrensposition cipher
var ColumnarTranspositionCipherInit = function (config) {

    var encryptInit = function () {

        $('#encryptButton').click(function () {

            var model = {
                message: $('#inputEncrypt').val(),
                key: $("#keyEncrypt").val()
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
    var decryptInit = function () {
        $('#decryptButton').click(function () {

            var model = {
                message: $('#inputDecrypt').val(),
                key: $("#keyDecrypt").val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.decryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    $('#outputDecrypt').val(data);
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });

        });
    }

    var init = function () {
        encryptInit();
        decryptInit();
    }

    return {
        init: init
    }
}


var ColumnarTranspositionVisualizationInit = function (config) {

    function removeTables() {
        $('#sortedKey td').remove();
        $('#sortedKey tr').remove();
        $('#sortedKey tbody').remove();

        $('#numeredKey td').remove();
        $('#numeredKey tr').remove();
        $('#numeredKey tbody').remove();

        $('#encryptMatrix td').remove();
        $('#encryptMatrix tr').remove();
        $('#encryptMatrix tbody').remove();
    }

    var visualizationInit = function () {

        $('#startButton').click(function () {


            var model = {
                message: $('#inputCipher').val(),
                key: $("#keyVisualization").val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.visualizationUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    removeTables();

                    $('#outputCipher').val("");
                    $('#stepNumber').val("1");
                
                    _sortedKey = data[0];
                    _numeredKeyString = data[1];
                    _output = data[2];
                    _input = data[3];
                    _key = data[4];

                    var _numeredKey = [];
                    var _numeredKey = _numeredKeyString.split(','); 


                    var $table_sortedKey = $('#sortedKey');
                    var $tbody_sortedKey = $('<tbody></tbody>');

                    $table_sortedKey.append($tbody_sortedKey);

                    var $tr = $('<tr />');
                    for (i = 0; i < _sortedKey.length; i++) {
                        $tr.append($('<td style="width:30px"/>').html(_sortedKey[i]));
                    }
                    $tbody_sortedKey.append($tr);

                    $tr = $('<tr />');
                    for (i = 1; i < _sortedKey.length + 1; i++) {
                        $tr.append($('<td style="width:30px"/>').html(i));
                    }
                    $tbody_sortedKey.append($tr);

                    var $table_numeredKey = $('#numeredKey');
                    var $tbody_numeredKey = $('<tbody></tbody>');

                    $table_numeredKey.append($tbody_numeredKey);

                    $tr = $('<tr />');
                    for (i = 0; i < _key.length; i++) {
                        $tr.append($('<td style="width:30px"/>').html(_key[i]));
                    }
                    $tbody_numeredKey.append($tr);

                    $tr = $('<tr />');
                    for (i = 0; i < _numeredKey.length; i++) {
                        $tr.append($('<td style="width:30px"/>').html(_numeredKey[i]));
                    }
                    $tbody_numeredKey.append($tr);


                    ////matrix tests
                    var rowsMatrix = Math.ceil(_input.length / _key.length);

                    var $table_encryptMatrix = $('#encryptMatrix');
                    var $tbody_encryptMatrix = $('<tbody></tbody>');

                    $table_encryptMatrix.append($tbody_encryptMatrix);       

                    var messageCounter = 0;
                    for (i = 0; i < rowsMatrix; i++)
                    {
                        var $tr = $('<tr />');
                        for (j = 0; j < _key.length; j++)
                        {
                            if (messageCounter < _input.length) {
                                $tr.append($('<td style="width:30px"/>').html(_input[messageCounter]));
                            }
                              messageCounter++;
                            if (messageCounter > _input.length)
                                    {
                                $tr.append($('<td style="width:30px" />').html("X"));
                                    }
                        }
                        $tbody_encryptMatrix.append($tr);
                    }

                    if (_input != "") {
                        $('#labelEncryptMatrix').attr('hidden', false);
                        $('#nextButton').attr('disabled', false);
                    }
                    $('#labelSortedKey').attr('hidden', false);
                                  
                    $('#startButton').attr('disabled', true);

                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });
        });

        $('#nextButton').click(function () {

            var model = {
                message: $('#inputCipher').val(),
                key: $('#keyVisualization').val(),
            }

            $.ajax({
                type: 'POST',
                url: config.urls.visualizationUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    _sortedKey = data[0];
                    _numeredKeyString = data[1];
                    _output = data[2];
                    _input = data[3];
                    _key = data[4];
              

                    var currentStep = parseInt($('#stepNumber').val());
                    var columnNumber;
                    var tableKey = document.getElementById("numeredKey");
                    var rowsKey = tableKey.getElementsByTagName("tr");

            
                    for (var z = 0; z < _key.length; z++) {
                        if (rowsKey[1].cells[z].innerHTML == currentStep) {
                            columnNumber = z;
                            rowsKey[1].cells[z].style.backgroundColor = 'white';
                        }
                    }

                        var tableMatrix = document.getElementById("encryptMatrix");
                        var rowsMatrix = tableMatrix.getElementsByTagName("tr");  
                        
         
                    var columnValue="";
                    for (i = 0; i < rowsMatrix.length; i++) {
                            for (j = 0; j < _key.length; j++) {
                                if (j == columnNumber) {
                                    columnValue += rowsMatrix[i].cells[columnNumber].innerHTML;
                                    rowsMatrix[i].cells[columnNumber].style.backgroundColor = '#33b5e5';
                                    rowsKey[1].cells[columnNumber].style.backgroundColor = '#8fd1ea';
                                } else {
                                    rowsMatrix[i].cells[j].style.backgroundColor = 'white';
                                }                              
                            }
                            
                            }
                    var currentOutput = $('#outputCipher').val();
                    var newOutput = currentOutput + columnValue;
                    $('#outputCipher').val(newOutput);                  
                    currentStep += 1;
                    $('#stepNumber').val(currentStep);

                    if (currentStep > _key.length) {
                        $('#nextButton').attr('disabled', true);
                        $('#startButton').attr('disabled', false);
                    }
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });
        });

        $("#keyVisualization").on('input',
            function () {
                $('#outputCipher').val("");
                $('#startButton').attr('disabled', false);
                $('#nextButton').attr('disabled', true);

                removeTables();

                $('#labelSortedKey').attr('hidden', true);
                $('#labelEncryptMatrix').attr('hidden', true);
                if ($('#keyVisualization').val() == "") {
                    $('#startButton').attr('disabled', true);
                }

            });

        $("#inputCipher").on('input', function () {
            $('#outputCipher').val("");
            $('#startButton').attr('disabled', false);
            $('#nextButton').attr('disabled', true);

            removeTables();

            $('#labelSortedKey').attr('hidden', true);
            $('#labelEncryptMatrix').attr('hidden', true);

            if ($('#keyVisualization').val() == "") {
                $('#startButton').attr('disabled', true);
            }
        });
    }

    var init = function () {
        visualizationInit();
    }

    return {
        init: init
    }

}
//#endregion

//#region Fence cipher
var FenceCipherInit = function(config) {
    var encryptInit = function () {

        $('#encryptButton').click(function () {

            var model = {
                message: $('#inputEncrypt').val(),
                key: $("#keyEncrypt").val()
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
    var decryptInit = function () {
        $('#decryptButton').click(function () {

            var model = {
                message: $('#inputDecrypt').val(),
                key: $("#keyDecrypt").val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.decryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    $('#outputDecrypt').val(data);
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });

        });
    }

    var init = function () {
        encryptInit();
        decryptInit();
    }

    return {
        init: init
    }
}

var FenceVisualizationInit = function (config) {

    function removeTables() {
        $('#fenceTable th').remove();
        $('#fenceTable td').remove();
        $('#fenceTable tr').remove();
        $('#fenceTable tbody').remove();
    }

    var visualizationInit = function () {
       
        $('#startButton').click(function () {

            var model = {
                message: $('#inputCipher').val(),
                key: $('#keyVisualization').val(),
            }

            $.ajax({
                type: 'POST',
                url: config.urls.visualizationUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    var _input = data[1];
                    var _output = data[0];
                    var _table = data[2];
                    var _key = $('#keyVisualization').val();

                    removeTables();
                    $('#outputCipher').val("");
                    $('#stepNumber').val("1");

                    var $table = $('#fenceTable');
                    var $tbody = $('<tbody></tbody>');

                    $table.append($tbody);

                    var index = 0;
                    for (i = 0; i < _key; i++)
                    {
                        var $tr = $('<tr />');
                        for (j = 0; j < _input.length; j++)
                         {
                            $tr.append($('<td style="width:30px;height:30px"/>').html(_table[index]));
                            index++;
                        }
                        $tbody.append($tr);
                    }


                    if ($('#inputCipher').val() != "") {
                        $('#nextButton').attr('disabled', false);
                        $('#startButton').attr('disabled', true);
                    }
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });
        });


        $('#nextButton').click(function () {

            var model = {
                message: $('#inputCipher').val(),
                key: $('#keyVisualization').val(),
            }

            $.ajax({
                type: 'POST',
                url: config.urls.visualizationUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {
                    var currentStep = parseInt($('#stepNumber').val());
                    var _output = data[0];
                    var table = document.getElementById("fenceTable");
                    var rows = table.getElementsByTagName("tr");
                    var columns = rows[0].getElementsByTagName("td");
                    

                    var counter = 0;
                    for (i = 0; i < rows.length; i++)
                    {
                        for (z = 0; z < columns.length; z++) 
                        {
                            if (rows[i].cells[z].innerHTML != "")
                            {    
                                counter++;
                                if (counter == currentStep) {
                                    rows[i].cells[z].style.backgroundColor = '#33b5e5';
                                }
                                else {
                                    rows[i].cells[z].style.backgroundColor = 'white';
                                }
                                
                            }
                        }
                    }
                 
                    var currentOutput = $('#outputCipher').val();
                    var newOutput = currentOutput + _output[currentStep-1];
                    $('#outputCipher').val(newOutput);
                    currentStep += 1;
                    $('#stepNumber').val(currentStep);

                    if (currentStep > _output.length) {
                        $('#nextButton').attr('disabled', true);
                        $('#startButton').attr('disabled', false);
                    }
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });
        });



        $("#keyVisualization").on('change',
            function () {
                $('#outputCipher').val("");
                $('#startButton').attr('disabled', false);
                removeTables();
                $('#nextButton').attr('disabled', true);
                if ($('#inputCipher').val() == "") {
                    $('#startButton').attr('disabled', true);
                }
            });

        $("#inputCipher").on('input', function () {
            $('#outputCipher').val("");
            $('#startButton').attr('disabled', false);
            removeTables();
            $('#outputCipher').val("");
            $('#nextButton').attr('disabled', true);
            if ($('#inputCipher').val() == "") {
                $('#startButton').attr('disabled', true);
            }
        });

    }

    var init = function () {
        visualizationInit();
    }

    return {
        init: init
    }

}



//#endregion

//#region Playfair cipher
var PlayfairCipherInit = function (config) {
    var encryptInit = function () {

        $('#encryptButton').click(function () {

            var model = {
                message: $('#inputEncrypt').val(),
                key: $("#keyEncrypt").val()
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
    var decryptInit = function () {
        $('#decryptButton').click(function () {

            var model = {
                message: $('#inputDecrypt').val(),
                key: $("#keyDecrypt").val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.decryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    $('#outputDecrypt').val(data);
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });

        });
    }

    var init = function () {
        encryptInit();
        decryptInit();
    }

    return {
        init: init
    }
}
//#endregion

//#region Route cipher
var RouteCipherInit = function (config) {
    var encryptInit = function () {

        $('#encryptButton').click(function () {

            var key = parseInt($("#keyEncrypt option:selected").val());
            var mode = parseInt($("#encryptMode option:selected").val());

            var model = {
                message: $('#inputEncrypt').val(),
                key: key,
                mode: mode
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
    var decryptInit = function () {
        $('#decryptButton').click(function () {

            var key = parseInt($("#keyDecrypt option:selected").val());
            var mode = parseInt($("#decryptMode option:selected").val());
            console.log(key);
            var model = {
                message: $('#inputDecrypt').val(),
                key: key,
                mode: mode
            }

            $.ajax({
                type: 'POST',
                url: config.urls.decryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    $('#outputDecrypt').val(data);
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });

        });
    }

    var init = function () {
        encryptInit();
        decryptInit();
    }

    return {
        init: init
    }
}
//#endregion

//#region Vigenere cipher
var VigenereCipherInit = function (config) {
    var encryptInit = function () {

        $('#encryptButton').click(function () {

            var model = {
                message: $('#inputEncrypt').val(),
                key: $("#keyEncrypt").val(),
                alphabetType: $('#alphabetEncrypt option:selected').val()
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
    var decryptInit = function () {
        $('#decryptButton').click(function () {

            var model = {
                message: $('#inputDecrypt').val(),
                key: $("#keyDecrypt").val(),
                alphabetType: $('#alphabetDecrypt option:selected').val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.decryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    $('#outputDecrypt').val(data);
                },
                error: function (response) {
                    alert(response.responseJSON.message);
                }
            });

        });
    }

    var init = function () {
        encryptInit();
        decryptInit();
    }

    return {
        init: init
    }
}
//#endregion
