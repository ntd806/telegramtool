using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace telegramtool
{
    public class userIO
    {
        public List<User> users;
        public void readUserByFile(string path)
        {
            this.users = new List<User>();
            using (var file = new StreamReader(path))
            {
                string line;
                int id = 1;
                while ((line = file.ReadLine()) != null)
                {
                    var user = convertString(line, id);
                    this.users.Add(user);
                    id++;
                }

                file.Close();
            }

        }

        public User convertString(string line, int id)
        {
            string[] subs = line.Split(',');
            User user = new User(id,subs[0],subs[1], subs[2], subs[3], subs[4], subs[5], subs[6], subs[7], subs[8], subs[9]);

            return user;
        }

        public void WriteUserByFile(string path)
        {
            StreamWriter writer = new StreamWriter(path);
            foreach (var user in this.users)
            {
                Console.WriteLine(user.toString());
                writer.WriteLine(user.toString());
            }
            writer.Close();
        }

        public void readUserByString(string text)
        {
            StringReader sr = new StringReader(text);
            string line;
           

            if (this.users != null)
            {
                int id = this.users.Count() + 1;
                while ((line = sr.ReadLine()) != null)
                {
                    var user = convertString(line, id);
                    this.users.Add(user);
                    id++;
                }
            }
            else
            {
                this.users = new List<User>();
                int id = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    var user = convertString(line, id);
                    this.users.Add(user);
                    id++;
                }
            }
        }

        public void addUser(User user)
        {
            this.users.Add(user);
        }
        public void removeUser(int id)
        {
            var itemToRemove = this.users.Single(r => r.Id == id);
            if (itemToRemove != null)
            {
                this.users.Remove(itemToRemove);
            }
        }
        public void editUser(User user, int id)
        {
            var index = this.users.FindIndex(p => p.Id == id);
            this.users[index] = user;
        }

        public string getPath()
        {
            string fileName = "filesystem.txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"file\", fileName);
            return path;
        }
    }
    public class User
    {
        private int _id = 0;
        private string _UID = "";
        private string _name = "";
        private string _email = "";
        private string _password = "";
        private string _ld = "";
        private string _group = "";
        private string _friend = "";
        private string _privateKey = "";
        private string _note = "";
        private string _status = "";
        private ulong _phone = 0;
        private int _code = 0;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string UID
        {
            get { return _UID; }
            set { _UID = value; }

        }

        public int Code
        {
            get { return _code; }
            set { _code = value; }

        }
        public ulong Phone
        {
            get { return _phone; }
            set { _phone = value; }

        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }

        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }

        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }

        }

        public string Ld
        {
            get { return _ld; }
            set { _ld = value; }

        }
        public string Group
        {
            get { return _group; }
            set { _group = value; }

        }
        public string Friend
        {
            get { return _friend; }
            set { _friend = value; }

        }
        public string PrivateKey
        {
            get { return _privateKey; }
            set { _privateKey = value; }

        }

        public string Note
        {
            get { return _note; }
            set { _note = value; }

        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }

        }

        public User() {}
        public User(int id, string UID, string name, string email, string password, string ld, string group, string friend, string privateKey, string note, string status)
        {
            this._id = id;
            this._UID = UID;
            this._name = name;
            this._email = email;
            this._password = password;
            this._ld = ld;
            this._group = group;
            this._friend = friend;
            this._privateKey = privateKey;
            this._note = note;
            this._status = status;
            this.getcode();
        }
        public string toString()
        {
            string s = "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}";
            return String.Format(s, this.UID, this.Name, this.Email, this.Password, this.PrivateKey, this.Friend, this.Group, this.Ld, this.Note, this.Status);
        }
        private void getcode()
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(this.UID, null);
            this.Code = phoneNumber.CountryCode;
            this.Phone = phoneNumber.NationalNumber;
        }
    }
}
