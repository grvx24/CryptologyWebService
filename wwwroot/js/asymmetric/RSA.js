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
        regexp = /^(-?\d+,)*-?\d+$/;
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
                    beforeSend: function () {
                        showLoading3();
                    },
                    complete: function () {
                        hideLoading3();
                    },
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
                beforeSend: function () {
                    showLoading1();
                },
                complete: function () {
                    hideLoading1();
                },
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
                    beforeSend: function () {
                        showLoading2();
                    },
                    complete: function () {
                        hideLoading2();
                    },
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
        } else {
            alert('Niepoprawny format szyfrogramu!');
        }
    }

    var validatePQInputs = function() {

        var reg = /^-?\d+$/;
        var p_input = $('#p_value').val();
        var q_input = $('#q_value').val();

        if (p_input == q_input) {
            alert("Wartości P i Q muszą być różne!");
            return;
        }

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

    var loadFileInit = function() {

        $('#fileMessage').change(function () {
            var filename = $(this).val().split('\\').pop();
            $('#filenameMessage').val(filename);

            var file = document.getElementById('fileMessage').files[0];
            if (file) {
                var reader = new FileReader();
                reader.readAsText(file, "UTF-8");
                reader.onload = function(evt) {
                    document.getElementById("inputEncrypt").value = evt.target.result;
                }
                reader.onerror = function(evt) {
                    alert("Wystąpił błąd podczas wczytywania pliku!");
                }
            }
            $('#fileMessage').val(null);
        });

        $('#fileCipherText').change(function () {
            var filename = $(this).val().split('\\').pop();
            $('#filenameCipherText').val(filename);

            var file = document.getElementById('fileCipherText').files[0];
            if (file) {
                var reader = new FileReader();
                reader.readAsText(file, "UTF-8");
                reader.onload = function (evt) {
                    document.getElementById("inputDecrypt").value = evt.target.result;
                }
                reader.onerror = function (evt) {
                    alert("Wystąpił błąd podczas wczytywania pliku!");
                }
            }
            $('#fileCipherText').val(null);
        });


        $('#filePublicKey').change(function() {


            var file = document.getElementById('filePublicKey').files[0];
            if (file) {
                var reader = new FileReader();
                reader.readAsText(file, "UTF-8");
                reader.onload = function (evt) {
                    document.getElementById("publicKey").value = evt.target.result;
                }
                reader.onerror = function (evt) {
                    alert("Wystąpił błąd podczas wczytywania pliku!");
                }
            }
            $('#filePublicKey').val(null);

            
        });

        $('#filePrivateKey').change(function () {

            var file = document.getElementById('filePrivateKey').files[0];
            if (file) {
                var reader = new FileReader();
                reader.readAsText(file, "UTF-8");
                reader.onload = function (evt) {
                    document.getElementById("privateKey").value = evt.target.result;
                }
                reader.onerror = function (evt) {
                    alert("Wystąpił błąd podczas wczytywania pliku!");
                }
            }
            $('#filePrivateKey').val(null);
        });

    }

    function downloadFile(filename, text) {
        var element = document.createElement('a');
        element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
        element.setAttribute('download', filename);

        element.style.display = 'none';
        document.body.appendChild(element);

        element.click();

        document.body.removeChild(element);
    }


    var saveToFileInit = function() {
        $('#saveCipherText').click(function() {
            var text = $('#outputEncrypt').val();
            if (text != '') {
                downloadFile('szyfrogram.txt', text);
            }
        });

        $('#saveMessageText').click(function () {
            var text = $('#outputDecrypt').val();
            if (text != '') {
                downloadFile('tekst.txt', text);
            }
        });

        $('#savePublicKey').click(function () {
            var text = $('#publicKey').val();
            if (text != '') {
                downloadFile('klucz_publiczny.txt', text);
            }
        });

        $('#savePrivateKey').click(function () {
            var text = $('#privateKey').val();
            if (text != '') {
                downloadFile('klucz_prywatny.txt', text);
            }
        });
    }


    var toolTipInit = function() {
        $('[data-toggle="tooltip"]').tooltip({ trigger: "hover" });
    }

    function showLoading1() {
        $('#loading1').show();
    }
    function showLoading2() {
        $('#loading2').show();
    }
    function showLoading3() {
        $('#loading3').show();
    }

    function hideLoading1() {
        $('#loading1').hide();
    }
    function hideLoading2() {
        $('#loading2').hide();
    }
    function hideLoading3() {
        $('#loading3').hide();
    }


    var init = function() {
        toolTipInit();
        buttonsInit();
        copyBtnsInit();
        loadFileInit();
        saveToFileInit();
    }


    return {
        init: init
    };
}