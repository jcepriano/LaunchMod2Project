﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();

        public User(string name, string username)
        {
            Name = name;
            Username = username;
        }
    }
}
