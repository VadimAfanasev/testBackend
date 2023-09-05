using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBackend.Api.Models.Data;

namespace TestBackend.ApiTests
{
    internal class MockAppContext: ApplicationContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseInMemoryDatabase("PersonControllerTest")
        //        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseInMemoryDatabase("PersonControllerTest")
        //        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        //}

        public MockAppContext(DbContextOptions<ApplicationContext> options)
        : base(options => options.UseInMemoryDatabase("PersonControllerTest"))
        {

        }

    }
}
