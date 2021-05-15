iisreset /stop

# copy files
# D:\techgration\WTFCar\WTFCar\WTFCar\bin
# Copy-Item "C:\Wabash\Logfiles\mar1604.log.txt" -Destination "C:\Presentation"

Copy-Item "D:\techgration\WTFCarWeb" -Destination "D:\inetpub\wwwroot" -Recurse -Force

iisreset /start