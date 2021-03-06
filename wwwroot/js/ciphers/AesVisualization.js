﻿var AesVisualizationInit = function() {


    var sBox = new Array(16);

    sBox[0] = [0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76];
    sBox[1] = [0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0];
    sBox[2] = [0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15];
    sBox[3] = [0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75];
    sBox[4] = [0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84];
    sBox[5] = [0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf];
    sBox[6] = [0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8];
    sBox[7] = [0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2];
    sBox[8] = [0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73];
    sBox[9] = [0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb];
    sBox[10] = [0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79];
    sBox[11] = [0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08];
    sBox[12] = [0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a];
    sBox[13] = [0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e];
    sBox[14] = [0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf];
    sBox[15] = [0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16];

    var mixColumnsBox = new Array(4);
    mixColumnsBox[0] = [0x02, 0x03, 0x01, 0x01];
    mixColumnsBox[1] = [0x01, 0x02, 0x03, 0x01];
    mixColumnsBox[2] = [0x01, 0x01, 0x02, 0x03];
    mixColumnsBox[3] = [0x03, 0x01, 0x01, 0x02];



    var numrows = 4;
    var numcols = 4;

    var matrix = new Array(numrows);
    for (var i = 0; i < numrows; i++) {
        matrix[i] = new Array(numcols);
        for (var j = 0; j < numcols; j++) {
            matrix[i][j] = Math.floor(Math.random() * 256);
        }
    }

    var mainKey = new Array(numrows);
    for (var i = 0; i < numrows; i++) {
        mainKey[i] = new Array(numcols);
        for (var j = 0; j < numcols; j++) {
            mainKey[i][j] = Math.floor(Math.random() * 256);
        }
    }



    var newKeyDictionary = KeySchedule(mainKey, 1);

    var roundZeroResult = XorOnBlocks(matrix, mainKey);

    var subBytesMatrix = SubBytes(matrix, sBox);
    var shiftRowsMatrix = ShiftRows(subBytesMatrix);
    var mixColumnsMatrix = MixColumns(shiftRowsMatrix);

    VisualizeDataBlock(matrix, 'roundZeroData', 'cell-rect-data-nohover');
    VisualizeDataBlock(mainKey, 'roundZeroKey', 'cell-rect-key');
    VisualizeDataBlock(roundZeroResult, 'roundZeroResult', 'cell-rect-result');

    VisualizeDataBlock(mainKey, 'oldKey', 'cell-rect-key');
    VisualizeDataBlock(newKeyDictionary.result, 'newKey', 'cell-rect-result');



    VisualizeDataBlock(matrix, 'dataBlock', 'cell-rect-data');
    VisualizeSBox(sBox);
    VisualizeDataBlock(subBytesMatrix, 'subBytesBlock', 'cell-rect-result');

    VisualizeDataBlock(subBytesMatrix, 'shiftBlockBefore', 'cell-rect-data-nohover');
    VisualizeDataBlock(shiftRowsMatrix, 'shiftBlockAfter', 'cell-rect-result');

    VisualizeDataBlock(shiftRowsMatrix, 'mixColumnsBlockBefore', 'cell-rect-data-nohover');
    VisualizeDataBlock(mixColumnsBox, 'mixColumnsHelperMatrix', 'cell-rect-helpers');
    VisualizeDataBlock(mixColumnsMatrix, 'mixColumnsBlockAfter', 'cell-rect-result');

    VisualizeDataBlock(mixColumnsBox, 'roundKeyData', 'cell-rect-data-nohover');
    VisualizeDataBlock(newKeyDictionary.result, 'roundKeyKey', 'cell-rect-key');
    VisualizeDataBlock(mixColumnsMatrix, 'roundKeyResult', 'cell-rect-result');

    SetMouseEventOnDataBlock();
    SetMouseEventOnShiftBlock();
    SetMouseEventOnMixColumns();
    SetMouseEventOnKeySchedule();
    SetMouseEventOnRoundZero();
    SetMouseEventOnKeyViz();
    SetMouseEventOnKeyVizFirstColumn();


    function IntToHexString(d) {
        var result = d.toString(16).toUpperCase();
        return result.length < 2 ? "0" + result : result;
    }


    function VisualizeSBox(matrix) {

        var numrows = 16;
        var numcols = 16;

        var margin = { top: 100, right: 100, bottom: 100, left: 100 },
            width = 400,
            height = 400;

        var textOffsetX = (width / numcols) / 2;
        var textOffsetY = (height / numrows) / 2;

        var svg = d3.select("#sboxBlock")
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

        svg.append("g")
            .attr("class", "description")
            .append("text")
            .attr("class", "description-text")
            .attr("x", 200)
            .attr("y", 450)
            .text("Tabela podstawień (SBOX).");

        var x = d3.scale.ordinal()
            .domain(d3.range(numcols))
            .rangeBands([0, width]);

        var y = d3.scale.ordinal()
            .domain(d3.range(numrows))
            .rangeBands([0, height]);


        var row = svg.selectAll(".row")
            .data(matrix)
            .enter().append("g")
            .attr("class", "row")
            .attr("id", function (d, i) {
                return "row" + i;
            })
            .attr("transform", function (d, i) { return "translate(0," + y(i) + ")"; });

        row.selectAll(".cell")
            .data(function (d) { return d; })
            .enter().append("g")
            .attr("id", function (d, i) { return "cell" + i })
            .attr("class", "cell")
            .append("rect")
            .attr("class", "cell-rect-sbox")
            .attr("x", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", y.rangeBand());

        row.selectAll(".cell").append("text")
            .attr("class", "cell-text-sbox")
            .attr("x", function (d, i) { return x(i) + textOffsetX; })
            .attr("y", textOffsetY)
            .text(function (d) {
                return IntToHexString(d);
            });


        //index row

        var indexRowArray = new Array(16);
        for (var i = 0; i < 16; i++) {
            indexRowArray[i] = i;
        }

        var indexRow = d3.select("#sboxBlock")
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top / 2 + ")");

        indexRow.selectAll(".index-row")
            .data(indexRowArray)
            .enter().append("g")
            .attr("class", "index-cell-row")
            .append("rect")
            .attr("class", "cell-rect-index")
            .attr("x", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", y.rangeBand());


        indexRow.selectAll("g")
            .append("text")
            .attr("class", "cell-text-sbox")
            .attr("x", function (d, i) { return x(i) + textOffsetX; })
            .attr("y", textOffsetY)
            .text(function (d) {
                return IntToHexString(d);
            });

        //index column

        var indexColumn = d3.select("#sboxBlock")
            .append("g")
            .attr("transform", "translate(" + margin.left / 2 + "," + margin.top + ")");

        indexColumn.selectAll(".index-column")
            .data(indexRowArray)
            .enter().append("g")
            .attr("class", "index-cell-column")
            .append("rect")
            .attr("class", "cell-rect-index")
            .attr("y", function (d, i) { return y(i); })
            .attr("width", x.rangeBand())
            .attr("height", y.rangeBand());

        indexColumn.selectAll("g")
            .append("text")
            .attr("class", "cell-text-sbox")
            .attr("y", function (d, i) { return x(i) + textOffsetX; })
            .attr("x", textOffsetY)
            .text(function (d) {
                return IntToHexString(d);
            });
    }

    function VisualizeDataBlock(matrix, svgId, cellRectClass) {

        var numrows = matrix.length;
        var numcols = matrix[0].length;

        var margin = { top: 0, right: 0, bottom: 0, left: 0 },
            width = 250,
            height = 250;

        var textOffsetX = (width / numcols) / 2;
        var textOffsetY = (height / numrows) / 2;

        var svg = d3.select("#" + svgId)
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .style("margin-left", -margin.left + "px")
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

        var x = d3.scale.ordinal()
            .domain(d3.range(numcols))
            .rangeBands([0, width]);

        var y = d3.scale.ordinal()
            .domain(d3.range(numrows))
            .rangeBands([0, height]);

        var row = svg.selectAll(".row")
            .data(matrix)
            .enter().append("g")
            .attr("class", "row")
            .attr("transform", function (d, i) { return "translate(0," + y(i) + ")"; });

        row.selectAll(".cell")
            .data(function (d) { return d; })
            .enter().append("g")
            .attr("class", "cell")
            .append("rect")
            .attr("class", cellRectClass)
            .attr("x", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", y.rangeBand());


        row.selectAll(".cell").append("text")
            .attr("class", "cell-text-data")
            .attr("x", function (d, i) { return x(i) + textOffsetX; })
            .attr("y", textOffsetY)
            .text(function (d) {
                return IntToHexString(d);
            });
    }

    function VisualizeKeyFirstColumn(key1, resultDictionary, svgId) {

        var oldKeyLastColumn = new Array(4);
        for (var i = 0; i < 4; i++) {
            oldKeyLastColumn[i] = key1[i][3];
        }


        svg = d3.select('#' + svgId);
        var group1 = svg
            .append("g")
            .attr("transform", "translate(100,5)")
            .attr("class", "g1");

        var group2 = svg
            .append("g")
            .attr("transform", "translate(200,5)")
            .attr("class", "g2");

        var group3 = svg
            .append("g")
            .attr("transform", "translate(300,5)")
            .attr("class", "g3");

        var group4 = svg
            .append("g")
            .attr("transform", "translate(450,5)")
            .attr("class", "g4");
        var group5 = svg
            .append("g")
            .attr("transform", "translate(550,5)")
            .attr("class", "g5");
        var group6 = svg
            .append("g")
            .attr("transform", "translate(650,5)")
            .attr("class", "g6");
        var group7 = svg
            .append("g")
            .attr("transform", "translate(800,5)")
            .attr("class", "g7");


        group1.selectAll(".cell")
            .data(oldKeyLastColumn).enter()
            .append("g")
            .attr("class", "cell");

        group2.selectAll(".cell")
            .data(resultDictionary.rotation).enter()
            .append("g")
            .attr("class", "cell");

        group3.selectAll(".cell")
            .data(resultDictionary.subbytes).enter()
            .append("g")
            .attr("class", "cell");

        group4.selectAll(".cell")
            .data(resultDictionary.firstColumn).enter()
            .append("g")
            .attr("class", "cell");
        group5.selectAll(".cell")
            .data(resultDictionary.subbytes).enter()
            .append("g")
            .attr("class", "cell");

        group6.selectAll(".cell")
            .data(resultDictionary.rcon).enter()
            .append("g")
            .attr("class", "cell");
        group7.selectAll(".cell")
            .data(resultDictionary.firstNewColumn).enter()
            .append("g")
            .attr("class", "cell");

        var textGroup = svg.append("g")
            .attr("class", "description-text")
            .attr("transform", "translate(0,350)");

        textGroup.append("text")
            .attr("class", "description-text-simple")
            .attr("x", 500)
            .attr("y", 0)
            .text("Przesunięcie (1,2), podstawienie zgodnie z tabelą podstawień (3),");
        textGroup.append("text")
            .attr("class", "description-text-simple")
            .attr("x", 500)
            .attr("y", 50)
            .text("XOR na blokach 4,5,6. Wartość bloku 6 zależy od numeru rundy i wyraża się wzorem:");
        group1.append("text")
            .attr("class", "cell-text-data")
            .attr("x", 30)
            .attr("y", 300)
            .text("1");

        group2.append("text")
            .attr("class", "cell-text-data")
            .attr("x", 30)
            .attr("y", 300)
            .text("2");

        group3.append("text")
            .attr("class", "cell-text-data")
            .attr("x", 30)
            .attr("y", 300)
            .text("3");

        group4.append("text")
            .attr("class", "cell-text-data")
            .attr("x", 30)
            .attr("y", 300)
            .text("4");
        group5.append("text")
            .attr("class", "cell-text-data")
            .attr("x", 30)
            .attr("y", 300)
            .text("5");
        group6.append("text")
            .attr("class", "cell-text-data")
            .attr("x", 30)
            .attr("y", 300)
            .text("6");
        group7.append("text")
            .attr("class", "cell-text-data")
            .attr("x", 30)
            .attr("y", 300)
            .text("7");


        var numrows = oldKeyLastColumn.length;

        var margin = { top: 0, right: 0, bottom: 0, left: 0 },
            width = 250,
            height = 250;


        var x = d3.scale.ordinal()
            .domain(d3.range(numrows))
            .rangeBands([0, width]);

        var y = d3.scale.ordinal()
            .domain(d3.range(numrows))
            .rangeBands([0, height]);

        var textOffsetX = x(1) / 2;
        var textOffsetY = y(1) / 2;


        group1.selectAll(".cell")
            .append("rect")
            .attr("class", "cell-rect-selected")
            .attr("x", function (d, i) { return x(0); })
            .attr("y", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", x.rangeBand())
            .filter(function (d, i) {
                return i !== 0;
            }).attr("class", "cell-rect-data-2");


        group1.selectAll(".cell").append("text")
            .attr("class", "cell-text-data")
            .attr("x", function (d, i) { return x(0) + textOffsetX; })
            .attr("y", function (d, i) { return y(i) + textOffsetY; })
            .text(function (d) {
                return IntToHexString(d);
            });

        group2.selectAll(".cell")
            .append("rect")
            .attr("class", "cell-rect-selected")
            .attr("x", function (d, i) { return x(0); })
            .attr("y", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", x.rangeBand())
            .filter(function (d, i) {
                return i !== 3;
            }).attr("class", "cell-rect-data-2");;

        group2.selectAll(".cell").append("text")
            .attr("class", "cell-text-data")
            .attr("x", function (d, i) { return x(0) + textOffsetX; })
            .attr("y", function (d, i) { return y(i) + textOffsetY; })
            .text(function (d) {
                return IntToHexString(d);
            });

        group3.selectAll(".cell")
            .append("rect")
            .attr("class", "cell-rect-selected-2")
            .attr("x", function (d, i) { return x(0); })
            .attr("y", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", x.rangeBand());

        group3.selectAll(".cell").append("text")
            .attr("class", "cell-text-data")
            .attr("x", function (d, i) { return x(0) + textOffsetX; })
            .attr("y", function (d, i) { return y(i) + textOffsetY; })
            .text(function (d) {
                return IntToHexString(d);
            });

        group4.selectAll(".cell")
            .append("rect")
            .attr("class", "cell-rect-data-nohover")
            .attr("x", function (d, i) { return x(0); })
            .attr("y", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", x.rangeBand());

        group4.selectAll(".cell").append("text")
            .attr("class", "cell-text-data")
            .attr("x", function (d, i) { return x(0) + textOffsetX; })
            .attr("y", function (d, i) { return y(i) + textOffsetY; })
            .text(function (d) {
                return IntToHexString(d);
            });

        group5.selectAll(".cell")
            .append("rect")
            .attr("class", "cell-rect-data-nohover")
            .attr("x", function (d, i) { return x(0); })
            .attr("y", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", x.rangeBand());

        group5.selectAll(".cell").append("text")
            .attr("class", "cell-text-data")
            .attr("x", function (d, i) { return x(0) + textOffsetX; })
            .attr("y", function (d, i) { return y(i) + textOffsetY; })
            .text(function (d) {
                return IntToHexString(d);
            });

        group6.selectAll(".cell")
            .append("rect")
            .attr("class", "cell-rect-helpers")
            .attr("x", function (d, i) { return x(0); })
            .attr("y", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", x.rangeBand());

        group6.selectAll(".cell").append("text")
            .attr("class", "cell-text-data")
            .attr("x", function (d, i) { return x(0) + textOffsetX; })
            .attr("y", function (d, i) { return y(i) + textOffsetY; })
            .text(function (d) {
                return IntToHexString(d);
            });

        group7.selectAll(".cell")
            .append("rect")
            .attr("class", "cell-rect-result")
            .attr("x", function (d, i) { return x(0); })
            .attr("y", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", x.rangeBand());

        group7.selectAll(".cell").append("text")
            .attr("class", "cell-text-data")
            .attr("x", function (d, i) { return x(0) + textOffsetX; })
            .attr("y", function (d, i) { return y(i) + textOffsetY; })
            .text(function (d) {
                return IntToHexString(d);
            });

    }

    function VisualizeKeysXor(key1, key2, key3, svgId) {

        svg = d3.select('#' + svgId);
        var group1 = svg
            .append("g")
            .attr("transform", "translate(200,100)")
            .attr("class", "g1");

        var cells = group1.selectAll(".cell")
            .data(key1).enter()
            .append("g")
            .attr("class", "cell");

        var width = 75;
        var height = 75;
        var offsetX = width / 2;
        var offsetY = height / 2;


        var xor = svg.append("g")
            .attr("class", "textGroup")
            .attr("transform", "translate(300,100)")
            .append("text")
            .attr("class", "cell-text-data")
            .attr("x", width / 2)
            .attr("y", height * 2)
            .text("XOR");

        var arrow = svg.append("g")
            .attr("class", "textGroup")
            .attr("transform", "translate(500,100)")
            .append("text")
            .attr("class", "cell-text-data")
            .attr("x", width / 2)
            .attr("y", height * 2)
            .text("==>");

        group1.selectAll(".cell")
            .append("rect")
            .attr("class", "cell-rect-selected-2")
            .attr("width", width)
            .attr("height", height)
            .attr("x", 0)
            .attr("y", function (d, i) { return i * height; });

        group1.selectAll(".cell")
            .append("text")
            .attr("class", "cell-text-data")
            .attr("x", offsetX)
            .attr("y", function (d, i) { return i * height + offsetY; })
            .text(function (d, i) { return IntToHexString(d); });

        var group2 = svg.append("g")
            .attr("class", "g2")
            .attr("transform", "translate(400,100)");

        var cells = group2.selectAll(".cell")
            .data(key2).enter()
            .append("g")
            .attr("class", "cell");

        group2.selectAll(".cell")
            .append("rect")
            .attr("class", "cell-rect-selected-2")
            .attr("width", width)
            .attr("height", height)
            .attr("x", 0)
            .attr("y", function (d, i) { return i * height; });

        group2.selectAll(".cell")
            .append("text")
            .attr("class", "cell-text-data")
            .attr("x", offsetX)
            .attr("y", function (d, i) { return i * height + offsetY; })
            .text(function (d, i) { return IntToHexString(d); });


        var group3 = svg.append("g")
            .attr("class", "g3")
            .attr("transform", "translate(600,100)");

        var cells = group3.selectAll(".cell")
            .data(key3).enter()
            .append("g")
            .attr("class", "cell");

        group3.selectAll(".cell")
            .append("rect")
            .attr("class", "cell-rect-selected")
            .attr("width", width)
            .attr("height", height)
            .attr("x", 0)
            .attr("y", function (d, i) { return i * height; });

        group3.selectAll(".cell")
            .append("text")
            .attr("class", "cell-text-data")
            .attr("x", offsetX)
            .attr("y", function (d, i) { return i * height + offsetY; })
            .text(function (d, i) { return IntToHexString(d); });


    }

    function VisualizeDataArray(array, svgId, cellRectClass) {
        var numrows = array.length;

        var margin = { top: 0, right: 0, bottom: 0, left: 0 },
            width = 250,
            height = 250;


        var svg = d3.select("#" + svgId)
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .style("margin-left", -margin.left + "px")
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

        var x = d3.scale.ordinal()
            .domain(d3.range(numrows))
            .rangeBands([0, width]);

        var y = d3.scale.ordinal()
            .domain(d3.range(numrows))
            .rangeBands([0, height]);

        var textOffsetX = x(1) / 2;
        var textOffsetY = y(1) / 2;


        var cell = svg.selectAll(".cell")
            .data(array)
            .enter().append("g")
            .attr("class", "cell")
            .append("rect")
            .attr("class", cellRectClass)
            .attr("x", function (d, i) { return x(0); })
            .attr("y", function (d, i) { return x(i); })
            .attr("width", x.rangeBand())
            .attr("height", x.rangeBand());

        svg.selectAll(".cell").append("text")
            .attr("class", "cell-text-sbox")
            .attr("x", function (d, i) { return x(0) + textOffsetX; })
            .attr("y", function (d, i) { return y(i) + textOffsetY; })
            .text(function (d) {
                return d;
            });
    }

    function handleMouseOverOnDataBlock(d, i) {

        var row = d >> 4;
        var column = d & 15;

        d3.select("#row" + row).select("#cell" + column)
            .select("rect")
            .attr("class", "cell-rect-selected");

        d3.selectAll(".index-cell-column")
            .filter(function (d, i) {
                return i === row;
            })
            .select("rect")
            .attr("class", "cell-rect-selected-2");

        d3.selectAll(".index-cell-row")
            .filter(function (d, i) {
                return i === column;
            })
            .select("rect")
            .attr("class", "cell-rect-selected-2");

        d3.select("#subBytesBlock").selectAll(".cell")
            .filter(function (d, index) {
                return index === i;
            }).select("rect")
            .attr("class", "cell-rect-selected");

    }

    function handleMouseOutOnDataBlock(d, i) {

        var row = d >> 4;
        var column = d & 15;

        d3.select("#row" + row).select("#cell" + column)
            .select("rect")
            .attr("class", "cell-rect-sbox");

        d3.selectAll(".index-cell-column")
            .filter(function (d, i) {
                return i === row;
            })
            .select("rect")
            .attr("class", "cell-rect-index");

        d3.selectAll(".index-cell-row")
            .filter(function (d, i) {
                return i === column;
            })
            .select("rect")
            .attr("class", "cell-rect-index");

        d3.select("#subBytesBlock").selectAll(".cell")
            .filter(function (d, index) {
                return index === i;
            }).select("rect")
            .attr("class", "cell-rect-result");
    }

    function SetMouseEventOnDataBlock(d, i) {

        d3.select('#dataBlock')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnDataBlock)
            .on('mouseout', handleMouseOutOnDataBlock);
    }

    function handleMouseOverOnShiftBlock(d, i) {

        if (i < 4) {
            d3.select('#shiftBlockBefore')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index < 4;
                })
                .select('rect')
                .attr("class", "cell-rect-selected-2");

            d3.select('#shiftBlockAfter')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index < 4;
                })
                .select('rect')
                .attr("class", "cell-rect-selected-2");
        }
        else if (i >= 4 && i < 8) {

            var cellsBefore = d3.select('#shiftBlockBefore')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 4 && index < 8;
                });


            cellsBefore.select('rect')
                .attr("class", function (d, index) {
                    //local index, so value starts from 0
                    if (index == 0) {
                        return "cell-rect-selected";
                    } else {
                        return "cell-rect-selected-2";
                    }
                });

            var cellsAfter = d3.select('#shiftBlockAfter')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 4 && index < 8;
                });

            cellsAfter.select('rect')
                .attr("class", function (d, index) {
                    //local index, so value starts from 0
                    if (index == 3) {
                        return "cell-rect-selected";
                    } else {
                        return "cell-rect-selected-2";
                    }
                });
        } else if (i >= 8 && i < 12) {
            var cellsBefore = d3.select('#shiftBlockBefore')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 8 && index < 12;
                });


            cellsBefore.select('rect')
                .attr("class", function (d, index) {
                    //local index, so value starts from 0
                    if (index == 0 || index == 1) {
                        return "cell-rect-selected";
                    } else {
                        return "cell-rect-selected-2";
                    }
                });

            var cellsAfter = d3.select('#shiftBlockAfter')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 8 && index < 12;
                });

            cellsAfter.select('rect')
                .attr("class", function (d, index) {
                    //local index, so value starts from 0
                    if (index == 3 || index == 2) {
                        return "cell-rect-selected";
                    } else {
                        return "cell-rect-selected-2";
                    }
                });
        } else if (i >= 12 && i < 16) {
            var cellsBefore = d3.select('#shiftBlockBefore')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 12 && index < 16;
                });


            cellsBefore.select('rect')
                .attr("class", function (d, index) {
                    //local index, so value starts from 0
                    if (index == 0 || index == 1 || index == 2) {
                        return "cell-rect-selected";
                    } else {
                        return "cell-rect-selected-2";
                    }
                });

            var cellsAfter = d3.select('#shiftBlockAfter')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 12 && index < 16;
                });

            cellsAfter.select('rect')
                .attr("class", function (d, index) {
                    //local index, so value starts from 0
                    if (index == 3 || index == 2 || index === 1) {
                        return "cell-rect-selected";
                    } else {
                        return "cell-rect-selected-2";
                    }
                });
        }
    }

    function handleMouseOutOnShiftBlock(d, i) {

        if (i < 4) {
            d3.select('#shiftBlockBefore')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index < 4;
                })
                .select('rect')
                .attr("class", "cell-rect-data-nohover");

            d3.select('#shiftBlockAfter')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index < 4;
                })
                .select('rect')
                .attr("class", "cell-rect-result");
        }
        else if (i >= 4 && i < 8) {

            var cellsBefore = d3.select('#shiftBlockBefore')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 4 && index < 8;
                });

            cellsBefore.select('rect')
                .attr("class", "cell-rect-data-nohover");

            var cellsAfter = d3.select('#shiftBlockAfter')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 4 && index < 8;
                });

            cellsAfter.select('rect')
                .attr("class", "cell-rect-result");
        }
        else if (i >= 8 && i < 12) {
            var cellsBefore = d3.select('#shiftBlockBefore')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 8 && index < 12;
                });

            cellsBefore.select('rect')
                .attr("class", "cell-rect-data-nohover");

            var cellsAfter = d3.select('#shiftBlockAfter')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 8 && index < 12;
                });

            cellsAfter.select('rect')
                .attr("class", "cell-rect-result");
        }
        else if (i >= 12 && i < 16) {
            var cellsBefore = d3.select('#shiftBlockBefore')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 12 && index < 16;
                });

            cellsBefore.select('rect')
                .attr("class", "cell-rect-data-nohover");

            var cellsAfter = d3.select('#shiftBlockAfter')
                .selectAll('.cell')
                .filter(function (d, index) {
                    return index >= 12 && index < 16;
                });
            cellsAfter.select('rect')
                .attr("class", "cell-rect-result");
        }
    }

    function SetMouseEventOnShiftBlock() {
        d3.select('#shiftBlockBefore')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnShiftBlock)
            .on('mouseout', handleMouseOutOnShiftBlock);
    }


    function handleMouseOverOnMixColumns(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-selected");

        var valuesFromCellsBefore = [];
        var valuesFromHelperMatrix = [];

        var cellsBefore = d3.select('#mixColumnsBlockBefore')
            .selectAll(".cell")
            .filter(function (d, index) {
                var imod = i % 4;
                return (index % 4) == imod;
            })
            .each(function (d, i) {
                valuesFromCellsBefore.push(d);
            });

        cellsBefore.select("rect")
            .attr("class", function (d, index) {
                switch (index) {
                    case 0: { return "maroon-cell"; }
                    case 1: { return "red-cell"; }
                    case 2: { return "orange-cell"; }
                    case 3: { return "yellow-cell"; }

                }
            });

        var helperMatrix = d3.select('#mixColumnsHelperMatrix')
            .selectAll(".cell")
            .filter(function (d, index) {

                var row = Math.floor(i / 4);
                return index >= row * 4 && index < row * 4 + 4;
            })
            .each(function (d, i) {
                valuesFromHelperMatrix.push(IntToHexString(d));
            });

        helperMatrix.select("rect")
            .attr("class", function (d, index) {
                switch (index) {
                    case 0: { return "maroon-cell"; }
                    case 1: { return "red-cell"; }
                    case 2: { return "orange-cell"; }
                    case 3: { return "yellow-cell"; }
                }
            });


        var formulaSvg = d3.select("#mixColumnsFormula")
            .append("g")
            .attr("transform", "translate(0,50)");

        var resultFormula = [];
        var products = [];

        var mul = '';


        for (var i = 0; i < valuesFromCellsBefore.length; i++) {
            switch (valuesFromHelperMatrix[i]) {
                case '01':
                    {
                        var value = valuesFromCellsBefore[i];
                        var hexValue = value.toString(16).toUpperCase();
                        if (hexValue.length == 1) {
                            hexValue = '0' + hexValue;
                        }
                        mul = valuesFromHelperMatrix[i] + ' x ' + hexValue;
                        resultFormula.push(mul + ' ----> ' + hexValue);

                        products.push(value);
                        break;
                    }
                case '02':
                    {
                        var value = valuesFromCellsBefore[i];
                        var valueHex = value.toString(16).toUpperCase();
                        var msb = valuesFromCellsBefore[i] & 128;

                        if (msb == 128) {
                            mul = valuesFromHelperMatrix[i] + ' x ' + valueHex;
                            var mulStart = '(' + valueHex + ' << 1) = ';
                            var mulAfterShift = value << 1;
                            var mulAfterShiftHex = mulAfterShift.toString(16).toUpperCase();
                            var mulAfterShiftHex8Bits = mulAfterShiftHex.substring(1);


                            var valueInGF256 = mulAfterShift & 255;
                            var xor = 0x1b;
                            var xored = valueInGF256 ^ xor;
                            var xoredHex = xored.toString(16).toUpperCase();
                            if (xoredHex.length == 1) {
                                xoredHex = '0' + xoredHex;
                            }

                            resultFormula.push(mul + ' ----> ' + mulStart +
                                mulAfterShiftHex + ' ----> ' + mulAfterShiftHex8Bits + ' xor 1B = ' + xoredHex);

                            products.push(xored);

                        } else {
                            mul = valuesFromHelperMatrix[i] + ' x ' + valueHex;
                            var mulStart = '(' + valueHex + ' << 1) = ';
                            var mulAfterShift = value << 1;
                            var mulAfterShiftHex = mulAfterShift.toString(16).toUpperCase();
                            if (mulAfterShiftHex.length == 1) {
                                mulAfterShiftHex = '0' + mulAfterShiftHex;
                            }

                            resultFormula.push(mul + ' ----> ' + mulStart+
                                mulAfterShiftHex);

                            products.push(mulAfterShift);
                        }

                        
                        break;
                    }
                case '03':
                    {
                        var value = valuesFromCellsBefore[i];
                        var valueHex = value.toString(16).toUpperCase();
                        if (valueHex.length == 1) {
                            valueHex = '0' + valueHex;
                        }
                        var msb = valuesFromCellsBefore[i] & 128;

                        if (msb == 128) {
                            mul = valuesFromHelperMatrix[i] + ' x ' + valueHex;
                            var mulStart = '(' + valueHex + ' << 1) = ';
                            var mulAfterShift = value << 1;
                            var mulAfterShiftHex = mulAfterShift.toString(16).toUpperCase();
                            var mulAfterShiftHex8Bits = mulAfterShiftHex.substring(1);


                            var valueInGF256 = mulAfterShift & 255;
                            var xor = 0x1b;
                            var xored = valueInGF256 ^ xor;
                            var xoredHex = xored.toString(16).toUpperCase();
                            if (xoredHex.length == 1) {
                                xoredHex = '0' + xoredHex;
                            }
                            var secondxored = xored ^ value;
                            var secondxoredHex = secondxored.toString(16).toUpperCase();
                            if (secondxoredHex.length == 1) {
                                secondxoredHex = '0' + secondxoredHex;
                            }

                            resultFormula.push(mul + ' ----> ' + mulStart +
                                mulAfterShiftHex + ' ----> ' + mulAfterShiftHex8Bits + ' xor 1B = ' + xoredHex + ' ----> '
                                + xoredHex + ' xor ' + valueHex + ' = ' + secondxoredHex);

                            products.push(secondxored);

                        } else {
                            mul = valuesFromHelperMatrix[i] + ' x ' + valueHex;
                            var mulStart = '(' + valueHex + ' << 1) = ';
                            var mulAfterShift = value << 1;
                            var mulAfterShiftHex = mulAfterShift.toString(16).toUpperCase();
                            if (mulAfterShiftHex.length == 1) {
                                mulAfterShiftHex = '0' + mulAfterShiftHex;
                            }

                            var secondxored = mulAfterShift ^ value;
                            var secondxoredHex = secondxored.toString(16).toUpperCase();
                            if (secondxoredHex.length == 1) {
                                secondxoredHex = '0' + secondxoredHex;
                            }

                            resultFormula.push(mul + ' ----> ' + mulStart +
                                mulAfterShiftHex + ' ----> '
                                + mulAfterShiftHex + ' xor ' + valueHex + ' = ' + secondxoredHex);

                            products.push(secondxored);
                        }
                        break;
                    }
            }
        }

        var productsHex = '';
        var productsXOR = 0;
        for (var i = 0; i < products.length - 1; i++) {
            productsXOR ^= products[i];
            var hex = products[i].toString(16).toUpperCase();
            if (hex.length == 1) {
                hex = '0' + hex;
            }
            productsHex += hex+ ' xor '; 
        }

        var lastHex = products[products.length - 1].toString(16).toUpperCase();
        if (lastHex.length == 1) {
            lastHex = '0' + lastHex;
        }

        productsXOR ^= products[products.length - 1];
        var productsXORHex = productsXOR.toString(16).toUpperCase();
        if (productsXORHex.length == 1) {
            productsXORHex = '0' + productsXORHex;
        }

        productsHex += lastHex.toString(16).toUpperCase()
            + ' = ' + productsXORHex;
        

            formulaSvg.append("text")
                .attr("class", "description-text-simple")
                .attr("x", 0)
                .attr("y", 0)
            .text(resultFormula[0]);

        formulaSvg.append("text")
            .attr("class", "description-text-simple")
            .attr("x", 0)
            .attr("y", 25)
            .text(resultFormula[1]);
        formulaSvg.append("text")
            .attr("class", "description-text-simple")
            .attr("x", 0)
            .attr("y", 50)
            .text(resultFormula[2]);
        formulaSvg.append("text")
            .attr("class", "description-text-simple")
            .attr("x", 0)
            .attr("y", 75)
            .text(resultFormula[3]);

        formulaSvg.append("text")
            .attr("class", "description-text-simple-big")
            .attr("x", 0)
            .attr("y", 125)
            .text(productsHex);

    }
    function handleMouseOutOnMixColumns(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-result");

        var cellsBefore = d3.select('#mixColumnsBlockBefore')
            .selectAll(".cell")
            .filter(function (d, index) {
                var imod = i % 4;
                return index % 4 == imod;
            });

        cellsBefore.select("rect")
            .attr("class", "cell-rect-data-nohover");

        var helperMatrix = d3.select('#mixColumnsHelperMatrix')
            .selectAll(".cell")
            .filter(function (d, index) {

                var row = Math.floor(i / 4);
                return index >= row * 4 && index < row * 4 + 4;
            });

        helperMatrix.select("rect")
            .attr("class", "cell-rect-helpers");

        document.getElementById("mixColumnsFormula").innerHTML = '';

    }
    function SetMouseEventOnMixColumns() {
        d3.select('#mixColumnsBlockAfter')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnMixColumns)
            .on('mouseout', handleMouseOutOnMixColumns);
    }

    function handleMouseOverOnKeySchedule(d, i) {
        d3.select(this)
            .select("rect")
            .attr("class", "cell-rect-selected");

        var keyBlock = d3.select('#roundKeyKey')
            .selectAll('.cell')
            .filter(function (d, index) {
                return index == i;
            });

        var keyData = '';
        keyBlock.select("rect")
            .attr("class", "cell-rect-selected")
            .each(function (d, i) {
                keyData = d;
            });

        var resultBlock = d3.select('#roundKeyResult')
            .selectAll('.cell')
            .filter(function (d, index) {
                return index == i;
            });

        var resultData = '';
        resultBlock.select("rect")
            .attr("class", "cell-rect-selected")
            .each(function (d, i) {
                resultData = d;
            });

        var bin = dec2bin(d);
        bin = "00000000".substr(bin.length) + bin;

        var bin2 = dec2bin(keyData);
        bin2 = "00000000".substr(bin2.length) + bin2;

        var bin3 = dec2bin(resultData);
        bin3 = "00000000".substr(bin3.length) + bin3;

        var div = document.getElementById('XOR-viz');
        div.innerHTML = bin + '<br>' + bin2 + '<br>' + '___________' + '<br>' + bin3;
    }

    function handleMouseOutOnKeySchedule(d, i) {
        d3.select(this)
            .select("rect")
            .attr("class", "cell-rect-data-nohover");

        var keyBlock = d3.select('#roundKeyKey')
            .selectAll('.cell')
            .filter(function (d, index) {
                return index == i;
            });

        keyBlock.select("rect")
            .attr("class", "cell-rect-key");

        var result = d3.select('#roundKeyResult')
            .selectAll('.cell')
            .filter(function (d, index) {
                return index == i;
            });

        result.select("rect")
            .attr("class", "cell-rect-result");

        document.getElementById('XOR-viz').innerHTML =
            '--------' +
            '<br>--------' +
            '<br>___________' +
            '<br>--------';
    }

    function SetMouseEventOnKeySchedule() {
        d3.select('#roundKeyData')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnKeySchedule)
            .on('mouseout', handleMouseOutOnKeySchedule);
    }

    function handleMouseOverOnRoundZero(d, i) {

        d3.select(this)
            .select("rect")
            .attr("class", "cell-rect-selected");

        var keyBlock = d3.select('#roundZeroKey')
            .selectAll('.cell')
            .filter(function (d, index) {
                return index == i;
            });

        var keyData = '';
        keyBlock.select("rect")
            .attr("class", "cell-rect-selected")
            .each(function (d, i) {
                keyData = d;
            });

        var resultBlock = d3.select('#roundZeroResult')
            .selectAll('.cell')
            .filter(function (d, index) {
                return index == i;
            });

        var resultData = '';
        resultBlock.select("rect")
            .attr("class", "cell-rect-selected")
            .each(function (d, i) {
                resultData = d;
            });

        var bin = dec2bin(d);
        bin = "00000000".substr(bin.length) + bin;

        var bin2 = dec2bin(keyData);
        bin2 = "00000000".substr(bin2.length) + bin2;

        var bin3 = dec2bin(resultData);
        bin3 = "00000000".substr(bin3.length) + bin3;

        var div = document.getElementById('XOR-round-zero');
        div.innerHTML = bin + '<br>' + bin2 + '<br>' + '___________' + '<br>' + bin3;
    }

    function handleMouseOutOnRoundZero(d, i) {
        d3.select(this)
            .select("rect")
            .attr("class", "cell-rect-data-nohover");

        var keyBlock = d3.select('#roundZeroKey')
            .selectAll('.cell')
            .filter(function (d, index) {
                return index == i;
            });

        keyBlock.select("rect")
            .attr("class", "cell-rect-key");

        var result = d3.select('#roundZeroResult')
            .selectAll('.cell')
            .filter(function (d, index) {
                return index == i;
            });

        result.select("rect")
            .attr("class", "cell-rect-result");

        document.getElementById('XOR-round-zero').innerHTML =
            '--------' +
            '<br>--------' +
            '<br>___________' +
            '<br>--------';
    }

    function SetMouseEventOnRoundZero() {
        d3.select('#roundZeroData')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnRoundZero)
            .on('mouseout', handleMouseOutOnRoundZero);
    }

    function handleMouseOverOnKeyViz(d, i) {

        var oldKeyColumn = [];
        var newKeyColumn = [];
        var resultKeyColumn = [];


        if (i % 4 != 0) {

            var column = d3.select("#newKey")
                .selectAll(".cell")
                .filter(function (d, index) {
                    var imod = i % 4;
                    return index % 4 === imod;
                }).each(function (d, i) {
                    resultKeyColumn.push(d);
                });

            var previous = d3.select("#newKey")
                .selectAll(".cell")
                .filter(function (d, index) {
                    var imod = i % 4;
                    return index % 4 === imod - 1;
                }).each(function (d, i) {
                    newKeyColumn.push(d);
                });

            previous.select("rect")
                .attr("class", "cell-rect-selected-2");

            column.select("rect")
                .attr("class", "cell-rect-selected");

            var oldColumn = d3.select("#oldKey")
                .selectAll(".cell")
                .filter(function (d, index) {
                    var imod = i % 4;
                    return index % 4 === imod;
                }).each(function (d, i) {
                    oldKeyColumn.push(d);
                });

            oldColumn.select("rect")
                .attr("class", "cell-rect-selected-2");

            VisualizeKeysXor(oldKeyColumn, newKeyColumn, resultKeyColumn, 'keyViz');
        }

    }

    function handleMouseOutOnKeyViz(d, i) {

        var column = d3.select("#newKey")
            .selectAll(".cell")
            .select("rect")
            .attr("class", "cell-rect-result");


        var oldColumn = d3.select("#oldKey")
            .selectAll(".cell")
            .select("rect")
            .attr("class", "cell-rect-data-nohover");

        var vizNode = document.getElementById("keyViz");
        vizNode.innerHTML = '';
    }

    function SetMouseEventOnKeyViz() {
        d3.select('#newKey')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnKeyViz)
            .on('mouseout', handleMouseOutOnKeyViz);
    }

    function handleMouseOverOnKeyViKeyVizFirstColumn(d, i) {

        d3.select("#newKey")
            .selectAll(".cell")
            .filter(function (d, i) {
                return i % 4 === 0;
            })
            .select("rect")
            .attr("class", "cell-rect-selected");

        d3.select("#oldKey")
            .selectAll(".cell")
            .filter(function (d, i) {
                return i % 4 === 3;
            })
            .select("rect")
            .attr("class", "cell-rect-selected-2");

        VisualizeKeyFirstColumn(mainKey, newKeyDictionary, 'keyViz');
        d3.select("#rconMath")
            .select(".img-responsive")
            .attr("style", "display: inline;")
    }
    function handleMouseOutKeyViKeyVizFirstColumn(d, i) {

        d3.select("#newKey")
            .selectAll(".cell")
            .filter(function (d, i) {
                return i % 4 === 0;
            })
            .select("rect")
            .attr("class", "cell-rect-result");

        d3.select("#oldKey")
            .selectAll(".cell")
            .filter(function (d, i) {
                return i % 4 === 3;
            })
            .select("rect")
            .attr("class", "cell-rect-data-nohover");


        var div = document.getElementById('keyViz');
        div.innerHTML = '';
        d3.select("#rconMath")
            .select(".img-responsive")
            .attr("style", "display: none;")
    }

    function SetMouseEventOnKeyVizFirstColumn() {
        d3.select('#newKey')
            .selectAll('.cell')
            .filter(function (d, i) {
                return i % 4 === 0;
            })
            .on('mouseover', handleMouseOverOnKeyViKeyVizFirstColumn)
            .on('mouseout', handleMouseOutKeyViKeyVizFirstColumn);
    }


    function ChangeTextData(matrix, numrows, numcols, textClass) {

        d3.selectAll("." + textClass)
            .each(function (d, i) {
                d3.select(this).text(function () {

                    let x = i % numrows;
                    let y = Math.floor(i / numcols);

                    return matrix[x][y];
                })
            });
    }


    //backend
    function XorOnBlocks(block1, block2) {

        var result = new Array(4);
        for (var i = 0; i < 4; i++) {
            result[i] = new Array(4);
        }

        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < 4; j++) {
                result[i][j] = block1[i][j] ^ block2[i][j];
            }
        }

        return result;
    }

    function SubBytes(block, sbox) {
        let numrows = 4;
        let numcols = 4;

        let resultBlock = new Array(numrows);
        for (let i = 0; i < numrows; i++) {
            resultBlock[i] = new Array(numcols);
        }

        for (let i = 0; i < numrows; i++) {

            for (let j = 0; j < numcols; j++) {

                var row = block[i][j] >> 4;
                var column = block[i][j] & 15;

                resultBlock[i][j] = sbox[row][column];
            }
        }

        return resultBlock;
    }

    function GMul(a, b) {
        let p = 0;


        for (let counter = 0; counter < 8; counter++) {
            if ((b & 1) != 0) {
                p ^= a;
            }


            let hi_bit_set = (a & 0x80) != 0;
            a <<= 1;
            if (hi_bit_set) {
                a ^= 0x1B; /* x^8 + x^4 + x^3 + x + 1 */
            }
            b >>= 1;
        }

        return p & 255;
    }

    function checkHex(n) { return /^[0-9A-Fa-f]{1,64}$/.test(n) }
    function Hex2Bin(n) { if (!checkHex(n)) return 0; return parseInt(n, 16).toString(2) }
    function dec2bin(dec) {
        return (dec >>> 0).toString(2);
    }

    function MixColumns(s) {

        var block = new Array(4);
        for (var i = 0; i < 4; i++) {
            block[i] = new Array(4);
        }
        for (let c = 0; c < 4; c++) {
            block[0][c] = (GMul(0x02, s[0][c]) ^ GMul(0x03, s[1][c]) ^ s[2][c] ^ s[3][c]);
            block[1][c] = (s[0][c] ^ GMul(0x02, s[1][c]) ^ GMul(0x03, s[2][c]) ^ s[3][c]);
            block[2][c] = (s[0][c] ^ s[1][c] ^ GMul(0x02, s[2][c]) ^ GMul(0x03, s[3][c]));
            block[3][c] = (GMul(0x03, s[0][c]) ^ s[1][c] ^ s[2][c] ^ GMul(0x02, s[3][c]));

        }


        return block;

    }

    function ShiftRows(matrix) {

        let numrows = 4;
        let numcols = 4;

        var block = new Array(numrows);
        for (var i = 0; i < numrows; i++) {
            block[i] = new Array(numcols);
        }

        for (var i = 0; i < numrows; i++) {
            for (var j = 0; j < numcols; j++) {
                block[i][j] = matrix[i][j];
            }
        }


        let temp1_1 = block[1][0];
        for (var i = 0; i < numcols - 1; i++) {
            block[1][i] = block[1][i + 1];
        }
        block[1][3] = temp1_1;

        let temp2_1 = block[2][0];
        let temp2_2 = block[2][1];

        for (var i = 0; i < numcols - 2; i++) {
            block[2][i] = block[2][i + 2];
        }
        block[2][2] = temp2_1;
        block[2][3] = temp2_2;

        temp3 = block[3][3];

        for (var i = numcols - 1; i >= 1; i--) {
            block[3][i] = block[3][i - 1];
        }

        block[3][0] = temp3;

        return block;
    }

    function AddRoundKey(matrix, key) {

        var result = new Array(4);
        for (var i = 0; i < 4; i++) {
            result = new Array(4);
        }

        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < 4; j++) {
                result[i][j] = matrix[i][j] ^ key[i][j];
            }
        }

        return result;
    }

    function GenerateRcon() {
        var Rcon = new Array(4);

        for (var i = 0; i < 4; i++) {
            Rcon[i] = new Array(10);
        }
        for (var i = 0; i < 8; i++) {
            Rcon[0][i] = 1 << i;
        }
        Rcon[0][8] = 0x1b;
        Rcon[0][9] = 0x36;

        for (var i = 1; i < 4; i++) {
            for (var j = 0; j < 10; j++) {
                Rcon[i][j] = 0;
            }
        }
        return Rcon;
    }


    function KeySchedule(oldKey, roundNumber) {
        var newKey = new Array(4);
        for (var i = 0; i < 4; i++) {
            newKey[i] = new Array(4);
        }

        var dict = {};

        var firstColumn = new Array(4);
        for (let i = 0; i < 4; i++) {
            firstColumn[i] = oldKey[i][0];
        }

        dict.firstColumn = firstColumn.slice();

        var lastColumn = new Array(4);
        for (let i = 0; i < 4; i++) {
            lastColumn[i] = oldKey[i][3];
        }
        //rotation
        var tempColumn = new Array(4);
        tempColumn[3] = oldKey[0][3];


        for (var i = 0; i < 3; i++) {
            tempColumn[i] = oldKey[i + 1][3];
        }

        dict.rotation = tempColumn.slice();

        //subbytes
        for (var i = 0; i < 4; i++) {
            var row = tempColumn[i] >> 4;
            var column = tempColumn[i] & 15;

            tempColumn[i] = sBox[row][column];
        }

        dict.subbytes = tempColumn.slice();

        var rcon = GenerateRcon();



        var rconColumn = new Array(4);
        for (var i = 0; i < 4; i++) {
            rconColumn[i] = rcon[i][roundNumber - 1];
        }
        dict.rcon = rconColumn.slice();

        for (let i = 0; i < 4; i++) {
            tempColumn[i] = firstColumn[i] ^ tempColumn[i] ^ rconColumn[i];
        }

        dict.firstNewColumn = tempColumn.slice();

        var result = new Array(4);

        for (var i = 0; i < 4; i++) {
            result[i] = new Array(4);
        }

        for (var i = 0; i < 4; i++) {
            result[i][0] = tempColumn[i];
        }

        for (var i = 0; i < 4; i++) {
            result[i][1] = oldKey[i][1] ^ result[i][0];
        }

        for (var i = 0; i < 4; i++) {
            result[i][2] = oldKey[i][2] ^ result[i][1];
        }
        for (var i = 0; i < 4; i++) {
            result[i][3] = oldKey[i][3] ^ result[i][2];
        }

        dict.result = result.slice();

        return dict;
    }

}