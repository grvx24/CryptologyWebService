var BlockCiphersInit = function(config) {

    //Useful Functions
    function checkBin(n) { return /^[01]{1,64}$/.test(n) }
    function checkDec(n) { return /^[0-9]{1,64}$/.test(n) }
    function checkHex(n) { return /^[0-9A-Fa-f]{1,64}$/.test(n) }
    function pad(s, z) { s = "" + s; return s.length < z ? pad("0" + s, z) : s }
    function unpad(s) { s = "" + s; return s.replace(/^0+/, '') }

    //Decimal operations
    function Dec2Bin(n) { if (!checkDec(n) || n < 0) return 0; return n.toString(2) }
    function Dec2Hex(n) { if (!checkDec(n) || n < 0) return 0; return n.toString(16) }

    //Binary Operations
    function Bin2Dec(n) { if (!checkBin(n)) return 0; return parseInt(n, 2).toString(10) }
    function Bin2Hex(n) { if (!checkBin(n)) return 0; return parseInt(n, 2).toString(16) }

    //Hexadecimal Operations
    function Hex2Bin(n) { if (!checkHex(n)) return 0; return parseInt(n, 16).toString(2) }
    function Hex2Dec(n) { if (!checkHex(n)) return 0; return parseInt(n, 16).toString(10) }



    var encryptInit = function () {

        $('#encryptButton').click(function() {

            //get file object
            

            if (validateEncryptionForm() == false) {
                return;
            }

            var file = document.getElementById('fileEncrypt').files[0];
            if (file) {
                // create reader
                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function () {
                    console.log(reader.result);
                };
            }

            var model = {
                Message: $('#inputEncrypt').val(),
                Key: $('#keyEncrypt').val(),
                IV: $('#IVEncrypt').val(),
                Mode: $('#algorithmSelectModeEncrypt option:selected').val(),
                Encoding: $("input[name='inputFormatEncrypt']:checked").val(),
                KeyEncoding: $("input[name='keyFormatEncrypt']:checked").val(),
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

    var decryptInit = function () {

        $('#decryptButton').click(function () {

            if (validateDecryptionForm() == false) {
                return;
            }

            var model = {
                message: $('#inputDecrypt').val(),
                key: $('#keyDecrypt').val(),
                IV: $('#IVDecrypt').val(),
                mode: $('#algorithmSelectModeDecrypt option:selected').val(),
                encoding: $("input[name='inputFormatDecrypt']:checked").val(),
                keyEncoding: $("input[name='keyFormatDecrypt']:checked").val(),
                IVEncoding: $("input[name='IVFormatDecrypt']:checked").val(),
                Padding: $("#paddingToggle").is(':checked')
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

    var txtFileChangeInit = function() {
        $('#inputModeEncrypt').on('change',function () {

            if (this.value === 'text') {
                $('#filePanelEncrypt').hide();
                $('#textPanelEncrypt').show();
                $('#outputGroupEncrypt').show();

            } else if (this.value === 'file') {
                $('#filePanelEncrypt').show();
                $('#textPanelEncrypt').hide();
                $('#outputGroupEncrypt').hide();
            }

        });

        $('#inputModeDecrypt').on('change',function () {

            if (this.value === 'text') {
                $('#filePanelDecrypt').hide();
                $('#textPanelDecrypt').show();
                $('#outputGroupDecrypt').show();

            } else if (this.value === 'file') {
                $('#filePanelDecrypt').show();
                $('#textPanelDecrypt').hide();
                $('#outputGroupDecrypt').hide();
            }

        });
    }
    //  random hex string generator
    var randHex = function (len) {
        var maxlen = 8,
            min = Math.pow(16, Math.min(len, maxlen) - 1);
        max = Math.pow(16, Math.min(len, maxlen)) - 1,
            n = Math.floor(Math.random() * (max - min + 1)) + min,
            r = n.toString(16);
        while (r.length < len) {
            r = r + randHex(len - maxlen);
        }
        return r.toUpperCase();
    };

    var randBinary = function(len) {

        var result = "";

        for (var i = 0; i < len; i++) {
            result+=Math.floor((Math.random() * 2)).toString();
        }
        return result;
    }



    var buttonsEncryptSideInit = function() {

        $('#btnRandomKeyEncrypt').click(function () {

            var format = $("input[name='keyFormatEncrypt']:checked").val();

            var keyLength=$("input[name='keyLengthEncrypt']:checked").val();

            var keyEncryptInput = $('#keyEncrypt');

            switch (format) {
                case "1":
                    {
                        keyEncryptInput.val(randHex(keyLength / 4));
                        keyEncryptInput.keyup();
                        break;
                    }
                case "2":
                    {
                        keyEncryptInput.val(randBinary(keyLength));
                        keyEncryptInput.keyup();
                        break;
                    }
                default:
                    {
                    alert("Format not found");
                    }
            }
        });

        $('#btnRandomIVEncrypt').click(function() {
            var format = $("input[name='IVFormatEncrypt']:checked").val();

            var IVEncryptInput = $('#IVEncrypt');
            var keyLength = parseInt($("input[name='keyLengthEncrypt']:checked").val());;

            switch (format) {
            case "1":
            {
                IVEncryptInput.val(randHex(keyLength / 4));
                IVEncryptInput.keyup();
                break;
            }
            case "2":
            {
                IVEncryptInput.val(randBinary(keyLength));
                IVEncryptInput.keyup();
                break;
            }
            default:
            {
                alert("Format not found");
            }
            }
        });
    }

    var buttonsDecryptSideInit = function () {

        $('#btnRandomKeyDecrypt').click(function () {

            var format = $("input[name='keyFormatDecrypt']:checked").val();

            var keyLength = $("input[name='keyLengthDecrypt']:checked").val();

            var keyDecryptInput = $('#keyDecrypt');

            switch (format) {
            case "1":
            {
                keyDecryptInput.val(randHex(keyLength / 4));
                keyDecryptInput.keyup();
                break;
            }
            case "2":
            {
                keyDecryptInput.val(randBinary(keyLength));
                keyDecryptInput.keyup();
                break;
            }
            default:
            {
                alert("Format not found");
            }
            }
        });

        $('#btnRandomIVDecrypt').click(function () {

            var format = $("input[name='IVFormatDecrypt']:checked").val();

            var IVDecryptInput = $('#IVDecrypt');
            var keyLength = 128;

            switch (format) {
            case "1":
            {
                IVDecryptInput.val(randHex(keyLength / 4));
                IVDecryptInput.keyup();
                break;
            }
            case "2":
            {
                IVDecryptInput.val(randBinary(keyLength));
                IVDecryptInput.keyup();
                break;
            }
            default:
            {
                alert("Format not found");
            }
            }
        });
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

    function isBinaryString(str) {
        regexp = /^[01]+$/;
        if (regexp.test(str)) {
            return true;
        } else {
            return false;
        }
    }


    var textChangeInit = function () {

        var keyEncryptInput = $('#keyEncrypt');
        var keyDecryptInput = $('#keyDecrypt');

        keyEncryptInput.on('keyup', function() {
            var length = keyEncryptInput.val().length;
            $('#keyCounterEncrypt').html(length);
        });

        var IVEncryptInput = $('#IVEncrypt');

        IVEncryptInput.on('keyup', function () {
            var length = IVEncryptInput.val().length;
            $('#IVCounterEncrypt').html(length);
        });

        var keyDecryptInput = $('#keyDecrypt');

        keyDecryptInput.on('keyup', function () {
            var length = keyDecryptInput.val().length;
            $('#keyCounterDecrypt').html(length);
        });

        var IVDecryptInput = $('#IVDecrypt');

        IVDecryptInput.on('keyup', function () {
            var length = IVDecryptInput.val().length;
            $('#IVCounterDecrypt').html(length);
        });
    }


    var validateEncryptionForm = function () {

        var correct = true;
        var alerts = [];

        var key = $('#keyEncrypt').val();
        var iv = $('#IVEncrypt').val();
        var keyLength = parseInt($("input[name='keyLengthEncrypt']:checked").val());
        var ivLength = 128;
        var inputTextarea = $('#inputEncrypt').val();


        //0 - CBC 2 - ECB
        var mode = $('#algorithmSelectModeEncrypt option:selected').val();
        
        keyFormat = $("input[name='keyFormatEncrypt']:checked").val();
        ivFormat = $("input[name='IVFormatEncrypt']:checked").val();
        

        switch (keyFormat) {

            case "1"://Hex
                {
                    if (key.length != keyLength/4) {
                        alerts.push('Niepoprawna długość klucza');
                        correct = false;
                    }

                    if (!isHexadecimal(key)) {
                        correct = false;
                        alerts.push('Niepoprawny format heksadecymalny dla klucza');
                    }

                    break;
                }
            case "2"://Binary
                {
                    if (key.length != keyLength) {
                        alerts.push('Niepoprawna długość klucza');
                        correct = false;
                    }

                    if (!isBinaryString(key)) {
                        correct = false;
                        alerts.push('Niepoprawny format binarny dla klucza');
                        break;
                    }
                    break;
                }
        }

        if (mode != "2") {
            switch (ivFormat) {
            case "1":
            {
                if (iv.length != ivLength / 4) {
                    alerts.push('Niepoprawna długość wektora inicjalizacyjnego')
                    correct = false;
                }

                if (!isHexadecimal(iv)) {
                    correct = false;
                    alerts.push('Niepoprawny format heksadecymalny dla wektora');
                }

                break;
            }
            case "2":
            {
                if (iv.length != ivLength) {
                    alerts.push('Niepoprawna długość wektora inicjalizacyjnego')
                    correct = false;
                }

                if (!isBinaryString(iv)) {
                    correct = false;
                    alerts.push('Niepoprawny format binarny dla wektora');
                }
                break;
            }
            }
        }

        var inputMode = $("#inputModeEncrypt option:selected").val();
        if (inputMode === "file") {

            var file = document.getElementById('fileEncrypt').files[0];
            if (!file) {
                alerts.push('Nie wybrano pliku');
            }
        }

        if (inputMode === "text") {
            if (inputTextarea.length == 0) {
                alerts.push('Pole tekstowe jest puste');
                correct = false;
            }
        }
        if (!correct) {
            alert(alerts.join('\n'));
        }
        
        return correct;
    }

    var validateDecryptionForm = function () {

        var correct = true;
        var alerts = [];

        var key = $('#keyDecrypt').val();
        var iv = $('#IVDecrypt').val();
        var keyLength = parseInt($("input[name='keyLengthDecrypt']:checked").val());
        var ivLength = 128;
        var inputTextarea = $('#inputDecrypt').val();


        //0 - CBC 2 - ECB
        var mode = $('#algorithmSelectModeDecrypt option:selected').val();

        keyFormat = $("input[name='keyFormatDecrypt']:checked").val();
        ivFormat = $("input[name='IVFormatDecrypt']:checked").val();


        switch (keyFormat) {

            case "1"://Hex
                {
                    if (key.length != keyLength / 4) {
                        alerts.push('Niepoprawna długość klucza');
                        correct = false;
                    }

                    if (!isHexadecimal(key)) {
                        correct = false;
                        alerts.push('Niepoprawny format heksadecymalny dla klucza');
                    }

                    break;
                }
            case "2"://Binary
                {
                    if (key.length != keyLength) {
                        alerts.push('Niepoprawna długość klucza');
                        correct = false;
                    }

                    if (!isBinaryString(key)) {
                        correct = false;
                        alerts.push('Niepoprawny format binarny dla klucza');
                        break;
                    }
                    break;
                }
        }

        if (mode != "2") {

            switch (ivFormat) {
            case "1":
            {
                if (iv.length != ivLength / 4) {
                    alerts.push('Niepoprawna długość wektora inicjalizacyjnego')
                    correct = false;
                }

                if (!isHexadecimal(iv)) {
                    correct = false;
                    alerts.push('Niepoprawny format heksadecymalny dla wektora');
                }

                break;
            }
            case "2":
            {

                if (iv.length != ivLength) {
                    alerts.push('Niepoprawna długość wektora inicjalizacyjnego')
                    correct = false;
                }

                if (!isBinaryString(iv)) {
                    correct = false;
                    alerts.push('Niepoprawny format binarny dla wektora');
                }
                break;
            }
            }
        }
        

        if (inputTextarea.length == 0) {
            alerts.push('Pole tekstowe jest puste');
            correct = false;
        }


        if (!correct) {
            alert(alerts.join('\n'));
        }

        return correct;
    }

    var DisableIVAtECB = function() {
        var selectEncrypt = $('#algorithmSelectModeEncrypt');

        ivTextEncrypt = $('#IVEncrypt');

        btnRandomEncrypt = $('#btnRandomIVEncrypt');

        selectEncrypt.on('change',
            function () {
                var mode = $('#algorithmSelectModeEncrypt option:selected').val();

                if (mode === "2") {
                    ivTextEncrypt.attr('readonly', true);
                    btnRandomEncrypt.attr('disabled', true);
                } else {
                    ivTextEncrypt.attr('readonly', false);
                    btnRandomEncrypt.attr('disabled', false);
                }

            });

        var selectDecrypt = $('#algorithmSelectModeDecrypt');

        ivTextDecrypt = $('#IVDecrypt');
        btnRandomDecrypt = $('#btnRandomIVDecrypt');

        selectDecrypt.on('change',
            function () {
                var mode = $('#algorithmSelectModeDecrypt option:selected').val();

                if (mode === "2") {
                    ivTextDecrypt.attr('readonly', true);
                    btnRandomDecrypt.attr('disabled', true);
                } else {
                    ivTextDecrypt.attr('readonly', false);
                    btnRandomDecrypt.attr('disabled', false);
                }

            });
    }


    var init = function() {
        encryptInit();
        decryptInit();
        fileLoadInit();
        txtFileChangeInit();
        buttonsEncryptSideInit();
        buttonsDecryptSideInit();
        DisableIVAtECB();

        textChangeInit();
    }

    return {
        init: init
    }
}