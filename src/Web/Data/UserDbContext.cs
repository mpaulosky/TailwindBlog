using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Data;

public class UserDbContext
(
		DbContextOptions<UserDbContext> options
) : IdentityDbContext<ApplicationUser>(options) { }