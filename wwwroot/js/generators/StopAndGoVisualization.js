﻿var StopAndGoVisualizationInit = function () {

    var maxWidth = 400;
    var maxHeight = 50;

    var registers = [];

    var feedback = [];
    //helpers
    function isBinaryString(str) {
        regexp = /^[01]+$/;
        if (regexp.test(str)) {
            return true;
        } else {
            return false;
        }
    }


    function isCommaSeparatedNumber(str) {
        regexp = /^(\d+,)*\d+$/;
        if (regexp.test(str)) {
            return true;
        } else {
            return false;
        }
    }

    Array.prototype.contains = function (v) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] === v) return true;
        }
        return false;
    };

    Array.prototype.unique = function () {
        var arr = [];
        for (var i = 0; i < this.length; i++) {
            if (!arr.includes(this[i])) {
                arr.push(this[i]);
            }
        }
        return arr;
    }


    function DrawOutputLines() {

        var group = d3.select('#outputLines');


        var output1 = group.append("text")
            .attr("id","output1")
            .attr("x", 0)
            .attr("y", 35)
            .attr("class","register-cell-text")
            .text("");

        var output2 = group.append("text")
            .attr("id", "output2")
            .attr("x", 50)
            .attr("y", 330)
            .attr("class", "register-cell-text")
            .text("1 + 1");

        var mainOut = group.append("text")
            .attr("id", "mainOutput")
            .attr("x", 220)
            .attr("y", 330)
            .attr("class", "register-cell-text")
            .text(""); 

        var line1 = group.append("line")
            .attr("x1", -50)
            .attr("y1", 425)
            .attr("x2", 150)
            .attr("y2", 425)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        var line2 = group.append("line")
            .attr("x1", -50)
            .attr("y1", 225)
            .attr("x2", 150)
            .attr("y2", 225)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        var line3 = group.append("line")
            .attr("x1", 150)
            .attr("y1", 225)
            .attr("x2", 150)
            .attr("y2", 425)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        var circle = group.append("circle")
            .attr("cx",150)
            .attr("cy", 325)
            .attr("stroke", "black")
            .attr("stroke-width", 3)
            .attr("fill-opacity", 0)
            .attr("r", 15);

        var outLines = group.append("line")
            .attr("x1", 150)
            .attr("y1", 325)
            .attr("x2", 200)
            .attr("y2", 325)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        group.append("line")
            .attr("x1", 135)
            .attr("y1", 325)
            .attr("x2", 165)
            .attr("y2", 325)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

    }

    function CreateLfsr(data, functions, svgId, registerName, functionId) {

        $('#' + svgId).empty();

        var xorBits = [];

        var lastIndex = data.length - 1;

        var width = maxWidth / data.length;

        var textX = width;
        var textY = maxHeight * 1.7;

        var svg = d3.select('#' + svgId);

        globalTextX = textX + width / 2;
        globalTextY = textY;

        var groups = svg.selectAll("g")
            .data(data)
            .enter()
            .append("g")
            .attr("class", ".cell")
            .attr("transform", "translate(100,0)");

        var rects = groups.append("rect")
            .attr("x",
                function (d, i) {
                    return i * width;
                })
            .attr("y", 50)
            .attr("width", width)
            .attr("height", maxHeight)
            .attr("class", "register-rect");


        svg.append("line")
            .attr("x1", maxWidth + 100)
            .attr("y1", 75)
            .attr("x2", maxWidth + 150)
            .attr("y2", 75)
            .attr("stroke-width", 4)
            .attr("stroke", "black");


        svg.append("line")
            .attr("x1", 125)
            .attr("y1", 150)
            .attr("x2", 75)
            .attr("y2", 150)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        svg.append("line")
            .attr("x1", 75)
            .attr("y1", 150)
            .attr("x2", 75)
            .attr("y2", 75)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        svg.append("line")
            .attr("x1", 75)
            .attr("y1", 75)
            .attr("x2", 100)
            .attr("y2", 75)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        var text = groups.append("text")
            .text(function (d) { return d; })
            .attr("x",
                function (d, i) {
                    return i * textX + width / 2;
                })
            .attr("y", textY)
            .attr("class", "register-cell-text");


        var lastItem = groups
            .filter(function (d, i) {
                return i === lastIndex;
            });

        var cellsForLines = groups.filter(function (d, i) {
            return functions.includes((i + 1));
        });

        cellsForLines.each(function (d) {
            xorBits.push(d);
        });


        var fLines = cellsForLines.selectAll("line")
            .data(functions)
            .enter();
        fLines.append("line")
            .attr("x1",
                function (d, i) {
                    return (d - 1) * width + width / 2;
                })
            .attr("y1", 100)
            .attr("x2",
                function (d, i) {
                    return (d - 1) * width + width / 2;
                })
            .attr("y2", function (d, i) {
                if (i === functions.length - 1) {
                    return 150;
                } else {
                    return 162;
                }
            })
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        fLines.append('circle')
            .filter(function (d, i) {
                return i <= functions.length - 2;
            })
            .attr("cx",
                function (d, i) {
                    return (d - 1) * width + width / 2;
                })
            .attr("cy", 150)
            .attr("stroke", "black")
            .attr("stroke-width", 3)
            .attr("fill-opacity", 0)
            .attr("r", 12);

        fLines.append("line")
            .attr("x1", 0)
            .attr("y1", 150)
            .attr("x2",
                function (d, i) {
                    return (d - 1) * (width) + width / 2;
                })
            .attr("y2", 150)
            .attr("stroke-width", 4)
            .attr("stroke", "black");


        var outputArray = [];

        var feedbackText = svg.append('text')
            .attr('id', functionId)
            .attr('x', 100)
            .attr('y', 200)
            .text(function () {
                var s = '';
                var result = 0;
                for (var i = 0; i < xorBits.length; i++) {
                    result ^= xorBits[i];
                }
                for (var i = 0; i < xorBits.length - 1; i++) {
                    s += xorBits[i] + '+';
                }
                var txt = result.toString() + '\t<===\t' + s + xorBits[xorBits.length - 1];
                return txt;
            })
            .attr("class", "register-cell-text");


        svg.append('text')
            .attr('id', 'outputBits')
            .attr('x', 100)
            .attr('y', 250)
            .text(outputArray)
            .attr("class", "register-cell-text");

        svg.append("text")
            .attr("x", 100)
            .attr("y", 40)
            .attr("class", "register-text")
            .text(registerName);


    }

    function changeFeedbackText() {
        feedbackText
            .text(function () {
                var s = '';

                var newBit = 0;
                for (var i = 0; i < xorBits.length; i++) {
                    newBit ^= xorBits[i];
                }

                for (var i = 0; i < xorBits.length - 1; i++) {
                    s += xorBits[i] + '+';
                }
                var txt = newBit.toString() + '\t<===\t' + s + xorBits[xorBits.length - 1];
                return txt;
            });
    }

    var outputText = [];

    function update() {

        $('#nextStepBtn').attr("disabled", true);

        var r1 = d3.select('#lfsr1').selectAll("g");
        var r2 = d3.select('#lfsr2').selectAll("g");
        var r3 = d3.select('#lfsr3').selectAll("g");
        var r1Len = registers[0].length;
        var r2Len = registers[1].length;
        var r3Len = registers[2].length;

        var o1 = registers[0][r1Len - 1];

        r1.filter(function(d, i) {
                return i === r1Len - 1;
            }).select("text")
            .transition()
            .duration(300)
            .attr("x", 500)
            .attr("y", 85);

        setTimeout(function() {
                d3.select("#output1")
                    .text(function () {
                        if (o1 === 1) {
                            return "1 ==> taktujemy Rejestr2";
                        } else {
                            return "0 ==> taktujemy Rejestr3";
                        }
                    });
            },
            300);

        if (o1 === 1) {
            r2
                .select("rect")
                .attr("class", "incorrect-rect");
        } else {
            r3
                .select("rect")
                .attr("class", "incorrect-rect");
        }

        setTimeout(function() {
                if (o1 === 1) {

                    shiftBits('lfsr2', registers[1], feedback[1]);
                    var xor2 = changeFeedbackValuesInfo(registers[1], feedback[1]);
                    updateFeedbackText('F2', xor2);

                } else {

                    shiftBits('lfsr3', registers[2], feedback[2]);
                    var xor3 = changeFeedbackValuesInfo(registers[2], feedback[2]);
                    updateFeedbackText('F3', xor3);
                }
            },
            500);


        setTimeout(function() {

                r2.filter(function(d, i) {
                        return i === r1Len - 1;
                    }).select("rect")
                    .attr("class", "correct-rect");

                r3.filter(function(d, i) {
                        return i === r1Len - 1;
                    }).select("rect")
                    .attr("class", "correct-rect");

                var o2 = registers[1][r2Len - 1]
                var o3 = registers[2][r3Len - 1]

                d3.select("#output2")
                    .text(o2 + " + " + o3);
                d3.select("#mainOutput")
                    .text(o2 ^ o3);

            },
            700);


        setTimeout(function() {

                shiftBits('lfsr1', registers[0], feedback[0]);
                var xor1 = changeFeedbackValuesInfo(registers[0], feedback[0]);
                updateFeedbackText('F1', xor1);

                r2
                    .select("rect")
                    .attr("class", "register-rect");

                r3
                    .select("rect")
                    .attr("class", "register-rect");

                d3.select("#output1")
                    .text("");

                $('#nextStepBtn').attr("disabled", false);

            },
            2000);

    }

    function changeFeedbackValuesInfo(registerArr, feedbackArr) {
        var xorArr = [];
        feedbackArr.sort();
        for (var i = 0; i < feedbackArr.length; i++) {
            xorArr.push(registerArr[feedbackArr[i] - 1]);
        }
        return xorArr;
    }
    function shiftBits(lfsrId, register, feedback) {
        //shift

        var width = maxWidth / register.length;
        var textX = width;
        var textY = maxHeight * 1.7;

        var newBit = 0;
        for (var i = 0; i < feedback.length; i++) {
            newBit ^= register[feedback[i] - 1];
        }
        register.pop();
        register.unshift(newBit);

        d3.select('#' + lfsrId)
            .selectAll("g")
            .data(register)
            .select("text")
            .text(function (d) { return d; })
            .attr("x", function (d, i) {
                return i * textX + width / 2;
            })
            .attr("y", textY);

        d3.select('#' + lfsrId)
            .selectAll("g")
            .select("rect");
            //.attr("class", 'register-rect');
    }

    function updateFeedbackText(feedbackId, xor) {
        d3.select('#' + feedbackId)
            .text(function () {
                var s = '';

                var newBit = 0;
                for (var i = 0; i < xor.length; i++) {
                    newBit ^= xor[i];
                }

                for (var i = 0; i < xor.length - 1; i++) {
                    s += xor[i] + '+';
                }
                var txt = newBit.toString() + '\t<===\t' + s + xor[xor.length - 1];
                return txt;
            })
    }

    function btnsInit() {
        $('#nextStepBtn').click(function () {
            update();
        });

        $('#createRegisterBtn').click(function () {
            CreateRegister(1, 'registerText1', 'feedbackFunctionViz1', 'lfsr1', 'Rejestr 1', 'F1');
            CreateRegister(2, 'registerText2', 'feedbackFunctionViz2', 'lfsr2', 'Rejestr 2', 'F2');
            CreateRegister(3, 'registerText3', 'feedbackFunctionViz3', 'lfsr3', 'Rejestr 3', 'F3');

        });

    }

    btnsInit();


    function CreateRegister(rNumber, registerText, polynomialId, svgId, registerName, functionId) {

        var register = $('#' + registerText).val();
        if (isBinaryString(register)) {
            if (register.length > 10) {
                alert('Maksymalna dozwolona długość rejestru wynosi: 10!');
                return;
            }

            if (register.length < 2) {
                alert('Minimalna dozwolona długość rejestru wynosi: 2!');
                return;
            }

            var feedbackFunction = $('#' + polynomialId).val();
            var polynomial = feedbackFunction.split(',');
            polynomial = polynomial.unique();

            registers[rNumber - 1] = register.split('').map(Number);
            feedback[rNumber - 1] = polynomial.map(Number);

            if (isCommaSeparatedNumber(polynomial)) {

                registerContent = register.split('').map(Number);
                feedbackContent = polynomial.map(Number);

                if ((Math.max(...feedbackContent)) > registerContent.length) {
                    alert('Maksymalny stopień wielomanu przy tym rejestrze może wynosić: ' + registerContent.length);
                    return;
                }
                $('#' + polynomialId).val(polynomial.join(','));

                CreateLfsr(registerContent, feedbackContent, svgId, registerName, functionId);

            } else {

                alert('Niepoprawny format wielomianu!');
                return;
            }
        } else {
            alert('Rejestr musi mieć wartości 0 lub 1!');
            return;
        }
    }

    DrawOutputLines();
    $('#createRegisterBtn').click();
}