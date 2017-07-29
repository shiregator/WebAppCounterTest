# WebAppCounterTest
C# ASP.NET Web Application Counter Test -- Memory Mapped File (in RAM) implementation


This is a simple example application that demonstrates how to share a simple data object between two processes.  To download and run:

1. Download branch 
2. Build all 3 contained projects
3. Edit line 29 (Process.Start) in ModulePortal.aspx.cs based on your directory location
4. Debug->Start Without Debugging
5. Wait for web page to open
6. Click "Start Module"
7. Wait for new process to start in command window and set data.Value = 5
8. Click "Add Module" on the web page
9. Watch data.Value increment to 6

