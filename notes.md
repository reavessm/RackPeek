```
dotnet publish -c Release -r osx-arm64 --self-contained true -p:PublishSingleFile=true
sudo mv bin/Release/net*/osx-arm64/publish/RackPeek /usr/local/bin/rpk
sudo chmod +x /usr/local/bin/rpk

```

```
cd vhs
vhs ./rpk.tape
```