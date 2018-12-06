var LfsrVisualizationInit = function() {

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

    $('#createRegisterBtn').click(function () {

        var register = $('#registerText').val();
        if (isBinaryString(register)) {
            if (register.length > 10) {
                alert('Maksymalna dozwolona długość rejestru wynosi: 10!');
                return;
            }

            if (register.length < 2) {
                alert('Minimalna dozwolona długość rejestru wynosi: 2!');
                return;
            }

            var feedbackFunction = $('#feedbackFunctionViz').val();
            var polynomial = feedbackFunction.split(',');
            polynomial = polynomial.unique();
            if (isCommaSeparatedNumber(polynomial)) {

                registerContent = register.split('').map(Number);
                feedbackContent = polynomial.map(Number);

                if ((Math.max(...feedbackContent)) > registerContent.length) {
                    alert('Maksymalny stopień wielomanu przy tym rejestrze może wynosić: ' + registerContent.length);
                    return;
                }
                $('#feedbackFunctionViz').val(polynomial.join(','));
                CreateLfsr(registerContent, feedbackContent);

            } else {
                alert('Niepoprawny format wielomianu!');
                return;
            }


        } else {
            alert('Rejestr musi mieć wartości 0 lub 1!');
            return;
        }

    });


    function CreateLfsr(data, functions) {

        document.getElementById('lfsrSvg').innerHTML = '';

        var xorBits = [];

        var lastIndex = data.length - 1;

        var maxWidth = 800;
        var maxHeight = 100;

        var width = maxWidth / data.length;

        var textX = width;
        var textY = maxHeight * 1.5;

        var svg = d3.select('#lfsrSvg');


        var groups = svg.selectAll("g")
            .data(data)
            .enter()
            .append("g")
            .attr("class", ".cell")
            .attr("id",
                function (d, i) {
                    return 'cell' + i;
                })
            .attr("transform", "translate(100,0)");

        var rects = groups.append("rect")
            .attr("x",
                function (d, i) {
                    return i * width;
                })
            .attr("y", 100)
            .attr("width", width)
            .attr("height", maxHeight)
            .attr("class", "register-rect");


        svg.append("line")
            .attr("x1", maxWidth + 100)
            .attr("y1", 150)
            .attr("x2", maxWidth + 165)
            .attr("y2", 150)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        svg.append("line")
            .attr("x1", maxWidth + 160)
            .attr("y1", 150)
            .attr("x2", maxWidth + 160)
            .attr("y2", 300)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        svg.append("line")
            .attr("x1", maxWidth + 160)
            .attr("y1", 300)
            .attr("x2", 40)
            .attr("y2", 300)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        svg.append("line")
            .attr("x1", 40)
            .attr("y1", 300)
            .attr("x2", 40)
            .attr("y2", 150)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        svg.append("line")
            .attr("x1", 40)
            .attr("y1", 150)
            .attr("x2", 100)
            .attr("y2", 150)
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
                    return (d - 1) * (width)+width / 2;
                })
            .attr("y1", 200)
            .attr("x2",
                function (d, i) {
                    return (d - 1) * (width)+width/2;
                })
            .attr("y2", 320)
            .attr("stroke-width", 4)
            .attr("stroke", "black");

        fLines.append('circle')
            .attr("cx",
                function (d, i) {
                    return (d-1) * width + width / 2;
                })
            .attr("cy", 300)
            .attr("stroke", "black")
            .attr("stroke-width", 3)
            .attr("fill-opacity", 0)
            .attr("r", 20);

        var outputArray = [];

        var feedbackText = svg.append('text')
            .attr('id', 'feedbackFunction')
            .attr('x', 100)
            .attr('y', 350)
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
            .attr('x', 975)
            .attr('y', 150)
            .text(outputArray)
            .attr("class", "register-cell-text");


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
                })
        }

        function update() {

            $('#nextStepBtn').attr("disabled", true);

            lastItem.select("text").transition().duration(500)
                .attr("y", 150)
                .attr("x", 875);

            var newBit = 0;
            for (var i = 0; i < xorBits.length; i++) {
                newBit ^= xorBits[i];
            }
            var output = data.pop();
            data.unshift(newBit);

            outputArray.unshift(output);

            var groups = svg.selectAll("g").data(data);

            var outputBits = svg.select('#outputBits');

            groups.select("text")
                .transition()
                .delay(1000)
                .duration(0)
                .text(function (d) { return d; })
                .attr("x",
                    function (d, i) {
                        $('#nextStepBtn').attr("disabled", false);

                        outputBits.text(function () {
                            var txt = outputArray[0].toString();
                            return txt;
                        });
                        changeFeedbackText();

                        if (outputArray.length > 1) {
                            outputArray.pop();
                        }
                        return i * textX + width / 2;
                    })
                .attr("y", textY);

            xorBits = []
            cellsForLines.each(function (d) {
                xorBits.push(d);
            });
        }

        function btnsInit() {
            $('#nextStepBtn').click(function () {
                update();
            });
        }

        btnsInit();

    }

    $('#createRegisterBtn').click();
}