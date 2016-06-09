﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oratr.Models;
using System.Data.Entity;

namespace Oratr.DAL
{
    public class OratrRepository
    {
        public OratrContext context { get; set; }
        public IDbSet<ApplicationUser> Users { get { return context.Users; } }

        public OratrRepository()
        {
            context = new OratrContext();
        }

        public OratrRepository(OratrContext _context)
        {
            context = _context;
        }

        public List<Speech> GetSpeeches()
        {
            return context.Speeches.ToList();
        }

        public int GetSpeechCount()
        {
            return context.Speeches.Count();
        }

        public void AddSpeech(string speechTitle, string speechBody, ApplicationUser created_by)
        {
            //these fields need to be fully fleshed out
            Speech new_speech = new Speech { SpeechTitle = speechTitle, SpeechBody = speechBody, CreatedBy = created_by};
            context.Speeches.Add(new_speech);
            context.SaveChanges();

        }

        public Speech GetSpeech(int _speech_id)
        {
            Speech speech;
            try
            {
                speech = context.Speeches.First(s => s.SpeechId == _speech_id);
            } catch (Exception)
            {
                throw new NotFoundException();
            }
            return speech;
        }

        public int StringLength(string some_string)
        {
            char[] delimiterChars = { ' ' };
            string[] some_string_array = some_string.Split(delimiterChars);
            int stringLength = some_string_array.Length;
            return stringLength;
        }

        public void CalculateDeliveryTime(ApplicationUser some_user, Speech found_speech)
        {
            // set char array to so as to split the string on spaces, this will ensure better accuracy when calculating wpm
            char[] delimiterChars = { ' ' };
            int wpm;
            if (some_user.UserWPM == 0)
            {
                wpm = 130;
            }
            else
            {
                wpm = some_user.UserWPM;
            }
            //get speech string and split so as to find speech length
            string speech_string = found_speech.SpeechBody;
            int speechLength = StringLength(speech_string);
            
            // calculate minute double so as to get timespan from minutes method
            double minutesDouble = speechLength / wpm;
            TimeSpan TargetDeliveryTime = TimeSpan.FromMinutes(minutesDouble);

            // set TargetDeliveryTime property
            found_speech.TargetDeliveryTime = TargetDeliveryTime;           
        }

        public void RemoveSpeech(int _speech_id)
        {
            Speech speech = GetSpeech(_speech_id);
            context.Speeches.Remove(speech);
            context.SaveChanges();

        }

        public void CalculateUserWPM(ApplicationUser some_user, string oneMinuteWordCount)
        {
            int speechLength = StringLength(oneMinuteWordCount);
            some_user.UserWPM = speechLength;
        }
    }

}