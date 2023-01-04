public class Handshake
{
    public bool success { get; set; }
    public MapObject? MAP { get; set; }
    public HandshakeError? error { get; set; }
    public bool isHost { get; set; }

    public Handshake(bool success, MapObject? MAP, HandshakeError? error, bool isHost)
    {
        this.success = success;
        this.MAP = MAP;
        this.error = error;
        this.isHost = isHost;
    }
}

public enum HandshakeError
{
    SER_MAX_CAP,
    GAME_STARTED,
    MAT_USR
}