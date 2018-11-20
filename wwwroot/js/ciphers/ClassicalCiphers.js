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

