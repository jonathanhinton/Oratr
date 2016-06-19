app.service('speechRecognition', [function(){

    //construct recognition object
    var SpeechRecognition = SpeechRecognition || webkitSpeechRecognition;
    // var SpeechGrammarList = SpeechGrammarList || webkitSpeechGrammarList; <-- I will implement this later
    var recognition = new SpeechRecognition();

    //create empty array to store results of final speech
    var result = [];
    var resultString;
    var speechEl = angular.element(document.getElementById('theSpeechTest'));

    //define parameters
    recognition.continuous = true;
    recognition.interimResults = true;
    recognition.lang = "en-US";
    recognition.maxAlternatives = 1;

    recognition.onstart = function () {
        //visible feedback goes here
    };

    recognition.onend = function () {
        //again visible feedback goes here
    };

    recognition.onresult = function (event) {
        if (typeof (event.results) === 'undefined') {
            console.log("undefined speech");
            recognition.stop();
            return;
        }

        for (var i = event.resultIndex; i < event.results.length; i++) {
            if (event.results[i].isFinal) {
                console.log("I think you said: " + event.results[i][0].transcript);
                result.push(event.results[i][0].transcript);
                console.log("result array contains: " + result);
            } else {
                //console.log("interim result: " + event.results[i][0].transcript);
            }
        } //end for loop
        resultString = result.join(' ');
        speechEl.html(resultString);
        return resultString;
    }; //end onresult function
    function startRecognition() {
        recognition.start();
    }
    function stopRecognition() {
        recognition.stop();
    }
    return {
        start: startRecognition,
        stop: stopRecognition
    }
}]);