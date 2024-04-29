using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Auth
{
    public class RefreshToken
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsArchived { get; set; }
        public string Token { get; set; }
        public string RefreshKey { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
    }
}