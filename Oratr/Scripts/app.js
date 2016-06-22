var app = angular.module("OratrApp", []);

app.controller("speechCtrl",
    ['countdown',
     'speechRecognition',
     function (countdown, speechRecog) {
         var self = this;

         self.startSpeech = function (e) {
             speechRecog.start();
             countdown.startTimer(1000);
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

}]);