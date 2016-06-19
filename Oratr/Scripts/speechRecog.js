app.service('speechRecognition', [function(){

    //construct recognition object
    var SpeechRecognition = SpeechRecognition || webkitSpeechRecognition;
    // var SpeechGrammarList = SpeechGrammarList || webkitSpeechGrammarList; <-- I will implement this later
    var recognition = new SpeechRecognition();
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
                //$theSpeechText.append("<b>" + event.results[i][0].transcript + "</b>");
            } else {
                console.log("interim result: " + event.results[i][0].transcript);
            }
        } //end for loop
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