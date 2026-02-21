# Testing Principles

We deliberately test at the **edges of the system** this gives us:

* Freedom to refactor
* Confidence to optimise
* Stable, long-lived tests
* Reduced coupling to implementation details

We write **high-level, black-box integration tests** that focus only on observable outcomes (behaviour not implementation details).

If a refactor breaks a test without changing observable behaviour, the test was too coupled.

---

## Test Projects

We maintain two test suites:

```
./Tests        → CLI tests
./Tests.E2e    → Blazor Web UI tests (Playwright)
```

All tests must pass before a PR can be merged into `main`.

> If something is worth testing manually, it’s worth automating.

When adding features:

* Add tests with them
* If tests are missing, add them
* If behaviour changes, update tests intentionally

When fixing bugs: 

* Reproduce manually
* Reproduce repeatably with a `failing` test
* Make the test pass

---

# Web UI (E2E) Tests

We use **Playwright** to test the Blazor Server app.

These tests are:

* Slow(ish)
* Primarily happy-path + critical flows

Avoid:

* Testing styling details
* Over-asserting UI minutiae

---

## Running E2E Tests

You must build the Docker image before running:

```bash
cd RackPeek
docker build -t rackpeek:ci -f RackPeek.Web/Dockerfile .

cd Tests.E2e
dotnet tool install --global Microsoft.Playwright.CLI
dotnet build

playwright install
dotnet test
```

Rebuild the image whenever the Web project changes.

---

## Page Object Model (POM)

Each page/component has a **POM (Page Object Model)** abstraction.

The POM:

* Encapsulates selectors
* Encapsulates browser interactions
* Exposes intent-level methods (`AddDesktopAsync`, `GotoHardwareAsync`)
* Shields tests from UI churn

Tests should read like workflows — not like browser scripts.

Example:

```csharp
[Fact]
public async Task User_Can_Add_And_Delete_Desktop()
{
    var (context, page) = await CreatePageAsync();
    var resourceName = $"e2e-ap-{Guid.NewGuid():N}"[..16];

    try
    {
        await page.GotoAsync(fixture.BaseUrl);

        var layout = new MainLayoutPom(page);
        await layout.AssertLoadedAsync();
        await layout.GotoHardwareAsync();

        var hardwarePage = new HardwareTreePom(page);
        await hardwarePage.AssertLoadedAsync();
        await hardwarePage.GotoDesktopsListAsync();

        var listPage = new DesktopsListPom(page);
        await listPage.AssertLoadedAsync();
        await listPage.AddDesktopAsync(resourceName);
        await listPage.AssertDesktopExists(resourceName);
        await listPage.DeleteDesktopAsync(resourceName);
        await listPage.AssertDesktopDoesNotExist(resourceName);
    }
    catch (Exception)
    {
        _output.WriteLine("TEST FAILED — Capturing diagnostics");
        _output.WriteLine($"Current URL: {page.Url}");

        var html = await page.ContentAsync();
        _output.WriteLine("==== DOM SNAPSHOT START ====");
        _output.WriteLine(html);
        _output.WriteLine("==== DOM SNAPSHOT END ====");

        throw;
    }
    finally
    {
        await context.CloseAsync();
    }
}
```

### Good E2E Test Traits

* Single responsibility
* Independent (no ordering dependencies)
* Idempotent
* Generates unique test data
* Cleans up after itself
* Fails with useful diagnostics

---

## Debugging E2E Tests

You may temporarily modify:

`Tests.E2e/infra/PlaywrightFixture.cs`

To debug visually:

```csharp
Browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
{
    Headless = false,
    SlowMo = 500,
    Args = new[]
    {
        "--disable-dev-shm-usage",
        "--no-sandbox"
    }
});
```

⚠ **Before committing**, revert to CI-safe settings:

```csharp
Browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
{
    Headless = true,
    Args = new[]
    {
        "--disable-dev-shm-usage",
        "--no-sandbox"
    }
});
```

CI must always run headless.

---

# CLI Tests

CLI tests are faster and more precise.

They behave more like unit/integration hybrids (intragrationtests if you like):

* Validate both happy + unhappy paths
* Assert command output
* Assert YAML written to disk
* Avoid UI overhead

Run them with:

```bash
cd Tests
dotnet test
```

Example:

```csharp
[Fact]
public async Task servers_tree_cli_workflow_test()
{
    await File.WriteAllTextAsync(Path.Combine(fs.Root, "config.yaml"), "");

    var (output, yaml) = await ExecuteAsync("servers", "add", "node01");
    Assert.Equal("Server 'node01' added.\n", output);
    Assert.Contains("name: node01", yaml);

    (output, yaml) = await ExecuteAsync("systems", "add", "host01");
    Assert.Equal("System 'host01' added.\n", output);

    (output, yaml) = await ExecuteAsync("systems", "add", "host02");
    Assert.Equal("System 'host02' added.\n", output);

    (output, yaml) = await ExecuteAsync("systems", "add", "host03");
    Assert.Equal("System 'host03' added.\n", output);

    (output, yaml) = await ExecuteAsync(
        "systems", "set", "host01",
        "--runs-on", "node01"
    );
    Assert.Equal("System 'host01' updated.\n", output);

    (output, yaml) = await ExecuteAsync("services", "add", "immich");
    Assert.Equal("Service 'immich' added.\n", output);

    (output, yaml) = await ExecuteAsync("servers", "tree", "node01");
    Assert.Equal("""
                 node01
                 ├── System: host01
                 │   ├── Service: immich
                 │   └── Service: paperless
                 ├── System: host02
                 └── System: host03

                 """, output);
}
```

---

## CLI Testing Best Practices

* Assert exact output where meaningful
* Validate file side effects
* Test invalid arguments and failure modes
* Avoid brittle whitespace assertions unless intentional
* Keep tests deterministic
* Avoid shared filesystem state

---

# General Testing Standards

### 1. Behaviour First

Test what the user observes — not how we implement it.

### 2. Prefer Integration Over Micro-Mocking

We value confidence over isolation purity.

### 3. Fast Feedback

* CLI tests should be fast
* E2E tests should be minimal but meaningful

### 4. Tests Are Documentation

A good test explains:

* What the feature does
* How it’s expected to behave
* What regressions look like

### 5. Stability > Coverage %

High-value tests matter more than coverage numbers.

### 6. No Flaky Tests

If a test is flaky:

* Fix it immediately
* Or remove it
* Flaky tests erode trust

---

# Definition of Done

A feature is complete when:

* Behaviour is implemented
* Tests exist
* Tests pass locally
* Tests pass in CI
* Edge cases are covered
* No debug flags remain enabled

---

We optimise for:

* Confidence
* Refactorability
* Clarity
* Long-term maintainability

Tests are a first-class citizen of this project.
