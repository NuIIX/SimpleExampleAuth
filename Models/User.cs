using SimpleExampleAuth.Controllers;
using System.ComponentModel.DataAnnotations;

namespace SimpleExampleAuth.Models
{
    public interface IUser
    {
        void PublishingPost();
        void CommentOn();
        void LeaveReview();
        void Delete();
        void Change();
    }

    public class User : IUser
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DOB { get; set; }
        public long? Phone { get; set; }

        #region Methods
        public void PublishingPost()
        {
            Console.WriteLine($"{UserName} is publishing a post.");
        }

        public void CommentOn()
        {
            Console.WriteLine($"{UserName} is commenting on a post.");
        }

        public void LeaveReview()
        {
            Console.WriteLine($"{UserName} is leaving a review.");
        }

        public void Delete()
        {
            Console.WriteLine($"{UserName} is deleting a post.");
        }

        public void Change()
        {
            Console.WriteLine($"{UserName} is changing account settings.");
        }
        #endregion
    }

    public class UsersGenerator
    {
        private static readonly string[] _userName = { "gamer", "proWiner", "noobPlayer", "chessMaster", "pianoMan", "coder", "runner", "swimmer", "cyclist", "driver", "pilot", "captain", "engineer", "doctor", "nurse", "teacher", "student", "artist", "musician", "actor" };
        private static readonly string[] _email = { "@gmail.com", "@mail.ru", "@yahoo.com", "@outlook.com", "@yandex.ru", "@protonmail.com", "@icloud.com", "@zoho.com", "@aol.com", "@hotmail.com", "@inbox.ru", "@list.ru", "@bk.ru", "@internet.ru", "@msn.com", "@live.com", "@comcast.net", "@sbcglobal.net", "@verizon.net", "@earthlink.net" };
        private static readonly string?[] _firstName = { null, "Tom", "Bob", "Alice", "Charlie", "David", "Eve", "Frank", "Grace", "Helen", "Ivan", "Jack", "Karen", "Larry", "Mona", "Nick", "Oscar", "Paul", "Quincy", "Rita", "Steve", "Tina", "Uma", "Vince", "Wendy", "Xavier", "Yvonne", "Zack" };
        private static readonly string?[] _lastName = { null, "Jackson", "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson", "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez", "Moore", "Martin", "Jackson", "Thompson", "White", "Lopez", "Lee", "Gonzalez", "Harris", "Clark", "Lewis", "Robinson", "Walker", "Perez", "Hall", "Young", "Allen" };
        private static readonly DateTime?[] _dOB = { null, new DateTime(2000, 12, 11), new DateTime(2020, 2, 11), new DateTime(1995, 5, 15), new DateTime(1980, 7, 20), new DateTime(1975, 1, 30), new DateTime(1990, 3, 3), new DateTime(1985, 4, 4), new DateTime(2001, 5, 5), new DateTime(2002, 6, 6), new DateTime(2003, 7, 7), new DateTime(2004, 8, 8), new DateTime(2005, 9, 9), new DateTime(2006, 10, 10), new DateTime(2007, 11, 11), new DateTime(2008, 12, 12), new DateTime(2009, 1, 1), new DateTime(2010, 2, 2), new DateTime(2011, 3, 3), new DateTime(2012, 4, 4) };
        private static readonly string?[] _phone = { null, "913", "923", "933", "943", "953", "963", "973", "983", "993", "903", "912", "922", "932", "942", "952", "962", "972", "982", "992", "902" };

        private static readonly Random rand = new();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static async Task GenerateRandomUsersAsync(UserContext db, int countUsers)
        {
            for (int i = 0; i < countUsers; i++)
            {
                string userName = _userName[rand.Next(_userName.Length)] + rand.Next(100000);
                var user = new User
                {
                    UserName = userName,
                    Email = userName + _email[rand.Next(_email.Length)],
                    Password = AccountController.CreatePasswordHash(GeneratePassword(10)),
                    FirstName = _firstName[rand.Next(_firstName.Length)],
                    LastName = _lastName[rand.Next(_lastName.Length)],
                    DOB = _dOB[rand.Next(_dOB.Length)],
                    Phone = Convert.ToInt64("8" + _phone[rand.Next(_phone.Length)] + rand.Next(100000000))
                };

                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
        }

        private static string GeneratePassword(int length)
        {
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rand.Next(s.Length)]).ToArray());
        }
    }
}
