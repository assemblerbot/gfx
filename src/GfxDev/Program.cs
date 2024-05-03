using System.Numerics;
using Gfx;
using Silk.NET.Maths;
using Silk.NET.Windowing;

GfxTestApplication app = new ();
app.Run();

internal unsafe class GfxTestApplication
{
	private struct Vertex
	{
		public static uint Size => 3 * sizeof(float) + 4 * sizeof(float);
		
		public Vector3 Position;
		public Vector4 Color;
	}

	private const int         _desiredFramesInFlight = 2;
	private const SampleCount _sampleCount    = SampleCount.Count2; // TODO - device dependent, Count1 crashes! fix!

	private const GraphicsBackend _graphicsBackend = GraphicsBackend.Vulkan;
	private       IWindow         _window;
	public        IWindow         NativeWindow => _window;
	public        IView           View         => _window;

	private Api?            _api;
	private PhysicalDevice? _physicalDevice;
	private LogicalDevice?  _logicalDevice;
	private SwapChain?      _swapChain;

	// mesh
	private DeviceBuffer?                   _vertexBuffer;
	private DeviceMemory?                   _vertexBufferMemory;
	private DeviceBuffer?                   _indexBuffer;
	private DeviceMemory?                   _indexBufferMemory;
	private VertexInputBindingDescription   _vertexInputBindingDescription;
	private VertexInputAttributeDescription[] _vertexInputAttributeDescriptions;
	
	// shaders
	private Shader? _vertexShader;
	private Shader? _pixelShader;
	
	// commands
	private CommandBuffer[]? _commandBuffers;
	
	// pipeline stuff
	private DescriptorSetLayout? _descriptorSetLayout;
	private PipelineLayout?      _pipelineLayout;
	private RenderPass?          _renderPass;
	private GraphicsPipeline?    _pipeline;

	private int _swapChainBufferIndex = 0;
	
	public void Run()
	{
		InitWindow();
		_window.Run();

		// this cannot be called from OnClose .. for some reason
		_logicalDevice?.WaitIdle();
		CleanUp();
	}

	private void InitWindow()
	{
		//GraphicsBackend graphicsBackend = GfxApi.GetDefaultBackend();
		
		WindowOptions opts = new()
		                     {
			                     Title                   = "GfxTest",
			                     Position                = new Vector2D<int>(100,  100),
			                     Size                    = new Vector2D<int>(1024, 720),
			                     API                     = _graphicsBackend.ToGraphicsAPI(),
			                     VSync                   = true,
			                     ShouldSwapAutomatically = false,
			                     WindowState             = WindowState.Normal,
			                     WindowBorder            = WindowBorder.Resizable,
			                     IsVisible               = true,
		                     };
        
		_window = Window.Create(opts);
		
		_window.Load    += OnLoad;
		_window.Update  += OnUpdate;
		_window.Render  += OnRender;
		_window.Closing += OnClose;
		_window.Resize  += OnResize;
	}

	private void InitGfx()
	{
		ApiOptions options = new ApiOptions(_graphicsBackend, _window, DebugMessageLog);
		_api = Api.Create(options);
	}

	private void PickPhysicalDevice()
	{
		// physical device, TODO - move selection code to api, this shoud be just one line
		IReadOnlyList<PhysicalDevice> physicalDevices = _api!.EnumeratePhysicalDevices();

		foreach (PhysicalDevice device in physicalDevices)
		{
			Console.WriteLine($"{device.Name} kind={device.Kind}");
		}

		PhysicalDevice? bestPhysicalDevice = physicalDevices.FirstOrDefault(device => device.Kind == PhysicalDeviceKind.DiscreteGpu && device.SupportsGraphics);
		bestPhysicalDevice ??= physicalDevices.FirstOrDefault(device => device.Kind == PhysicalDeviceKind.IntegratedGpu && device.SupportsGraphics);
		bestPhysicalDevice ??= physicalDevices.FirstOrDefault(device => device.Kind == PhysicalDeviceKind.Cpu           && device.SupportsGraphics);

		Console.WriteLine($"Selected physical device: {bestPhysicalDevice?.Name}");
		if (bestPhysicalDevice == null)
		{
			return;
		}

		_physicalDevice = bestPhysicalDevice;
	}
	
	private void ShowPhysicalDeviceInfo()
	{
		var memoryProperties = _physicalDevice.GetMemoryInfo();
		foreach (var memoryInfo in memoryProperties.Memory)
		{
			Console.WriteLine($"  Memory kind:{memoryInfo.Properties:X} Heap index:{memoryInfo.HeapIndex}");
		}

		foreach (var heapInfo in memoryProperties.Heap)
		{
			Console.WriteLine($"  Heap kind:{heapInfo.Properties:X} Heap size:{heapInfo.Size}");
		}
	}
	
	private void CreateLogicalDevice()
	{
		_logicalDevice = _api!.CreateLogicalDevice(new LogicalDeviceOptions(_physicalDevice!));
	}

