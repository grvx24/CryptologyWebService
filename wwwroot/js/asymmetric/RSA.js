var RsaInit = function(config) {

    function isPrime(number) {
        if (typeof number !== 'number' || !Number.isInteger(number)) {
            return false;
        }

        if (number < 2) {
            return false;
        }

        if (number === 2) {
            return true;
        } else if (number % 2 === 0) {
            return false;
        }

        for (var i = 3; i * i <= number; i += 2) {
            if (number % i === 0) {
                return false;
            }
        }
        return true;

    }

    function isCommaSeparatedNumber(str) {
        regexp = /^(\d+,)*\d+$/;
        if (regexp.test(str)) {
            return true;
        } else {
            return false;
        }
    }


    var buttonsInit = function() {

        $('#generateKeys').click(function() {
            var result = validatePQInputs();

            if (typeof result !== 'undefined') {

                var model = {
                    P: result.P,
                    Q: result.Q
                };
                $.ajax({
                    type: 'POST',
                    url: config.urls.generateKeysUrl,
                    dataType: 'json',
                    contentType: "application/json",
                    data: JSON.stringify(model),
                    success: function (data) {

                        $('#privateKey').val(data.privateKey);
                        $('#publicKey').val(data.publicKey);
                    },
                    error: function (response) {
                        alert(response.responseJSON.message);
                    }
                });
            }
        });

        $('#encryptButton').click(function() {
            sendMessageToEncrypt();
        });

        $('#decryptButton').click(function() {
            sendMessageToDecrypt();
        });
    }

    var sendMessageToEncrypt = function() {

        var message = $('#inputEncrypt').val();
        if (message === '') {
            return;
        }

        var key = $('#encryptKey').val();

        if (isCommaSeparatedNumber(key)) {

            var pair = key.split(',');

            var model = {
                Message: message,
                N: pair[0],
                EorD: pair[1]
            };
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

        } else {
            alert("Niepoprawny format klucza szyfrującego!");
        }

    }

    var sendMessageToDecrypt = function() {

        var message = $('#inputDecrypt').val();
        if (message === '') {
            return;
        }
        if (isCommaSeparatedNumber(message)) {

            var key = $('#decryptKey').val();

            if (isCommaSeparatedNumber(key)) {

                var numbers = message.split(',');

                var pair = key.split(',');

                var model = {
                    Message: numbers,
                    N: pair[0],
                    EorD: pair[1]
                };
                $.ajax({
                    type: 'POST',
                    url: config.urls.decryptUrl,
                    dataType: 'json',
                    contentType: "application/json",
                    data: JSON.stringify(model),
                    success: function(data) {

                        $('#outputDecrypt').val(data);
                    },
                    error: function(response) {
                        alert(response.responseJSON.message);
                    }
                });

            } else {
                alert('Niepoprawny format szyfrogramu!');
            }

        }
    }

    var validatePQInputs = function() {

        var reg = /^\d+$/;
        var p_input = $('#p_value').val();
        var q_input = $('#q_value').val();


        if (reg.test(p_input) && reg.test(q_input)) {

            if (p_input.length < 3 || q_input.length < 3) {
                alert("Wartości P i Q muszą być conajmniej 3 cyfrowe!");
                return;
            }

            var p = parseInt(p_input);
            var q = parseInt(q_input);
            if (!isPrime(p) || !isPrime(q)) {
                alert('Pola P oraz Q muszą być liczbami pierwszymi!');
                return;
            }

            return {P:p,Q:q}

        } else {
            alert("Pola P oraz Q muszą być liczbami pierwszymi!");
            return;
        }
    }

    var copyBtnsInit = function() {

        $('#publicKeyCopyEncrypt').click(function() {
            var key = $('#publicKey').val();
            $('#encryptKey').val(key);
        });
        $('#privateKeyCopyEncrypt').click(function () {
            var key = $('#privateKey').val();
            $('#encryptKey').val(key);
        });

        $('#publicKeyCopyDecrypt').click(function () {
            var key = $('#publicKey').val();
            $('#decryptKey').val(key);
        });
        $('#privateKeyCopyDecrypt').click(function () {
            var key = $('#privateKey').val();
            $('#decryptKey').val(key);
        });
    }


    var toolTipInit = function() {
        $('[data-toggle="tooltip"]').tooltip({ trigger: "hover" });
    }


    var init = function() {
        toolTipInit();
        buttonsInit();
        copyBtnsInit();
    }


    return {
        init: init
    };
}