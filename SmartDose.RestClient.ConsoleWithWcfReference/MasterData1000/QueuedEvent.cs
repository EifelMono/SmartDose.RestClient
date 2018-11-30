﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterData1000
{
    public class QueuedEvent<TEvent> where TEvent : Enum
    {
        protected ConcurrentQueue<TEvent> Queue = new ConcurrentQueue<TEvent>();
        protected object CompletionSourceLock = new object();
        protected TaskCompletionSource<bool> CompletionSource
                    = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

        public QueuedEvent()
        {
            Init();
        }

        public Action<TEvent> OnNew { get; set; }

        public void New(TEvent newEvent)
        {
            try
            {
                Task.Run(() =>
                {
                    try
                    {
                        Queue.Enqueue(newEvent);
                        var setResult = false;
                        lock (CompletionSourceLock)
                            try
                            {
                                setResult = CompletionSource.TrySetResult(true);
                            }
                            catch { }

                        Task.Run(() =>
                        {
                            try
                            {
                                OnFire?.Invoke(newEvent);
                            }
                            catch { } // let the user Handle this
                            if (!setResult)
                            {
                                var tries = 0;
                                while (tries++ < 10)
                                {
                                    try
                                    {
                                        if (CompletionSource.TrySetResult(true))
                                            break;
                                    }
                                    catch { }
                                    Thread.Sleep(100);
                                }
                            }
                        });
                    }
                    catch { }
                });
            }
            catch { }
        }

        public void Init()
        {
            lock (CompletionSourceLock)
                CompletionSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        }

        public async Task<(bool Ok, TEvent Value)> Next()
        {
            var value = default(TEvent);
            if (!Queue.TryDequeue(out value))
            {
                try
                {
                    await (CompletionSource.Task).ConfigureAwait(false);
                }
                catch { }
                Init();
                if (!Queue.TryDequeue(out value))
                    return (false, default(TEvent));
            }
            return (true, value);
        }
    }
}
