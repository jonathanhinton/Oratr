var app = angular.module("OratrApp", []);

app.controller("speechCtrl",
    ['countdown',
     'speechRecognition',
     function (countdown, speechRecog) {
         var self = this;

         self.startSpeech = function (e) {
             speechRecog.start();
             countdown.startTimer();
         }

         self.stopSpeech = function (e) {
             speechRecog.stop();
             countdown.resetTimer();
             console.log("speech stopped");
         }

         self.toggleLang = function (e) {
             //console.log("click");
             speechRecog.langToggle();
         }

         self.clearSpeech = function (e) {
             speechRecog.clearSpeech();
             $("#theSpeechTest").html('');
         }

         //Edit the text...for future use.
         function speechClicked() {
             var speechText = $("#theSpeechTest").html();
             var editableText = $("<textarea id='textToChange'></textarea>")
             editableText.val(speechText);
             $("#theSpeechTest").replaceWith(editableText);
             editableText.focus();
             editableText.blur(editableTextBlurred);
         }

         function editableTextBlurred() {
             var html = $("#textToChange").val();
             var viewableText = $("<div id='theSpeechTest'></div>");
             viewableText.html(html);
             $("#textToChange").replaceWith(viewableText);
             $(viewableText).click(speechClicked);
         }

         $("#theSpeechTest").click(speechClicked);

}]);