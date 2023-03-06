using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace HOKTutorial.Data;

/// <summary>
/// Context for the User database.
/// </summary>
public class UserContext : DbContext
{
    /// <summary>
    /// Magic string.
    /// </summary>
    public static readonly string RowVersion = nameof(RowVersion);

    /// <summary>
    /// Magic strings.
    /// </summary>
    public static readonly string UserDb = nameof(UserDb).ToLower();

    /// <summary>
    /// Inject options.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions{UserContext}"/>
    /// for the context
    /// </param>
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
        Debug.WriteLine($"{ContextId} context created.");
    }

    /// <summary>
    /// List of <see cref="User"/>.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Dispose pattern.
    /// </summary>
    public override void Dispose()
    {
        Debug.WriteLine($"{ContextId} context disposed.");
        base.Dispose();
    }

    /// <summary>
    /// Dispose pattern.
    /// </summary>
    /// <returns>A <see cref="ValueTask"/></returns>
    public override ValueTask DisposeAsync()
    {
        Debug.WriteLine($"{ContextId} context disposed async.");
        return base.DisposeAsync();
    }
}
