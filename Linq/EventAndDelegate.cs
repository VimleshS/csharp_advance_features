using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventAndDelegate
{

    //Events and delegates
    /*
     * Without it breaks the OC principles
     * Event programming / Pub-Sub pattern / Observer
     * 
     * 1- Define a delegate (***EventHandler)
     * 2- Define an event based on that delegate (past tense like "VideoEncoded")
     * 3- Raise that event ( protected, virtual, void, and starts with On..  )
     * 
     * Use Inbuild eventHandler 
     * EventHandler, EventHandler<TEventArgs> instead of creating a delegate and event
     */
    public class MailService
    {
        public void OnVideoEncoded(object sender, EventArgs args)
        {
            Console.WriteLine("Sending mail");
        }
    }

    public class Video
    {
        public string Name { get; set; }
    }

    public class VideoEncoder
    {
        public delegate void VideoEncodedEventHandler(object sender, EventArgs args);
        public event VideoEncodedEventHandler VideoEncoded; 
        public void Encode(Video video)
        {
            Console.WriteLine("Encoding");
            Thread.Sleep(3000);

            OnVideoEncoded();
        }

        protected virtual void OnVideoEncoded()
        {
            if (VideoEncoded != null)
                VideoEncoded(this, EventArgs.Empty);
        }
    }

    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }

    public class MailServiceWithVideoArgs
    {
        public void OnVideoEncoded(object sender, VideoEventArgs args)
        {
            Console.WriteLine("Sending mail MailServiceWithVideoArgs " + args.Video.Name);
        }
    }


    public class VideoEncoderWithCLRDelegate
    {
        //public delegate void VideoEncodedEventHandler(object sender, EventArgs args);
        //public event VideoEncodedEventHandler VideoEncoded;

        public EventHandler<VideoEventArgs> VideoEncoded;

        public void Encode(Video video)
        {
            Console.WriteLine("Encoding with VideoEncoderWithCLRDelegate " + video.Name);
            Thread.Sleep(3000);

            OnVideoEncoded(video);
        }

        protected virtual void OnVideoEncoded(Video video)
        {
            if (VideoEncoded != null)
                VideoEncoded(this, new VideoEventArgs() {Video = video });
        }
    }

}
