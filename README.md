# Portfolio Value Calculator
This solution implements a portfolio value calculation system. The goal is to calculate the value of an investor’s portfolio based on various investment types, transaction histories, and market quotes.

The project contains two implementations:
- Script-style implementation in the PortfolioValueCalculator project for quick prototyping.
- Domain-Driven Design (DDD) reimplementation for a clean, maintainable, and extensible architecture.

## Project Structure
├── docs
├── src
│   ├── Application              # Application layer: use cases, services
│   ├── Cli                      # Command-line interface for running the calculator
│   ├── Domain                   # Core domain models and business logic (DDD)
│   ├── Persistence              # Data access layer, repositories
│   └── PortfolioValueCalculator # Script-style, procedural implementation
├── tests
│   ├── Domain.Tests             # Unit tests for domain logic
│   └── PortfolioValueCalculator.Tests # Tests for script-style calculator
├── qblix.sln                   # Solution file
├── README.md                   # This file :)
└── Directory.Packages.props    # Centralized package version management

## Implementation Overview

### Script-style Implementation
- Located in src/PortfolioValueCalculator. 
- Contains a straightforward procedural approach to read CSV files, process data, and calculate portfolio values. 
- Suitable for quick prototyping and validation of business rules. 
- Tests under tests/PortfolioValueCalculator.Tests.

```bash
cd src/PortfolioValueCalculator
dotnet run
```

### Domain-Driven Design (DDD) Implementation
- Found in src/Domain, src/Application, and src/Persistence.
- Models the core business concepts such as Investment, Portfolio, Stock, Fund, etc.
- Uses clean architecture principles separating domain, application, and infrastructure layers.
- Domain logic is thoroughly unit-tested under tests/Domain.Tests.


```bash
cd src/Cli
dotnet run
```

## Improvements
Currently, the application loads the entire CSV data files (Investments, Quotes, Transactions) at startup. While this simplifies data handling, it has two main drawbacks:
- Slower startup time: Loading all data upfront delays the application readiness.
- Higher memory usage: Storing all data in memory consumes more resources, even if only part of it is needed.

A more efficient approach would be to:
- Load only the necessary data on-demand while calculating an individual investor’s portfolio.
- Cache loaded data in lookup structures to avoid repeated file reads during the same session.

This would reduce memory footprint and improve startup performance, but it would also introduce more complexity in the data loading and caching logic.
