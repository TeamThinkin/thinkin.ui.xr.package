using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class TabletNetworkSyncModel
{
    //[RealtimeProperty(1, true, true)]
    //public int myProp;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class TabletNetworkSyncModel : RealtimeModel {
    public TabletNetworkSyncModel() : base(null) {
    }
    
    protected override int WriteLength(StreamContext context) {
        return 0;
    }
    protected override void Write(WriteStream stream, StreamContext context) {
    }
    protected override void Read(ReadStream stream, StreamContext context) {
    }
    private void UpdateBackingFields() {
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
