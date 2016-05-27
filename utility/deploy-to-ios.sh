PROJECT_BASE=`pwd`/`dirname $(dirname $BASH_SOURCE)`
APP_NAME=photon-practice.app

/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -projectPath $PROJECT_BASE -executeMethod BatchBuild.BuildProject_iOS -quit
xcodebuild -project $PROJECT_BASE/Device/Unity-iPhone.xcodeproj -target Unity-iPhone -configuration Release build
ios-deploy --justlaunch --debug --bundle $PROJECT_BASE/Device/build/Release-iphoneos/$APP_NAME
