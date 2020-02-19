using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LiteDB;

namespace DbOperation.lib
{
    public class DbConnection
    {
        string Path { get; set; }
        public DbConnection(string path)
        {
            this.Path = path;
        }

        public string CheckDataBase()
        {
            // Проверяем существует ли файл на диске
            if (File.Exists(Path))
            {
                using (var ldb = new LiteDatabase(Path))
                {
                    // проверяем есть ли в БД документ User
                    var excb = ldb.CollectionExists("User");
                    if (ldb.CollectionExists("User"))
                    {
                        var checkFirstRecord = ldb.GetCollection<User>("User");
                        return null;
                    }
                    return "Внимание! Для продолжения работы с программой необходимо зарегистрировать первого пользователя.";
                }
            }
            return "Внимание! Файл БД не найден." +
                   "\n\nВозможная причина:\n" +
                   " - первый запуск программы\n" +
                   " - файл БД был удален\n";
        }

        public string AddUser(User newUser)
        {
            try
            {
                using (var ldb = new LiteDatabase(Path))
                {
                    var users = ldb.GetCollection<User>("user");

                    User searchUser = users.FindOne(f => f.Login == newUser.Login);

                    if (searchUser == null)
                        users.Insert(newUser);
                    else
                    {
                        return "Пользователь с таким именем уже существует";
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return "Ошибка " + e;
            }
        }

        public User GetUser(string login, string password, out Exception exc)
        {
            User userDB = null;
            try
            {
                using (var ldb = new LiteDatabase(Path))
                {
                    userDB = ldb.GetCollection<User>("user").FindOne(user => user.Login == login && user.Password == password);
                }
                exc = null;
                return userDB;
            }
            catch (Exception e)
            {
                exc = e;
                return userDB;
            }
        }

        public User GetUserById(int userId)
        {
            User userDB = null;

            using (var ldb = new LiteDatabase(Path))
            {
                userDB = ldb.GetCollection<User>("user").FindById(userId);
                return userDB;
            }
        }

        public void AddLoggedHistory(User user)
        {
            using (var ldb = new LiteDatabase(Path))
            {
                var loggedUser = new LoggedHistory(user.Id);
                var loggedHistory = ldb.GetCollection<LoggedHistory>("LoggedHistory");
                loggedHistory.Insert(loggedUser);
            }
        }

        public User GetUserInfo()
        {
            User user = new User();

            using (var ldb = new LiteDatabase(Path))
            {
                // подсчет кол-ва входов пользователя для статистики
                var records = ldb.GetCollection<LoggedHistory>("LoggedHistory").FindAll().ToList();
                int currentUserId = records.Last().LoggedUserId;
                var recordsByCurrentUserId = ldb.GetCollection<LoggedHistory>("LoggedHistory").Find(f => f.LoggedUserId == currentUserId);
                user.LoginCount = recordsByCurrentUserId.Count();
            }

            return user;
        }

//--------------------------------------------------------------------------------------------------------

        public string AddSubject(Subject newSubject)
        {
            try
            {
                using (var ldb = new LiteDatabase(Path))
                {
                    var subjects = ldb.GetCollection<Subject>("Subject");

                    Subject searchUser = subjects.FindOne(f => f.Name == newSubject.Name);

                    if (searchUser == null)
                        subjects.Insert(newSubject);
                    else
                        return "Пользователь с таким именем уже существует";
                }
                return null;
            }
            catch (Exception e)
            {
                return "Ошибка " + e;
            }
        }

        public List<Subject> GetSubjects()
        {
            List<Subject> subjects = null;

            using (var ldb = new LiteDatabase(Path))
            {
                subjects = ldb.GetCollection<Subject>("Subject").FindAll().ToList();
            }

            return subjects;
        }

        //public User GetUser(string login, string password, out Exception exc)
        //{
        //    User userDB = null;
        //    try
        //    {
        //        using (var ldb = new LiteDatabase(Path))
        //        {
        //            userDB = ldb.GetCollection<User>("user").FindOne(user => user.Login == login && user.Password == password);
        //        }
        //        exc = null;
        //        return userDB;
        //    }
        //    catch (Exception e)
        //    {
        //        exc = e;
        //        return userDB;
        //    }
        //}

        //public User GetUserById(int userId)
        //{
        //    User userDB = null;

        //    using (var ldb = new LiteDatabase(Path))
        //    {
        //        userDB = ldb.GetCollection<User>("user").FindById(userId);
        //        return userDB;
        //    }
        //}

        //public void AddLoggedHistory(User user)
        //{
        //    using (var ldb = new LiteDatabase(Path))
        //    {
        //        var loggedUser = new LoggedHistory(user.Id);
        //        var loggedHistory = ldb.GetCollection<LoggedHistory>("LoggedHistory");
        //        loggedHistory.Insert(loggedUser);
        //    }
        //}

        //public User GetUserInfo()
        //{
        //    User user = new User();

        //    using (var ldb = new LiteDatabase(Path))
        //    {
        //        // подсчет кол-ва входов пользователя для статистики
        //        var records = ldb.GetCollection<LoggedHistory>("LoggedHistory").FindAll().ToList();
        //        int currentUserId = records.Last().LoggedUserId;
        //        var recordsByCurrentUserId = ldb.GetCollection<LoggedHistory>("LoggedHistory").Find(f => f.LoggedUserId == currentUserId);
        //        user.LoginCount = recordsByCurrentUserId.Count();
        //    }

        //    return user;
        //}
    }


}
