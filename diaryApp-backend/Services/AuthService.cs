using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using diaryApp_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;



namespace diaryApp_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly diaryAppContext db = new diaryAppContext();

        public AuthService()
        {
        }

        public async Task<User> Login(string email, string password) {

            var user = await db.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            if (user == null) {
                return null;
            }
            else
            {
                if (user.Password == password)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }

            return user;
        }

        public async Task<User> Register(User newUser, string password)
        {

    
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Fname = newUser.Fname,
                Lname = newUser.Lname,
                Nickname = newUser.Nickname,
                Birthdate = newUser.Birthdate,
                Email = newUser.Email,
                Password = password
            };


            try
            {


                db.Add(user);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException err) {
                throw err;
            }

            return newUser;
        }

    }


}
