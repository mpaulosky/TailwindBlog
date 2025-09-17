using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Data;

public class SecurityDbContext
(
		DbContextOptions<SecurityDbContext> options
) : IdentityDbContext<ApplicationUser>(options) { }