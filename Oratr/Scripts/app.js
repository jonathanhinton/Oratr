//construct recognition object
var recognition = new webkitSpeechRecognition();
//define parameters
recognition.continuous = true;
recognition.interimResults = true;
recognition.lang = "en-US";
recognition.maxAlternatives = 1;

