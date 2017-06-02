# Radzen change log
## 1.4.0 - 2017-05-30

### Improvements
- OData entities filtering on infer using treeview witch checkboxes.
- New page dialog now can create CRUD pages for the specified data-source schema.
- User management customization

## 1.3.1 - 2017-05-26
### Improvements
- Support 200 as OData PATCH response code..
- Support UTC dates.
- Tables and Views in the data-source infer treeview are sorted alphabetically.
- Form submit changes only option.
- Auto scroll the content of a popup dialog.

### Fixes
- Naming relation properties will ignore case when search for existing property
- Excluded relations warning will not show if you exclude tables and then include excluded tables again
- Computed columns excluded
- Use input type number instead of p-spinner in order to handle decimal

## 1.3.0 - 2017-05-25
### Improvements
- Pick SQL Server tables
- Support for SQL Server views
### Fixes
- CRUD with tables that have GUID keys does not work.

## 1.2.2 - 2017-05-19
### Improvements
- Ability to set color and icon properties as expressions.
- Add visible and empty text data grid properties.
- Add icon property to the data grid.
- Add data grid column width property.
- Add ability to register custom components and services.
### Fixes
- MS SQL Column name with spaces fixed
- MS SQL Table name equal to app name fixed
- MS SQL Relations with composite and multiple keys fixed
## 1.2.1 - 2017-05-17
### Fixes
- Fix the casing of the components (button, grid, dropdown) and their properties.

## 1.2.0 - 2017-05-17
### Improvements
- Custom styles support.
- Add support for button icons.
- New sample OData service.
- Add card component.

### Fixes
- Set default form field type to string. Fixes the rz-undefined-form-field runtime error.
- Tables with reserved names renamed.
- Entity type used if no x-foreignKey.name and foreignKey.parentEntityType.
- Avoid output pane button scrolling out of view.
- Initialize the selectbar option value to text if not set.
- Fix the navigation height so all items are visible.

## 1.1.6 - 2017-05-11
### Improvements
- Text form field.
- Rating form field.
- Selectbar form field.
- Form can update data on change (updateDataOnChange property false by default).
- Form buttonPosition none option added.
- Ability to set a form field default value from route parameters.
### Fixes
- Adding a form field has default string type.
- Handling OData Edm.Binary type.
- DataGrid new data-source code generation will respect count parameter for OData V3.
- Server (C#) relations and property naming fixed (System.Data.SqlClient.SqlException: Invalid column name XXX).
- Form fields and grid column reordering is not persisted.
- OAuth login redirects twice to login page.
## 1.1.5 - 2017-05-09
### Fixes
- TypeError: page.generated.ts.ejs:92 args.find is not a function

## 1.1.4 - 2017-05-09
### Improvements
- Improved foreign key inferring from OData service
- Auto-generated edit form will now submit ${event} 

### Fixes
- TypeError: Cannot read property of undefined when setting form field or data grid column properties
## 1.1.3 - 2017-05-08
### Improvements
- Reorder forum fields and grid columns.
- Easier way to specify a form field type.
### Fixes
- Cannot redeclare block-scoped variable 'result'.
- Validation error about duplicate data source name when editing a data source.
- TypeError: Cannot read property 'operationId' of null when inferring OData service.
## 1.1.2 - 2017-05-05
### Fixes
- Empty DataGrid design-time 'hasOwnProperty()' error fixed.
## 1.1.1 - 2017-05-05
### Fixes
- Generation of OData CRUD pages undefined or null object error fixed.
## 1.1.0 - 2017-05-04
### Improvements
- Upgrade PrimeNG and ngx-charts.
- Filtering UI for date DataGrid columns.
- Enable DataGrid responsive mode.
- Prevent the output pane from blocking the design canvas and property grid.
- Ability to specify the position of the form buttons.
- Page generation uses Angular date pipe to format date columns.
- Upload component and upload form fields. Enable by setting the `format` of a string form field to `base64`.
- Sample SQL Server database has images.
- Ability to specify default form field values.
- Cache the response of OData services.
- Page generation displays dates in DataGrid columns as UTC by default.
### Fixes
- Popups don't always appear centered.
- Boolean component properties are serialized as strings.
- OData response code for POST is fixed.
- Remove webpack build warnings.
## 1.0.5 - 2017-04-27
### Improvements
- Expose the Count property of the DataGrid component.
- Support OData v3 in the new DataGrid data source dialog.
### Fixes
- Compilation error when a table has a column named after the table itself.
- Code generation error when a table name contains underscore.
- Compilation error when the TextBox or DropDownList components are used.
- Cannot select a data source method in the new DataGrid data source dialog.
## 1.0.4 - 2017-04-25
### Improvements
- Compatibility with SQL Server 2008 and dynamic ports.
- Hide pages based on the current user role.
### Fixes
- Runtime error when creating ASP.NET Core Identity tables.
- Cannot delete entities after inferring the schema from SQL Server. 
- Avoid generating duplicate properties for relationships.
- Disable filtering of date properties until proper filtering UI is implemented.
- Endless HTTP calls during filtering.
## 1.0.3 - 2017-04-22
### Fixes
- Trial version expires after upgrade to 1.0.2.
- Error while saving a page
## 1.0.1 - 2017-04-19

## 1.0.2 - 2017-04-21

### Enhancements

- Support for SQL Server dynamic ports and named instances.
- OData v3 support.
- Logging unhandled exceptions. The log file is called `log.txt` and can be found in `C:\Users\<user>\AppData\Roaming\Radzen` on Windows and `~/Library/Application Support/Radzen` on macOS.

### Fixes

- SQL Azure schema inferring hangs.
- Sporadic 'Error: ENOENT: no such file or directory' when creating a page.

## 1.0.1 - 2017-04-19

### Enhancements

- Inject services in ngOnInit in order to support service replacement via dependency injection.
- Warn if a database already exists when creating sample MS SQL schema.
- Help / About menu item in Windows

### Fixes

- Bug when deleting entities from an OData schema.
- Inferring SQL Azure database schema hangs.
- Compilation error during production build due to a property being private.

## 1.0.0 - 2017-04-18

### Enhancements

- Generate CRUD pages for OData services
- Build output window
- Component names
- Display foreign keys in automatically generated pages
- Regenerate code on property changes
- Upgrade to Angular 4 and Angular CLI 1.0
- Remove build indicator overlay
- Stop auto-hiding unexpected errors
- Allow the user to quickly find pages and components

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

### Breaking changes

- Old themes are removed. You have to change the current theme.
- Angular application is generated in the `client` directory.

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
