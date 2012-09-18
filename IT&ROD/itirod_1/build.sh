cd $HOME/workspace/itirod_lab_1
rm -r bin/* zoo.jar
javac -sourcepath ./src -d bin src/zookeeper/Zookeeper.java 
jar -cef zookeeper.Zookeeper  zoo.jar  -C bin .
$JAVA_HOME/bin/java -jar zoo.jar


