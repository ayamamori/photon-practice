PROJECT_UTILITY_DIR=`pwd`/`dirname $BASH_SOURCE`
PROJECT_BASE=`dirname $PROJECT_UTILITY_DIR`
APP_NAME=photon-practice.app

cd $PROJECT_UTILITY_DIR

echo "Copying sources"
rm -r Assets 2>/dev/null
rm -r ProjectSettings 2>/dev/null
cp -r $PROJECT_BASE/Assets/ Assets/
cp -r $PROJECT_BASE/ProjectSettings/ ProjectSettings/

echo "Building Unity"
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -projectPath $PROJECT_UTILITY_DIR -executeMethod BatchBuild.BuildProject_iOS -quit
if [ $? != 0 ]; then
  echo "Unity Build failed"
  exit $?
fi

echo "Building Xcode Project"
xcodebuild -project $PROJECT_UTILITY_DIR/Device/Unity-iPhone.xcodeproj -target Unity-iPhone -configuration Release build
if [ $? != 0 ]; then
  echo "Xcode Build failed"
  exit $?
fi

echo "Deploying to the device"
ios-deploy --debug --bundle $PROJECT_UTILITY_DIR/Device/build/Release-iphoneos/$APP_NAME
if [ $? -eq 254 ]; then
  echo "Deploying finished, but couldn't run app"
  exit $?
elif [ $? -ne 0 ]; then
  echo "Deploying failed"
  exit $?
fi
