﻿using System.Collections.Generic;

namespace Aiwins.Rocket.VirtualFileSystem {
    public class VirtualFileSetList : List<IVirtualFileSet> {
        public List<string> PhysicalPaths { get; }

        public VirtualFileSetList () {
            PhysicalPaths = new List<string> ();
        }
    }
}