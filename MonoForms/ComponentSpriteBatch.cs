// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentSpriteBatch.cs" company="">
//   Copyright (c) 2014 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonoForms
{
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Used by the components of XnaForms to improve and secure the use of a common <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/>.
    /// </summary>
    public class ComponentSpriteBatch
    {
        private SpriteBatch spriteBatch;
        private BatchState state;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentSpriteBatch"/> class.
        /// </summary>
        /// <param name="device">
        /// A <see cref="GraphicsDevice"/> where graphics will be drawn onto.
        /// </param>
        public ComponentSpriteBatch(GraphicsDevice device)
        {
            this.spriteBatch = new SpriteBatch(device);
            this.state = BatchState.Initialized;
        }

        /// <summary>
        /// Gets the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> used by this <see cref="ComponentSpriteBatch"/>. It is not recommended to call its <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.Begin()"/> and <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.End()"/> methods.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get
            {
                return this.spriteBatch;
            }
        }

        /// <summary>
        /// Gets a <see cref="BatchState"/> indicating the current state of this <see cref="ComponentSpriteBatch"/>.
        /// </summary>
        public BatchState State
        {
            get
            {
                return this.state;
            }
        }

        /// <summary>
        /// Begins a sprite batch operation using deferred sort and default state objects (<see cref="BlendState.AlphaBlend"/>, <see cref="SamplerState.LinearClamp"/>, <see cref="DepthStencilState.None"/>, <see cref="RasterizerState.CullCounterClockwise"/>).
        /// </summary>
        /// <param name="forceBegin">
        /// A value indicating whether the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> of this <see cref="ComponentSpriteBatch"/> should be forced to begin.
        /// </param>
        /// <returns>
        /// A value indicating whether the operation was successful.
        /// </returns>
        public bool Begin(bool forceBegin)
        {
            return this.BeginSpriteBatch(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, forceBegin);
        }

        /// <summary>
        /// Begins a sprite batch operation using the specified sort and blend state object and default state objects
        /// (<see cref="SamplerState.LinearClamp"/>, <see cref="DepthStencilState.None"/>, <see cref="RasterizerState.CullCounterClockwise"/>). If you pass a null blend state,
        /// the default is <see cref="BlendState.AlphaBlend"/>.
        /// </summary>
        /// <param name="sortMode">
        /// Sprite drawing order.
        /// </param>
        /// <param name="blendState">
        /// Blending options.
        /// </param>
        /// <param name="forceBegin">
        /// A value indicating whether the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> of this <see cref="ComponentSpriteBatch"/> should be forced to begin.
        /// </param>
        /// <returns>
        /// A value indicating whether the operation was successful.
        /// </returns>
        public bool Begin(SpriteSortMode sortMode, BlendState blendState, bool forceBegin)
        {
            if (blendState == null)
            {
                blendState = BlendState.AlphaBlend;
            }

            return this.BeginSpriteBatch(sortMode, blendState, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, forceBegin);
        }

        /// <summary>
        /// Begins a sprite batch operation using the specified sort, blend, sampler, depth stencil and rasterizer state objects.
        /// Passing null for any of the state objects selects the default state objects
        /// (<see cref="BlendState.AlphaBlend"/>, <see cref="SamplerState.LinearClamp"/>, <see cref="DepthStencilState.None"/>, <see cref="RasterizerState.CullCounterClockwise"/>).
        /// </summary>
        /// <param name="sortMode">
        /// Sprite drawing order.
        /// </param>
        /// <param name="blendState">
        /// Blending options.
        /// </param>
        /// <param name="samplerState">
        /// Texture sampling options.
        /// </param>
        /// <param name="depthStencilState">
        /// Depth and stencil options.
        /// </param>
        /// <param name="rasterizerState">
        /// Rasterization options.
        /// </param>
        /// <param name="forceBegin">
        /// A value indicating whether the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> of this <see cref="ComponentSpriteBatch"/> should be forced to begin.
        /// </param>
        /// <returns>
        /// A value indicating whether the operation was successful.
        /// </returns>
        public bool Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, bool forceBegin)
        {
            if (blendState == null)
            {
                blendState = BlendState.AlphaBlend;
            }

            if (samplerState == null)
            {
                samplerState = SamplerState.LinearClamp;
            }

            if (depthStencilState == null)
            {
                depthStencilState = DepthStencilState.None;
            }

            if (rasterizerState == null)
            {
                rasterizerState = RasterizerState.CullCounterClockwise;
            }

            return this.BeginSpriteBatch(sortMode, blendState, samplerState, depthStencilState, rasterizerState, forceBegin);
        }

        /// <summary>
        /// Flushes the <see cref="SpriteBatch"/> of this <see cref="ComponentSpriteBatch"/> and restores the <see cref="GraphicsDevice"/> state to how it was before.
        /// </summary>
        /// <param name="forceEnd">
        /// A value indicating whether the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> of this <see cref="ComponentSpriteBatch"/> should be forced to end.
        /// </param>
        /// <returns>
        /// A value indicating whether the operation was successful.
        /// </returns>
        public bool End(bool forceEnd)
        {
            if (this.state == BatchState.Started)
            {
                this.spriteBatch.End();
                this.state = BatchState.Ended;
                return true;
            }

            if (forceEnd)
            {
                this.spriteBatch.Begin();
                this.spriteBatch.End();
                return true;
            }

            return false;
        }

        private bool BeginSpriteBatch(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, bool forceBegin)
        {
            if (this.state == BatchState.Initialized || this.state == BatchState.Ended)
            {
                this.spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState);
                this.state = BatchState.Started;
                return true;
            }

            if (forceBegin)
            {
                this.spriteBatch.End();
                this.spriteBatch.Begin();
                this.state = BatchState.Started;
                return true;
            }

            return false;
        }
    }
}
