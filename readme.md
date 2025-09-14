
---

### 📁 **2. Create a New WebAPI Project**

```bash
dotnet new webapi -n YourWebApiProject
dotnet sln add YourWebApiProject
```

---

### 🏗️ **3. Create AppHost and ServiceDefaults Projects**
```bash
# Create AppHost project
dotnet new aspire-apphost -n YourAppHost
dotnet sln add YourAppHost

# Create ServiceDefaults project
dotnet new aspire-servicedefaults -n YourServiceDefaults
dotnet sln add YourServiceDefaults
```

---

### 🔗 **4. Add Project References**
```bash
# Add reference from AppHost to WebAPI project
dotnet add YourAppHost reference YourWebApiProject

# Add reference from WebAPI to ServiceDefaults
dotnet add YourWebApiProject reference YourServiceDefaults
```

---

### ⚙️ **5. Configure AppHost Project**
Edit `YourAppHost/Program.cs` to include your WebAPI project:
```csharp
var builder = DistributedApplication.CreateBuilder(args);

// Add your WebAPI project to orchestration
builder.AddProject<Projects.YourWebApiProject>("yourwebapi");

builder.Build().Run();
```

---

### 🔧 **6. Configure WebAPI Project**
Edit `YourWebApiProject/Program.cs` to include service defaults:
```csharp
var builder = WebApplication.CreateBuilder(args);

// Add service defaults for observability and resilience
builder.AddServiceDefaults();

// Add services to the container
builder.Services.AddControllers();

var app = builder.Build();

app.MapDefaultEndpoints(); // For health checks
app.MapControllers();
app.Run();
```

---

### 🚀 **7. Run the Application**
```bash
# Set AppHost as startup project and run
dotnet run --project YourAppHost