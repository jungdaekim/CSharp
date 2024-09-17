﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace MultiThreadedPing
{
    internal class NetPinger
    {
        #region Initialize
        public string BaseIP = "192.168.0.";
        public int StartIP = 1;
        public int StopIP = 255;
        public string ip;
        public int timeout = 100;
        public int nFound = 0;

        static object lockObj = new object();
        Stopwatch stopWatch = new Stopwatch();
        public TimeSpan ts;

        // Event stuff
        public event EventHandler<string> PingEvent;

        public IPHostEntry host;
        public List<HostData> hosts = new List<HostData>();
        #endregion

        #region Ping Methods
        public async void RunPingSweep_Async()
        {
            // Pinging of each of the 255 IP addresses will be an individual
            // task/thread, and those Tasks will be stored in a List<Task>
            var tasks=new List<Task>();

            stopWatch.Restart();
            nFound = 0;

            for(int i=StartIP;i<=StopIP; i++)
            {
                // Construct the full IP address for each Task to ping
                ip = BaseIP + i.ToString();

                // Make a new Ping object for each IP address to be pin'ed
                Ping p = new Ping();
                var task = PingAndUpdateAsync(p, ip);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks).ContinueWith(t =>
            {
                stopWatch.Stop();
                ts = stopWatch.Elapsed;
            });

            PingEvent?.Invoke(this, ts.ToString());
        }



        private async Task PingAndUpdateAsync(Ping ping, string ip)
        {
            // Do the actual Ping'ing to each IP address using System.Net.Ping.dll;
            // The "ConfigureAwait(false)" allows any thread other than the main UI
            // thread to continue the method when the SendPingAsync is done.This frees
            // the UI thread.
            var reply=await ping.SendPingAsync(ip, timeout).ConfigureAwait(false);

            if(reply.Status==IPStatus.Success)
            {
                // If a device ("host") was found, get its host properties(name, etc)
                host=Dns.GetHostEntry(ip);
                hosts.Add(new HostData(host, ip));

                // Synchronizes access to the private "nFound" field by
                // locking on a dedicated "lockObj" instance. This ensures
                // that the nFound field cannot be updated simultaneously
                // by two threads attempting to call the Ping methods
                // simultaneously. Instead, one will get access while the
                // other waits its turn.

                lock (lockObj)
                {
                    nFound++;
                }

            }
        }

        #endregion
    }
}
