using ClothBazar.database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ConfigurationService
    {
        public Config GetConfig(string Key)
        {
            using(var Context = new CBContext())
            {
                return Context.Configurations.Find(Key);
            }
        }
    }
}
