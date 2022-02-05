using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using TravelGuide.Entities.Entity;

namespace TravelGuide.DAL.DataAccess
{
    public class MyInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            // Roles - Admin
            Role role_admin = new Role()
            {
                Name = "admin",
                CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                CreatedUsername = "admin"
            };
            
            // Roles - Standart
            Role role_standart = new Role()
            {
                Name = "standart",
                CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                CreatedUsername = "admin"
            };
            context.Roles.Add(role_admin);
            context.Roles.Add(role_standart);
            context.SaveChanges();

            //Admin Member
            Member admin_member = new Member()
            {
                Name = "Welat",
                Surname = "BARAN",
                Username = "wbaran",
                Email = "baranvelat021@gmail.com",
                Password = Crypto.SHA256("2121"),
                RePassword = Crypto.SHA256("2121"),
                RoleId = 1,
                ProfileImage = "user_boy.png",
                CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                CreatedUsername = "admin"
,
            };
            context.Members.Add(admin_member);

            //Standart Members with FakeData
            for (int i = 0; i < FakeData.NumberData.GetNumber(5, 10); i++)
            {
                Member standart_member = new Member()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Username = $"user_{i}",
                    Email = FakeData.NetworkData.GetEmail(),
                    Password = Crypto.SHA256("2121"),
                    RePassword = Crypto.SHA256("2121"),
                    RoleId = 2,
                    ProfileImage = "user_boy.png",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    CreatedUsername = "admin"
                };
                context.Members.Add(standart_member);
            }

            // About with FakeData
            About about = new About()
            {
                AboutImage = "about_bg.jpg",
                Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(100,150)),
                Description = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(20, 30)),
                CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                CreatedUsername = "admin"
            };
            context.About.Add(about);

            // Contact with FakeData
            Contact contact = new Contact()
            {
                Address = FakeData.PlaceData.GetAddress(),
                Email = FakeData.NetworkData.GetEmail(),
                Phone = FakeData.PhoneNumberData.GetPhoneNumber(),
                Github = "https://github.com/velatbaran",
                Twitter = "https://twitter.com/baranvelat021",
                Instagram = "https://www.instagram.com/baranvelat021/",
                Telegram = "https://t.me/baranvelat",
                CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                CreatedUsername = "admin"
            };
            context.Contact.Add(contact);
            context.SaveChanges();

            // Country with FakeData
            for (int i = 0; i < 5; i++)
            {
                Country country = new Country()
                {
                    Name = FakeData.PlaceData.GetCountry(),
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    CreatedUsername = "admin"
                };
                context.Countries.Add(country);

                // City with FakeData
                for (int j = 1; j < FakeData.NumberData.GetNumber(5, 9); j++)
                {
                    City city = new City()
                    {
                        Name = FakeData.PlaceData.GetCity(),
                        Image = $"sehir{j}",
                        Slogan = FakeData.TextData.GetSentence(),
                        DateInformation = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(5, 25)),
                        WanderPlaces = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(5, 25)),
                        Foods = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(5, 10)),
                        OtherInformations = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(5, 25)),
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        CreatedUsername = "admin"
                    };
                    country.Cities.Add(city);

                    // Comment with FakeData
                    for (int n = 0; n < FakeData.NumberData.GetNumber(3, 5); n++)
                    {
                        List<Member> memberList = context.Members.ToList();
                        Member owner_comment = memberList[FakeData.NumberData.GetNumber(0, memberList.Count() - 1)];
                        Comment comment = new Comment()
                        {
                            CommentText = FakeData.TextData.GetSentence(),
                            CommentDate = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            Owner = owner_comment,
                        };
                        city.Comments.Add(comment);
                    }

                    // Place with FakeData
                    for (int k = 0; k < FakeData.NumberData.GetNumber(2, 5); k++)
                    {
                        Place place = new Place()
                        {
                            Name = FakeData.PlaceData.GetStreetName(),
                            Image = $"sehir{k}",
                            Address = FakeData.PlaceData.GetAddress(),
                            Phone = FakeData.PhoneNumberData.GetPhoneNumber(),
                            Description = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(5, 25)),
                            RoadDescribe = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                            Latitude = FakeData.NumberData.GetDouble().ToString(),
                            Longitude = FakeData.NumberData.GetDouble().ToString(),
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            CreatedUsername = "admin"
                        };
                        city.Places.Add(place);

                        // RoadDescribeUnit with FakeData
                        for (int m = 0; m < FakeData.NumberData.GetNumber(2, 5); m++)
                        {
                            RoadDescribeUnit roadDescribeUnit = new RoadDescribeUnit()
                            {
                                Detail = FakeData.TextData.GetSentence(),
                                CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                                CreatedUsername = "admin"
                            };
                            place.RoadDescribeUnits.Add(roadDescribeUnit);
                        }
                    }

                }
            }
            context.SaveChanges();
        }
    }
}
