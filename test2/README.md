# Autofac builder.Update() usage #

1. Project default system: 
- Archlinux x64
- dotnet version 2.1.4

2. Project default folders:
```
/at/c#/test2/lib1
/at/c#/test2/lib2
/at/c#/test2/server
```
These folders are used in the two json project files:
```
$ cat autofac.json
{
    "defaultAssembly": "server",
    "modules":
    [
    {
        "type": "AutoFac.ServiceModule",
        "properties": {
            "service": "/at/c#/test2/lib1/bin/Debug/netcoreapp2.0/lib1.dll",
        }
    }
    ]
}
$ cat autofac2.json
{
    "defaultAssembly": "server",
    "modules":
    [
    {
        "type": "AutoFac.ServiceModule",
        "properties": {
            "service": "/at/c#/test2/lib2/bin/Debug/netcoreapp2.0/lib2.dll",
        }
    }
    ]
}
```
3. Installation:
```
$ cd /at/c#/test2/lib1
$ dotnet build
$ cd /at/c#/test2/lib2
$ dotnet build
$ cd /at/c#/test2/server
$ dotnet build
```
4. Run:
```
$ cd /at/c#/test2/server
$ dotnet run --no-build
```
5. My output is:
```
ContainerBuilder time (ms): 1.4679
ConfigurationBuilder time (ms): 17.3975
config.Build() time (ms): 62.8747
Create module time (ms): 0.3222
Register module time (ms): 1.884
/at/c#/test2/lib1/bin/Debug/netcoreapp2.0/lib1.dll
Load time (ms): 42.4435
Lib1
BUILD
Build or Update time (ms): 240.074
Container work time (ms): 363.3694
________________________________
ContainerBuilder time (ms): 0.0047
ConfigurationBuilder time (ms): 0.1945
config.Build() time (ms): 1.2209
Create module time (ms): 0.0028
Register module time (ms): 0.0157
/at/c#/test2/lib2/bin/Debug/netcoreapp2.0/lib2.dll
Load time (ms): 0.663
Lib2
UPDATE
Build or Update time (ms): 7.6691
Container work time (ms): 9.212
```
