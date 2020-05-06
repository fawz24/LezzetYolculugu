using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LezzetYolculugu.Data
{
    public enum RolesEnum
    {
        Anonymous,
        Normal,
        Admin
    }

    public static class RolesRegistry
    {
        public static string Normal { get => "Normal"; }
        public static string Admin { get => "Admin"; }
        public static string Anonymous { get => "Anonymous"; }
    }
}
