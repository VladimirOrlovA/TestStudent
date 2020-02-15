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
        public string CheckDataBase(out int count)
        {
            // Проверяем существует ли файл на диске
            if (File.Exists(Path))
            {
                using (var ldb = new LiteDatabase(Path))
                {
                    // проверяем есть в БД таблица checkDB
                    if (ldb.CollectionExists("checkDB"))
                    {
                        var startCount = ldb.GetCollection<int>("checkDB");
                        startCount.Insert(1);
                        // кол-во записей в checkDB соответствует кол-ву запусков программы
                        count = startCount.Count();
                        return null;
                    }
                }
            }
            count = 0;
            return "Файл БД не найден";
        }
        string Path { get; set; }
        public DbConnection(string path)
        {
            this.Path = path;
        }

        public Exception AddUser(User newUser)
        {
            try
            {
                using (var ldb = new LiteDatabase(Path))
                {
                    var users = ldb.GetCollection<User>("user");
                    users.Insert(newUser);
                }
                return null;
            }
            catch (Exception e)
            {
                return e;
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
