﻿using Microsoft.EntityFrameworkCore.Update.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SoW.Tracker.WebAPI.ServiceInterface
{
    public interface IEmailCommunication
    {
        public string EmailSend(string sowNo);
        public string EmailSend_TestArcReviewProcess(string testEmail, string ArcEmail, string sowNo);
        public string EmailSendManagerReview(string Email, string sowNo, string Manager);
    }
}







    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    









































































