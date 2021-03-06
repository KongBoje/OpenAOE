﻿using OpenAOE.Engine.Data;
using OpenAOE.Engine.Exceptions;

namespace OpenAOE.Engine.Entity
{
    /// <summary>
    /// Read-only interface to an entity. This is the only interface that should leak from the Engine to the rendering/host
    /// application.
    /// </summary>
    public interface IReadOnlyEntity : IHasComponents
    {
        /// <summary>
        /// Unique ID used to refer to this entity.
        /// </summary>
        uint Id { get; }

        /// <summary>
        /// Returns the component of type <typeparamref name="T" /> from the current tick.
        /// </summary>
        /// <exception cref="ComponentAccessException">Thrown if the component does not exist on the entity.</exception>
        /// <typeparam name="T">The interface type of the component you wish to fetch.</typeparam>
        /// <returns>The component of type <typeparamref name="T" /> from the current tick.</returns>
        T Current<T>() where T : class, IComponent;
    }
}
