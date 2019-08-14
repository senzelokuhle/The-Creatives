namespace TheCreatives.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class i : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointment",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        Description = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        ThemeColor = c.String(),
                        IsFullDay = c.Boolean(nullable: false),
                        Email = c.String(),
                        Cellphone = c.String(),
                        EMailBody = c.String(),
                        EmailSubject = c.String(),
                        EmailCC = c.String(),
                        EmailBCC = c.String(),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.DermatologyServices",
                c => new
                    {
                        DemServiceId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DemServiceId);
            
            CreateTable(
                "dbo.DermatologistsDoctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProfId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DoctorId)
                .ForeignKey("dbo.DoctorProfesions", t => t.ProfId, cascadeDelete: true)
                .Index(t => t.ProfId);
            
            CreateTable(
                "dbo.DoctorProfesions",
                c => new
                    {
                        ProfId = c.Int(nullable: false, identity: true),
                        ProfName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProfId);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        GenderId = c.Int(nullable: false, identity: true),
                        _Gender = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GenderId);
            
            CreateTable(
                "dbo.PatientRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        PatientName = c.String(nullable: false),
                        DateOfBirth = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        HomeTelephone = c.String(nullable: false, maxLength: 10),
                        CellPhone = c.String(nullable: false, maxLength: 10),
                        IdentityNo = c.String(nullable: false),
                        Employer = c.String(),
                        WorkNumber = c.String(nullable: false, maxLength: 10),
                        EmergencyContact = c.String(nullable: false, maxLength: 10),
                        NextOfKin = c.String(),
                        HomePhone = c.String(nullable: false, maxLength: 10),
                        InsuranceName = c.String(),
                        PolicyNumber = c.String(),
                        InsuranceNumber = c.String(nullable: false, maxLength: 10),
                        GenderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .Index(t => t.GenderId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SiteUser",
                c => new
                    {
                        SiteUserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(),
                        IsValid = c.Boolean(),
                    })
                .PrimaryKey(t => t.SiteUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PatientRecords", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.DermatologistsDoctors", "ProfId", "dbo.DoctorProfesions");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PatientRecords", new[] { "GenderId" });
            DropIndex("dbo.DermatologistsDoctors", new[] { "ProfId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SiteUser");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PatientRecords");
            DropTable("dbo.Genders");
            DropTable("dbo.DoctorProfesions");
            DropTable("dbo.DermatologistsDoctors");
            DropTable("dbo.DermatologyServices");
            DropTable("dbo.Appointment");
        }
    }
}
