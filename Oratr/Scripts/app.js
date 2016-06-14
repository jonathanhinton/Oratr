var $startRecorder = $("#startRecording");
var $stopRecorder = $("#stopRecording");
var $theSpeechText = $("#theSpeechText");
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
        recognition.stop();
        return;
    }

    for (var i = event.resultIndex; i < event.results.length; i++) {
        if (event.results[i].isFinal) {
            $theSpeechText.append(event.results[i][0].transcript);
        } else {
            $theSpeechText.append(event.results[i][0].transcript);
        }
    } //end for loop
}; //end onresult function


$startRecorder.on('click', function () {
    console.log("recording started");
    recognition.start();
});

$stopRecorder.on('click', function () {
    console.log("recording ended");
    recognition.stop();
});