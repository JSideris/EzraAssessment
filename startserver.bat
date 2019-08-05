@echo off

cd EzraAssessmentServer

echo Starting Server. Press CTRL+C and choose "N" to restart.

:start

dotnet run

goto start