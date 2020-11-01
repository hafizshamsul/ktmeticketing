using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Assignment1_MuhammadHafiz.Models
{
    public class User
    {
        [Key]
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        //[Required(ErrorMessage = "Enter a name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Enter an email")]
        [EmailAddress(ErrorMessage = "Enter an appropriate email")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Enter a password")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Enter an IC/Passport number")]
        public string ICpass { get; set; }

        public string Role { get; set; }

        [Display(Name = "Registered")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d MMM yyyy}")]
        public DateTime DateRegistered { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }

    public class Ticket
    {
        [Key]
        [Display(Name = "Ticket Id")]
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Select an origin")]
        public string StartPoint { get; set; }

        [Required(ErrorMessage = "Select a destination")]
        public string EndPoint { get; set; }

        public string Category { get; set; }

        public string WayType { get; set; }

        [Required(ErrorMessage = "Enter a quantity")]
        [Range(1, 12, ErrorMessage = "Invalid ticket quantity")]
        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "RM{0:n2}")]
        public double InitialPrice { get; set; }

        [DisplayFormat(DataFormatString = "RM{0:n2}")]
        public double BaseDiscountRate { get; set; }

        [DisplayFormat(DataFormatString = "RM{0:n2}")]
        public double CatDiscountRate { get; set; }

        [DisplayFormat(DataFormatString = "RM{0:n2}")]
        public double TotalPrice { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy, h:mm tt}")]
        public DateTime DateOfPurchase { get; set; }

        public virtual User Users { get; set; }
    }

    public class StoreContext : DbContext
    {
        public StoreContext() : base("name=StoreContext") // constructor
        { // optional for explicit naming of
        } // connection string
        public DbSet<User> User { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}