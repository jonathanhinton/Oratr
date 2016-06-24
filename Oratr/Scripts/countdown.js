﻿app.service('countdown', [function () {
    var countdown = $("#countdown");
    var initial = 6000;
    var count = initial;
    var counter; //10 will  run it every 100th of a second

    function timer() {
        if (count <= 0) {
            clearInterval(counter);
            return;
        }
        count--;
        displayCount(count);
    }

    function displayCount(count) {
        var res = count / 100;
        countdown[0].innerHTML = res.toPrecision(count.toString().length) + " secs";
    }

    function startTimer() {
        //console.log("Timer Started");
        clearInterval(counter);
        counter = setInterval(timer, 10);
        displayCount(count);
    }

    function clearTimer() {
        clearInterval(counter);
    }

    function resetTimer() {
        //console.log("Timer reset");
        clearInterval(counter);
        count = initial;
        displayCount(count);
    }
    return {
        startTimer: startTimer,
        clearTimer: clearTimer,
        resetTimer: resetTimer
    };
}]);