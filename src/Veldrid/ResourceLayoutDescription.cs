using System;

namespace Veldrid
{
    /// <summary>
    /// Describes the layout of <see cref="BindableResource"/> objects for a <see cref="Pipeline"/>.
    /// </summary>
    public struct ResourceLayoutDescription : IEquatable<ResourceLayoutDescription>
    {
        /// <summary>
        /// An array of <see cref="ResourceLayoutElementDescription"/> objects, describing the properties of each resource
        /// element in the <see cref="ResourceLayout"/>.
        /// </summary>
        public ResourceLayoutElementDescription[] Elements;

        /// <summary>
        /// Whether the last element is a variable amount of textures or a single texture.
        /// </summary>
        public bool LastElementParams;

        /// <summary>
        /// Constructs a new ResourceLayoutDescription.
        /// </summary>
        /// <param name="elements">An array of <see cref="ResourceLayoutElementDescription"/> objects, describing the properties
        /// of each resource element in the <see cref="ResourceLayout"/>.</param>
        public ResourceLayoutDescription(params ResourceLayoutElementDescription[] elements)
        {
            LastElementParams = false;
            Elements = elements;
        }

        /// <summary>
        /// Constructs a new ResourceLayoutDescription.
        /// </summary>
        /// <param name="lastElementParams">VULKAN ONLY! Set true if the last element is a variable amount of elements of the same type.</param>
        /// <param name="elements">An array of <see cref="ResourceLayoutElementDescription"/> objects, describing the properties
        /// of each resource element in the <see cref="ResourceLayout"/>.</param>
        public ResourceLayoutDescription(bool lastElementParams, params ResourceLayoutElementDescription[] elements)
        {
            LastElementParams = lastElementParams;
            Elements = elements;
        }

        /// <summary>
        /// Element-wise equality.
        /// </summary>
        /// <param name="other">The instance to compare to.</param>
        /// <returns>True if all array elements are equal; false otherswise.</returns>
        public bool Equals(ResourceLayoutDescription other)
        {
            return Util.ArrayEqualsEquatable(Elements, other.Elements);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return HashHelper.Array(Elements);
        }
    }
}
