@echo off
rem set up test directories

set CATALOG="XProc\CoreUnitTest\TestData\Input\Catalog"

for /l %%x in (1, 1, 100) do copy /Y %CATALOG%\catalog0.xml %CATALOG%\catalog%%x.xml
