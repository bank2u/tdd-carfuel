@echo off

 
Packages\xunit.runner.console.2.1.0\tools\xunit.console ^
	CarFuel.Facts\bin\Debug\CarFuel.Facts.dll ^
	-parallel all ^
	-html Result.html ^
	-nologo
	
@echo on