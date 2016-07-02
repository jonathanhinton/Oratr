﻿var app = angular.module("OratrApp", []);

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

<<<<<<< Updated upstream
=======
         self.postUserText = function () {
             var speechText = $("#theSpeechTest").html();
             var userString = {
                 stringText: speechText
             }
             console.log(speechText);
             $http({
                 method: 'POST',
                 data: userString,
                 url: '/Speech/SetWPM'
             }).then(function successCallback(response) {
                 console.log("response", response);
             }, function errorCallback(response) {
             });
         }

>>>>>>> Stashed changes
         //Edit the text...for future use.
         function speechClicked() {
             var speechText = $("#theSpeechTest").html();
             var editableText = $("<textarea id='textToChange'/>")
             editableText.val(speechText);
             $("#theSpeechTest").replaceWith(editableText);
             editableText.focus();
             editableText.blur(editableTextBlurred);
         }

         function editableTextBlurred() {
             var html = $("#textToChange").val();
             var viewableText = $("<div id='theSpeechTest'>");
             viewableText.html(html);
             $("#textToChange").replaceWith(viewableText);
             $(viewableText).click(speechClicked);
         }

         $("#theSpeechTest").click(speechClicked);

}]);