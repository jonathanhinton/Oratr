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

        public void TargetDeliveryTimeCalculator(ApplicationUser created_by)
        {
            int wpm;
            if (created_by.UserWPM == 0)
            {
                wpm = 130;
            }
            else
            {
                wpm = created_by.UserWPM;
            }
        }
    }
}