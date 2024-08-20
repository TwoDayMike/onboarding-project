namespace Application.Common.Security.Attributes
{
    /// <summary>
    /// Specifies that the command or query that this attribute is applied to does require authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute, IAuthAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class. 
        /// </summary>

        public AuthorizeAttribute(Role[]? roles = null, Scope[]? scopes = null, bool evaluateAll = false)
        {
            Roles = roles != null ? roles : Array.Empty<Role>();
            Scopes = scopes != null ? scopes : Array.Empty<Scope>();
            EvaluateAll = evaluateAll;
        }

        public AuthorizeAttribute(bool evaluateAll, params Role[] roles)
        {
            Roles = roles != null ? roles : Array.Empty<Role>();
            EvaluateAll = evaluateAll;
        }

        public AuthorizeAttribute(bool evaluateAll, params Scope[] scopes)
        {
            Scopes = scopes != null ? scopes : Array.Empty<Scope>();
            EvaluateAll = evaluateAll;
        }

        public AuthorizeAttribute(params Role[] roles)
        {
            Roles = roles != null ? roles : Array.Empty<Role>();
        }

        public AuthorizeAttribute(params Scope[] scopes)
        {
            Scopes = scopes != null ? scopes : Array.Empty<Scope>();
        }

        public AuthorizeAttribute()
        {
        }

        /// <summary>
        /// Gets or sets a list of roles that are allowed to access the resource.
        /// </summary>
        public Role[] Roles { get; set; } = Array.Empty<Role>();

        /// <summary>
        /// Gets or sets a list of roles name that determines access to the resource.
        /// </summary>
        public Scope[] Scopes { get; set; } = Array.Empty<Scope>();

        /// <summary>
        /// Gets or sets the EvaluateAll flag that determines if every given role and scope should be evaluated.
        /// </summary>
        public bool EvaluateAll { get; set; }
    }
}
