﻿var DesVisualizationInit = function() {

    function generateInput() {
        var inputData = [];

        var binaryMatrix = new Array(8);
        for (var i = 0; i < binaryMatrix.length; i++) {
            binaryMatrix[i] = new Array(8);
        }

        var bytesMatrix = new Array(2);
        for (var i = 0; i < bytesMatrix.length; i++) {
            bytesMatrix[i] = new Array(4);
        }

        var currentByte = 0;
        for (var i = 0; i < bytesMatrix.length; i++) {
            for (var j = 0; j < bytesMatrix[0].length; j++) {
                let rand = Math.floor(Math.random() * 256);

                bytesMatrix[i][j] = rand;
                inputData.push(bytesMatrix[i][j]);

                var bin = rand.toString(2);
                bin = "00000000".substring(bin.length) + bin;
                for (var k = 0; k < bin.length; k++) {
                    binaryMatrix[currentByte][k] = bin[k];
                }
                currentByte++;
            }
        }

        return binaryMatrix;
    }
    function permutationData(binaryDataMatrix, permutationMatrix) {

        var resultMatrix = new Array(8);
        for (var i = 0; i < resultMatrix.length; i++) {
            resultMatrix[i] = new Array(8);
        }


        var len = binaryDataMatrix.length;
        var len2 = binaryDataMatrix[0].length;
        data = new Array(len2);

        for (var i = 0; i < len2; i++) {
            data[i] = '';
        }

        for (var i = 0; i < len; i++) {
            for (var j = 0; j < len2; j++) {

                var position = permutationMatrix[i][j] - 1;
                var firstIndex = Math.floor(position / len2);
                var secondIndex = position % len2;

                resultMatrix[i][j] = binaryDataMatrix[firstIndex][secondIndex];
            }
        }

        for (var i = 0; i < len; i++) {
            for (var j = 0; j < len2; j++) {

                data[i] += resultMatrix[i][j];
            }
        }

        var resultData = [];
        for (var i = 0; i < data.length; i++) {
            resultData[i] = parseInt(data[i], 2);
        }

        var resultDataMatrix = new Array(2);
        for (var i = 0; i < resultDataMatrix.length; i++) {
            resultDataMatrix[i] = new Array(4);
        }

        var resultDataCounter = 0;
        for (var i = 0; i < resultDataMatrix.length; i++) {
            for (var j = 0; j < resultDataMatrix[0].length; j++) {

                resultDataMatrix[i][j] = resultData[resultDataCounter];
                resultDataCounter++;
            }
        }

        return resultMatrix;
    }

    function firstPermutation(input) {
        var permutationMatrix = new Array(8);
        permutationMatrix[0] = [58, 50, 42, 34, 26, 18, 10, 2];
        permutationMatrix[1] = [60, 52, 44, 36, 28, 20, 12, 4];
        permutationMatrix[2] = [62, 54, 46, 38, 30, 22, 14, 6];
        permutationMatrix[3] = [64, 56, 48, 40, 32, 24, 16, 8];
        permutationMatrix[4] = [57, 49, 41, 33, 25, 17, 9, 1];
        permutationMatrix[5] = [59, 51, 43, 35, 27, 19, 11, 3];
        permutationMatrix[6] = [61, 53, 45, 37, 29, 21, 13, 5];
        permutationMatrix[7] = [63, 55, 47, 39, 31, 23, 15, 7];

        var result = permutationData(input, permutationMatrix)

        var results = {};
        results.input = input;
        results.permutationMatrix = permutationMatrix;
        results.result = result;

        return results;

    }

    function permutationKey1(key) {

        var pc1 = new Array(8);

        pc1[0] = [57, 49, 41, 33, 25, 17, 9];
        pc1[1] = [1, 58, 50, 42, 34, 26, 18];
        pc1[2] = [10, 2, 59, 51, 43, 35, 27];
        pc1[3] = [19, 11, 3, 60, 52, 44, 36];
        pc1[4] = [63, 55, 47, 39, 31, 23, 15];
        pc1[5] = [7, 62, 54, 46, 38, 30, 22];
        pc1[6] = [14, 6, 61, 53, 45, 37, 29];
        pc1[7] = [21, 13, 5, 28, 20, 12, 4];


        var resultMatrix = new Array(8);
        for (var i = 0; i < resultMatrix.length; i++) {
            resultMatrix[i] = new Array(8);
        }
        var len = key.length;
        var len2 = key[0].length;

        data = new Array(len2);
        for (var i = 0; i < len2; i++) {
            data[i] = '';
        }

        for (var i = 0; i < len; i++) {
            for (var j = 0; j < len2 - 1; j++) {

                var newPosition = pc1[i][j] - 1;
                var firstIndex = Math.floor(newPosition / len2);
                var secondIndex = newPosition % len2;

                resultMatrix[i][j] = key[firstIndex][secondIndex];
            }
        }

        for (var i = 0; i < len; i++) {
            for (var j = 0; j < len2; j++) {

                data[i] += resultMatrix[i][j];
            }
        }

        return {
            key: key,
            permutationMatrix: pc1,
            result: resultMatrix
        }
    }
    function getRotationMatrix() {

        var rotationArray = new Array(16);
        rotationArray[0] = 1;
        rotationArray[1] = 1;
        rotationArray[2] = 2;
        rotationArray[3] = 2;
        rotationArray[4] = 2;
        rotationArray[5] = 2;
        rotationArray[6] = 2;
        rotationArray[7] = 2;
        rotationArray[8] = 1;
        rotationArray[9] = 2;
        rotationArray[10] = 2;
        rotationArray[11] = 2;
        rotationArray[12] = 2;
        rotationArray[13] = 2;
        rotationArray[14] = 2;
        rotationArray[15] = 1;

        return rotationArray;
    }

    function getRotationValue(roundNumber) {

        var rotationArray = new Array(16);
        rotationArray[0] = 1;
        rotationArray[1] = 1;
        rotationArray[2] = 2;
        rotationArray[3] = 2;
        rotationArray[4] = 2;
        rotationArray[5] = 2;
        rotationArray[6] = 2;
        rotationArray[7] = 2;
        rotationArray[8] = 1;
        rotationArray[9] = 2;
        rotationArray[10] = 2;
        rotationArray[11] = 2;
        rotationArray[12] = 2;
        rotationArray[13] = 2;
        rotationArray[14] = 2;
        rotationArray[15] = 1;

        return rotationArray[roundNumber - 1];
    }

    function rotateKey(keyMatrix, roundNumber) {

        rotationValue = getRotationValue(roundNumber);
        rotationArray = new Array(2);

        rotationArray[0] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];
        rotationArray[1] = getRotationMatrix();
        var leftMatrix = new Array(4);
        var rightMatrix = new Array(4);

        for (var i = 0; i < leftMatrix.length; i++) {
            leftMatrix[i] = new Array(7);
        }

        for (var i = 0; i < rightMatrix.length; i++) {
            rightMatrix[i] = new Array(7);
        }

        for (var i = 0; i < leftMatrix.length; i++) {
            for (var j = 0; j < leftMatrix[0].length; j++) {
                leftMatrix[i][j] = keyMatrix[i][j];
            }
        }

        for (var i = 0; i < rightMatrix.length; i++) {
            for (var j = 0; j < rightMatrix[0].length; j++) {
                rightMatrix[i][j] = keyMatrix[i + 4][j];
            }
        }

        var leftMResult = RotateLeft(leftMatrix, rotationValue);
        var rightMResult = RotateLeft(rightMatrix, rotationValue);

        return {
            leftM: leftMatrix,
            leftResult: leftMResult,
            rightM: rightMatrix,
            rightResult: rightMResult,
            mergedMatrix: MergeMatrix(leftMResult, rightMResult),
            rotationMatrix: rotationArray
        }
    }

    function permutationKey2(keyMatrix) {
        var permutationMatrix = new Array(8);
        permutationMatrix[0] = [14, 17, 11, 24, 1, 5];
        permutationMatrix[1] = [3, 28, 15, 6, 21, 10];
        permutationMatrix[2] = [23, 19, 12, 4, 26, 8];
        permutationMatrix[3] = [16, 7, 27, 20, 13, 2];
        permutationMatrix[4] = [41, 52, 31, 37, 47, 55];
        permutationMatrix[5] = [30, 40, 51, 45, 33, 48];
        permutationMatrix[6] = [44, 49, 39, 56, 34, 53];
        permutationMatrix[7] = [46, 42, 50, 36, 29, 32];

        var len = permutationMatrix.length;
        var len2 = permutationMatrix[0].length;

        var resultMatrix = new Array(len);
        for (var i = 0; i < resultMatrix.length; i++) {
            resultMatrix[i] = new Array(len2);
        }

        for (var i = 0; i < len; i++) {
            for (var j = 0; j < len2; j++) {

                var newPosition = permutationMatrix[i][j] - 1;
                var firstIndex = Math.floor(newPosition / len);
                var secondIndex = newPosition % len2;

                resultMatrix[i][j] = keyMatrix[firstIndex][secondIndex];
            }
        }

        return {
            permutationMatrix: permutationMatrix,
            resultKey: resultMatrix
        }
    }

    function splitToLeftAndRight(dataMatrix) {

        var lMatrix = new Array(dataMatrix.length / 2);
        var rMatrix = new Array(dataMatrix.length / 2);

        for (var i = 0; i < lMatrix.length; i++) {
            lMatrix[i] = dataMatrix[i].slice(0, 8);
        }

        for (var i = 0; i < 4; i++) {
            rMatrix[i] = dataMatrix[i + 4].slice(0, 8);
        }

        return {
            LeftMatrix: lMatrix,
            RightMatrix: rMatrix
        }
    }

    function expandRightBlock(block) {

        var expansionMatrix = new Array(8);
        expansionMatrix[0] = [32, 1, 2, 3, 4, 5];
        expansionMatrix[1] = [4, 5, 6, 7, 8, 9];
        expansionMatrix[2] = [8, 9, 10, 11, 12, 13];
        expansionMatrix[3] = [12, 13, 14, 15, 16, 17];
        expansionMatrix[4] = [16, 17, 18, 19, 20, 21];
        expansionMatrix[5] = [20, 21, 22, 23, 24, 25];
        expansionMatrix[6] = [24, 25, 26, 27, 28, 29];
        expansionMatrix[7] = [28, 28, 30, 31, 32, 1];

        var result = new Array(8);
        for (var i = 0; i < result.length; i++) {
            result[i] = new Array(6);
        }

        var len = block.length;
        var len2 = block[0].length;

        for (var i = 0; i < result.length; i++) {
            for (var j = 0; j < result[0].length; j++) {

                var newPosition = expansionMatrix[i][j] - 1;
                var firstIndex = Math.floor(newPosition / len2);
                var secondIndex = newPosition % len2;

                result[i][j] = block[firstIndex][secondIndex];
            }
        }
        var results = {};
        results.expandedData = result;
        results.expansionMatrix = expansionMatrix;
        return results;
    }

    function XOR(m1, m2) {


        if (m1.length === m2.length) {

            var result = new Array(m1.length);
            for (var i = 0; i < result.length; i++) {
                result[i] = new Array(m1[0].length);
            }

            for (var i = 0; i < result.length; i++) {
                for (var j = 0; j < result[0].length; j++) {
                    result[i][j] = m1[i][j] ^ m2[i][j];
                }
            }

            return result;
        }
        return "";
    }

    function substitutionBitGroup(bits, sbox) {
        var binRow = '';
        binRow = bits[0].toString() + bits[bits.length - 1].toString();
        var row = parseInt(binRow, 2);

        var binCol = '';
        binCol = bits[1].toString() + bits[2].toString() + bits[3].toString() + bits[4].toString();
        var col = parseInt(binCol, 2);

        var result = sbox[row][col];

        var bitsResult = result.toString(2);
        bitsResult = "0000".substring(bitsResult.length) + bitsResult;

        return bitsResult.split("");
    }

    function substituteRightGroups(xored) {
        var groups = new Array(8);
        for (var i = 0; i < groups.length; i++) {
            groups[i] = substitutionBitGroup(xored[i], SboxPermutation(i + 1));
        }

        return groups;
    }

    function lastPermutationInFunction(matrix) {
        //permutation
        var permutationMatrix = new Array(8);
        permutationMatrix[0] = [16, 7, 20, 21];
        permutationMatrix[1] = [29, 12, 28, 17];
        permutationMatrix[2] = [1, 15, 23, 26];
        permutationMatrix[3] = [5, 18, 31, 10];
        permutationMatrix[4] = [2, 8, 24, 14];
        permutationMatrix[5] = [32, 27, 3, 9];
        permutationMatrix[6] = [19, 13, 30, 6];
        permutationMatrix[7] = [22, 11, 4, 25];

        var afterPermutation = new Array(8);
        for (var i = 0; i < afterPermutation.length; i++) {
            afterPermutation[i] = new Array(4);
        }

        let len = afterPermutation.length;
        let len2 = afterPermutation[0].length;


        for (var i = 0; i < len; i++) {
            for (var j = 0; j < len2; j++) {

                var position = permutationMatrix[i][j] - 1;
                var firstIndex = Math.floor(position / len2);
                var secondIndex = position % len2;

                afterPermutation[i][j] = matrix[firstIndex][secondIndex];
            }
        }

        return {
            permutationMatrix: permutationMatrix,
            result: afterPermutation
        }

    }

    function changeRowsAndColumns(matrix) {

        var originalRows = matrix.length;
        var originalCols = matrix[0].length;

        var result = new Array(originalCols);
        for (var i = 0; i < result.length; i++) {
            result[i] = new Array(originalRows);
        }

        list = [];
        for (var i = 0; i < originalRows; i++) {
            for (var j = 0; j < originalCols; j++) {
                list.push(matrix[i][j]);
            }
        }
        var counter = 0;
        for (var i = 0; i < result.length; i++) {
            for (var j = 0; j < result[0].length; j++) {
                result[i][j] = list[counter];
                counter++;
            }
        }

        return result;
    }

    function lastPermutation(input) {
        var lastStepPermutation = new Array(8);
        lastStepPermutation[0] = [40, 8, 48, 16, 56, 24, 64, 32];
        lastStepPermutation[1] = [39, 7, 47, 15, 55, 23, 63, 31];
        lastStepPermutation[2] = [38, 6, 46, 14, 54, 22, 62, 30];
        lastStepPermutation[3] = [37, 5, 45, 13, 53, 21, 61, 29];
        lastStepPermutation[4] = [36, 4, 44, 12, 52, 20, 60, 28];
        lastStepPermutation[5] = [35, 3, 43, 11, 51, 19, 59, 27];
        lastStepPermutation[6] = [34, 2, 42, 10, 50, 18, 58, 26];
        lastStepPermutation[7] = [33, 1, 41, 9, 49, 17, 57, 25];

        var result = permutationData(input, lastStepPermutation)

        var results = {};
        results.input = input;
        results.permutationMatrix = lastStepPermutation;
        results.result = result;

        return results;
    }

    function RotateLeft(matrix, shiftNumber) {
        var result = new Array(matrix.length);
        for (var i = 0; i < result.length; i++) {
            result[i] = new Array(matrix[0].length);
        }

        for (var i = 0; i < result.length; i++) {
            for (var j = 0; j < result[0].length - shiftNumber; j++) {
                result[i][j] = matrix[i][j + shiftNumber];
            }
        }

        for (var i = 0; i < result.length; i++) {
            for (var j = result[0].length - shiftNumber; j < result[0].length; j++) {
                result[i][j] = matrix[i][j - result[0].length + shiftNumber];
            }
        }

        return result;
    }

    function MergeMatrix(m1, m2) {
        if (m1[0].length === m2[0].length) {

            var newMatrix = new Array(m1.length + m2.length);
            for (var i = 0; i < newMatrix.length; i++) {
                newMatrix[i] = new Array(m1[0].length);
            }

            for (var i = 0; i < m1.length; i++) {
                for (var j = 0; j < newMatrix[0].length; j++) {
                    newMatrix[i][j] = m1[i][j];
                }
            }

            for (var i = m1.length; i < m1.length + m2.length; i++) {
                for (var j = 0; j < newMatrix[0].length; j++) {
                    newMatrix[i][j] = m2[i - m2.length][j];
                }
            }
            return newMatrix;
        }
        return undefined;
    }

    function IntToHexString(d) {
        var result = d.toString(16).toUpperCase();
        return result.length < 2 ? "0" + result : result;
    }

    function VisualizeDataBlock(matrix, svgId, cellRectClass, hexValue = true) {

        var numrows = matrix.length;
        var numcols = matrix[0].length;

        var margin = { top: 0, right: 0, bottom: 0, left: 0 },
            width = 300,
            height = 300;

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
                if (hexValue) {
                    return IntToHexString(d);
                } else {
                    return d;
                }
            });
    }


    //start
    var input = generateInput();
    var key = generateInput();

    var inputRes = firstPermutation(input);
    var keyP1 = permutationKey1(key);

    var keyAfterRotation = rotateKey(keyP1.key, 1);

    var keyP2 = permutationKey2(keyAfterRotation.mergedMatrix);

    var dividedInput = splitToLeftAndRight(inputRes.result);

    var expanded = expandRightBlock(dividedInput.RightMatrix);

    var xored = XOR(expanded.expandedData, keyP2.resultKey);

    var substitutionsResult = substituteRightGroups(xored);

    var permutationInFunction = lastPermutationInFunction(substitutionsResult);

    var rightBlockBeforeLastXor = changeRowsAndColumns(permutationInFunction.result);

    var lastXor = XOR(dividedInput.LeftMatrix, rightBlockBeforeLastXor);

    var lastStepInput = generateInput();


    var finalResult = lastPermutation(lastStepInput);

    //step1
    VisualizeDataBlock(inputRes.input, 'binaryMatrix', 'cell-rect-data', false);
    VisualizeDataBlock(inputRes.permutationMatrix, 'permutationMatrix', 'cell-rect-helpers', false);
    VisualizeDataBlock(inputRes.result, 'permutationMatrixResult', 'cell-rect-result', false);
    //step2
    VisualizeDataBlock(keyP1.key, 'binaryKeyMatrix', 'cell-rect-data', false);
    VisualizeDataBlock(keyP1.permutationMatrix, 'permutationKeyMatrix', 'cell-rect-helpers', false);
    VisualizeDataBlock(keyP1.result, 'permutationKeyMatrixResult', 'cell-rect-result', false);
    //step3
    VisualizeDataBlock(keyAfterRotation.leftM, 'leftKeyMatrix', 'cell-rect-data', false);
    VisualizeDataBlock(keyAfterRotation.leftResult, 'leftKeyResult', 'cell-rect-result', false);
    VisualizeDataBlock(keyAfterRotation.rightM, 'rightKeyMatrix', 'cell-rect-data', false);
    VisualizeDataBlock(keyAfterRotation.rightResult, 'rightKeyResult', 'cell-rect-result', false);
    //step3.5
    VisualizeDataBlock(keyAfterRotation.mergedMatrix, 'permutationkey2Input', 'cell-rect-data', false);
    VisualizeDataBlock(keyP2.permutationMatrix, 'permutationkey2Matrix', 'cell-rect-helpers', false);
    VisualizeDataBlock(keyP2.resultKey, 'permutationkey2Result', 'cell-rect-result', false);
    //step4
    VisualizeDataBlock(dividedInput.LeftMatrix, 'leftBlockData', 'cell-rect-result', false);
    VisualizeDataBlock(dividedInput.RightMatrix, 'rightBlockData', 'cell-rect-data', false);
    //step5
    VisualizeDataBlock(dividedInput.RightMatrix, 'blockBeforeExpansion', 'cell-rect-data', false);
    VisualizeDataBlock(expanded.expansionMatrix, 'expansionMatrix', 'cell-rect-helpers', false);
    VisualizeDataBlock(expanded.expandedData, 'blockAfterExpansion', 'cell-rect-result', false);
    //step6
    VisualizeDataBlock(expanded.expandedData, 'xorinput', 'cell-rect-data', false);
    VisualizeDataBlock(keyP2.resultKey, 'xorkey', 'cell-rect-key', false);
    VisualizeDataBlock(xored, 'xorresult', 'cell-rect-result', false);
    //step7
    VisualizeDataBlock(xored, 'bitsGroupInput', 'cell-rect-data', false);
    VisualizeSBox(SboxPermutation(1), 'sbox', false);
    VisualizeDataBlock(substitutionsResult, 'bitsAfterSbox', 'cell-rect-data', false);

    //step8
    VisualizeDataBlock(permutationInFunction.permutationMatrix, 'permutationInFunction', 'cell-rect-helpers', false);
    VisualizeDataBlock(permutationInFunction.result, 'permutationInFunctionResult', 'cell-rect-result', false);

    //step9
    VisualizeDataBlock(dividedInput.LeftMatrix, 'xorLeft', 'cell-rect-data', false);
    VisualizeDataBlock(rightBlockBeforeLastXor, 'xorRight', 'cell-rect-key', false);
    VisualizeDataBlock(lastXor, 'xorRightLeftResult', 'cell-rect-result', false);

    //step10
    VisualizeDataBlock(lastXor, 'leftResult', 'cell-rect-result', false);
    VisualizeDataBlock(dividedInput.RightMatrix, 'rightResult', 'cell-rect-data', false);

    //step11
    VisualizeDataBlock(finalResult.input, 'lastStepInput', 'cell-rect-data', false);
    VisualizeDataBlock(finalResult.permutationMatrix, 'lastStepPermutation', 'cell-rect-helpers', false);
    VisualizeDataBlock(finalResult.result, 'finalResult', 'cell-rect-result', false);

    //initialize events
    BitsGroupInputHover();
    Point1Event();
    Point2Event();
    Point3Event();
    Point3Event2();
    Point4Event();
    Point6Event();
    Point6Event_2();
    Point6Event_3();
    Point7Event();
    Point9Event();


    function BlockPermutationVisualize() {



        VisualizeDataBlock(binaryMatrix, 'binaryMatrix', 'cell-rect-data', false);
        VisualizeDataBlock(permutationMatrix, 'permutationMatrix', 'cell-rect-data', false);
        VisualizeDataBlock(permutationJson.resultBinMatrix, 'permutationMatrixResult', 'cell-rect-data', false);

        return {
            binMatrix: binaryMatrix,
            permutationMatrix: permutationMatrix,
            resultBinMatrix: permutationJson.resultBinMatrix
        }

    }

    function KeyPermutationVisualize() {

        var permutationMatrix = new Array(8);
        permutationMatrix[0] = [57, 49, 41, 33, 25, 17, 9];
        permutationMatrix[1] = [1, 58, 50, 42, 34, 26, 18];
        permutationMatrix[2] = [10, 2, 59, 51, 43, 35, 27];
        permutationMatrix[3] = [19, 11, 3, 60, 52, 44, 36];
        permutationMatrix[4] = [63, 55, 47, 39, 31, 23, 15];
        permutationMatrix[5] = [7, 62, 54, 46, 38, 30, 22];
        permutationMatrix[6] = [14, 6, 61, 53, 45, 37, 29];
        permutationMatrix[7] = [21, 13, 5, 28, 20, 12, 4];

        inputData = [];

        var binaryKeyMatrix = new Array(8);
        for (var i = 0; i < binaryKeyMatrix.length; i++) {
            binaryKeyMatrix[i] = new Array(8);
        }


        var bytesKeyMatrix = new Array(1);
        for (var i = 0; i < bytesKeyMatrix.length; i++) {
            bytesKeyMatrix[i] = new Array(8);
        }

        var currentByte = 0;
        for (var i = 0; i < bytesKeyMatrix.length; i++) {
            for (var j = 0; j < bytesKeyMatrix[0].length; j++) {

                let rand = Math.floor(Math.random() * 256);

                bytesKeyMatrix[i][j] = rand;
                inputData.push(bytesKeyMatrix[i][j]);

                var bin = rand.toString(2);
                bin = "00000000".substring(bin.length) + bin;
                for (var k = 0; k < 8; k++) {
                    binaryKeyMatrix[currentByte][k] = bin[k];
                }
                currentByte++;
            }
        }

        var permutationJson = PermutationKey(binaryKeyMatrix, permutationMatrix);

        VisualizeDataBlock(binaryKeyMatrix, 'binaryKeyMatrix', 'cell-rect-data', false);
        VisualizeDataBlock(permutationMatrix, 'permutationKeyMatrix', 'cell-rect-data', false);
        VisualizeDataBlock(permutationJson.resultBinMatrix, 'permutationKeyMatrixResult', 'cell-rect-data', false);

        return {
            binMatrix: binaryKeyMatrix,
            permutationMatrix: permutationMatrix,
            resultMatrix: permutationJson.resultBinMatrix
        }
    }



    function PermutationKey2Visualize(keyMatrix) {


        var result = PermutationKey2(keyMatrix, permutationMatrix);

        return result;
    }

    function VisualizeRoundWithFunction(leftMatrix, rightMatrix, key) {
        VisualizeDataBlock(leftMatrix, 'leftBlockData', 'cell-rect-result', false);
        VisualizeDataBlock(rightMatrix, 'rightBlockData', 'cell-rect-data', false);
        VisualizeDataBlock(expandedData, 'xorinput', 'cell-rect-data', false);
        VisualizeDataBlock(key, 'xorkey', 'cell-rect-data', false);
        VisualizeDataBlock(xorWithKey, 'xorresult', 'cell-rect-data', false);

        var rightResult = SubstituteRightGroups(xorWithKey);



        VisualizeDataBlock(permutationMatrix, 'permutationInFunction', 'cell-rect-data', false);


        Permutation
        VisualizeDataBlock(afterPermutation, 'permutationInFunctionResult', 'cell-rect-data', false);

        return rightResult;
    }

    function SboxPermutation(groupNumber) {

        if (groupNumber <= 0 || groupNumber > 8) {
            return;
        }


        var sbox = new Array(8);
        for (var i = 0; i < 8; i++) {
            sbox[i] = new Array(4);
        }

        sbox[0][0] = [14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7];
        sbox[0][1] = [0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8];
        sbox[0][2] = [4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0];
        sbox[0][3] = [15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 3];

        sbox[1][0] = [15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10];
        sbox[1][1] = [3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5];
        sbox[1][2] = [0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15];
        sbox[1][3] = [13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9];

        sbox[2][0] = [10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8];
        sbox[2][1] = [13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1];
        sbox[2][2] = [13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7];
        sbox[2][3] = [1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12];

        sbox[3][0] = [7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15];
        sbox[3][1] = [13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9];
        sbox[3][2] = [10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4];
        sbox[3][3] = [3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14];


        sbox[4][0] = [2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9];
        sbox[4][1] = [14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6];
        sbox[4][2] = [4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14];
        sbox[4][3] = [11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3];

        sbox[5][0] = [12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11];
        sbox[5][1] = [10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8];
        sbox[5][2] = [9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6];
        sbox[5][3] = [4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13];

        sbox[6][0] = [4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1];
        sbox[6][1] = [13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6];
        sbox[6][2] = [1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2];
        sbox[6][3] = [6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12];

        sbox[7][0] = [13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7];
        sbox[7][1] = [1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2];
        sbox[7][2] = [7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8];
        sbox[7][3] = [2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11];


        return sbox[groupNumber - 1];
    }

    function VisualizeSBox(matrix, svgId, hexValues = true) {

        var numrows = matrix.length;
        var numcols = matrix[0].length;

        var margin = { top: 100, right: 100, bottom: 20, left: 100 },
            width = 600,
            height = 180;

        var textOffsetX = (width / numcols) / 2 - 10;
        var textOffsetY = (height / numrows) / 2;

        var svg = d3.select("#" + svgId)
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

        svg.append("g")
            .attr("class", "description")
            .append("text")
            .attr("class", "description-text")
            .attr("x", 150)
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
            .attr("x", function (d, i) { return x(i) + textOffsetX + 10; })
            .attr("y", textOffsetY)
            .text(function (d) {
                if (hexValues)
                    return IntToHexString(d);
                else
                    return d;
            });


        //index row

        var indexRowArray = new Array(numrows);
        for (var i = 0; i < numrows; i++) {
            indexRowArray[i] = i;
        }

        var indexColumnArray = new Array(numcols);
        for (var i = 0; i < numcols; i++) {
            indexColumnArray[i] = i;
        }

        var indexRow = d3.select("#" + svgId)
            .append("g")
            .attr("transform", "translate(" + margin.left + "," + margin.top / 2 + ")");

        indexRow.selectAll(".index-row")
            .data(indexColumnArray)
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
            .attr("x", function (d, i) { return x(i) + textOffsetX + 10; })
            .attr("y", textOffsetY)
            .text(function (d) {
                if (hexValues)
                    return IntToHexString(d);
                else {
                    return d;
                }
            });

        //index column

        var indexColumn = d3.select("#" + svgId)
            .append("g")
            .attr("transform", "translate(" + (margin.left / 2) + "," + margin.top + ")");

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
            .attr("y", function (d, i) { return y(i) + textOffsetY; })
            .attr("x", textOffsetX + 10)
            .text(function (d) {
                if (hexValues)
                    return IntToHexString(d);
                else {
                    return d;
                }
            });
    }



    //events
    function handleMouseOverOnBitsGroup(d, i) {

        var imod = Math.floor(i / 6);

        var row = d3.select('#bitsGroupInput')
            .selectAll('.cell')
            .filter(function (d, index) {
                return Math.floor((index / 6)) == imod;
            });


        var sboxRow = '';
        var sboxColumn = '';

        var len = row[0].length;
        row.filter(function (d, index) {
            return index !== 0 && index !== len - 1;
        }).select("rect")
            .attr("class", "cell-rect-selected")
            .each(function (d, i) {
                sboxColumn += d;
            });

        var len = row[0].length;
        row.filter(function (d, index) {
            return index === 0 || index === len - 1;
        }).select("rect")
            .attr("class", "cell-rect-selected-2")
            .each(function (d, i) {
                sboxRow += d;
            });

        //change sbox here
        document.getElementById('sbox').innerHTML = '';
        var sbox = SboxPermutation(imod + 1);
        VisualizeSBox(sbox, 'sbox', false);

        var colIndex = parseInt(sboxColumn, 2);
        var rowIndex = parseInt(sboxRow, 2);

        var sBoxSvg = d3.select('#sbox');
        var columns = sBoxSvg.selectAll('.index-cell-row');
        columns.select("rect")
            .filter(function (d, i) {
                return i === colIndex;
            })
            .attr("class", "cell-rect-selected");

        var rows = d3.select('#sbox').selectAll('.index-cell-column');

        rows.select("rect")
            .filter(function (d, i) {
                return i === rowIndex;
            })
            .attr("class", "cell-rect-selected-2");

        var selectedValue = 0;
        sBoxSvg.select('#row' + rowIndex)
            .select("#cell" + colIndex)
            .select("rect")
            .attr("class", "cell-rect-selected-3")
            .each(function (d, i) {
                selectedValue = d[colIndex];
            });



        sBoxSvg.append('g')
            .attr('id', 'sboxBinDescription')
            .append('text')
            .attr('x', 400)
            .attr('y', 25)
            .attr('class', 'description-text')
            .text(function () {

                var bin = selectedValue.toString(2);
                bin = "0000".substring(bin.length) + bin;
                return selectedValue.toString() + " ---> " + bin;
            });

        bitsAfterSbox = d3.select("#bitsAfterSbox");
        bitsAfterSbox.selectAll(".cell")
            .filter(function (d, index) {
                return Math.floor((index / 4)) == imod;
            }).select("rect").attr("class", "cell-rect-selected-3");

    }
    function handleMouseOutOnBitsGroup(d, i) {
        d3.select('#bitsGroupInput')
            .selectAll('.cell')
            .select("rect")
            .attr("class", "cell-rect-data");

        var sBoxSvg = d3.select('#sbox');
        var columns = sBoxSvg.selectAll('.index-cell-row');
        columns.select("rect")
            .attr("class", "cell-rect-index");

        var rows = d3.select('#sbox').selectAll('.index-cell-column');

        rows.select("rect")
            .attr("class", "cell-rect-index");

        sBoxSvg.select('.cell-rect-selected-3')
            .attr("class", "cell-rect-sbox");

        d3.select('#sboxBinDescription')
            .select("text")
            .text('');

        bitsAfterSbox = d3.select("#bitsAfterSbox");
        bitsAfterSbox.selectAll(".cell")
            .select("rect").attr("class", "cell-rect-data")

    }

    function BitsGroupInputHover() {
        d3.select('#bitsGroupInput')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnBitsGroup)
            .on('mouseout', handleMouseOutOnBitsGroup);
    }


    function handleMouseOverOnPoint1(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-selected");

        var inputCell = d3.select("#binaryMatrix")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            });

        inputCell
            .select("rect")
            .attr("class", "cell-rect-selected");

        var outputCell = d3.select("#permutationMatrixResult")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === i;
            });

        outputCell
            .select("rect")
            .attr("class", "cell-rect-selected");
    }

    function handleMouseOutOnPoint1(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-helpers");

        var inputCell = d3.select("#binaryMatrix")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            }).select("rect")
            .attr("class", "cell-rect-data");

        var outputCell = d3.select("#permutationMatrixResult")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-result");
    }


    function Point1Event() {
        d3.select('#permutationMatrix')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnPoint1)
            .on('mouseout', handleMouseOutOnPoint1);
    }

    function handleMouseOverOnPoint2(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-selected");

        var inputCell = d3.select("#binaryKeyMatrix")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            });

        inputCell
            .select("rect")
            .attr("class", "cell-rect-selected");

        var outputCell = d3.select("#permutationKeyMatrixResult")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === (i + Math.floor(i / 7));
            });

        outputCell
            .select("rect")
            .attr("class", "cell-rect-selected");
    }

    function handleMouseOutOnPoint2(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-helpers");

        var inputCell = d3.select("#binaryKeyMatrix")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            }).select("rect")
            .attr("class", "cell-rect-data");

        var outputCell = d3.select("#permutationKeyMatrixResult")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === (i + Math.floor(i / 7));;
            }).select("rect").attr("class", "cell-rect-result");
    }


    function Point2Event() {
        d3.select('#permutationKeyMatrix')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnPoint2)
            .on('mouseout', handleMouseOutOnPoint2);
    }

    function handleMouseOverOnPoint3(d, i) {

        row = Math.floor(i / 7);

        //left
        var rowCells = d3.select('#leftKeyMatrix')
            .selectAll('.cell')
            .filter(function (d, index) {
                return Math.floor(index / 7) === row;
            });
        rowCells.select("rect")
            .attr("class", "cell-rect-selected");

        rowCells.filter(function (d, i) {
            return i === 0;
        }).select("rect").attr("class", "cell-rect-selected-2");

        var resultRowCells = d3.select('#leftKeyResult')
            .selectAll('.cell')
            .filter(function (d, index) {
                return Math.floor(index / 7) === row;
            });
        resultRowCells.select("rect")
            .attr("class", "cell-rect-selected");

        resultRowCells.filter(function (d, i) {
            return i % 7 === 6;
        }).select("rect").attr("class", "cell-rect-selected-2");



    }

    function handleMouseOutOnPoint3(d, i) {

        row = Math.floor(i / 7);

        //left
        d3.select('#leftKeyMatrix')
            .selectAll('.cell')
            .filter(function (d, index) {
                return Math.floor(index / 7) === row;
            }).select("rect")
            .attr("class", "cell-rect-data");

        d3.select('#leftKeyResult')
            .selectAll('.cell')
            .filter(function (d, index) {
                return Math.floor(index / 7) === row;
            }).select("rect")
            .attr("class", "cell-rect-result");
    }

    function handleMouseOverOnPoint3_2(d, i) {

        row = Math.floor(i / 7);
        //right
        var rowCells = d3.select('#rightKeyMatrix')
            .selectAll('.cell')
            .filter(function (d, index) {
                return Math.floor(index / 7) === row;
            });
        rowCells.select("rect")
            .attr("class", "cell-rect-selected");

        rowCells.filter(function (d, i) {
            return i === 0;
        }).select("rect").attr("class", "cell-rect-selected-2");

        var resultRowCells = d3.select('#rightKeyResult')
            .selectAll('.cell')
            .filter(function (d, index) {
                return Math.floor(index / 7) === row;
            });
        resultRowCells.select("rect")
            .attr("class", "cell-rect-selected");

        resultRowCells.filter(function (d, i) {
            return i % 7 === 6;
        }).select("rect").attr("class", "cell-rect-selected-2");
    }

    function handleMouseOutOnPoint3_2(d, i) {

        row = Math.floor(i / 7);
        //right
        d3.select('#rightKeyMatrix')
            .selectAll('.cell')
            .filter(function (d, index) {
                return Math.floor(index / 7) === row;
            }).select("rect")
            .attr("class", "cell-rect-data");

        d3.select('#rightKeyResult')
            .selectAll('.cell')
            .filter(function (d, index) {
                return Math.floor(index / 7) === row;
            }).select("rect")
            .attr("class", "cell-rect-result");
    }

    function Point3Event() {
        d3.select('#leftKeyMatrix')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnPoint3)
            .on('mouseout', handleMouseOutOnPoint3);
    }

    function Point3Event2() {
        d3.select('#rightKeyMatrix')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnPoint3_2)
            .on('mouseout', handleMouseOutOnPoint3_2);
    }

    function handleMouseOverOnPoint4(d, i) {

        d3.select(this).select("rect")
            .attr("class", "cell-rect-selected");

        var inputCell = d3.select("#permutationkey2Input")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            });

        inputCell
            .select("rect")
            .attr("class", "cell-rect-selected");

        var outputCell = d3.select("#permutationkey2Result")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === i;
            });

        outputCell
            .select("rect")
            .attr("class", "cell-rect-selected");

    }
    function handleMouseOutOnPoint4(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-helpers");

        var inputCell = d3.select("#permutationkey2Input")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            }).select("rect")
            .attr("class", "cell-rect-data");

        var outputCell = d3.select("#permutationkey2Result")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-result");
    }

    function Point4Event() {

        d3.select('#permutationkey2Matrix')
            .selectAll('.cell')
            .on('mouseover', handleMouseOverOnPoint4)
            .on('mouseout', handleMouseOutOnPoint4);
    }

    function handleMouseOverOnPoint6(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-selected");

        var inputCell = d3.select("#blockBeforeExpansion")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            });

        inputCell
            .select("rect")
            .attr("class", "cell-rect-selected");

        var outputCell = d3.select("#blockAfterExpansion")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === i;
            });

        outputCell
            .select("rect")
            .attr("class", "cell-rect-selected");
    }

    function handleMouseOutOnPoint6(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-helpers");

        var inputCell = d3.select("#blockBeforeExpansion")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            }).select("rect")
            .attr("class", "cell-rect-data");

        var outputCell = d3.select("#blockAfterExpansion")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-result");
    }

    function Point6Event() {

        d3.select("#expansionMatrix")
            .selectAll(".cell")
            .on('mouseover', handleMouseOverOnPoint6)
            .on('mouseout', handleMouseOutOnPoint6);
    }

    function handleMouseOverOnPoint6_2(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-selected");

        d3.select("#xorkey").selectAll(".cell")
            .filter(function (d, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-selected");

        d3.select("#xorresult").selectAll(".cell")
            .filter(function (d, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-selected");

    }

    function handleMouseOutOnPoint6_2(d, i) {

        d3.select(this).select("rect")
            .attr("class", "cell-rect-data");

        d3.select("#xorkey").selectAll(".cell")
            .filter(function (d, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-key");

        d3.select("#xorresult").selectAll(".cell")
            .filter(function (d, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-result");

    }

    function Point6Event_2() {
        d3.select("#xorinput")
            .selectAll(".cell")
            .on('mouseover', handleMouseOverOnPoint6_2)
            .on('mouseout', handleMouseOutOnPoint6_2);
    }

    function handleMouseOverOnPoint6_3(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-selected");

        var inputCell = d3.select("#bitsAfterSbox")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            });

        inputCell
            .select("rect")
            .attr("class", "cell-rect-selected");

        var outputCell = d3.select("#permutationInFunctionResult")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === i;
            });

        outputCell
            .select("rect")
            .attr("class", "cell-rect-selected");
    }

    function handleMouseOutOnPoint6_3(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-helpers");

        var inputCell = d3.select("#bitsAfterSbox")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            }).select("rect")
            .attr("class", "cell-rect-data");

        var outputCell = d3.select("#permutationInFunctionResult")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-result");
    }

    function Point6Event_3() {
        d3.select("#permutationInFunction")
            .selectAll(".cell")
            .on('mouseover', handleMouseOverOnPoint6_3)
            .on('mouseout', handleMouseOutOnPoint6_3);
    }

    function handleMouseOverOnPoint7(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-selected");

        d3.select("#xorRight").selectAll(".cell")
            .filter(function (d, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-selected");

        d3.select("#xorRightLeftResult").selectAll(".cell")
            .filter(function (d, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-selected");

    }

    function handleMouseOutOnPoint7(d, i) {

        d3.select(this).select("rect")
            .attr("class", "cell-rect-data");

        d3.select("#xorRight").selectAll(".cell")
            .filter(function (d, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-key");

        d3.select("#xorRightLeftResult").selectAll(".cell")
            .filter(function (d, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-result");

    }

    function Point7Event() {
        d3.select("#xorLeft")
            .selectAll(".cell")
            .on('mouseover', handleMouseOverOnPoint7)
            .on('mouseout', handleMouseOutOnPoint7);
    }

    function handleMouseOverOnPoint9(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-selected");

        var inputCell = d3.select("#lastStepInput")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            });

        inputCell
            .select("rect")
            .attr("class", "cell-rect-selected");

        var outputCell = d3.select("#finalResult")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === i;
            });

        outputCell
            .select("rect")
            .attr("class", "cell-rect-selected");
    }

    function handleMouseOutOnPoint9(d, i) {
        d3.select(this).select("rect")
            .attr("class", "cell-rect-helpers");

        var inputCell = d3.select("#lastStepInput")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === d - 1;
            }).select("rect")
            .attr("class", "cell-rect-data");

        var outputCell = d3.select("#finalResult")
            .selectAll(".cell")
            .filter(function (data, index) {
                return index === i;
            }).select("rect").attr("class", "cell-rect-result");
    }

    function Point9Event() {
        d3.select("#lastStepPermutation")
            .selectAll(".cell")
            .on('mouseover', handleMouseOverOnPoint9)
            .on('mouseout', handleMouseOutOnPoint9);
    }
}