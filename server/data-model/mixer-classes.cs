// interfaces delivered by the API
// as well as some internal ones

using AudioSwitcher.AudioApi.CoreAudio;
using AudioSwitcher.AudioApi.Session;
using AudioSwitcherFace;
using System.Data;

namespace MixerTypes
{
    // incoming session object
    public class SessionObj
    {
        public double Volume { get; set; }
        public int ProcessId { get; set; }
        public bool Muted { get; set; }
        public string? Name { get; set; }
    }

    public class ResSessionObj : SessionObj
    {
        public ResSessionObj(double sVolume, int sProcessId, bool sMuted, string sName)
        {
            Volume = sVolume;
            ProcessId = sProcessId;
            Muted = sMuted;
            Name = (sName == "@%SystemRoot%\\System32\\AudioSrv.Dll,-202") ? "System Sounds" : sName;
        }
    }

    // upgrade request
    public class CSessionUpdate() : SessionObj
    {
        private IAudioSession _AudioSession;
        public Mixer SMixer => SMixer;

        public CSessionUpdate(SessionObj seshObject, Mixer SMixer) : this()
        {
            Volume = seshObject.Volume;
            ProcessId = seshObject.ProcessId;
            Muted = seshObject.Muted;
            _AudioSession = SMixer.session_list.First(session => session.ProcessId == ProcessId);
            ApplyUpdate();
        }

        public void ApplyUpdate()
        {
            _AudioSession.SetVolumeAsync(Volume);
            _AudioSession.SetMuteAsync(Muted);
        }
    }
}
