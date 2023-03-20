using Microsoft.EntityFrameworkCore;
using Carwale.Domain.Entities;


namespace Carwale.Domain
{
    public class DbInitializer
    {
        private readonly IConfiguration _configuration;
        private readonly CwDbContext _context;

        public DbInitializer(IConfiguration config, CwDbContext context)
        {
            this._configuration = config;
            this._context = context;
        }

        public async Task MigrateDbsAsync()
        {
            if (_configuration["RunDbMigrations"].ToLower() == "true")
                await _context.Database.MigrateAsync();
        }

        public async Task SeedDataAsync()
        {
            string defaultPassword = "Abc@12345";
            if (_context.Tenants.Count() == 0)
            {
                _context.Tenants.AddRange(
                    new Tenant
                    {
                        UId = UIdGenerator.GenerateUId(),
                        Name = "XBoss Car Company",
                        Users = new List<CwUser>
                        {
                            new CwUser
                            {
                                 UId=UIdGenerator.GenerateUId(),
                                 Password=defaultPassword,
                                 Name="Manoj XBoss",
                                 UserName="muser1"
                            },
                            new CwUser
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Password=defaultPassword,
                                Name="Ram XBoss",
                                UserName="muser2"
                            }
                        }
                    }
                );

                _context.Tenants.AddRange(
                    new Tenant
                    {
                        UId = UIdGenerator.GenerateUId(),
                        Name = "Zoom Car",
                        Users = new List<CwUser>
                        {
                            new CwUser
                            {
                                 UId=UIdGenerator.GenerateUId(),
                                 Password=defaultPassword,
                                 Name="Shyarm Zoom",
                                 UserName="muser3"
                            },
                            new CwUser
                            {
                                 UId=UIdGenerator.GenerateUId(),
                                 Password=defaultPassword,
                                 Name="Ram Zoom",
                                 UserName="muser4"
                            }
                        }
                    }
                );
            }

            await _context.SaveChangesAsync();


            if (_context.Makes.Count() == 0)
            {
                _context.Makes.AddRange(
                    new Make
                    {
                        Name = "Maruti",
                        UId = UIdGenerator.GenerateUId(),
                        Models = new List<Model>
                        {
                            new Model
                            {
                                Name="Swif",
                                UId=UIdGenerator.GenerateUId(),
                            },
                            new Model
                            {
                                Name="Swift Dezire",
                                UId=UIdGenerator.GenerateUId(),
                            },
                            new Model
                            {
                                Name="Baleno",
                                UId=UIdGenerator.GenerateUId(),
                            },
                        }
                    },
                    new Make
                    {
                        Name = "BMW",
                        UId = UIdGenerator.GenerateUId(),
                        Models = new List<Model>
                        {
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="X1"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="X3"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="X7"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="5 Series"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="7 Series"
                            },
                        }
                    },
                    new Make
                    {
                        Name = "Honda",
                        UId = UIdGenerator.GenerateUId(),
                        Models = new List<Model>
                        {
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="City"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="WRV"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="Accord"
                            },
                        }
                    },
                    new Make
                    {
                        UId = UIdGenerator.GenerateUId(),
                        Name = "Hyundai",
                        Models = new List<Model>
                        {
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="Verna"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="Creta"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="i10"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="i20"
                            },
                        }
                    },
                    new Make
                    {
                        Name = "Tata",
                        UId = UIdGenerator.GenerateUId(),
                        Models = new List<Model>
                        {
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="Indica"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="Safari"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="Harrier"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="Nexon"
                            },
                        }
                    },
                    new Make
                    {
                        UId = UIdGenerator.GenerateUId(),
                        Name = "Audi",
                        Models = new List<Model>
                        {
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="A3"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="A5"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="A8"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="Q3"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="Q5"
                            },
                            new Model
                            {
                                UId=UIdGenerator.GenerateUId(),
                                Name="Q7"
                            },
                        }
                    }
                );

                await _context.SaveChangesAsync();
            }
        }

    }
}
