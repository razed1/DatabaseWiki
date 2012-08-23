**Really fast and poor explanation until I get a better readme up.**


What it does is grabs every table from every database on a server, populates two tables (One for all table information, and the other with all the column information.  From there, a web page allows a user to search for table (by name), and add notes to a selected table.  Also, when a user selects a table, all columns are shown for said table.  If a column is selected, notes can be added to it.

Stuff used:
	Database:
		NHibernate
		Fluent.NHibernate
		NHibernate.Linq
	
	Web Framework:
		ASP MVC 3
	
	UI:
		JQuery/CoffeeScript
		SASS

	Testing:
		NSubstitute
		MSTest


Enable Package Restore on the solution
	right click "Solution" in solution explorer
	Click enable package restore 

Open nuget package manager
	Tools -> Library Package Manager -> Package Manager Console
	Install-Package NuGetPowerTools
	Enable-PackageRestore
	Rebuild

search for name="TableSearchDev"
	add connection string

Search for new SchemaExport(configuration).Create(false, false))
	Set to true, true

TableSearch.Data.Structure.Test.MappingTest.ColumnMappingTest
	Run test "ItemCreated"

Search for new SchemaExport(configuration).Create(false, false))
	Set to false, false


Create and populate the Wiki tables (tables to hold the table and column information)
	Go to TableSearch.Data.Structure.Test.Utility.GetDatabaseInfomation
	Run test "TablesAreFound"
	Run test "ColumnsAreFound"

Should be good to go

** Issues **

Using the built in Test runner for Visual Studio

	"Error loading ~\TableSearch\TableSearch.Mvc.Engine.vsmdi: Input file not found: C:\Delete\TableSearch\Ta
bleSearch.Mvc.Engine.vsmdi."
  Remove read only from the pareant folder.  You will still get this in the output, but you can run tests.
