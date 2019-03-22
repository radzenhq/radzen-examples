# Radzen Audit sample application

This sample application shows how to perform record auditing in Radzen.

## Custom code

Two files are added to a default Radzen application to enable auditing.

### [Startup.Audit.cs](server/Startup.Audit.cs)

To avoid adding the `[Authorize]` attribute to every OData controller.

```cs
public partial class Startup
{
    partial void OnConfigureServices(IServiceCollection services)
    {
        var policy = new AuthorizationPolicyBuilder()
        {
          AuthenticationSchemes = new [] {"Bearer"}
        }
        .RequireAuthenticatedUser()
        .Build();

        services.AddMvc(options =>
        {
            options.Filters.Add(new AuthorizeFilter(policy));
        });
    }
}
```

### [AuditTrailDbContext.Audit.cs](server/Data/AuditTrailDbContext.Audit.cs)

Implements the actual logging.

```cs
public class Auditing
{
    private static void SetProperty(Type type, EntityEntry entityEntry, string name, object value)
    {
        var propertyInfo = type.GetProperty(name);

        if (propertyInfo != null)
        {
            entityEntry.Property(name).CurrentValue = value;
        }
    }

    public static void Audit(EntityEntry entityEntry, string userName)
    {
        var type = entityEntry.Entity.GetType();

        if (entityEntry.State == EntityState.Added)
        {
            SetProperty(type, entityEntry, "CreatedBy", userName);
            SetProperty(type, entityEntry, "CreatedAt", DateTime.UtcNow);
        }
        else if (entityEntry.State == EntityState.Modified)
        {
            SetProperty(type, entityEntry, "UpdatedBy", userName);
            SetProperty(type, entityEntry, "UpdatedAt", DateTime.UtcNow);
        }
    }
}

public partial class AuditTrailDbContext
{
    public override int SaveChanges()
    {
        var userName = httpAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

        var auditables = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach(var auditable in auditables)
        {
            Auditing.Audit(auditable, userName);
        }

        return base.SaveChanges();
    }
}
```
