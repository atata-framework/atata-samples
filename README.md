# Atata Samples

[![Build status](https://dev.azure.com/atata-framework/atata-samples/_apis/build/status/atata-samples-ci?branchName=main)](https://dev.azure.com/atata-framework/atata-samples/_build/latest?definitionId=30&branchName=main)
[![Atata docs](https://img.shields.io/badge/docs-Atata_Framework-orange.svg)](https://atata.io)
[![Gitter](https://badges.gitter.im/atata-framework/atata.svg)](https://gitter.im/atata-framework/atata)
[![Slack](https://img.shields.io/badge/join-Slack-green.svg?colorB=4EB898)](https://join.slack.com/t/atata-framework/shared_invite/zt-5j3lyln7-WD1ZtMDzXBhPm0yXLDBzbA)
[![Twitter](https://img.shields.io/badge/follow-@AtataFramework-blue.svg)](https://twitter.com/AtataFramework)

Automated UI test sample projects based on **[Atata Framework](https://atata.io)**.

## Projects

### From [Atata Tutorials](https://atata.io/tutorials/)

- **Workflow Test** - [`/SampleApp.UITests`](../../tree/main/SampleApp.UITests)
  | [download](../../raw/main/_archives/SampleApp.UITests.zip).
  Used in **[Atata - C# Web Test Automation Framework](https://www.codeproject.com/Articles/1158365/Atata-New-Test-Automation-Framework)** introduction article.
- **Verification of Page** - [`/PageVerification`](../../tree/main/PageVerification)
  | [download](../../raw/main/_archives/PageVerification.zip).
  Used in **[Verification of Page](https://atata.io/tutorials/verification-of-page/)** tutorial.
  Demonstrates how to verify web page data using different approaches of Atata Framework.
- **Verification of Validation Messages** - [`/ValidationMessagesVerification`](../../tree/main/ValidationMessagesVerification)
  | [download](../../raw/main/_archives/ValidationMessagesVerification.zip).
  Used in **[Verification of Validation Messages](https://atata.io/tutorials/verification-of-validation-messages/)** tutorial.
  Demonstrates how to verify validation messages on web pages.
- **Handle Confirmation Popups** - [`/ConfirmationPopups`](../../tree/main/ConfirmationPopups)
  | [download](../../raw/main/_archives/ConfirmationPopups.zip).
  Used in **[Handle Confirmation Popups](https://atata.io/tutorials/handle-confirmation-popups/)** tutorial.
  Demonstrates how to handle different confirmation popups using Atata Framework.
- **Multi-Browser Configuration via .runsettings files** - [`/MultipleBrowsersViaRunSettings`](../../tree/main/MultipleBrowsersViaRunSettings)
  | [download](../../raw/main/_archives/MultipleBrowsersViaRunSettings.zip).
  Demonstrates how to configure multi-browser tests application using `.runsettings` files.
  Used in **[Multi-Browser Configuration via .runsettings files](https://atata.io/tutorials/multi-browser-configuration-via-runsettings-files/)** tutorial.
- **Multi-Browser Configuration via Fixture Arguments** - [`/MultipleBrowsersViaFixtureArguments`](../../tree/main/MultipleBrowsersViaFixtureArguments)
  | [download](../../raw/main/_archives/MultipleBrowsersViaFixtureArguments.zip).
  Demonstrates how to configure multi-browser tests application using NUnit fixture arguments.
  Used in **[Multi-Browser Configuration via Fixture Arguments](https://atata.io/tutorials/multi-browser-configuration-via-fixture-arguments/)** tutorial.
- **Extent Reports** - [`/ExtentReports`](../../tree/main/ExtentReports)
  | [download](../../raw/main/_archives/ExtentReports.zip).
  Used in **[Reporting to Extent Reports](https://atata.io/tutorials/reporting-to-extentreports/)** tutorial.
  Demonstrates the Atata reporting to [Extent Reports](https://extentreports.com/).

### Test frameworks

- **NUnit / Basic Test Project** - [`/NUnit.BasicTestProject`](../../tree/main/NUnit.BasicTestProject)
  | [download](../../raw/main/_archives/NUnit.BasicTestProject.zip).
  Demonstrates a basic Atata + NUnit test project.
- **NUnit / Advanced Test Project** - [`/NUnit.AdvancedTestProject`](../../tree/main/NUnit.AdvancedTestProject)
  | [download](../../raw/main/_archives/NUnit.AdvancedTestProject.zip).
  Demonstrates an advanced Atata + NUnit test project.
- **Using Xunit** - [`/Xunit`](../../tree/main/Xunit)
  | [download](../../raw/main/_archives/Xunit.zip).
- **Using MSTest** - [`/MSTest`](../../tree/main/MSTest)
  | [download](../../raw/main/_archives/MSTest.zip).
- **Using Reqnroll** - [`/Reqnroll`](../../tree/main/Reqnroll)
  | [download](../../raw/main/_archives/Reqnroll.zip).

### Sessions

- **Session Sharing** - [`/SessionSharing`](../../tree/main/SessionSharing)
  | [download](../../raw/main/_archives/SessionSharing.zip).
  Demonstrates how to configure Atata to reuse the same session instance by tests in a test suite.
- **Session Pool** - [`/SessionPool`](../../tree/main/SessionPool)
  | [download](../../raw/main/_archives/SessionPool.zip).
  Configures Atata to use a pool of sessions for tests execution.
  After test finishes, a session is returned to pool and can be reused by one of the next tests.
- **Authorization Role-Based Session Pools** - [`/AuthorizationRoleBasedSessionPools`](../../tree/main/AuthorizationRoleBasedSessionPools)
  | [download](../../raw/main/_archives/AuthorizationRoleBasedSessionPools.zip).
  Configures Atata to use a pool of authorization role-based web sessions.
  Has ability to specify the required user-role session for each test method or test suite.
- **Multiple `WebDriverSession`s in Test** - [`/MultipleWebDriverSessionsInTest`](../../tree/main/MultipleWebDriverSessionsInTest)
  | [download](../../raw/main/_archives/MultipleWebDriverSessionsInTest.zip).
  Demonstrates a usage of multiple `WebDriverSession`s in a test.

### Configuration

- **Configuration: Multi-environment via .runsettings files** - [`/Configuration.MultiEnvViaRunSettings`](../../tree/main/Configuration.MultiEnvViaRunSettings)
  | [download](../../raw/main/_archives/Configuration.MultiEnvViaRunSettings.zip).
  Demonstrates the way to support multiple environments using .runsettings files.
- **Configuration: Multi-environment via .runsettings and .json files** - [`/Configuration.MultiEnvViaRunSettingsAndJson`](../../tree/main/Configuration.MultiEnvViaRunSettingsAndJson)
  | [download](../../raw/main/_archives/Configuration.MultiEnvViaRunSettingsAndJson.zip).
  Demonstrates the way to support multiple environments using .runsettings and .json files.
- **Configuration: Mixed** - [`/Configuration.Mixed`](../../tree/main/Configuration.Mixed)
  | [download](../../raw/main/_archives/Configuration.Mixed.zip).
  Demonstrates how to implement complex configuration for different environments using
  .runsettings files, NUnit parameters, environment variables, `Microsoft.Extensions.Configuration` and user secrets.
- **App.Config Configuration** - [`/AppConfig`](../../tree/main/AppConfig)
  | [download](../../raw/main/_archives/AppConfig.zip).

### Cloud platform configuration

- **SauceLabs Configuration** - [`/SauceLabs`](../../tree/main/SauceLabs)
  | [download](../../raw/main/_archives/SauceLabs.zip).
  Demonstrates how to configure Atata for SauceLabs.

### UI components

- **Material UI Components** - [`/MaterialUI`](../../tree/main/MaterialUI)
  | [download](../../raw/main/_archives/MaterialUI.zip).
  Demonstrates the creation of Atata custom controls using Material UI components as examples.
- **Salesforce Lightning UI Components** - [`/SalesforceLightning`](../../tree/main/SalesforceLightning)
  | [download](../../raw/main/_archives/SalesforceLightning.zip).
  Demonstrates the creation of Atata custom controls using Salesforce Lightning components as examples.
- **DevExtreme Components** - [`/DevExtreme`](../../tree/main/DevExtreme)
  | [download](../../raw/main/_archives/DevExtreme.zip).
  Demonstrates the creation of Atata custom controls using DevExpress DevExtreme components as examples.
- **jQuery UI Components** - [`/JQueryUI`](../../tree/main/JQueryUI)
  | [download](../../raw/main/_archives/JQueryUI.zip).
  Demonstrates the creation of Atata custom controls using jQuery UI widgets as examples.
- **Ext JS Components** - [`/ExtJS`](../../tree/main/ExtJS)
  | [download](../../raw/main/_archives/JQueryUI.zip).

### Other

- **NUnit Generic Page Tests** - [`/NUnit.GenericPageTests`](../../tree/main/NUnit.GenericPageTests)
  | [download](../../raw/main/_archives/NUnit.GenericPageTests.zip).
  Demonstrates the implementation of generic NUnit tests to execute against similar pages or to perform generic checks,
  like page title, as well as more complex workflows.
- **Using CSV Data Source** - [`/CsvDataSource`](../../tree/main/CsvDataSource)
  | [download](../../raw/main/_archives/CsvDataSource.zip).
- **Table with Row-Spanned Cells** - [`/TableWithRowSpannedCells`](../../tree/main/TableWithRowSpannedCells)
  | [download](../../raw/main/_archives/TableWithRowSpannedCells.zip).
  Demonstrates 3 different approaches to work with table that has cells with `rowspan`.
- **Performance Practices for ControlList** - [`/Performance.ControlList`](../../tree/main/Performance.ControlList)
  | [download](../../raw/main/_archives/Performance.ControlList.zip).
  Demonstrates the performance practices to enumerate a big list of controls (500 `<tr>` elements).
- **Using Headless Edge** - [`/HeadlessEdge`](../../tree/main/HeadlessEdge)
  | [download](../../raw/main/_archives/HeadlessEdge.zip).
  Configures Atata to use headless Edge browser based on Chromium.
  Demonstrates the creation of Atata custom controls using Ext JS components as examples.
- **Download File** - [`/DownloadFile`](../../tree/main/DownloadFile)
  | [download](../../raw/main/_archives/DownloadFile.zip).
  Demonstrates how to configure downloads directory of Chrome and verify that file is downloaded.

You are also able to clone the whole repository and open `AtataSamples.sln`,
which contains all sample projects.

## License

Atata is an open source software, licensed under the Apache License 2.0.
See [LICENSE](LICENSE) for details.
