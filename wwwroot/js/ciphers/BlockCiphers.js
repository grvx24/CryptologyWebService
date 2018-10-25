var BlockCiphersInit = function(config) {

    var encryptInit = function() {


        $('#encryptButton').click(function() {

            var model = {
                message: $('#inputEncrypt').val(),
                key: $('#keyEncrypt').val(),
                IV: $('#IVEncrypt').val(),
                mode: $('#algorithmSelectModeEncrypt option:selected').val(),
                encoding: $("input[name='inputFormatEncrypt']:checked").val(),
                keyEncoding: $("input[name='keyFormatEncrypt']:checked").val(),
                IVEncoding: $("input[name='IVFormatEncrypt']:checked").val()
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

    var decryptInit = function() {
        $('#decryptButton').click(function () {

            var model = {
                message: $('#inputDecrypt').val(),
                key: $('#keyDecrypt').val(),
                IV: $('#IVDecrypt').val(),
                mode: $('#algorithmSelectModeDecrypt option:selected').val(),
                encoding: $("input[name='keyFormatDecrypt']:checked").val()
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


    var fileLoadInit = function()
    {
        $('#fileEncrypt').change(function () {
            var filename = $(this).val().split('\\').pop();
            $('#FilenameToEncrypt').val(filename);
        });

        $('#fileDecrypt').change(function () {
            var filename = $(this).val().split('\\').pop();
            $('#FilenameToDecrypt').val(filename);
        });
    }

    var radioButtonsInit = function() {
        $('input[type=radio][name=encryptInputMode]').change(function () {

            if (this.value === 'text') {
                $('#filePanelEncrypt').hide();
                $('#textPanelEncrypt').show();
                $('#outputEncrypt').show();

            } else if (this.value === 'file') {
                $('#filePanelEncrypt').show();
                $('#textPanelEncrypt').hide();
                $('#outputEncrypt').hide();
            }

        });

        $('input[type=radio][name=decryptInputMode]').change(function () {

            if (this.value === 'text') {
                $('#filePanelDecrypt').hide();
                $('#textPanelDecrypt').show();
                $('#outputDecrypt').show();

            } else if (this.value === 'file') {
                $('#filePanelDecrypt').show();
                $('#textPanelDecrypt').hide();
                $('#outputDecrypt').hide();
            }

        });
    }

    var ButtonsInit = function() {

        $('#btnRandomKeyDecrypt');
    }
    
    function isHexadecimal(str) {
        regexp = /^[0-9a-fA-F]+$/;

        if (regexp.test(str)) {
            return true;
        }
        else {
            return false;
        }
    }


    var ValidationInit = function() {

    }


    var init = function() {
        encryptInit();
        decryptInit();
        fileLoadInit();
        radioButtonsInit();
        ButtonsInit();


        ValidationInit();
    }

    return {
        init: init
    }
}