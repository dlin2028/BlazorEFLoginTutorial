## Blazor/Entity Framework Tutorial

# Installing .NET 7.0

Installing with APT can be done with a few commands. Before you install .NET, run the following commands to add the Microsoft package signing key to your list of trusted keys and add the package repository.

Open a terminal and run the following commands:

```
wget https://packages.microsoft.com/config/ubuntu/22.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
```
### Install the SDK

The .NET SDK allows you to develop apps with .NET. If you install the .NET SDK, you don't need to install the corresponding runtime. To install the .NET SDK, run the following commands:

```
sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-7.0
```

### Install the runtime

The ASP.NET Core Runtime allows you to run apps that were made with .NET that didn't provide the runtime. The following commands install the ASP.NET Core Runtime, which is the most compatible runtime for .NET. In your terminal, run the following commands:

```
sudo apt-get update && \
  sudo apt-get install -y aspnetcore-runtime-7.0
```

### Verify .NET is working
Run `dotnet --info ` The output should look like this

```
davidzl2@SPACEHEATER1000:~/hok$ dotnet --info
.NET SDK:
 Version:   7.0.201
 Commit:    68f2d7e7a3

Runtime Environment:
 OS Name:     ubuntu
 OS Version:  22.04
 OS Platform: Linux
 RID:         ubuntu.22.04-x64
 Base Path:   /usr/share/dotnet/sdk/7.0.201/

Host:
  Version:      7.0.3
  Architecture: x64
  Commit:       0a2bda10e8

.NET SDKs installed:
  7.0.201 [/usr/share/dotnet/sdk]

.NET runtimes installed:
  Microsoft.AspNetCore.App 7.0.3 [/usr/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.NETCore.App 7.0.3 [/usr/share/dotnet/shared/Microsoft.NETCore.App]

Other architectures found:
  None

Environment variables:
  Not set

global.json file:
  Not found
```

# Create your app

In your terminal, run the following command to create your app:
```
dotnet new blazorserver -o HOKTutorial --no-https -f net7.0
```

This command creates your new Blazor app project and places it in a new directory called HOKTutorial inside your current location.

Navigate to the new HOKTutorial directory created by the previous command:
```
cd HOKTutorial
```
If you want to use VSCode, type in
```
code .
```
If you want to use another dev environment, that's fine but I won't be able to offer the same level of support.

# Run your app

In your terminal, run the following command:

```
dotnet watch
```

The dotnet watch command will build and start the app, and then update the app whenever you make code changes. You can stop the app at any time by selecting Ctrl+C.

Wait for the app to display that it's listening on http://localhost:<port number> and then open a browser and navigate to that address. In this example, dotnet watch showed it was listening on http://localhost:7178.

Once you get to the following page, you have successfully run your first Blazor app!

