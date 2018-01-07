using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Element.Interfaces;
using TexturePackerLoader;

namespace Element.Interfaces
{
    public interface IDebug : IDraw, IUpdate
    {
        bool Enabled { get; }
        void Add(string text, double duration);
    }
}

namespace Element
{

    class Debug : IDebug
    {
        private sealed class Message
        {
            public string Text { get; set; }
            public double Duration { get; set; }
            private double _timeAlive { get; set; }

            public Message(string text, double duration)
            {
                this.Text = text;
                this.Duration = duration;
            }
            public void Update(GameTime gameTime)
            {
                this._timeAlive += gameTime.ElapsedGameTime.TotalSeconds;
            }

            public bool Expired
            {
                get
                {
                    if (this._timeAlive > this.Duration)
                        return true;
                    else
                        return false;
                }
            }
        }


        public Vector2 Position { get; set; }

        private bool _enabled;
        private SpriteFont font;
        private List<Message> _messages;
        private IContentManager _contentManager;

        public Debug(IContentManager contentManager)
        {
            this._messages = new List<Message>();
            this._contentManager = contentManager;
            font = _contentManager.GetFont("Arial");
            this._enabled = false;
            this._messages = new List<Message>();
        }

        public void Add(string text, double duration)
        {
            this._messages.Add(new Message(text, duration));
        }

        public bool Enabled
        {
            get { return this._enabled; }
            private set { this._enabled = value; }
        }

        public void Draw(SpriteRender spriteRender)
        {
            if (this.Enabled)
            { 
               float y = this.Position.Y;
               foreach (Message message in this._messages)
               {
                 spriteRender.spriteBatch.DrawString(font, message.Text, new Vector2(this.Position.X, y), Color.White);
                 y += this.font.LineSpacing; // use height of font to find next row
               }
            }
        }

        public float Height { get => this.font.LineSpacing * this._messages.Count; }
        public int Count { get => this._messages.Count;  }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _messages.Count; i++)
            {
                this._messages[i].Update(gameTime);
                if (this._messages[i].Expired)
                {
                    this._messages.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
