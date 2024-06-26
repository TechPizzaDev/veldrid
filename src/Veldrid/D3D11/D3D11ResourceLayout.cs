﻿namespace Veldrid.D3D11
{
    internal sealed class D3D11ResourceLayout : ResourceLayout
    {
        private readonly ResourceBindingInfo[] _bindingInfosByVdIndex;
        private string? _name;
        private bool _disposed;

        public int UniformBufferCount { get; }
        public int StorageBufferCount { get; }
        public int TextureCount { get; }
        public int SamplerCount { get; }

        public D3D11ResourceLayout(in ResourceLayoutDescription description)
            : base(description)
        {
            ResourceLayoutElementDescription[] elements = description.Elements;
            _bindingInfosByVdIndex = new ResourceBindingInfo[elements.Length];

            int cbIndex = 0;
            int texIndex = 0;
            int samplerIndex = 0;
            int unorderedAccessIndex = 0;

            for (int i = 0; i < _bindingInfosByVdIndex.Length; i++)
            {
                int slot = elements[i].Kind switch
                {
                    ResourceKind.UniformBuffer => cbIndex++,
                    ResourceKind.StructuredBufferReadOnly => texIndex++,
                    ResourceKind.StructuredBufferReadWrite => unorderedAccessIndex++,
                    ResourceKind.TextureReadOnly => texIndex++,
                    ResourceKind.TextureReadWrite => unorderedAccessIndex++,
                    ResourceKind.Sampler => samplerIndex++,
                    _ => Illegal.Value<ResourceKind, int>(),
                };

                _bindingInfosByVdIndex[i] = new ResourceBindingInfo(
                    slot,
                    elements[i].Stages,
                    elements[i].Kind,
                    (elements[i].Options & ResourceLayoutElementOptions.DynamicBinding) != 0);
            }

            UniformBufferCount = cbIndex;
            StorageBufferCount = unorderedAccessIndex;
            TextureCount = texIndex;
            SamplerCount = samplerIndex;
        }

        public ResourceBindingInfo GetDeviceSlotIndex(int resourceLayoutIndex)
        {
            if (resourceLayoutIndex >= _bindingInfosByVdIndex.Length)
            {
                void Throw()
                {
                    throw new VeldridException($"Invalid resource index: {resourceLayoutIndex}. Maximum is: {_bindingInfosByVdIndex.Length - 1}.");
                }
                Throw();
            }

            return _bindingInfosByVdIndex[resourceLayoutIndex];
        }

        public override string? Name
        {
            get => _name;
            set => _name = value;
        }

        public override bool IsDisposed => _disposed;

        public override void Dispose()
        {
            _disposed = true;
        }

        internal readonly struct ResourceBindingInfo
        {
            public readonly int Slot;
            public readonly ShaderStages Stages;
            public readonly ResourceKind Kind;
            public readonly bool DynamicBuffer;

            public ResourceBindingInfo(int slot, ShaderStages stages, ResourceKind kind, bool dynamicBuffer)
            {
                Slot = slot;
                Stages = stages;
                Kind = kind;
                DynamicBuffer = dynamicBuffer;
            }
        }
    }
}
