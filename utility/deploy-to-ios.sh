PROJECT_BASE=`pwd`/`dirname $(dirname $BASH_SOURCE)`
APP_NAME=photon-practice.app

echo "Copying sources"
rm -r Assets
rm -r ProjectSettings
cp -r ~/git/photon-practice/Assets/ Assets/
cp -r ~/git/photon-practice/ProjectSettings/ ProjectSettings

echo "Building Unity"
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -projectPath $PROJECT_BASE -executeMethod BatchBuild.BuildProject_iOS -quit
if [ $? != 0 ]; then
  echo "Unity Build failed"
  exit $?
fi

echo "Building Xcode Project"
xcodebuild -project $PROJECT_BASE/Device/Unity-iPhone.xcodeproj -target Unity-iPhone -configuration Release build
if [ $? != 0 ]; then
  echo "Xcode Build failed"
  exit $?
fi

echo "Deploying to the device"
ios-deploy --justlaunch --debug --bundle $PROJECT_BASE/Device/build/Release-iphoneos/$APP_NAME
if [ $? != 0 ]; then
  echo "Deploying failed"
  exit $?
fi
