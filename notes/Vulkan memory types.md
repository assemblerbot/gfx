# Vulkan memory types
First of all, the API will tell you which memory types support the resource you're trying to create. From those options, you pick one that fits your particular use case.

You'll want a memory type that is DEVICE_LOCAL for textures, storage buffers written by the GPU and especially for render targets/depth stencil images. GPU access to DEVICE_LOCAL memory is much faster.

If you want to access it on the CPU, you'll need HOST_VISIBLE. On systems that support resizable BAR, the entire VRAM is HOST_VISIBLE. On systems that don't support it, there's usually a third memory heap with a size of 256MB that's both HOST_VISIBLE and DEVICE_LOCAL.

If you have a buffer that you want to read on the CPU, you basically need HOST_CACHED because reading uncached memory is insanely slow. If you just write (memcpy) to it, you don't need that.

Then there's HOST_COHERENT that decides whether or not you need to invalidate/flush the memory before and after accessing it on the CPU. If you use a HOST_COHERENT memory type, you don't need to do that. On desktop GPUs all HOST_VISIBLE memory heaps are usually also HOST_COHERENT.

Here are a couple of examples for what you'd typically use:

Render targets, depth stencil images, storage images/buffers written by the GPU: DEVICE_LOCAL

Textures/vertex or index buffers: DEVICE_LOCAL, but falling back to HOST_VISIBLE when you run out of VRAM is less catastrophic than for the category above.

Staging buffer you use to do a copy on the GPU into another resource: HOST_VISIBLE

Uniform buffers: HOST_VISIBLE + DEVICE_LOCAL, fallback to pure HOST_VISIBLE

Read back buffer you copy GPU results into to read on the CPU: HOST_VISIBLE + HOST_CACHED