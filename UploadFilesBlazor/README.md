# Radzen File Uploads demo

This sample application shows how to upload files in a Radzen Blazor application.

## Features

1. Files are saved in the wwwroot directory so they can be served painlessly.
2. File names and path are persisted in a table

## Database

Use the following SQL to create the sample MSSQL database

```
CREATE DATABASE UploadDB
GO
USE UploadDB
GO
CREATE TABLE UploadDB.dbo.Files (
  Id int NOT NULL IDENTITY(1,1),
  Name varchar(100),
  [Path] varchar(512),
  CONSTRAINT Files_PK PRIMARY KEY (Id)
)
GO
```

## Implementation

The code which saves the files is in the [UploadController.cs](./server/Controllers/UploadController.cs). It is invoked by RadzenUpload - its `Url` property is set to `upload/multiple`.

File deleting is done in in [Home.razor.cs](./server/Pages/Home.razor.cs).
