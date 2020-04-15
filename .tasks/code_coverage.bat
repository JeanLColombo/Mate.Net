dotnet test ..\Mate.Net.sln /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=../.coverage/lcov /p:Include=[Mate.Net]*  /p:Threshold=90 
pause  
