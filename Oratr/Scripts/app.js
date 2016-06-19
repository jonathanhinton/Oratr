var app = angular.module("OratrApp", []);

app.controller("speechCtrl",
    ['countdown',
     'speechRecognition',
     function (countdown, speechRecog) {
         var self = this;

         self.startSpeech = function (e) {
             speechRecog.start();
         }

         self.stopSpeech = function (e) {
             speechRecog.stop();
         }
}]);