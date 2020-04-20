﻿using System.Threading.Tasks;

namespace Aiwins.Rocket.ExceptionHandling {
    public class NullExceptionNotifier : IExceptionNotifier {
        public static NullExceptionNotifier Instance { get; } = new NullExceptionNotifier ();

        private NullExceptionNotifier () {

        }

        public Task NotifyAsync (ExceptionNotificationContext context) {
            return Task.CompletedTask;
        }
    }
}