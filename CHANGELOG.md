# Radzen change log

## 1.0.0-beta.1 - 2017-03-30

### Enhancements

- Use Angular CLI for building
- Built-in user management for SQL Server data sources
- Generate CRUD pages for SQL Server data sources
- Generate user management pages
- Ability to open pages in dialogs
- Twelve new themes
- Lookup form fields - ability to pick a value from a dropdown
- Form fields for boolean properties
- Live rebuild and reload

## 1.0.0-alpha.13 - 2017-02-21

### Enhancements
- DataGrid paging, sorting and filtering support
- MSSQL paging sorting and filtering support
- Two-way data-binding support for DropDownList and TextBox components
- Placeholder option for DropDownList

## 1.0.0-alpha.12 - 2017-02-15

### Enhancements
- MSSQL Server CRUD support
- MSSQL Server TrustServerCertificate configuration option

### Fixes
- The property grid displays old values for certain properties

## 1.0.0-alpha.11 - 2017-02-09

### Fixes
- Build error when the user hasn't specified a logo
- Error when the user cancels application import



## 1.0.0-alpha.10 - 2017-02-06

### Enhancements
- No longer require a log when creating a new application.
- No longer require the target directory to be empty.
- Create the target directory if it does not exist.
- Support for 64bit Windows.
- Support for 64bit Ubuntu.

## 1.0.0-alpha.9 - 2017-01-20

### Enhancements
- A lot faster way to data-bind components. Automatically creates page properties and invokes data source methods.
- DropDown component.

## 1.0.0-alpha.8 - 2016-11-30

### Enhancements
- MS SQL Server support.

### Fixes
- Design-time error when the user changes the invoke method.

## 1.0.0-alpha.7 - 2016-11-14

### Enhancements
- Allow the user to pick the navigation path from a drop down.

### Fixes
- Design-time error if the user types too quickly.

## 1.0.0-alpha.6 - 2016-11-03

### Enhancements
- Upgrade to Angular 2.1.2.
- Ubuntu support

### Fixes
- Dialogs go behind Radzen window in Ubuntu

## 1.0.0-alpha.5 - 2016-10-29

### Enhancements
- Improved production build.
- Help menu item.
- Upgrade to TypeScript 2.

## 1.0.0-alpha.4 - 2016-10-27

### Enhancements
- Grid column templates.
- Action that allows code execution.

## 1.0.0-alpha.3 - 2016-10-24

### Enhancements
- Improve autocomplete behaviour.
- Delete data items from the grid.

### Fixes
- Moving components via drag and drop sometimes fails.
- Generator outputs invalid TypeScript if a property value starts with expression.
- Code generation fails if a notify action doesn't have the detail or summary set.
- The submit event of the form component has wrong event argument.

## 1.0.0-alpha.2 - 2016-10-18

### Fixes
- OAuth doesn't really work. The `client_id` parameter was missing from the login URL.
- Cannot tab out of certain property editors in the property grid.
- Crashes when the **run** button is clicked in a newly created application.

## 1.0.0-alpha.1 - 2016-10-18

First release!
