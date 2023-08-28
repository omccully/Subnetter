# Subnetter

A C# CLI program created in 2012 to provide information about a given subnet mask or IPv4 address. 

## Demo

![Demo](/Demo/demo.gif)

Here is a text output showing more usage (&gt; is the input prompt):

```
>?
clear
exit
ip
sm
title
width
beep
color
>sm
SM>255.255.224.0
Info for 255.255.224.0:
11111111.11111111.11100000.00000000
Class: N/A
8190 hosts per subnet
8 subnetworks
Possible subnet addresses:
        000     NNN.NNN.0.HHH   1-30
        001     NNN.NNN.32.HHH  33-62
        010     NNN.NNN.64.HHH  65-94
        011     NNN.NNN.96.HHH  97-126
        100     NNN.NNN.128.HHH 129-158
        101     NNN.NNN.160.HHH 161-190
        110     NNN.NNN.192.HHH 193-222
        111     NNN.NNN.224.HHH 225-254
SM>ip
IP>10.23.22.1
Info for 10.23.22.1:
00001010.00010111.00010110.00000001
Class: A
Usability: Private
Default subnet mask: 255.0.0.0
IP>172.17.1.1
Info for 172.17.1.1:
10101100.00010001.00000001.00000001
Class: B
Usability: Private
Default subnet mask: 255.255.0.0
IP>172.34.52.12
Info for 172.34.52.12:
10101100.00100010.00110100.00001100
Class: B
Usability: Public
Default subnet mask: 255.255.0.0
IP>
```

