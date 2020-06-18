# CatalogStore
An experimental .NET Core library to more efficiently reverse engineer a SQL Server database.

## Overview
All database tooling needs to reverse engineer a database.  Tooling does this by querying the system catalog about the objects
in the database (e.g. tables, views, triggers, store procedures, etc).  Here we compare DacFx and a new strategy for reverse 
engineering a database: CatalogStore.

## DacFx Reverse Engineering
Today, when DacFx reverse engineers a database, it issues a brutal query that punches SQL Server in the face.  The query is 
380K in size, and does all kinds of complicated joins and fancy TSQL manipulation.  See file 
[DacFxReverseEngineeringQuery2016.sql](ReverseEngineeringTableDump/DacFxReverseEngineeringQuery2016.sql).
After viewing that file and changing your pants, you can probably guess that executing this query is very inefficient. Executing 
this query against an Azure 'Basic SKU' database, pegs the DTU quota, making the database unusable for a few seconds after 
execution. 

Not only are these queries inefficient, they are expensive to maintain.  All of the queries are handcrafted against a SQL 
version.  When we release a new version of SQL, we cut and paste the query text, along with the SqlClient processing code,
and then update the copy to handle the new system catalog tables and columns.

## CatalogStore Reverse Engineering
This project take a different approach.  SQL Server exposes rich meta data over the system catalog.  We leverage this, and code
generate a library that can execute simple 'select *' statements for each system catalog table.  We take the results, and 
place them all in a sqlite database.  Then, we code generate an entity framework model over the sqlite system 
catalog tables.  This give us a nice type-safe query experience over the system catalog, with all processing occurring on 
the client side.  Because everything is code generated, maintenance should be lower.

This design can potentially have other benefits
* Opens the door to implement an efficient global search over all database objects.
* The sqlite system catalog database can be persisted and cached on disk.
* Intellisense cache

## Benchmarks
Initial benchmarks indicate CatalogStore is in-line with DacFx when reverse engineering small databases.  However as object
counts grow, CatalogStore scales better than DacFx.

| Engine              | Database                   | Object Count  | Time      |
| ------------------- | -------------------------- |:-------------:| ---------:|
| DacFx               | AdventureWorks             | 1603          | 1 sec     |
| CatalogStore        | AdventureWorks             | 1603          | 1 sec     |
| DacFx               | WideWorldImporters         | 2618          | 1 sec     |
| CatalogStore        | WideWorldImporters         | 2618          | 1 sec     |
| DacFx               | Dynamics                   | 5230          | 6 sec     |
| CatalogStore        | Dynamics                   | 5230          | 2 sec     |
| DacFx               | Dynamics x 2               | 7871          | 18 sec    |
| CatalogStore        | Dynamics x 2               | 7871          | 4 sec     |
| DacFx               | Dynamics x 5               | 15794         | 1 min     |
| CatalogStore        | Dynamics x 5               | 15794         | 11 sec    |
| DacFx               | Dynamics x 10              | 28999         | 3 min     |
| CatalogStore        | Dynamics x 10              | 28999         | 21 sec    |
| DacFx               | Dynamics x 20              | 55409         | 14 min    |
| CatalogStore        | Dynamics x 20              | 55409         | 43 sec    |
| DacFx               | Azure Basic AdventureWorks | 1601          | 6 sec     |
| CatalogStore        | Azure Basic AdventureWorks | 1601          | 8 sec     |
| DacFx               | Azure S3 AdventureWorks    | 1601          | 2 sec     |
| CatalogStore        | Azure S3 AdventureWorks    | 1601          | 2 sec     |
| DacFx               | Azure P1 AdventureWorks    | 1603          | 1 sec     |
| CatalogStore        | Azure P1 AdventureWorks    | 1603          | 1 sec     |

## Projects

#### Microsoft.SqlServer.CatalogStore
* The code generated CatalogStore

#### CatalogStoreCodeGenerator
* Code generates the files in the [Microsoft.SqlServer.CatalogStore\CodeGen](Microsoft.SqlServer.CatalogStore/CodeGen) folder
* Uses the [Catalog.Whitelist.txt](CatalogStoreCodeGenerator/Catalog.Whitelist.txt) to get all system catalog tables used for reverse engineering.
* Generates and caches the system catalog list (e.g. [Catalog.2005.txt](https://raw.githubusercontent.com/pensivebrian/catalogstore/master/CatalogStoreCodeGenerator/Catalog.2005.txt)) and catalog xml meta data files (e.g [Catalog.2005.xml](CatalogStoreCodeGenerator/Catalog.2005.xml)) for each SQL Server version.


#### Microsoft.SqlServer.CatalogStore.Tests
* Has tests to compare the execution of the DacFx query with the CatalogStore version.

#### ReverseEngineeringTableDump
* Uses ScriptDom to parse the DacFx reverse engineering queries for Azure and SQL 2016, and generates the 
[Catalog.Whitelist.txt](CatalogStoreCodeGenerator/Catalog.Whitelist.txt) file, which is input into the code generation process.  This way, both DacFx and CatalogStore query the same tables.

## TODO
*  Explicit mapping of primary keys
* Figure out how to handle sql_variants - currently not mapped
* Handle timespan column types - currently not mapped
* Introduce loading profiles: lazy, object explorer, reverse engineer
