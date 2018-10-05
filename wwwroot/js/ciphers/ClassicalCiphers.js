var CaesarCipherInit = function(config) {


    var caesarCipherInit = function() {

        $('#encryptButton').click(function() {

            var model = {
                message: $('#inputEncrypt').val(),
                key: $('#keyEncrypt').val(),
                alphabetType: $('#alphabetEncrypt option:selected').val()
        }

            $.ajax({
                type: 'POST',
                url: config.urls.caesarEncryptUrl,
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

        $('#decryptButton').click(function() {

            var model = {
                message: $('#inputDecrypt').val(),
                key: $('#keyDecrypt').val(),
                alphabetType: $('#alphabetDecrypt option:selected').val()
            }

            $.ajax({
                type: 'POST',
                url: config.urls.caesarDecryptUrl,
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify(model),
                success: function (data) {

                    $('#outputDecrypt').val(data);
                },
                error: function (response) {
                    console.log(response.responseJSON.message);
                    alert(response.responseJSON.message);
                }
            });

        });
    }

    var init = function() {
        caesarCipherInit();
    }

    return {
        init: init
    }

}




