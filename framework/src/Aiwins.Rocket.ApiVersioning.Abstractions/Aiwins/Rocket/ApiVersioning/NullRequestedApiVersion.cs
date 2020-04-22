﻿namespace Aiwins.Rocket.ApiVersioning {
    public class NullRequestedApiVersion : IRequestedApiVersion {
        public static NullRequestedApiVersion Instance = new NullRequestedApiVersion ();

        public string Current => null;

        private NullRequestedApiVersion () {

        }
    }
}