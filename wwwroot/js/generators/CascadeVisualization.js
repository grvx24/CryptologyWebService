var CascadeVisualizationInit = function () {

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

        svg.append("rect")
            .attr("x", 50)
            .attr("y", 10)
            .attr("width", 550)
            .attr("height", 200)
            .attr("class", "cell-rect-background");

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


        svg.append("text")
            .attr("x", 300)
            .attr("y", 40)
            .attr("class", "register-text shift-info")
            .text("");


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

        var xor1 = 0;
        var xor2 = 0;

        var lfsr1 = d3.select('#lfsr1');
        var lfsr2 = d3.select('#lfsr2');
        var lfsr3 = d3.select('#lfsr3');

        var lastCell1 = lfsr1.selectAll('g').filter(function (d, i) {
            return i == registers[0].length - 1;
        });

        var lastCell2 = lfsr2.selectAll('g').filter(function (d, i) {
            return i == registers[1].length - 1;
        });
        var lastCell3 = lfsr3.selectAll('g').filter(function (d, i) {
            return i == registers[2].length - 1;
        });

        

        lastCell1.select("text")
            .transition()
            .duration(500)
            .text(function (d) {
                return d;
            })
            .attr("x", 550)
            .attr("y", 115);



        setTimeout(function () { 
            var output1 = registers[0][registers[0].length - 1];
            xor1 = output1 ^ 1;

            lfsr1.select("#inputR1")
                .text(output1);

            var lines = lfsr2.selectAll(".lfsr2-line");

            if (xor1) {

                lines
                    .attr("stroke", "green");
                lfsr1.select("#outputR1")
                    .attr("fill","green")
                    .text(xor1);

                lfsr2.selectAll(".shift-info")
                    .attr("fill", "green")
                    .text("Przesunięcie rejestru");


                //shift
                shiftBits('lfsr2', registers[1], feedback[1]);
                var xor2 = changeFeedbackValuesInfo(registers[1], feedback[1]);
                updateFeedbackText('F2', xor2);

            } else {
                lines
                    .attr("stroke", "red");
                lfsr1.select("#outputR1")
                    .attr("fill", "red")
                    .text(xor1);

                lfsr2.selectAll(".shift-info")
                    .attr("fill", "red")
                    .text("Brak przesunięcia");
            }

            lastCell2.select("text")
                .transition()
                .duration(500)
                .text(function (d) {
                    return d;
                })
                .attr("x", 550)
                .attr("y", 115);


        }, 1000);

        setTimeout(function () {

            var output2 = registers[1][registers[1].length - 1];
            var xor2 = output2 ^ xor1;


            var lines = lfsr3.selectAll(".lfsr3-line");

            if (xor2) {

                lines
                    .attr("stroke", "green");
                lfsr2.select("#outputR2")
                    .attr("fill", "green")
                    .text(xor2);

                lfsr3.selectAll(".shift-info")
                    .attr("fill","green")
                    .text("Przesunięcie rejestru");

                //shift
                shiftBits('lfsr3', registers[2], feedback[2]);
                var xor3 = changeFeedbackValuesInfo(registers[2], feedback[2]);
                updateFeedbackText('F3', xor3);

            } else {
                lines
                    .attr("stroke", "red");
                lfsr2.select("#outputR2")
                    .attr("fill", "red")
                    .text(xor2);

                lfsr3.selectAll(".shift-info")
                    .attr("fill", "red")
                    .text("Brak przesunięcia");
            }

            lastCell3.select("text")
                .transition()
                .duration(500)
                .text(function (d) {
                    return d;
                })
                .attr("x", 675)
                .attr("y", 80);

        }, 1500);

        setTimeout(function () {

            shiftBits('lfsr1', registers[0], feedback[0]);
            var xor1 = changeFeedbackValuesInfo(registers[0], feedback[0]);
            updateFeedbackText('F1', xor1);

            $('#nextStepBtn').attr("disabled", false);

        }, 2000)



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
            .select("rect")
            .attr("class", 'register-rect');
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

    function drawGates() {

        var lfsr1 = d3.selectAll("#lfsr1");

        lfsr1.append("svg:image")
            .attr("x", 350)
            .attr("y", 0)
            .attr("height", 100)
            .attr("width", 100)
            .attr("xlink:href", "/images/xor_gate.svg")
            .attr("transform", "scale(2.0)");


        var r1Height = 125;
        var r1Width = 550;

        lfsr1.append("line")
            .attr("x1", r1Width)
            .attr("x2", r1Width)
            .attr("y1", r1Height-50)
            .attr("y2", r1Height)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        lfsr1.append("line")
            .attr("x1", r1Width)
            .attr("x2", r1Width+200)
            .attr("y1", r1Height)
            .attr("y2", r1Height)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        lfsr1.append("text")
            .attr("class", "register-cell-text")
            .attr("x", 650)
            .attr("y", 60)
            .text("1");

        lfsr1.append("line")
            .attr("x1", 650)
            .attr("x2", 750)
            .attr("y1", r1Height-50)
            .attr("y2", r1Height - 50)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        lfsr1.append("text")
            .attr("class", "register-cell-text")
            .attr("x", 650)
            .attr("y", 115)
            .attr("id", "inputR1")
            .text("");

        lfsr1.append("text")
            .attr("class", "register-cell-text")
            .attr("x", 875)
            .attr("y", 90)
            .attr("id","outputR1")
            .text("");

        //lfsr1-lfsr2

        var lfsr2 = d3.selectAll("#lfsr2");

        lfsr2.append("line")
            .attr("x1", 700)
            .attr("x2", 300)
            .attr("y1", -25)
            .attr("y2", -25)
            .attr("stroke-width", 4)
            .attr("stroke", "black")
            .attr("class", "lfsr2-line");

        lfsr2.append("line")
            .attr("x1", 300)
            .attr("x2", 300)
            .attr("y1", -25)
            .attr("y2", 10)
            .attr("stroke-width", 4)
            .attr("stroke", "black")
            .attr("class", "lfsr2-line");


        //lfsr2

        lfsr2.append("svg:image")
            .attr("x", 350)
            .attr("y", 0)
            .attr("height", 100)
            .attr("width", 100)
            .attr("xlink:href", "/images/xor_gate.svg")
            .attr("transform", "scale(2.0)");


        var r1Height = 125;
        var r1Width = 550;

        lfsr2.append("line")
            .attr("x1", r1Width)
            .attr("x2", r1Width)
            .attr("y1", r1Height - 50)
            .attr("y2", r1Height)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        lfsr2.append("line")
            .attr("x1", r1Width)
            .attr("x2", r1Width + 200)
            .attr("y1", r1Height)
            .attr("y2", r1Height)
            .attr("stroke-width", 4)
            .attr("stroke", "black");


        lfsr2.append("line")
            .attr("x1", 700)
            .attr("x2", 750)
            .attr("y1", r1Height - 50)
            .attr("y2", r1Height - 50)
            .attr("stroke-width", 4)
            .attr("stroke", "black")
            .attr("class", "lfsr2-line");

        lfsr2.append("line")
            .attr("x1", 700)
            .attr("x2", 700)
            .attr("y1", -170)
            .attr("y2", 75)
            .attr("stroke-width", 4)
            .attr("stroke", "black")
            .attr("class", "lfsr2-line");

        lfsr2.append("line")
            .attr("x1", 660)
            .attr("x2", 700)
            .attr("y1", -167)
            .attr("y2", -167)
            .attr("stroke-width", 6)
            .attr("stroke", "black")
            .attr("class", "lfsr2-line");

        lfsr2.append("text")
            .attr("class", "register-cell-text")
            .attr("x", 875)
            .attr("y", 90)
            .attr("id", "outputR2")
            .text("");

        //lfsr2-lfsr3
        var lfsr3 = d3.selectAll("#lfsr3");

        lfsr3.append("line")
            .attr("x1", 660)
            .attr("x2", 700)
            .attr("y1", -168)
            .attr("y2", -168)
            .attr("stroke-width", 6)
            .attr("stroke", "black")
            .attr("class", "lfsr3-line");

        lfsr3.append("line")
            .attr("x1", 700)
            .attr("x2", 700)
            .attr("y1", -170)
            .attr("y2", -25)
            .attr("stroke-width", 4)
            .attr("stroke", "black")
            .attr("class", "lfsr3-line");

        lfsr3.append("line")
            .attr("x1", 300)
            .attr("x2", 700)
            .attr("y1", -25)
            .attr("y2", -25)
            .attr("stroke-width", 4)
            .attr("stroke", "black")
            .attr("class", "lfsr3-line");

        lfsr3.append("line")
            .attr("x1", 300)
            .attr("x2", 300)
            .attr("y1", -25)
            .attr("y2", 10)
            .attr("stroke-width", 4)
            .attr("stroke", "black")
            .attr("class", "lfsr3-line");

        lfsr3.append("line")
            .attr("x1", r1Width)
            .attr("x2", r1Width + 200)
            .attr("y1", r1Height-50)
            .attr("y2", r1Height-50)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

    }

    function btnsInit() {
        $('#nextStepBtn').click(function () {
            update();
        });

        $('#createRegisterBtn').click(function () {
            CreateRegister(1, 'registerText1', 'feedbackFunctionViz1', 'lfsr1', 'Rejestr 1', 'F1');
            CreateRegister(2, 'registerText2', 'feedbackFunctionViz2', 'lfsr2', 'Rejestr 2', 'F2');
            CreateRegister(3, 'registerText3', 'feedbackFunctionViz3', 'lfsr3', 'Rejestr 3', 'F3');
            drawGates();
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

    $('#createRegisterBtn').click();
}