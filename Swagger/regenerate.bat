java -jar swagger-codegen-cli.jar generate ^
  -i swagger.json ^
  -l typescript-angular ^
  -o proxy 

echo Copying generated API client to Angular project...
set SRC_DIR=proxy\api
set DEST_DIR=..\DataHarbor.Admin\admin-app\src\api
xcopy "%SRC_DIR%" "%DEST_DIR%" /E /Y /I

echo Copying generated model client to Angular project...
set SRC_DIR=proxy\model
set DEST_DIR=..\DataHarbor.Admin\admin-app\src\model
xcopy "%SRC_DIR%" "%DEST_DIR%" /E /Y /I

rmdir "proxy" /S /Q
