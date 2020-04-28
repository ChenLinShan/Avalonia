using System;
using Avalonia.Data;

#nullable enable

namespace Avalonia
{
    /// <summary>
    /// Provides information for a avalonia property change.
    /// </summary>
    public abstract class AvaloniaPropertyChangedEventArgs : EventArgs
    {
        public AvaloniaPropertyChangedEventArgs(
            IAvaloniaObject sender,
            BindingPriority priority)
        {
            Sender = sender;
            Priority = priority;
            IsEffectiveValueChange = true;
        }

        /// <summary>
        /// Gets the <see cref="AvaloniaObject"/> that the property changed on.
        /// </summary>
        /// <value>The sender object.</value>
        public IAvaloniaObject Sender { get; }

        /// <summary>
        /// Gets the property that changed.
        /// </summary>
        /// <value>
        /// The property that changed.
        /// </value>
        public AvaloniaProperty Property => GetProperty();

        /// <summary>
        /// Gets the old value of the property.
        /// </summary>
        /// <remarks>
        /// When <see cref="IsEffectiveValueChange"/> is true, returns the old value of the property on
        /// the object. When <see cref="IsEffectiveValueChange"/> is false, returns
        /// <see cref="AvaloniaProperty.UnsetValue"/>.
        /// </remarks>
        public object? OldValue => GetOldValue();

        /// <summary>
        /// Gets the new value of the property.
        /// </summary>
        /// <remarks>
        /// When <see cref="IsEffectiveValueChange"/> is true, returns the value of the property on the object.
        /// When <see cref="IsEffectiveValueChange"/> is false returns the changed value, or
        /// <see cref="AvaloniaProperty.UnsetValue"/> if the value was removed.
        /// </remarks>
        public object? NewValue => GetNewValue();

        /// <summary>
        /// Gets the priority of the binding that produced the value.
        /// </summary>
        /// <value>
        /// The priority of the new value.
        /// </value>
        public BindingPriority Priority { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the change represents a change to the effective value of
        /// the property.
        /// </summary>
        /// <remarks>
        /// A property listener created via <see cref="IAvaloniaObject.Listen{T}(StyledPropertyBase{T})"/>
        /// signals all property changes, regardless of whether a value with a higer priority is present.
        /// When this property is false, the change that is being signalled has not resulted in a change
        /// to the property value on the object.
        /// </remarks>
        public bool IsEffectiveValueChange { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the value of the property on the object has already
        /// changed since this change began notifying.
        /// </summary>
        public bool IsOutdated { get; private set; }

        internal void MarkOutdated() => IsOutdated = true;
        internal void MarkNonEffectiveValue() => IsEffectiveValueChange = false;
        protected abstract AvaloniaProperty GetProperty();
        protected abstract object? GetOldValue();
        protected abstract object? GetNewValue();
    }
}
