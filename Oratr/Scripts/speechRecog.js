app.service('speechRecognition', [function(){

    //construct recognition object
    var SpeechRecognition = SpeechRecognition || webkitSpeechRecognition;
    // var SpeechGrammarList = SpeechGrammarList || webkitSpeechGrammarList; <-- I will implement this later
    var recognition = new SpeechRecognition();

    //create empty array to store results of final speech
    var result = [];
    var resultString;
    var langSelect = $("#languageSelector");
    var countdownDiv = $("#countdown");
    //console.log(langSelect.val());

    //define parameters
    recognition.continuous = true;
    recognition.interimResults = true;
    recognition.lang="en-US"
    recognition.maxAlternatives = 1;

    recognition.onstart = function () {
        //visible feedback goes here
    };

    recognition.onend = function () {
        //again visible feedback goes here
    };

    recognition.onresult = function (event) {
        $("#theSpeechTest").html('');
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
        $("#theSpeechTest").html(resultString);
        //return resultString;
    }; //end onresult function
    function startRecognition() {
        console.log("start");
        recognition.start();
    }

    function stopRecognition() {
        console.log("stop");
        recognition.stop();
    }

    function clearSpeech() {
        result = [];
        resultString = '';
    }

    function langToggle() {
        if(langSelect.val() === "English")
        {
            recognition.lang = "ru";
            langSelect.html("Russian");
            langSelect.attr("value", "Russian");
            //console.log("now Russian");
        } else
        {
            recognition.lang = "en-US";
            langSelect.html("English")
            langSelect.attr("value", "English");
            //console.log("now English");
        }
    }
    return {
        start: startRecognition,
        stop: stopRecognition,
        clearSpeech : clearSpeech,
        langToggle : langToggle
    }
}]);