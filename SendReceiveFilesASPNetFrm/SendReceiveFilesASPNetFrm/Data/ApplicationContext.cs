using SendReceiveFilesASPNetFrm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SendReceiveFilesASPNetFrm.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("ApplicationContext")
        {
           
        }

        public DbSet<FileModel> Files { get; set; }

    }
}