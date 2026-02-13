# Marge
A .NET Framework library that provides query parameters to control size and format of the requested image.

> I'll optimise that for you, sweetie pie!

![I'll krump with you, sweetie pie!](doc/assets/krump.webp)

It uses the ImageMagick library for C# and then maps parameters to certain functionalities. 

These are the ones that the project started with:
- Format
- Dimensions
- Quality

To get it going, integrate it into your project and add this into the `web.config` file.

```xml
<configuration>
    <system.webServer>
        <modules>
            <add name="Marge" type="Marge.Module, Marge" />
        </modules>
    </system.webServer>
</configuration>
```

You can then query images like: `/images/example.jpeg?format=webp&width=300`

If it is the first time it sees this kind of reason, it may take a moment, but afterwards it'll quicken. This is as the resulting image will be saved, it'll be eventually pruned. If it's requested often, it stays for longer.

- [ ] Cache Background Service implemented
- [ ] Provide an extensive configuration possibilites
- [ ] A plugin system?  