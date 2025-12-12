# CinemaManagement.Tests

This project contains unit tests for the CinemaManagement web application.

## Test Framework

- **xUnit** - Main testing framework
- **Moq** - Mocking framework for dependencies
- **Microsoft.EntityFrameworkCore.InMemory** - In-memory database for testing
- **Microsoft.AspNetCore.Mvc.Testing** - ASP.NET Core testing utilities

## Test Structure

### Controllers Tests
- **MoviesControllerTests** - Tests for movie CRUD operations
- **HallsControllerTests** - Tests for hall management
- **BookingsControllerTests** - Tests for booking operations

### Models Tests
- **ModelValidationTests** - Tests for model validation attributes

## Running Tests

To run all tests:
```bash
dotnet test
```

To run tests with detailed output:
```bash
dotnet test --verbosity detailed
```

To run tests with coverage:
```bash
dotnet test /p:CollectCoverage=true
```

## Test Coverage

The test suite currently includes:
- 19 unit tests covering key controller actions
- Tests for successful operations (Index, Details, Create, Delete)
- Tests for error cases (NotFound scenarios, validation failures)
- Model validation tests

## Adding New Tests

1. Create a new test class in the appropriate folder (Controllers, Models, etc.)
2. Follow the Arrange-Act-Assert pattern
3. Use descriptive test method names that explain what is being tested
4. Use the in-memory database for controller tests that require data access
