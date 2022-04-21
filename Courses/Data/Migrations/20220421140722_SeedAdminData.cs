using Microsoft.EntityFrameworkCore.Migrations;

namespace Courses.Data.Migrations
{
    public partial class SeedAdminData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'9aedc898-c2ff-4276-b353-17f3ea450e0f', N'admin', N'ADMIN', N'e05f2065-3611-4f49-b2e6-f409d8c26f54')
SET IDENTITY_INSERT [dbo].[Universities] ON
INSERT INTO [dbo].[Universities] ([Id], [Name]) VALUES (1, N'Zagazig')
SET IDENTITY_INSERT [dbo].[Universities] OFF

INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Discriminator], [UniversityId]) VALUES (N'5541d758-57a0-46b3-b97d-91046320c15e', N'admin', N'ADMIN', N'admin@advisor.com', N'ADMIN@ADVISOR.COM', 0, N'AQAAAAEAACcQAAAAEIq3bx5akFeUQCh/AX1gwbXFKIwCjBxagwGBmopJsXQ0hN0t5qR/de713gGXi3uUXw==', N'XNBLHHNGWNQYWYTZXMXT7ZZFXFAXMKU7', N'e1d64a9c-9bce-4a03-aa1c-f0beba20280c', N'0120000000', 0, 0, NULL, 1, 0, N'ApplicationUser', 1)
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5541d758-57a0-46b3-b97d-91046320c15e', N'9aedc898-c2ff-4276-b353-17f3ea450e0f')

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
