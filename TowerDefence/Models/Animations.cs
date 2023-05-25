using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TowerDefence.Models;

public class Animation
{
    public int FrameWidth;
    public int FrameHeight;
    public int FramesCount;
    private int currentFrame;
    private readonly double frameTime;
    private double frameTimeLeft;
    private bool isAnimationStopped;
    private readonly Texture2D texture;
    private readonly List<Rectangle> frames = new();

    public Animation(Texture2D texture, int framesCount, double frameTime, int frameWidth, int frameHeight)
    {
        this.texture = texture;
        FramesCount = framesCount;
        this.frameTime = frameTime;
        FrameHeight = frameHeight;
        FrameWidth = frameWidth;
        for (var i = 0; i < texture.Height / FrameHeight; i++)
        {
            for (var j = 0; j < texture.Width / FrameWidth; j++)
            {
                frames.Add(new Rectangle(j * frameWidth, i * frameHeight, frameWidth, FrameHeight));
            }
        }
    }

    public void StopAnimation()
    {
        isAnimationStopped = true;
    }

    public void Update(GameTime gameTime)
    {
        if (isAnimationStopped)
            return;

        frameTimeLeft -= gameTime.ElapsedGameTime.TotalMilliseconds;

        if (frameTimeLeft > 0) return;
        frameTimeLeft += frameTime;
        currentFrame = (currentFrame + 1) % FramesCount;
    }

    public void Draw(SpriteBatch spriteBatch, Rectangle position, Color color)
    {
        spriteBatch.Draw(texture, position, frames[currentFrame], color);
    }
}
