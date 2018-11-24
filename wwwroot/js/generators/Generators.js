var GeneratorsInit = function(config) {

    var lastUsedMode = 0;

    var lfsrListInit = function () {
        $("#numOfLfsr").on('change',
            function() {

                var numOfLfsr = parseInt(this.value);
                var lfsrDiv = $("#lfsrBox");

                lfsrDiv.html('');

                for (var i = 0; i < numOfLfsr; i++) {
                    lfsrDiv.append('<label for="register' +
                        (i + 1) +
                        '">' + 'Rejestr nr ' +
                        (i + 1) +
                        '</label>' + '<div class="form-inline">' +

                        '<input maxlength = "20" id = "register' +
                        (i + 1) +
                        '" class="lfsr-registers form-control" type = "text" />' +
                        '<label id="registerCounter' + (i + 1) + '" class="btn btn-success">0</label>' + '<br>' +
                        '<label>Aktualny stan rejestru:</label>'+'<br/>'+
                        '<label id="register'+(i+1)+'-content" class="registers-font">01</label>'+'<br>');

                }

                charsCounterInit();

            });

    }

    var setRegistersInit = function () {

        $("#setRegistersBtn").click(function () {

            

            var numOfLfsr = parseInt($("#numOfLfsr option:selected").val());
            var alerts = [];
            for (var i = 1; i <= numOfLfsr; i++) {
                var valueToSet = $("#register" + i).val();
                if (!valueToSet.length == 0) {
                    if (!isBinaryString(valueToSet)) {
                        alerts.push("Pole nr " + i + " zawiera niepoprawne znaki (dozwolone są tylko 0 i 1)!");
                    } else if (valueToSet.length == 1) {
                        alerts.push("Rejestr nr " + i + " musi mieć conajmniej 2 bity!");
                    } 
                    else {
                        $("#register" + i + "-content").text(valueToSet);
                    }
                }

            }
            if (alerts.length != 0) {
                alert(alerts.join('\n'));
            }
        });

    }

    var generateBtnInit = function() {


        $("#generateBtn").click(function() {

            var seriesLength = parseInt($("#seriesLength").val());
            if (seriesLength < 0) {
                alert('Długość ciągu musi być liczbą dodatnią!');
                return;
            }
            if (isNaN(seriesLength)) {

                alert('Długość ciągu musi być liczbą!');
            } else {
                
                var registers = [];
                var feedbackFunctions = [];

                var numOfLfsr = parseInt($("#numOfLfsr option:selected").val());

                for (var i = 1; i <= numOfLfsr; i++) {
                    let register = $("#register" + i + "-content").text();
                    registers.push(register);
                    feedbackFunctions.push(GetFeedbackFunctionInfo(register.length));

                }

                var mode = parseInt($('input[name=outputFormat]:checked').val());

                var model = {};
                if ($("#k_value").length && $("#d_value").length) {

                    var k = parseInt($("#k_value option:selected").val());
                    var d = parseInt($("#d_value option:selected").val());

                    model = {
                        Registers: registers,
                        Length: seriesLength,
                        Mode: mode,
                        FeedbackFunctions : feedbackFunctions,
                        K_value: k,
                        D_value: d
                    }

                }
                else {
                    model = {
                        Registers: registers,
                        Length: seriesLength,
                        Mode: mode,
                        FeedbackFunctions: feedbackFunctions
                    }
                }

                $.ajax({
                    type: 'POST',
                    url: config.urls.generateUrl,
                    dataType: 'json',
                    contentType: "application/json",
                    data: JSON.stringify(model),
                    success: function (data) {

                        lastUsedMode = mode;
                        $('#outputConsole').val(data);

                        var downloadLink = $("#downloadBtn");

                        if (lastUsedMode == 2) {
                            var href = "data:application/octet-stream;charset=utf-16le;base64," + data;
                            downloadLink.attr("download", 'ciąg' + Date.now() + '.bin');
                            downloadLink.attr("href", href);

                        } else {
                            var href = "data:application/text;charset=utf-16le;," + data;
                            downloadLink.attr("download", 'ciąg' + Date.now() + '.txt');
                            downloadLink.attr("href", href);
                        }


                    },
                    error: function (response) {
                        alert(response.responseJSON.message);
                    }
                });

            }


        });
    }

    var tooltipInit = function() {
        $(function() {
            $('[data-toggle="tooltip"]').tooltip()

        });
    }

    var charsCounterInit = function () {

        $(".lfsr-registers").on('keyup', function () {
            
            var id = $(this).attr("id");
            var length = $(this).val().length;

            $('#registerCounter'+id.slice(-1)).html(length);
        });

    }

    var feedbackFunctionInit = function() {
        $("#feedbackFunctionBtn").click(function() {
            $("#feedbackFunction").slideToggle("slow");
            $(this).blur();

        });
    }


    var init = function() {
        lfsrListInit();
        setRegistersInit();
        generateBtnInit();
        tooltipInit();
        charsCounterInit();
        feedbackFunctionInit();
    }


    return {
         init:init
        }


    function GetFeedbackFunctionInfo(lfsrLength) {
        var correctRow = $(".lfsr-len" + lfsrLength);

        let counter = 0;
        var array = [];

        correctRow.each(function() {
            if ($(this).prop("checked")) {
                array.push(counter.toString());
            }
            counter++;
        });

        console.log(array);
        return array.join(",");

    }


    //helpers
    function isBinaryString(str) {
        regexp = /^[01]+$/;
        if (regexp.test(str)) {
            return true;
            } else {
            return false;
           }
        }




}



