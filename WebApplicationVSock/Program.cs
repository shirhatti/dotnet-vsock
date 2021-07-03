using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Tmds.Linux;
using WebApplicationVSock;

using static Tmds.Linux.LibC;

var handle = socket(AF_VSOCK, SOCK_STREAM, 0);
sockaddr_vm address = new sockaddr_vm()
{
    sa_family = AF_VSOCK,
    svm_reserved1 = 0,
    svm_port = uint.MaxValue, // VMADDR_PORT_ANY
    svm_cid = uint.MaxValue // VMADDR_CID_ANY
};
unsafe
{
    _ = bind(handle, (sockaddr*)&address, 16);
}

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenHandle((ulong)handle);
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", (Func<string>)(() => "Hello World!"));

app.Run();
