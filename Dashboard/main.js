const serverURL = "wss://velnet.ugavel.com/ws";

function share_video(roomName, subVideo) {

    let config = {
        iceServers: [
            {
                urls: "stun:stun.l.google.com:19302",
            },
        ],
    };

    let signalLocal = new Signal.IonSFUJSONRPCSignal(serverURL);

    let clientLocal = new IonSDK.Client(signalLocal, config);

    signalLocal.onopen = () => clientLocal.join(roomName);

    clientLocal.ontrack = (track, stream) => {
        console.log("got track: ", track.id, "for stream: ", stream.id);
        if (track.kind === "video") {
            subVideo.srcObject = stream;
            subVideo.play();
        }
    }

}
