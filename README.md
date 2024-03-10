Assumptions / Comments

 - I approached this task as building a quantitative library, hence a user interface for inputting loan parameters was not 
   developed. Instead, Program.cs shows sample usage of the library's functions.

 - If Program.cs is ran, it creates example_output.csv, which contains all the cashflows, given the exact same loan 
   parameters as provided on the spreadsheet. IRR rate is printed to the Console.

 - Valuation_Date is not used in the spreadsheet's IRR calculation and is therefore omitted from this project.

 - Outstanding_Balance is not used in the spreadsheet. Although it is included as a required parameter in the Loan class 
   constructor, it is not used in any of the calculations.

 - Percentage values, including the recovery rate which is represented as a decimal in the spreadsheet, are stored and 
   processed as numbers (e.g., 3.5 for 3.5%) in both storage and method parameters, not as 0.035.

 - I've added sample test classes, showcasing my testing approach but not covering everything.

 - I implemented data annotation-based validation directly within the Loan class to validate its parameters. 
   I recognize that alternative validation strategies exist, such as using a Factory design pattern, which could streamline 
   the Loan class into functioning more purely as a data container.

 - If more loan types were provided, the Loan class could be designed to use inheritance more effectively.

 - DatabaseDataProvider.cs is a placeholder, intended for future use when DefaultRate and PrepaymentSpeed are stored in a 
   database system.

 - LoanCashFlowCalculator takes IDataProvider as a parameter, utilizing the dependency injection pattern. This approach is 
   useful in test cases, allowing for easier mocking and testing of different scenarios.

 - DotNet 8.0 was used to build this project.

```
./LoanCalculator
├── LoanCalculator.csproj
├── appsettings.json
├── Program.cs
├── README.md
├── Calculators
│   ├── IRRCalculator.cs
│   └── LoanCashFlowCalculator.cs
├── Data
│   ├── IDataProvider.cs
│   ├── FileDataProvider.cs
│   ├── PrepaymentSpeed.yaml
│   ├── DefaultRate.csv
│   └── DatabaseDataProvider.cs
├── Models
│   ├── Loan.cs
│   └── LoanCashFlow.cs
├── Utils
│   ├── CashFlowExportUtil.cs
│   └── DateUtil.cs
└── example_output.csv     * This is output from Program.cs

./LoanCalculator.Tests
├── LoanCalculator.Tests.csproj
├── GlobalUsings.cs
├── Calculators
│   ├── IRRCalculatorTests.cs
│   └── LoanCashFlowCalculatorTests.cs
├── Models
│   └── LoanTests.cs
└── Utils
    └── DateUtilTests.cs
```