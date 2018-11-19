using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{
    public class Tasks
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string descriptionHtml { get; set; }
        public int assignedUserId { get; set; }
        public string deadline { get; set; }
        public int deadlineType { get; set; }
        public int clientId { get; set; }
        public int contactPersonId { get; set; }
        public int status { get; set; }
        public int reminderInterval { get; set; }
        public bool inCalendar { get; set; }
        public int parentTask { get; set; }
        public string fromDate { get; set; }
        public int createdBy { get; set; }
        public string createdOn { get; set; }
        public int changedBy { get; set; }
        public string changedOn { get; set; }
        public int version { get; set; }

        
        public string contactPersonName { get; set; }       
        public string customProperties { get; set; }
        

        public Tasks() { }

        public Tasks(int id, string title, string description, string descriptionHtml, int assignedUserId, string deadline, int deadlineType, int clientId, int contactPersonId, int status, int reminderInterval, bool inCalendar, int parentTask, string fromDate, int createdBy, string createdOn, int changedBy, string changedOn, int version)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.descriptionHtml = descriptionHtml;
            this.assignedUserId = assignedUserId;
            this.deadline = deadline;
            this.deadlineType = deadlineType;
            this.clientId = clientId;
            this.contactPersonId = contactPersonId;
            this.status = status;
            this.reminderInterval = reminderInterval;
            this.inCalendar = inCalendar;
            this.parentTask = parentTask;
            this.fromDate = fromDate;
            this.createdBy = createdBy;
            this.createdOn = createdOn;
            this.changedBy = changedBy;
            this.changedOn = changedOn;
            this.version = version;
        }
        
    }
    public class Items
    {
        public int id { get; set; }
        public IList<Tasks> items { get; set; }
        public int total { get; set; }
    }
}
