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

        public string CheckDataBase(out int startCount)
        {
            // Проверяем существует ли файл на диске
            if (File.Exists(Path))
            {
                using (var ldb = new LiteDatabase(Path))
                {
                    // проверяем есть ли в БД документ LoggedHistory
                    var excb = ldb.CollectionExists("LoggedHistory");
                    if (ldb.CollectionExists("LoggedHistory"))
                    {
                        var logInfo = ldb.GetCollection<LoggedHistory>("LoggedHistory");
                        // получаем кол-во записей входа в программу
                        startCount = logInfo.Count();
                        return null;
                    }
                    startCount = 0;
                    return "Внимание! Данные в БД изменены/повреждены вне программы!";
                }
            }
            startCount = 0;
            return "Внимание! Файл БД не найден!";
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

        public User GetUserById(int userId, out Exception exc)
        {
            User userDB = null;
            try
            {
                using (var ldb = new LiteDatabase(Path))
                {
                    userDB = ldb.GetCollection<User>("user").FindById(userId);
                    exc = null;
                    return userDB;
                }
            }
            catch (Exception e)
            {
                exc = e;
                return userDB;
            }
        }

    }
}
