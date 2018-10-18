using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{
    public class Ticket
    {
        public int id { get; set; }
        public int assignedUserId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string descriptionHtml { get; set; }
        public int priority { get; set; }
        public string priorityName { get; set; }
        public int status { get; set; }
        public string statusName { get; set; }
        public string ticketID { get; set; }
        public string resume { get; set; }
        public string estimatedDate { get; set; }
        public string changedOn { get; set; }
        public string createdOn { get; set; }
        public string estimatedOn { get; set; }

        public Ticket() { }

        public Ticket(int Id, int AssignedUserId, string Title, string Description, string DescriptionHtml, int Priority, string PriorityName, int Status, string StatusName,
            string TicketID, string Resume, string EstimatedDate, string ChangedOn, string CreatedOn, string EstimatedOn)
        {
            this.id = Id; this.assignedUserId = AssignedUserId; this.title = Title; this.description = Description; this.descriptionHtml = DescriptionHtml; this.priority = Priority;
            this.priorityName = PriorityName; this.status = Status; this.status = Status; this.statusName = StatusName; this.ticketID = TicketID; this.resume = Resume;
            this.estimatedDate = EstimatedDate; this.changedOn = ChangedOn; this.createdOn = CreatedOn; this.estimatedOn = EstimatedOn;
        }
    }
}
