# fsrs

portable f# reverse shell (self-contained executable)

**Requirements**

Ubuntu 18.04, works fine with kali 2019.4
https://www.mono-project.com/download/stable/#download-lin

```
root@kali:/opt/fsrs# apt-get install fsharp
Reading package lists... Done
Building dependency tree       
Reading state information... Done
fsharp is already the newest version (4.5-0xamarin9+ubuntu1804b1).
0 upgraded, 0 newly installed, 0 to remove and 156 not upgraded.
```

**Usage**

Edit listener IP/Domain, Port values in fsrs.fsx.

Generate strong name key, compile to standalone .exe. 

```
root@kali:/opt/fsrs# sn -k fsrs.snk
Mono StrongName - version 5.18.0.240
StrongName utility for signing assemblies
Copyright 2002, 2003 Motus Technologies. Copyright 2004-2008 Novell. BSD licensed.

A new 1024 bits strong name keypair has been generated in file 'fsrs.snk'.

root@kali:/opt/fsrs# fsharpc --target:winexe --keyfile:fsrs.snk --standalone fsrs.fsx 
Microsoft (R) F# Compiler version 10.2.3 for F# 4.5
Copyright (c) Microsoft Corporation. All Rights Reserved.

root@kali:/opt/fsrs# ls -lha
total 1.6M
drwxr-xr-x  2 root root 4.0K Nov 27 04:54 .
drwxr-xr-x 29 root root 4.0K Nov 27 02:56 ..
-rwxr-xr-x  1 root root 1.6M Nov 27 04:54 fsrs.exe
-rw-r--r--  1 root root 1.5K Nov 27 04:54 fsrs.fsx
-rw-------  1 root root  596 Nov 27 04:52 fsrs.snk
```

Drop execute fsrs.exe on host for reverse shell.


**AV**

Not bad out of the box, source code will need obfuscation for further bypass.

<img width="1307" alt="Screenshot 2019-11-27 at 04 57 39" src="https://user-images.githubusercontent.com/56988989/69695174-4a5d6500-10d3-11ea-8ec2-a13386a9b91e.png">


Enjoy~
