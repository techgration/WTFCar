﻿1433 | % { echo ((new-object Net.Sockets.TcpClient).Connect("techgration.ddns.net",$_)) "server listening on TCP port $_" }