using Rage;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

public delegate byte[] ProcessDataDelegate(string data);

public class SimpleServer
{
    private const int HandlerThread = 3;
    private readonly ProcessDataDelegate handler;
    private readonly HttpListener listener;

    public SimpleServer(HttpListener listener, string url, ProcessDataDelegate handler)
    {
        this.listener = listener;
        this.handler = handler;
        listener.Prefixes.Add(url);
    }

    public void Start()
    {
        if (listener.IsListening)
            return;

        try
        {
            listener.Start();
            for (int i = 0; i < HandlerThread; i++)
            {
                listener.GetContextAsync().ContinueWith(ProcessRequestHandler);
            }
        }
        catch (Exception ex)
        {
            Game.LogTrivial($"Server failed: {ex}");
        }
    }

    public void Stop()
    {
        if (listener.IsListening)
            listener.Stop();
    }

    private async void ProcessRequestHandler(Task<HttpListenerContext> result)
    {
        var context = await result;

        if (!listener.IsListening)
            return;

        // Avvia nuovo listener prima di elaborare
        listener.GetContextAsync().ContinueWith(ProcessRequestHandler);

        try
        {
            // Leggi la richiesta
            string request = new StreamReader(context.Request.InputStream).ReadToEnd();

            // Elabora la risposta
            var responseBytes = handler.Invoke(request);
            context.Response.ContentLength64 = responseBytes.Length;

            // Invia risposta
            await context.Response.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
        catch (Exception ex)
        {
            Game.LogTrivial($"Error: {ex}");
            context.Response.StatusCode = 500;
        }
        finally
        {
            context.Response.Close();
        }
    }
}