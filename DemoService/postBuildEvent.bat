Rem Copy files and subdirectories to another directory
@echo off
@break off
@title "Copy DemoService bin to DepPOC3"
@cls
set source=%1
set destinationRootFolder=%2
if not exist %destinationRootFolder%nul (mkdir %destinationRootFolder%)
xcopy %source% %destinationRootFolder% /h /i /c /k /e /r /y