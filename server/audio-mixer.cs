using AudioSwitcher.AudioApi.CoreAudio;
using AudioSwitcher.AudioApi.Session;
using MixerTypes;

namespace AudioSwitcherFace
{
    public class Mixer
    {
        public List<IAudioSession> session_list = new List<IAudioSession>();

        public Mixer ()
	    {
		    GenerateSessionList();
        }

        private static IAudioSessionController GetAudioProcesses()
        {
            CoreAudioDevice device = new CoreAudioController().DefaultPlaybackDevice;
            Console.WriteLine(device);
            IAudioSessionController sessionCtrl = device.GetCapability<IAudioSessionController>();
            return sessionCtrl;
        }

        private void GenerateSessionList()
        {
            foreach (IAudioSession session in GetAudioProcesses())
            {
                session_list.Add(session);
            }
        }

        public List<ResSessionObj> GetSessions()
        {   
            List<ResSessionObj> sessions = new List<ResSessionObj>();
            foreach (IAudioSession session in GetAudioProcesses())
            {
                sessions.Add(new ResSessionObj(
                    session.Volume,
                    session.ProcessId,
                    session.IsMuted,
                    session.DisplayName));
            }
            return sessions;
        }
    }
}

