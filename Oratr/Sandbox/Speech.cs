using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Speech.Recognition;
using System.Globalization;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Oratr.Sandbox
{
    public partial class Form1 : Form
    {

        private void Form1_Load(object sender, EventArgs e)
        {
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine(new CultureInfo("en-US"));
            sre.SetInputToDefaultAudioDevice();

            Choices colors = new Choices();
            colors.Add(new string[] { "red", "green", "blue" });

            GrammarBuilder gb = new GrammarBuilder();

            gb.Append(colors);

            Grammar g = new Grammar(gb);

            sre.LoadGrammar(g);

            sre.SpeechRecognized +=
                new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);

            sre.Recognize();
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show("Speech Recognized: " + e.Result.Text);
        }
    }

}