	private void CreateSwapChain()
	{
		_swapChain = _logicalDevice!.CreateSwapChain(new SwapChainOptions(DeviceFormat.B8G8R8Srgb, true, _desiredFramesInFlight, _sampleCount));
	}

	private void CreateMesh()
	{
		// prepare triangle
		Vertex[] vertices = new[]
		                    {
			                    new Vertex{Position = new(-0.5f, 0f, 0.5f), Color = new (1f,0f,0f,1f)},
			                    new Vertex{Position = new(0f, 0.5f, 0.5f), Color = new (0f,1f,0f,1f)},
			                    new Vertex{Position = new(0.5f, 0f, 0.5f), Color = new (0f,0f,1f,1f)},
		                    };

		ulong verticesSize = (ulong) (sizeof(Vertex) * vertices.Length); 

		{
			// create staging buffer
			using DeviceBuffer stagingBuffer = _logicalDevice.CreateBuffer(
				new DeviceBufferOptions(
					verticesSize,
					DeviceBufferUsage.TransferSrc,
					SharingMode.Exclusive
				)
			);

			stagingBuffer.GetMemoryRequirements(
				DeviceMemoryProperties.HostVisible | DeviceMemoryProperties.HostCoherent,
				out uint stagingMemoryIndex,
				out ulong stagingMemoryAlignment,
				out ulong stagingMemorySize
			);

			using DeviceMemory stagingBufferMemory = _logicalDevice.AllocateMemory(
				new DeviceMemoryOptions(
					stagingMemorySize,
					stagingMemoryIndex
				)
			);

			stagingBuffer.BindToMemory(stagingBufferMemory, 0);

			// fill staging buffer
			stagingBufferMemory.Write(0, vertices.AsSpan());
			
			// create vertex buffer
			_vertexBuffer = _logicalDevice.CreateBuffer(
				new DeviceBufferOptions(
					verticesSize,
					DeviceBufferUsage.TransferDst | DeviceBufferUsage.VertexBuffer,
					SharingMode.Exclusive
				)
			);

			_vertexBuffer.GetMemoryRequirements(
				DeviceMemoryProperties.DeviceLocal,
				out uint vertexBufferMemoryIndex,
				out ulong vertexBufferMemoryAlignment,
				out ulong vertexBufferMemorySize
			);

			_vertexBufferMemory = _logicalDevice.AllocateMemory(
				new DeviceMemoryOptions(
					vertexBufferMemorySize,
					vertexBufferMemoryIndex
				)
			);

			_vertexBuffer.BindToMemory(_vertexBufferMemory, 0);
			
			// copy data from staging buffer to vertex buffer
			{
				using CommandBuffer commandBuffer = _logicalDevice.CreateCommandBuffer(new CommandBufferOptions(CommandBufferLevel.Primary));

				commandBuffer.Begin(CommandBufferUsage.OneTimeSubmit);

				commandBuffer.CopyBuffer(stagingBuffer, _vertexBuffer, 0, 0, verticesSize);
				
				commandBuffer.End();

				_logicalDevice.QueueSubmit(DeviceQueue.Graphics, commandBuffer);
				_logicalDevice.QueueWaitIdle(DeviceQueue.Graphics);
			}
		}
		
		// vertex descriptions
		_vertexInputBindingDescription = new VertexInputBindingDescription(0, Vertex.Size, VertexInputRate.Vertex);
		_vertexInputAttributeDescriptions = new[]
		                                    {
			                                    new VertexInputAttributeDescription(
				                                    location: 0,
				                                    binding: 0,
				                                    deviceFormat: DeviceFormat.R32G32B32Sfloat,
				                                    offset: 0
			                                    ),
			                                    new VertexInputAttributeDescription(
				                                    location: 1,
				                                    binding: 0,
				                                    deviceFormat: DeviceFormat.R32G32B32A32Sfloat,
				                                    offset: 3*sizeof(float)
			                                    ),
		                                    };
	}

	private void CreateShaders()
	{
		byte[] vertexShaderCode = File.ReadAllBytes("test.vsh.spirv");
		_vertexShader = _logicalDevice.CreateShader(new ShaderOptions(vertexShaderCode, ShaderStage.Vertex, "main"));

		byte[] pixelShaderCode = File.ReadAllBytes("test.psh.spirv");
		_pixelShader = _logicalDevice.CreateShader(new ShaderOptions(pixelShaderCode, ShaderStage.Fragment, "main"));
	}

	private void CreateCommandBuffers()
	{
		_commandBuffers = new CommandBuffer[_swapChain.FramesInFlight];
		for (int i = 0; i < _swapChain.FramesInFlight; ++i)
		{
			_commandBuffers[i] = _logicalDevice.CreateCommandBuffer(new CommandBufferOptions(CommandBufferLevel.Primary));
		}
	}

