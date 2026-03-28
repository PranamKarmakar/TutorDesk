using Microsoft.AspNetCore.Mvc;
using TutorDesk.Domain.Modules.Auth.Entities;

public abstract class BaseController : ControllerBase
{
    protected Guid GetTenantId()
    {
        if (!Guid.TryParse(User.FindFirst("TenantId")?.Value, out var tenantId))
            throw new UnauthorizedAccessException("Tenant ID missing.");

        return tenantId;
    }
}