![](https://dotnet.microsoft.com/static/images/blazor-tutorial/screenshot-blazor-tutorial-run.png?v=oPBCcp2pugBKVA2j1NtvSjGMsFQplobgYwgUICAPcHM)

# Setting up the pages

Go to Shared/NavMenu.razor and delete the Counter and Fetch data entries, then add an entry for Create Account. It should look like this when you are done.

```html
<div class="top-row ps-3 navbar navbar-dark">
    <div class="container-fluid">
        <a class="navbar-brand" href="">HOKTutorial</a>
        <button title="Navigation menu" class="navbar-toggler" @onclick="ToggleNavMenu">
            <span class="navbar-toggler-icon"></span>
        </button>
    </div>
</div>

<div class="@NavMenuCssClass nav-scrollable" @onclick="ToggleNavMenu">
    <nav class="flex-column">
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="" Match="NavLinkMatch.All">
                <span class="oi oi-home" aria-hidden="true"></span> Home
            </NavLink>
        </div>
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="createaccount">
                <span class="oi oi-plus" aria-hidden="true"></span> Create Account
            </NavLink>
        </div>
    </nav>
</div>

@code {
    private bool collapseNavMenu = true;

    private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }
}

```

Go to the Pages directory then delete FetchData.razor and Counter.razor  
Create a new file called CreateAccount.razor and paste this code
```html
@page "/createaccount"
@using HOKTutorial.Data

<PageTitle>Create Account</PageTitle>

<h1>Create Account</h1>

<form onsubmit="return false;">
    <div class="container">
        <label for="uname"><b>Username</b></label>
        <input type="text" placeholder="Enter Username" name="uname" @bind="User.Username" required>
        <br/>
        <label for="psw"><b>Password</b></label>
        <input type="password" placeholder="Enter Password" name="psw" @bind="User.Password" required>
        <br/>
        <button type="submit" @onclick="AddUser">Create Account</button>
    </div>
    <label>@StatusMessage</label>
</form>

@code {
    private User? User;
    private string? StatusMessage = "";

    private void AddUser()
    {
        
    }
}
```

Go to Index.razor and replace it with this code

```html
@page "/"
@using HOKTutorial.Data

<PageTitle>Log In</PageTitle>

<h1>Log In</h1>

<form onsubmit="return false;">
    <div class="container">
        <label for="uname"><b>Username</b></label>
        <input type="text" placeholder="Enter Username" name="uname" @bind="User.Username" required>
        <br/>
        <label for="psw"><b>Password</b></label>
        <input type="password" placeholder="Enter Password" name="psw" @bind="User.Password" required>
        <br/>
        <button type="submit" @onclick="Login">Log In</button>
    </div>
    <label>@StatusMessage</label>
</form>

@code {
    private User? User;
    private string? StatusMessage = "";

    private void Login()
    {

    }
}
```

Note that these two pages are almost identical with the only difference being that one says Log In and one says Create Account.

# Setting up the data


Add entity framework packages if you haven't already

```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```
Install entity framework tools and reload the terminal
```
dotnet tool install --global dotnet-ef
reset
```
Go to the Data directory and delete WeatherForecast and WeatherForecastService. In their place, create a User class. This class will store the login data.

```cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOKTutorial.Data;

public class User
{    

    /// <summary>
    /// Unique identifier.
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters.")]
    public string? Username { get; set; }  
    
    [Required]
    [StringLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
    public string? Password { get; set; }
}
```

Create the DBContext. You car read more about them here
https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-7.0

```cs
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

```

Go to Program.cs and delete the WeatherForecastService, and add the dependencies
```cs
using Microsoft.EntityFrameworkCore;
using HOKTutorial.Data;
```

 then add this line
```cs
builder.Services.AddDbContextFactory<UserContext>(opt =>
    opt.UseSqlite($"Data Source={nameof(UserContext.UserDb)}.db"));
```
This creates a local database.

Now run
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
to create the database. The first command creates the scaffolding which will create the database. You can find it in the Migrations folder. The second command creates the database in the project folder called UserDb.db.

## Update the .razor pages

First in the createaccount page, add the dependencies for Entity Framework so the top 3 lines look like this.
```html
@page "/createaccount"
@using Microsoft.EntityFrameworkCore
@inject IDbContextFactory<UserContext> DbFactory

```
Then, create the initialization logic for the User object.

```cs
@code {
    private User? User;
    private string? StatusMessage = "";
    protected override Task OnInitializedAsync()
    {
        User = new();
        return base.OnInitializedAsync();
    }
}
```

Last, fill in the AddUser function.
```cs
    private void AddUser()
    {
        using var context = DbFactory.CreateDbContext();

        if (User is not null)
        {
            if(context.Users?.Where(x => x.Username == User.Username).Count() > 0)
            {
                StatusMessage = "Error:  " + User.Username + "Already Exists";
            }
            else
            {
                context.Users?.Add(User);
                StatusMessage = "Successfully added " + User.Username;
                User = new();
            }
        }

        context.SaveChanges();
    }
```


Here is the full createaccount page. Creating the Log In page is left as an exercise to the reader.

```cs
@page "/createaccount"

@using Microsoft.EntityFrameworkCore
@inject IDbContextFactory<UserContext> DbFactory

@using HOKTutorial.Data

<PageTitle>Create Account</PageTitle>

<h1>Create Account</h1>

<form onsubmit="return false;">
    <div class="container">
        <label for="uname"><b>Username</b></label>
        <input type="text" placeholder="Enter Username" name="uname" @bind="User.Username" required>
        <br/>
        <label for="psw"><b>Password</b></label>
        <input type="password" placeholder="Enter Password" name="psw" @bind="User.Password" required>
        <br/>
        <button type="submit" @onclick="AddUser">Create Account</button>
    </div>
    <label>@StatusMessage</label>
</form>

@code {
    private User User = new();
    private string? StatusMessage = "";
    
    private void AddUser()
    {
        using var context = DbFactory.CreateDbContext();

        if (User is not null)
        {
            if(context.Users?.Where(x => x.Username == User.Username).Count() > 0)
            {
                StatusMessage = "Error:  " + User.Username + "Already Exists";
            }
            else
            {
                context.Users?.Add(User);
                StatusMessage = "Successfully added " + User.Username;
                User = new();
            }
        }

        context.SaveChanges();
    }
}
```
