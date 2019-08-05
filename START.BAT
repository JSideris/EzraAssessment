@echo off

echo Starting Server.
PING localhost -n 2 >NUL

start startserver

PING localhost -n 5 >NUL

echo Starting Client.
PING localhost -n 2 >NUL

start startclient

PING localhost -n 6 >NUL

start http://localhost:8080/