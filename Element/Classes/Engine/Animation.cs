namespace Element
{
    public class Animation
    {
        public string Name { get; set; }
        public SpriteSheet Sprites { get; set; }
        public int StartFrame { get; set; }
        public int FrameCount { get; set; }
        public int[] FrameOrder { get; set; }
        public double SecondsPerFrame { get; set; }

        public Animation(string name, SpriteSheet sprites, int startFrame, int frameCount, double secondsPerFrame)
        {
            Name = name;
            Sprites = sprites;
            StartFrame = startFrame;
            FrameCount = frameCount;
            SecondsPerFrame = secondsPerFrame;
            FrameOrder = new int[FrameCount];
            for (int i = 0; i < FrameCount; i++)
                FrameOrder[i] = i;
        }

        public Animation(string name, SpriteSheet sprites, int startFrame, int[] frameOrder, double secondsPerFrame)
        {
            Name = name;
            Sprites = sprites;
            StartFrame = startFrame;
            FrameCount = frameOrder.Length;
            SecondsPerFrame = secondsPerFrame;
            FrameOrder = frameOrder;
        }
    }
}