	private void CleanUp()
	{
		_pipeline?.Dispose();
		_renderPass?.Dispose();
		_pipelineLayout?.Dispose();
		_descriptorSetLayout?.Dispose();

		if (_commandBuffers is not null)
		{
			for (int i = 0; i < _commandBuffers.Length; ++i)
			{
				_commandBuffers[i].Dispose();
			}
		}

		_vertexShader?.Dispose();
		_pixelShader?.Dispose();
		
		_vertexBuffer?.Dispose();
		_vertexBufferMemory?.Dispose();

		_indexBuffer?.Dispose();
		_indexBufferMemory?.Dispose();
		
		_swapChain?.Dispose();
		_logicalDevice?.Dispose();
		_api?.Dispose();
		_window.Dispose();
	}

	private void CreatePipeline()
	{
		_descriptorSetLayout = _logicalDevice.CreateDescriptorSetLayout(
			new DescriptorSetLayoutOptions() // no bindings yet
		);
		
		_pipelineLayout = _logicalDevice.CreatePipelineLayout(_descriptorSetLayout);

		_renderPass = _logicalDevice.CreateRenderPass(_swapChain, new RenderPassOptions());
		
		_pipeline = _logicalDevice.CreateGraphicsPipeline(
			new GraphicsPipelineOptions(
				PipelineCreateFlags.None,
				new Shader[]{_vertexShader, _pixelShader},
				new PipelineVertexInputStateOptions(new[]{_vertexInputBindingDescription}, _vertexInputAttributeDescriptions),
				new PipelineInputAssemblyStateOptions(PrimitiveTopology.TriangleList, false),
				new PipelineTessellationStateOptions(0),
				new PipelineViewportStateOptions(new Viewport[]{new Viewport(0,0,_swapChain.Width,_swapChain.Height,0,1)}, new Scissor[]{new Scissor(0,0,_swapChain.Width,_swapChain.Height)}),
				new PipelineRasterizationStateOptions(false, false, PolygonMode.Fill, CullMode.Back, FrontFace.CounterClockwise, false, 0, 0, 0, 1),
				new PipelineMultisampleStateOptions(_sampleCount, false, 0, default, false, false),
				new PipelineDepthStencilStateOptions(DepthStencilStateCreateFlags.None, true, true, CompareOp.Less, false, 0, 0, false, default, default),
				new PipelineColorBlendStateOptions(PipelineColorBlendStateCreateFlags.None, false, default,
					new PipelineColorBlendAttachmentState[]{
						                                       new (){BlendEnable = false, ColorWriteMask = ColorComponent.R|ColorComponent.G|ColorComponent.B|ColorComponent.A}
					                                       }
					),
				new PipelineDynamicStateOptions(default),
				_pipelineLayout,
				_renderPass,
				0
			)
		);
	}

	private void DebugMessageLog(DebugMessageSeverity severity, DebugMessageKind kind, string message)
	{
		Console.Out.WriteLine($"Severity:{severity} Kind:{kind} Message:{message}");
	}

	#region Main callbacks
	private void OnLoad()
	{
		InitGfx();
		PickPhysicalDevice();
		ShowPhysicalDeviceInfo();
		CreateLogicalDevice();
		CreateSwapChain();

		CreateMesh();
		CreateShaders();
		CreateCommandBuffers();
		CreatePipeline();
	}

	private void OnUpdate(double timeDelta)
	{
	}

	private void OnRender(double timeDelta)
	{
		
		// check this: https://medium.com/@sanskritdarshan/vulkan-in-5-minutes-c5f7ae5a8005
		
		_swapChain.WaitForFence(_swapChainBufferIndex);
		_swapChain.ResetFence(_swapChainBufferIndex);

		uint      imageIndex = 0;
		GfxResult result = _swapChain.AcquireNextImage(_swapChainBufferIndex, ref imageIndex);
		
		// TODO - rebuild by result

		if (result != GfxResult.Success && result != GfxResult.SuboptimalKhr)
		{
			throw new Exception("Failed to acquire swap chain image!");
		}

		CommandBuffer commandBuffer = _commandBuffers[_swapChainBufferIndex]; 
		commandBuffer.Begin();
		commandBuffer.BeginRenderPass(
			_renderPass, _swapChain, _swapChainBufferIndex,
			0.5f,0.2f,0.1f,1f,
			1f,0
		);

		commandBuffer.BindPipeline(_pipeline);
		commandBuffer.BindVertexBuffer(_vertexBuffer, 0, 0);

		commandBuffer.Draw(3, 1, 0, 0);
		
		commandBuffer.EndRenderPass();
		commandBuffer.End();

		_swapChain.Submit(_swapChainBufferIndex, commandBuffer);
		_swapChain.Present(_swapChainBufferIndex, imageIndex);

		_swapChainBufferIndex = (_swapChainBufferIndex + 1) % _swapChain.FramesInFlight;
		
	}

	private void OnClose()
	{
	}

	private void OnResize(Vector2D<int> size)
	{
		_logicalDevice?.WaitIdle();
		
		//TODO - destroy
		
		//TODO - create 
	}
	#endregion Main callbacks
}