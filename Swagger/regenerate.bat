java -jar swagger-codegen-cli.jar generate ^
  -i http://localhost:5237/swagger/v1/swagger.json ^
  -l typescript-angular ^
  -o proxy 

echo Copying generated API client to Angular project...
set SRC_DIR=proxy\api
set DEST_DIR=..\DataHarbor.Client\src\api
xcopy "%SRC_DIR%" "%DEST_DIR%" /E /Y /I

echo Copying generated model client to Angular project...
set SRC_DIR=proxy\model
set DEST_DIR=..\DataHarbor.Client\src\model
xcopy "%SRC_DIR%" "%DEST_DIR%" /E /Y /I

rmdir "proxy" /S /Q
