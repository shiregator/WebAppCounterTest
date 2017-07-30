# WebAppCounterTest
C# ASP.NET Web Application Counter Test

This is a simple example application that demonstrates how to share a serializable data object between two processes.  To download and run:

1. Download branch 
2. Build all 5 contained projects
3. Edit line 25 and 26 (Process.Start) in ModulePortal.aspx.cs based on your directory location
4. Create a C:\TEMP directory for the MMF to be written (in Process3 -> Program WriteObjectToMMF)
5. Debug->Start Without Debugging
6. Wait for web page to open
7. Click "Start Module"
8. Wait for new process to start in command window and shared string value to “Shared String 1234” in MMF Ram
9. See the web page display the shared string value “Shared String 1234” from MMF Ram
10. Wait for “process3” to start and write/read serialized objects to/from MMF file on disk (C:\TEMP\TEST.MMF)
11. Clicking "Add Module" does nothing yet
12. Uncommenting lines mamrked with “Uncommenting the following line” will result in errors due to unserializable objects
