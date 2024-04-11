using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanFormatExtensions
{
    public static ImageFormat Convert(this Format @this)
    {
        return @this switch
        {
            Format.Undefined => ImageFormat.Undefined,
            Format.R4G4UnormPack8 => ImageFormat.R4G4UnormPack8,
            Format.R4G4B4A4UnormPack16 => ImageFormat.R4G4B4A4UnormPack16,
            Format.B4G4R4A4UnormPack16 => ImageFormat.B4G4R4A4UnormPack16,
            Format.R5G6B5UnormPack16 => ImageFormat.R5G6B5UnormPack16,
            Format.B5G6R5UnormPack16 => ImageFormat.B5G6R5UnormPack16,
            Format.R5G5B5A1UnormPack16 => ImageFormat.R5G5B5A1UnormPack16,
            Format.B5G5R5A1UnormPack16 => ImageFormat.B5G5R5A1UnormPack16,
            Format.A1R5G5B5UnormPack16 => ImageFormat.A1R5G5B5UnormPack16,
            Format.R8Unorm => ImageFormat.R8Unorm,
            Format.R8SNorm => ImageFormat.R8SNorm,
            Format.R8Uscaled => ImageFormat.R8Uscaled,
            Format.R8Sscaled => ImageFormat.R8Sscaled,
            Format.R8Uint => ImageFormat.R8Uint,
            Format.R8Sint => ImageFormat.R8Sint,
            Format.R8Srgb => ImageFormat.R8Srgb,
            Format.R8G8Unorm => ImageFormat.R8G8Unorm,
            Format.R8G8SNorm => ImageFormat.R8G8SNorm,
            Format.R8G8Uscaled => ImageFormat.R8G8Uscaled,
            Format.R8G8Sscaled => ImageFormat.R8G8Sscaled,
            Format.R8G8Uint => ImageFormat.R8G8Uint,
            Format.R8G8Sint => ImageFormat.R8G8Sint,
            Format.R8G8Srgb => ImageFormat.R8G8Srgb,
            Format.R8G8B8Unorm => ImageFormat.R8G8B8Unorm,
            Format.R8G8B8SNorm => ImageFormat.R8G8B8SNorm,
            Format.R8G8B8Uscaled => ImageFormat.R8G8B8Uscaled,
            Format.R8G8B8Sscaled => ImageFormat.R8G8B8Sscaled,
            Format.R8G8B8Uint => ImageFormat.R8G8B8Uint,
            Format.R8G8B8Sint => ImageFormat.R8G8B8Sint,
            Format.R8G8B8Srgb => ImageFormat.R8G8B8Srgb,
            Format.B8G8R8Unorm => ImageFormat.B8G8R8Unorm,
            Format.B8G8R8SNorm => ImageFormat.B8G8R8SNorm,
            Format.B8G8R8Uscaled => ImageFormat.B8G8R8Uscaled,
            Format.B8G8R8Sscaled => ImageFormat.B8G8R8Sscaled,
            Format.B8G8R8Uint => ImageFormat.B8G8R8Uint,
            Format.B8G8R8Sint => ImageFormat.B8G8R8Sint,
            Format.B8G8R8Srgb => ImageFormat.B8G8R8Srgb,
            Format.R8G8B8A8Unorm => ImageFormat.R8G8B8A8Unorm,
            Format.R8G8B8A8SNorm => ImageFormat.R8G8B8A8SNorm,
            Format.R8G8B8A8Uscaled => ImageFormat.R8G8B8A8Uscaled,
            Format.R8G8B8A8Sscaled => ImageFormat.R8G8B8A8Sscaled,
            Format.R8G8B8A8Uint => ImageFormat.R8G8B8A8Uint,
            Format.R8G8B8A8Sint => ImageFormat.R8G8B8A8Sint,
            Format.R8G8B8A8Srgb => ImageFormat.R8G8B8A8Srgb,
            Format.B8G8R8A8Unorm => ImageFormat.B8G8R8A8Unorm,
            Format.B8G8R8A8SNorm => ImageFormat.B8G8R8A8SNorm,
            Format.B8G8R8A8Uscaled => ImageFormat.B8G8R8A8Uscaled,
            Format.B8G8R8A8Sscaled => ImageFormat.B8G8R8A8Sscaled,
            Format.B8G8R8A8Uint => ImageFormat.B8G8R8A8Uint,
            Format.B8G8R8A8Sint => ImageFormat.B8G8R8A8Sint,
            Format.B8G8R8A8Srgb => ImageFormat.B8G8R8A8Srgb,
            Format.A8B8G8R8UnormPack32 => ImageFormat.A8B8G8R8UnormPack32,
            Format.A8B8G8R8SNormPack32 => ImageFormat.A8B8G8R8SNormPack32,
            Format.A8B8G8R8UscaledPack32 => ImageFormat.A8B8G8R8UscaledPack32,
            Format.A8B8G8R8SscaledPack32 => ImageFormat.A8B8G8R8SscaledPack32,
            Format.A8B8G8R8UintPack32 => ImageFormat.A8B8G8R8UintPack32,
            Format.A8B8G8R8SintPack32 => ImageFormat.A8B8G8R8SintPack32,
            Format.A8B8G8R8SrgbPack32 => ImageFormat.A8B8G8R8SrgbPack32,
            Format.A2R10G10B10UnormPack32 => ImageFormat.A2R10G10B10UnormPack32,
            Format.A2R10G10B10SNormPack32 => ImageFormat.A2R10G10B10SNormPack32,
            Format.A2R10G10B10UscaledPack32 => ImageFormat.A2R10G10B10UscaledPack32,
            Format.A2R10G10B10SscaledPack32 => ImageFormat.A2R10G10B10SscaledPack32,
            Format.A2R10G10B10UintPack32 => ImageFormat.A2R10G10B10UintPack32,
            Format.A2R10G10B10SintPack32 => ImageFormat.A2R10G10B10SintPack32,
            Format.A2B10G10R10UnormPack32 => ImageFormat.A2B10G10R10UnormPack32,
            Format.A2B10G10R10SNormPack32 => ImageFormat.A2B10G10R10SNormPack32,
            Format.A2B10G10R10UscaledPack32 => ImageFormat.A2B10G10R10UscaledPack32,
            Format.A2B10G10R10SscaledPack32 => ImageFormat.A2B10G10R10SscaledPack32,
            Format.A2B10G10R10UintPack32 => ImageFormat.A2B10G10R10UintPack32,
            Format.A2B10G10R10SintPack32 => ImageFormat.A2B10G10R10SintPack32,
            Format.R16Unorm => ImageFormat.R16Unorm,
            Format.R16SNorm => ImageFormat.R16SNorm,
            Format.R16Uscaled => ImageFormat.R16Uscaled,
            Format.R16Sscaled => ImageFormat.R16Sscaled,
            Format.R16Uint => ImageFormat.R16Uint,
            Format.R16Sint => ImageFormat.R16Sint,
            Format.R16Sfloat => ImageFormat.R16Sfloat,
            Format.R16G16Unorm => ImageFormat.R16G16Unorm,
            Format.R16G16SNorm => ImageFormat.R16G16SNorm,
            Format.R16G16Uscaled => ImageFormat.R16G16Uscaled,
            Format.R16G16Sscaled => ImageFormat.R16G16Sscaled,
            Format.R16G16Uint => ImageFormat.R16G16Uint,
            Format.R16G16Sint => ImageFormat.R16G16Sint,
            Format.R16G16Sfloat => ImageFormat.R16G16Sfloat,
            Format.R16G16B16Unorm => ImageFormat.R16G16B16Unorm,
            Format.R16G16B16SNorm => ImageFormat.R16G16B16SNorm,
            Format.R16G16B16Uscaled => ImageFormat.R16G16B16Uscaled,
            Format.R16G16B16Sscaled => ImageFormat.R16G16B16Sscaled,
            Format.R16G16B16Uint => ImageFormat.R16G16B16Uint,
            Format.R16G16B16Sint => ImageFormat.R16G16B16Sint,
            Format.R16G16B16Sfloat => ImageFormat.R16G16B16Sfloat,
            Format.R16G16B16A16Unorm => ImageFormat.R16G16B16A16Unorm,
            Format.R16G16B16A16SNorm => ImageFormat.R16G16B16A16SNorm,
            Format.R16G16B16A16Uscaled => ImageFormat.R16G16B16A16Uscaled,
            Format.R16G16B16A16Sscaled => ImageFormat.R16G16B16A16Sscaled,
            Format.R16G16B16A16Uint => ImageFormat.R16G16B16A16Uint,
            Format.R16G16B16A16Sint => ImageFormat.R16G16B16A16Sint,
            Format.R16G16B16A16Sfloat => ImageFormat.R16G16B16A16Sfloat,
            Format.R32Uint => ImageFormat.R32Uint,
            Format.R32Sint => ImageFormat.R32Sint,
            Format.R32Sfloat => ImageFormat.R32Sfloat,
            Format.R32G32Uint => ImageFormat.R32G32Uint,
            Format.R32G32Sint => ImageFormat.R32G32Sint,
            Format.R32G32Sfloat => ImageFormat.R32G32Sfloat,
            Format.R32G32B32Uint => ImageFormat.R32G32B32Uint,
            Format.R32G32B32Sint => ImageFormat.R32G32B32Sint,
            Format.R32G32B32Sfloat => ImageFormat.R32G32B32Sfloat,
            Format.R32G32B32A32Uint => ImageFormat.R32G32B32A32Uint,
            Format.R32G32B32A32Sint => ImageFormat.R32G32B32A32Sint,
            Format.R32G32B32A32Sfloat => ImageFormat.R32G32B32A32Sfloat,
            Format.R64Uint => ImageFormat.R64Uint,
            Format.R64Sint => ImageFormat.R64Sint,
            Format.R64Sfloat => ImageFormat.R64Sfloat,
            Format.R64G64Uint => ImageFormat.R64G64Uint,
            Format.R64G64Sint => ImageFormat.R64G64Sint,
            Format.R64G64Sfloat => ImageFormat.R64G64Sfloat,
            Format.R64G64B64Uint => ImageFormat.R64G64B64Uint,
            Format.R64G64B64Sint => ImageFormat.R64G64B64Sint,
            Format.R64G64B64Sfloat => ImageFormat.R64G64B64Sfloat,
            Format.R64G64B64A64Uint => ImageFormat.R64G64B64A64Uint,
            Format.R64G64B64A64Sint => ImageFormat.R64G64B64A64Sint,
            Format.R64G64B64A64Sfloat => ImageFormat.R64G64B64A64Sfloat,
            Format.B10G11R11UfloatPack32 => ImageFormat.B10G11R11UfloatPack32,
            Format.E5B9G9R9UfloatPack32 => ImageFormat.E5B9G9R9UfloatPack32,
            Format.D16Unorm => ImageFormat.D16Unorm,
            Format.X8D24UnormPack32 => ImageFormat.X8D24UnormPack32,
            Format.D32Sfloat => ImageFormat.D32Sfloat,
            Format.S8Uint => ImageFormat.S8Uint,
            Format.D16UnormS8Uint => ImageFormat.D16UnormS8Uint,
            Format.D24UnormS8Uint => ImageFormat.D24UnormS8Uint,
            Format.D32SfloatS8Uint => ImageFormat.D32SfloatS8Uint,
            Format.BC1RgbUnormBlock => ImageFormat.BC1RgbUnormBlock,
            Format.BC1RgbSrgbBlock => ImageFormat.BC1RgbSrgbBlock,
            Format.BC1RgbaUnormBlock => ImageFormat.BC1RgbaUnormBlock,
            Format.BC1RgbaSrgbBlock => ImageFormat.BC1RgbaSrgbBlock,
            Format.BC2UnormBlock => ImageFormat.BC2UnormBlock,
            Format.BC2SrgbBlock => ImageFormat.BC2SrgbBlock,
            Format.BC3UnormBlock => ImageFormat.BC3UnormBlock,
            Format.BC3SrgbBlock => ImageFormat.BC3SrgbBlock,
            Format.BC4UnormBlock => ImageFormat.BC4UnormBlock,
            Format.BC4SNormBlock => ImageFormat.BC4SNormBlock,
            Format.BC5UnormBlock => ImageFormat.BC5UnormBlock,
            Format.BC5SNormBlock => ImageFormat.BC5SNormBlock,
            Format.BC6HUfloatBlock => ImageFormat.BC6HUfloatBlock,
            Format.BC6HSfloatBlock => ImageFormat.BC6HSfloatBlock,
            Format.BC7UnormBlock => ImageFormat.BC7UnormBlock,
            Format.BC7SrgbBlock => ImageFormat.BC7SrgbBlock,
            Format.Etc2R8G8B8UnormBlock => ImageFormat.Etc2R8G8B8UnormBlock,
            Format.Etc2R8G8B8SrgbBlock => ImageFormat.Etc2R8G8B8SrgbBlock,
            Format.Etc2R8G8B8A1UnormBlock => ImageFormat.Etc2R8G8B8A1UnormBlock,
            Format.Etc2R8G8B8A1SrgbBlock => ImageFormat.Etc2R8G8B8A1SrgbBlock,
            Format.Etc2R8G8B8A8UnormBlock => ImageFormat.Etc2R8G8B8A8UnormBlock,
            Format.Etc2R8G8B8A8SrgbBlock => ImageFormat.Etc2R8G8B8A8SrgbBlock,
            Format.EacR11UnormBlock => ImageFormat.EacR11UnormBlock,
            Format.EacR11SNormBlock => ImageFormat.EacR11SNormBlock,
            Format.EacR11G11UnormBlock => ImageFormat.EacR11G11UnormBlock,
            Format.EacR11G11SNormBlock => ImageFormat.EacR11G11SNormBlock,
            Format.Astc4x4UnormBlock => ImageFormat.Astc4x4UnormBlock,
            Format.Astc4x4SrgbBlock => ImageFormat.Astc4x4SrgbBlock,
            Format.Astc5x4UnormBlock => ImageFormat.Astc5x4UnormBlock,
            Format.Astc5x4SrgbBlock => ImageFormat.Astc5x4SrgbBlock,
            Format.Astc5x5UnormBlock => ImageFormat.Astc5x5UnormBlock,
            Format.Astc5x5SrgbBlock => ImageFormat.Astc5x5SrgbBlock,
            Format.Astc6x5UnormBlock => ImageFormat.Astc6x5UnormBlock,
            Format.Astc6x5SrgbBlock => ImageFormat.Astc6x5SrgbBlock,
            Format.Astc6x6UnormBlock => ImageFormat.Astc6x6UnormBlock,
            Format.Astc6x6SrgbBlock => ImageFormat.Astc6x6SrgbBlock,
            Format.Astc8x5UnormBlock => ImageFormat.Astc8x5UnormBlock,
            Format.Astc8x5SrgbBlock => ImageFormat.Astc8x5SrgbBlock,
            Format.Astc8x6UnormBlock => ImageFormat.Astc8x6UnormBlock,
            Format.Astc8x6SrgbBlock => ImageFormat.Astc8x6SrgbBlock,
            Format.Astc8x8UnormBlock => ImageFormat.Astc8x8UnormBlock,
            Format.Astc8x8SrgbBlock => ImageFormat.Astc8x8SrgbBlock,
            Format.Astc10x5UnormBlock => ImageFormat.Astc10x5UnormBlock,
            Format.Astc10x5SrgbBlock => ImageFormat.Astc10x5SrgbBlock,
            Format.Astc10x6UnormBlock => ImageFormat.Astc10x6UnormBlock,
            Format.Astc10x6SrgbBlock => ImageFormat.Astc10x6SrgbBlock,
            Format.Astc10x8UnormBlock => ImageFormat.Astc10x8UnormBlock,
            Format.Astc10x8SrgbBlock => ImageFormat.Astc10x8SrgbBlock,
            Format.Astc10x10UnormBlock => ImageFormat.Astc10x10UnormBlock,
            Format.Astc10x10SrgbBlock => ImageFormat.Astc10x10SrgbBlock,
            Format.Astc12x10UnormBlock => ImageFormat.Astc12x10UnormBlock,
            Format.Astc12x10SrgbBlock => ImageFormat.Astc12x10SrgbBlock,
            Format.Astc12x12UnormBlock => ImageFormat.Astc12x12UnormBlock,
            Format.Astc12x12SrgbBlock => ImageFormat.Astc12x12SrgbBlock,
            Format.Pvrtc12BppUnormBlockImg => ImageFormat.Pvrtc12BppUnormBlockImg,
            Format.Pvrtc14BppUnormBlockImg => ImageFormat.Pvrtc14BppUnormBlockImg,
            Format.Pvrtc22BppUnormBlockImg => ImageFormat.Pvrtc22BppUnormBlockImg,
            Format.Pvrtc24BppUnormBlockImg => ImageFormat.Pvrtc24BppUnormBlockImg,
            Format.Pvrtc12BppSrgbBlockImg => ImageFormat.Pvrtc12BppSrgbBlockImg,
            Format.Pvrtc14BppSrgbBlockImg => ImageFormat.Pvrtc14BppSrgbBlockImg,
            Format.Pvrtc22BppSrgbBlockImg => ImageFormat.Pvrtc22BppSrgbBlockImg,
            Format.Pvrtc24BppSrgbBlockImg => ImageFormat.Pvrtc24BppSrgbBlockImg,
            Format.R16G16S105NV => ImageFormat.R16G16S105NV,
            Format.A1B5G5R5UnormPack16Khr => ImageFormat.A1B5G5R5UnormPack16,
            Format.A8UnormKhr => ImageFormat.A8Unorm,
            Format.Astc4x4SfloatBlockExt => ImageFormat.Astc4x4SfloatBlock,
            Format.Astc5x4SfloatBlockExt => ImageFormat.Astc5x4SfloatBlock,
            Format.Astc5x5SfloatBlockExt => ImageFormat.Astc5x5SfloatBlock,
            Format.Astc6x5SfloatBlockExt => ImageFormat.Astc6x5SfloatBlock,
            Format.Astc6x6SfloatBlockExt => ImageFormat.Astc6x6SfloatBlock,
            Format.Astc8x5SfloatBlockExt => ImageFormat.Astc8x5SfloatBlock,
            Format.Astc8x6SfloatBlockExt => ImageFormat.Astc8x6SfloatBlock,
            Format.Astc8x8SfloatBlockExt => ImageFormat.Astc8x8SfloatBlock,
            Format.Astc10x5SfloatBlockExt => ImageFormat.Astc10x5SfloatBlock,
            Format.Astc10x6SfloatBlockExt => ImageFormat.Astc10x6SfloatBlock,
            Format.Astc10x8SfloatBlockExt => ImageFormat.Astc10x8SfloatBlock,
            Format.Astc10x10SfloatBlockExt => ImageFormat.Astc10x10SfloatBlock,
            Format.Astc12x10SfloatBlockExt => ImageFormat.Astc12x10SfloatBlock,
            Format.Astc12x12SfloatBlockExt => ImageFormat.Astc12x12SfloatBlock,
            Format.G8B8G8R8422UnormKhr => ImageFormat.G8B8G8R8422Unorm,
            Format.B8G8R8G8422UnormKhr => ImageFormat.B8G8R8G8422Unorm,
            Format.G8B8R83Plane420UnormKhr => ImageFormat.G8B8R83Plane420Unorm,
            Format.G8B8R82Plane420UnormKhr => ImageFormat.G8B8R82Plane420Unorm,
            Format.G8B8R83Plane422UnormKhr => ImageFormat.G8B8R83Plane422Unorm,
            Format.G8B8R82Plane422UnormKhr => ImageFormat.G8B8R82Plane422Unorm,
            Format.G8B8R83Plane444UnormKhr => ImageFormat.G8B8R83Plane444Unorm,
            Format.R10X6UnormPack16Khr => ImageFormat.R10X6UnormPack16,
            Format.R10X6G10X6Unorm2Pack16Khr => ImageFormat.R10X6G10X6Unorm2Pack16,
            Format.R10X6G10X6B10X6A10X6Unorm4Pack16Khr => ImageFormat.R10X6G10X6B10X6A10X6Unorm4Pack16,
            Format.G10X6B10X6G10X6R10X6422Unorm4Pack16Khr => ImageFormat.G10X6B10X6G10X6R10X6422Unorm4Pack16,
            Format.B10X6G10X6R10X6G10X6422Unorm4Pack16Khr => ImageFormat.B10X6G10X6R10X6G10X6422Unorm4Pack16,
            Format.G10X6B10X6R10X63Plane420Unorm3Pack16Khr => ImageFormat.G10X6B10X6R10X63Plane420Unorm3Pack16,
            Format.G10X6B10X6R10X62Plane420Unorm3Pack16Khr => ImageFormat.G10X6B10X6R10X62Plane420Unorm3Pack16,
            Format.G10X6B10X6R10X63Plane422Unorm3Pack16Khr => ImageFormat.G10X6B10X6R10X63Plane422Unorm3Pack16,
            Format.G10X6B10X6R10X62Plane422Unorm3Pack16Khr => ImageFormat.G10X6B10X6R10X62Plane422Unorm3Pack16,
            Format.G10X6B10X6R10X63Plane444Unorm3Pack16Khr => ImageFormat.G10X6B10X6R10X63Plane444Unorm3Pack16,
            Format.R12X4UnormPack16Khr => ImageFormat.R12X4UnormPack16,
            Format.R12X4G12X4Unorm2Pack16Khr => ImageFormat.R12X4G12X4Unorm2Pack16,
            Format.R12X4G12X4B12X4A12X4Unorm4Pack16Khr => ImageFormat.R12X4G12X4B12X4A12X4Unorm4Pack16,
            Format.G12X4B12X4G12X4R12X4422Unorm4Pack16Khr => ImageFormat.G12X4B12X4G12X4R12X4422Unorm4Pack16,
            Format.B12X4G12X4R12X4G12X4422Unorm4Pack16Khr => ImageFormat.B12X4G12X4R12X4G12X4422Unorm4Pack16,
            Format.G12X4B12X4R12X43Plane420Unorm3Pack16Khr => ImageFormat.G12X4B12X4R12X43Plane420Unorm3Pack16,
            Format.G12X4B12X4R12X42Plane420Unorm3Pack16Khr => ImageFormat.G12X4B12X4R12X42Plane420Unorm3Pack16,
            Format.G12X4B12X4R12X43Plane422Unorm3Pack16Khr => ImageFormat.G12X4B12X4R12X43Plane422Unorm3Pack16,
            Format.G12X4B12X4R12X42Plane422Unorm3Pack16Khr => ImageFormat.G12X4B12X4R12X42Plane422Unorm3Pack16,
            Format.G12X4B12X4R12X43Plane444Unorm3Pack16Khr => ImageFormat.G12X4B12X4R12X43Plane444Unorm3Pack16,
            Format.G16B16G16R16422UnormKhr => ImageFormat.G16B16G16R16422Unorm,
            Format.B16G16R16G16422UnormKhr => ImageFormat.B16G16R16G16422Unorm,
            Format.G16B16R163Plane420UnormKhr => ImageFormat.G16B16R163Plane420Unorm,
            Format.G16B16R162Plane420UnormKhr => ImageFormat.G16B16R162Plane420Unorm,
            Format.G16B16R163Plane422UnormKhr => ImageFormat.G16B16R163Plane422Unorm,
            Format.G16B16R162Plane422UnormKhr => ImageFormat.G16B16R162Plane422Unorm,
            Format.G16B16R163Plane444UnormKhr => ImageFormat.G16B16R163Plane444Unorm,
            Format.G8B8R82Plane444UnormExt => ImageFormat.G8B8R82Plane444Unorm,
            Format.G10X6B10X6R10X62Plane444Unorm3Pack16Ext => ImageFormat.G10X6B10X6R10X62Plane444Unorm3Pack16,
            Format.G12X4B12X4R12X42Plane444Unorm3Pack16Ext => ImageFormat.G12X4B12X4R12X42Plane444Unorm3Pack16,
            Format.G16B16R162Plane444UnormExt => ImageFormat.G16B16R162Plane444Unorm,
            Format.A4R4G4B4UnormPack16Ext => ImageFormat.A4R4G4B4UnormPack16,
            Format.A4B4G4R4UnormPack16Ext => ImageFormat.A4B4G4R4UnormPack16,
            _ => throw new ArgumentOutOfRangeException(nameof(@this), @this, null)
        };
    }

    public static Format ToVulkan(this ImageFormat @this)
    {
        return @this switch
        {
            ImageFormat.Undefined => Format.Undefined,
            ImageFormat.R4G4UnormPack8 => Format.R4G4UnormPack8,
            ImageFormat.R4G4B4A4UnormPack16 => Format.R4G4B4A4UnormPack16,
            ImageFormat.B4G4R4A4UnormPack16 => Format.B4G4R4A4UnormPack16,
            ImageFormat.R5G6B5UnormPack16 => Format.R5G6B5UnormPack16,
            ImageFormat.B5G6R5UnormPack16 => Format.B5G6R5UnormPack16,
            ImageFormat.R5G5B5A1UnormPack16 => Format.R5G5B5A1UnormPack16,
            ImageFormat.B5G5R5A1UnormPack16 => Format.B5G5R5A1UnormPack16,
            ImageFormat.A1R5G5B5UnormPack16 => Format.A1R5G5B5UnormPack16,
            ImageFormat.R8Unorm => Format.R8Unorm,
            ImageFormat.R8SNorm => Format.R8SNorm,
            ImageFormat.R8Uscaled => Format.R8Uscaled,
            ImageFormat.R8Sscaled => Format.R8Sscaled,
            ImageFormat.R8Uint => Format.R8Uint,
            ImageFormat.R8Sint => Format.R8Sint,
            ImageFormat.R8Srgb => Format.R8Srgb,
            ImageFormat.R8G8Unorm => Format.R8G8Unorm,
            ImageFormat.R8G8SNorm => Format.R8G8SNorm,
            ImageFormat.R8G8Uscaled => Format.R8G8Uscaled,
            ImageFormat.R8G8Sscaled => Format.R8G8Sscaled,
            ImageFormat.R8G8Uint => Format.R8G8Uint,
            ImageFormat.R8G8Sint => Format.R8G8Sint,
            ImageFormat.R8G8Srgb => Format.R8G8Srgb,
            ImageFormat.R8G8B8Unorm => Format.R8G8B8Unorm,
            ImageFormat.R8G8B8SNorm => Format.R8G8B8SNorm,
            ImageFormat.R8G8B8Uscaled => Format.R8G8B8Uscaled,
            ImageFormat.R8G8B8Sscaled => Format.R8G8B8Sscaled,
            ImageFormat.R8G8B8Uint => Format.R8G8B8Uint,
            ImageFormat.R8G8B8Sint => Format.R8G8B8Sint,
            ImageFormat.R8G8B8Srgb => Format.R8G8B8Srgb,
            ImageFormat.B8G8R8Unorm => Format.B8G8R8Unorm,
            ImageFormat.B8G8R8SNorm => Format.B8G8R8SNorm,
            ImageFormat.B8G8R8Uscaled => Format.B8G8R8Uscaled,
            ImageFormat.B8G8R8Sscaled => Format.B8G8R8Sscaled,
            ImageFormat.B8G8R8Uint => Format.B8G8R8Uint,
            ImageFormat.B8G8R8Sint => Format.B8G8R8Sint,
            ImageFormat.B8G8R8Srgb => Format.B8G8R8Srgb,
            ImageFormat.R8G8B8A8Unorm => Format.R8G8B8A8Unorm,
            ImageFormat.R8G8B8A8SNorm => Format.R8G8B8A8SNorm,
            ImageFormat.R8G8B8A8Uscaled => Format.R8G8B8A8Uscaled,
            ImageFormat.R8G8B8A8Sscaled => Format.R8G8B8A8Sscaled,
            ImageFormat.R8G8B8A8Uint => Format.R8G8B8A8Uint,
            ImageFormat.R8G8B8A8Sint => Format.R8G8B8A8Sint,
            ImageFormat.R8G8B8A8Srgb => Format.R8G8B8A8Srgb,
            ImageFormat.B8G8R8A8Unorm => Format.B8G8R8A8Unorm,
            ImageFormat.B8G8R8A8SNorm => Format.B8G8R8A8SNorm,
            ImageFormat.B8G8R8A8Uscaled => Format.B8G8R8A8Uscaled,
            ImageFormat.B8G8R8A8Sscaled => Format.B8G8R8A8Sscaled,
            ImageFormat.B8G8R8A8Uint => Format.B8G8R8A8Uint,
            ImageFormat.B8G8R8A8Sint => Format.B8G8R8A8Sint,
            ImageFormat.B8G8R8A8Srgb => Format.B8G8R8A8Srgb,
            ImageFormat.A8B8G8R8UnormPack32 => Format.A8B8G8R8UnormPack32,
            ImageFormat.A8B8G8R8SNormPack32 => Format.A8B8G8R8SNormPack32,
            ImageFormat.A8B8G8R8UscaledPack32 => Format.A8B8G8R8UscaledPack32,
            ImageFormat.A8B8G8R8SscaledPack32 => Format.A8B8G8R8SscaledPack32,
            ImageFormat.A8B8G8R8UintPack32 => Format.A8B8G8R8UintPack32,
            ImageFormat.A8B8G8R8SintPack32 => Format.A8B8G8R8SintPack32,
            ImageFormat.A8B8G8R8SrgbPack32 => Format.A8B8G8R8SrgbPack32,
            ImageFormat.A2R10G10B10UnormPack32 => Format.A2R10G10B10UnormPack32,
            ImageFormat.A2R10G10B10SNormPack32 => Format.A2R10G10B10SNormPack32,
            ImageFormat.A2R10G10B10UscaledPack32 => Format.A2R10G10B10UscaledPack32,
            ImageFormat.A2R10G10B10SscaledPack32 => Format.A2R10G10B10SscaledPack32,
            ImageFormat.A2R10G10B10UintPack32 => Format.A2R10G10B10UintPack32,
            ImageFormat.A2R10G10B10SintPack32 => Format.A2R10G10B10SintPack32,
            ImageFormat.A2B10G10R10UnormPack32 => Format.A2B10G10R10UnormPack32,
            ImageFormat.A2B10G10R10SNormPack32 => Format.A2B10G10R10SNormPack32,
            ImageFormat.A2B10G10R10UscaledPack32 => Format.A2B10G10R10UscaledPack32,
            ImageFormat.A2B10G10R10SscaledPack32 => Format.A2B10G10R10SscaledPack32,
            ImageFormat.A2B10G10R10UintPack32 => Format.A2B10G10R10UintPack32,
            ImageFormat.A2B10G10R10SintPack32 => Format.A2B10G10R10SintPack32,
            ImageFormat.R16Unorm => Format.R16Unorm,
            ImageFormat.R16SNorm => Format.R16SNorm,
            ImageFormat.R16Uscaled => Format.R16Uscaled,
            ImageFormat.R16Sscaled => Format.R16Sscaled,
            ImageFormat.R16Uint => Format.R16Uint,
            ImageFormat.R16Sint => Format.R16Sint,
            ImageFormat.R16Sfloat => Format.R16Sfloat,
            ImageFormat.R16G16Unorm => Format.R16G16Unorm,
            ImageFormat.R16G16SNorm => Format.R16G16SNorm,
            ImageFormat.R16G16Uscaled => Format.R16G16Uscaled,
            ImageFormat.R16G16Sscaled => Format.R16G16Sscaled,
            ImageFormat.R16G16Uint => Format.R16G16Uint,
            ImageFormat.R16G16Sint => Format.R16G16Sint,
            ImageFormat.R16G16Sfloat => Format.R16G16Sfloat,
            ImageFormat.R16G16B16Unorm => Format.R16G16B16Unorm,
            ImageFormat.R16G16B16SNorm => Format.R16G16B16SNorm,
            ImageFormat.R16G16B16Uscaled => Format.R16G16B16Uscaled,
            ImageFormat.R16G16B16Sscaled => Format.R16G16B16Sscaled,
            ImageFormat.R16G16B16Uint => Format.R16G16B16Uint,
            ImageFormat.R16G16B16Sint => Format.R16G16B16Sint,
            ImageFormat.R16G16B16Sfloat => Format.R16G16B16Sfloat,
            ImageFormat.R16G16B16A16Unorm => Format.R16G16B16A16Unorm,
            ImageFormat.R16G16B16A16SNorm => Format.R16G16B16A16SNorm,
            ImageFormat.R16G16B16A16Uscaled => Format.R16G16B16A16Uscaled,
            ImageFormat.R16G16B16A16Sscaled => Format.R16G16B16A16Sscaled,
            ImageFormat.R16G16B16A16Uint => Format.R16G16B16A16Uint,
            ImageFormat.R16G16B16A16Sint => Format.R16G16B16A16Sint,
            ImageFormat.R16G16B16A16Sfloat => Format.R16G16B16A16Sfloat,
            ImageFormat.R32Uint => Format.R32Uint,
            ImageFormat.R32Sint => Format.R32Sint,
            ImageFormat.R32Sfloat => Format.R32Sfloat,
            ImageFormat.R32G32Uint => Format.R32G32Uint,
            ImageFormat.R32G32Sint => Format.R32G32Sint,
            ImageFormat.R32G32Sfloat => Format.R32G32Sfloat,
            ImageFormat.R32G32B32Uint => Format.R32G32B32Uint,
            ImageFormat.R32G32B32Sint => Format.R32G32B32Sint,
            ImageFormat.R32G32B32Sfloat => Format.R32G32B32Sfloat,
            ImageFormat.R32G32B32A32Uint => Format.R32G32B32A32Uint,
            ImageFormat.R32G32B32A32Sint => Format.R32G32B32A32Sint,
            ImageFormat.R32G32B32A32Sfloat => Format.R32G32B32A32Sfloat,
            ImageFormat.R64Uint => Format.R64Uint,
            ImageFormat.R64Sint => Format.R64Sint,
            ImageFormat.R64Sfloat => Format.R64Sfloat,
            ImageFormat.R64G64Uint => Format.R64G64Uint,
            ImageFormat.R64G64Sint => Format.R64G64Sint,
            ImageFormat.R64G64Sfloat => Format.R64G64Sfloat,
            ImageFormat.R64G64B64Uint => Format.R64G64B64Uint,
            ImageFormat.R64G64B64Sint => Format.R64G64B64Sint,
            ImageFormat.R64G64B64Sfloat => Format.R64G64B64Sfloat,
            ImageFormat.R64G64B64A64Uint => Format.R64G64B64A64Uint,
            ImageFormat.R64G64B64A64Sint => Format.R64G64B64A64Sint,
            ImageFormat.R64G64B64A64Sfloat => Format.R64G64B64A64Sfloat,
            ImageFormat.B10G11R11UfloatPack32 => Format.B10G11R11UfloatPack32,
            ImageFormat.E5B9G9R9UfloatPack32 => Format.E5B9G9R9UfloatPack32,
            ImageFormat.D16Unorm => Format.D16Unorm,
            ImageFormat.X8D24UnormPack32 => Format.X8D24UnormPack32,
            ImageFormat.D32Sfloat => Format.D32Sfloat,
            ImageFormat.S8Uint => Format.S8Uint,
            ImageFormat.D16UnormS8Uint => Format.D16UnormS8Uint,
            ImageFormat.D24UnormS8Uint => Format.D24UnormS8Uint,
            ImageFormat.D32SfloatS8Uint => Format.D32SfloatS8Uint,
            ImageFormat.BC1RgbUnormBlock => Format.BC1RgbUnormBlock,
            ImageFormat.BC1RgbSrgbBlock => Format.BC1RgbSrgbBlock,
            ImageFormat.BC1RgbaUnormBlock => Format.BC1RgbaUnormBlock,
            ImageFormat.BC1RgbaSrgbBlock => Format.BC1RgbaSrgbBlock,
            ImageFormat.BC2UnormBlock => Format.BC2UnormBlock,
            ImageFormat.BC2SrgbBlock => Format.BC2SrgbBlock,
            ImageFormat.BC3UnormBlock => Format.BC3UnormBlock,
            ImageFormat.BC3SrgbBlock => Format.BC3SrgbBlock,
            ImageFormat.BC4UnormBlock => Format.BC4UnormBlock,
            ImageFormat.BC4SNormBlock => Format.BC4SNormBlock,
            ImageFormat.BC5UnormBlock => Format.BC5UnormBlock,
            ImageFormat.BC5SNormBlock => Format.BC5SNormBlock,
            ImageFormat.BC6HUfloatBlock => Format.BC6HUfloatBlock,
            ImageFormat.BC6HSfloatBlock => Format.BC6HSfloatBlock,
            ImageFormat.BC7UnormBlock => Format.BC7UnormBlock,
            ImageFormat.BC7SrgbBlock => Format.BC7SrgbBlock,
            ImageFormat.Etc2R8G8B8UnormBlock => Format.Etc2R8G8B8UnormBlock,
            ImageFormat.Etc2R8G8B8SrgbBlock => Format.Etc2R8G8B8SrgbBlock,
            ImageFormat.Etc2R8G8B8A1UnormBlock => Format.Etc2R8G8B8A1UnormBlock,
            ImageFormat.Etc2R8G8B8A1SrgbBlock => Format.Etc2R8G8B8A1SrgbBlock,
            ImageFormat.Etc2R8G8B8A8UnormBlock => Format.Etc2R8G8B8A8UnormBlock,
            ImageFormat.Etc2R8G8B8A8SrgbBlock => Format.Etc2R8G8B8A8SrgbBlock,
            ImageFormat.EacR11UnormBlock => Format.EacR11UnormBlock,
            ImageFormat.EacR11SNormBlock => Format.EacR11SNormBlock,
            ImageFormat.EacR11G11UnormBlock => Format.EacR11G11UnormBlock,
            ImageFormat.EacR11G11SNormBlock => Format.EacR11G11SNormBlock,
            ImageFormat.Astc4x4UnormBlock => Format.Astc4x4UnormBlock,
            ImageFormat.Astc4x4SrgbBlock => Format.Astc4x4SrgbBlock,
            ImageFormat.Astc5x4UnormBlock => Format.Astc5x4UnormBlock,
            ImageFormat.Astc5x4SrgbBlock => Format.Astc5x4SrgbBlock,
            ImageFormat.Astc5x5UnormBlock => Format.Astc5x5UnormBlock,
            ImageFormat.Astc5x5SrgbBlock => Format.Astc5x5SrgbBlock,
            ImageFormat.Astc6x5UnormBlock => Format.Astc6x5UnormBlock,
            ImageFormat.Astc6x5SrgbBlock => Format.Astc6x5SrgbBlock,
            ImageFormat.Astc6x6UnormBlock => Format.Astc6x6UnormBlock,
            ImageFormat.Astc6x6SrgbBlock => Format.Astc6x6SrgbBlock,
            ImageFormat.Astc8x5UnormBlock => Format.Astc8x5UnormBlock,
            ImageFormat.Astc8x5SrgbBlock => Format.Astc8x5SrgbBlock,
            ImageFormat.Astc8x6UnormBlock => Format.Astc8x6UnormBlock,
            ImageFormat.Astc8x6SrgbBlock => Format.Astc8x6SrgbBlock,
            ImageFormat.Astc8x8UnormBlock => Format.Astc8x8UnormBlock,
            ImageFormat.Astc8x8SrgbBlock => Format.Astc8x8SrgbBlock,
            ImageFormat.Astc10x5UnormBlock => Format.Astc10x5UnormBlock,
            ImageFormat.Astc10x5SrgbBlock => Format.Astc10x5SrgbBlock,
            ImageFormat.Astc10x6UnormBlock => Format.Astc10x6UnormBlock,
            ImageFormat.Astc10x6SrgbBlock => Format.Astc10x6SrgbBlock,
            ImageFormat.Astc10x8UnormBlock => Format.Astc10x8UnormBlock,
            ImageFormat.Astc10x8SrgbBlock => Format.Astc10x8SrgbBlock,
            ImageFormat.Astc10x10UnormBlock => Format.Astc10x10UnormBlock,
            ImageFormat.Astc10x10SrgbBlock => Format.Astc10x10SrgbBlock,
            ImageFormat.Astc12x10UnormBlock => Format.Astc12x10UnormBlock,
            ImageFormat.Astc12x10SrgbBlock => Format.Astc12x10SrgbBlock,
            ImageFormat.Astc12x12UnormBlock => Format.Astc12x12UnormBlock,
            ImageFormat.Astc12x12SrgbBlock => Format.Astc12x12SrgbBlock,
            ImageFormat.Pvrtc12BppUnormBlockImg => Format.Pvrtc12BppUnormBlockImg,
            ImageFormat.Pvrtc14BppUnormBlockImg => Format.Pvrtc14BppUnormBlockImg,
            ImageFormat.Pvrtc22BppUnormBlockImg => Format.Pvrtc22BppUnormBlockImg,
            ImageFormat.Pvrtc24BppUnormBlockImg => Format.Pvrtc24BppUnormBlockImg,
            ImageFormat.Pvrtc12BppSrgbBlockImg => Format.Pvrtc12BppSrgbBlockImg,
            ImageFormat.Pvrtc14BppSrgbBlockImg => Format.Pvrtc14BppSrgbBlockImg,
            ImageFormat.Pvrtc22BppSrgbBlockImg => Format.Pvrtc22BppSrgbBlockImg,
            ImageFormat.Pvrtc24BppSrgbBlockImg => Format.Pvrtc24BppSrgbBlockImg,
            ImageFormat.R16G16S105NV => Format.R16G16S105NV,
            ImageFormat.A1B5G5R5UnormPack16 => Format.A1B5G5R5UnormPack16Khr,
            ImageFormat.A8Unorm => Format.A8UnormKhr,
            ImageFormat.Astc4x4SfloatBlock => Format.Astc4x4SfloatBlockExt,
            ImageFormat.Astc5x4SfloatBlock => Format.Astc5x4SfloatBlockExt,
            ImageFormat.Astc5x5SfloatBlock => Format.Astc5x5SfloatBlockExt,
            ImageFormat.Astc6x5SfloatBlock => Format.Astc6x5SfloatBlockExt,
            ImageFormat.Astc6x6SfloatBlock => Format.Astc6x6SfloatBlockExt,
            ImageFormat.Astc8x5SfloatBlock => Format.Astc8x5SfloatBlockExt,
            ImageFormat.Astc8x6SfloatBlock => Format.Astc8x6SfloatBlockExt,
            ImageFormat.Astc8x8SfloatBlock => Format.Astc8x8SfloatBlockExt,
            ImageFormat.Astc10x5SfloatBlock => Format.Astc10x5SfloatBlockExt,
            ImageFormat.Astc10x6SfloatBlock => Format.Astc10x6SfloatBlockExt,
            ImageFormat.Astc10x8SfloatBlock => Format.Astc10x8SfloatBlockExt,
            ImageFormat.Astc10x10SfloatBlock => Format.Astc10x10SfloatBlockExt,
            ImageFormat.Astc12x10SfloatBlock => Format.Astc12x10SfloatBlockExt,
            ImageFormat.Astc12x12SfloatBlock => Format.Astc12x12SfloatBlockExt,
            ImageFormat.G8B8G8R8422Unorm => Format.G8B8G8R8422UnormKhr,
            ImageFormat.B8G8R8G8422Unorm => Format.B8G8R8G8422UnormKhr,
            ImageFormat.G8B8R83Plane420Unorm => Format.G8B8R83Plane420UnormKhr,
            ImageFormat.G8B8R82Plane420Unorm => Format.G8B8R82Plane420UnormKhr,
            ImageFormat.G8B8R83Plane422Unorm => Format.G8B8R83Plane422UnormKhr,
            ImageFormat.G8B8R82Plane422Unorm => Format.G8B8R82Plane422UnormKhr,
            ImageFormat.G8B8R83Plane444Unorm => Format.G8B8R83Plane444UnormKhr,
            ImageFormat.R10X6UnormPack16 => Format.R10X6UnormPack16Khr,
            ImageFormat.R10X6G10X6Unorm2Pack16 => Format.R10X6G10X6Unorm2Pack16Khr,
            ImageFormat.R10X6G10X6B10X6A10X6Unorm4Pack16 => Format.R10X6G10X6B10X6A10X6Unorm4Pack16Khr,
            ImageFormat.G10X6B10X6G10X6R10X6422Unorm4Pack16 => Format.G10X6B10X6G10X6R10X6422Unorm4Pack16Khr,
            ImageFormat.B10X6G10X6R10X6G10X6422Unorm4Pack16 => Format.B10X6G10X6R10X6G10X6422Unorm4Pack16Khr,
            ImageFormat.G10X6B10X6R10X63Plane420Unorm3Pack16 => Format.G10X6B10X6R10X63Plane420Unorm3Pack16Khr,
            ImageFormat.G10X6B10X6R10X62Plane420Unorm3Pack16 => Format.G10X6B10X6R10X62Plane420Unorm3Pack16Khr,
            ImageFormat.G10X6B10X6R10X63Plane422Unorm3Pack16 => Format.G10X6B10X6R10X63Plane422Unorm3Pack16Khr,
            ImageFormat.G10X6B10X6R10X62Plane422Unorm3Pack16 => Format.G10X6B10X6R10X62Plane422Unorm3Pack16Khr,
            ImageFormat.G10X6B10X6R10X63Plane444Unorm3Pack16 => Format.G10X6B10X6R10X63Plane444Unorm3Pack16Khr,
            ImageFormat.R12X4UnormPack16 => Format.R12X4UnormPack16Khr,
            ImageFormat.R12X4G12X4Unorm2Pack16 => Format.R12X4G12X4Unorm2Pack16Khr,
            ImageFormat.R12X4G12X4B12X4A12X4Unorm4Pack16 => Format.R12X4G12X4B12X4A12X4Unorm4Pack16Khr,
            ImageFormat.G12X4B12X4G12X4R12X4422Unorm4Pack16 => Format.G12X4B12X4G12X4R12X4422Unorm4Pack16Khr,
            ImageFormat.B12X4G12X4R12X4G12X4422Unorm4Pack16 => Format.B12X4G12X4R12X4G12X4422Unorm4Pack16Khr,
            ImageFormat.G12X4B12X4R12X43Plane420Unorm3Pack16 => Format.G12X4B12X4R12X43Plane420Unorm3Pack16Khr,
            ImageFormat.G12X4B12X4R12X42Plane420Unorm3Pack16 => Format.G12X4B12X4R12X42Plane420Unorm3Pack16Khr,
            ImageFormat.G12X4B12X4R12X43Plane422Unorm3Pack16 => Format.G12X4B12X4R12X43Plane422Unorm3Pack16Khr,
            ImageFormat.G12X4B12X4R12X42Plane422Unorm3Pack16 => Format.G12X4B12X4R12X42Plane422Unorm3Pack16Khr,
            ImageFormat.G12X4B12X4R12X43Plane444Unorm3Pack16 => Format.G12X4B12X4R12X43Plane444Unorm3Pack16Khr,
            ImageFormat.G16B16G16R16422Unorm => Format.G16B16G16R16422UnormKhr,
            ImageFormat.B16G16R16G16422Unorm => Format.B16G16R16G16422UnormKhr,
            ImageFormat.G16B16R163Plane420Unorm => Format.G16B16R163Plane420UnormKhr,
            ImageFormat.G16B16R162Plane420Unorm => Format.G16B16R162Plane420UnormKhr,
            ImageFormat.G16B16R163Plane422Unorm => Format.G16B16R163Plane422UnormKhr,
            ImageFormat.G16B16R162Plane422Unorm => Format.G16B16R162Plane422UnormKhr,
            ImageFormat.G16B16R163Plane444Unorm => Format.G16B16R163Plane444UnormKhr,
            ImageFormat.G8B8R82Plane444Unorm => Format.G8B8R82Plane444UnormExt,
            ImageFormat.G10X6B10X6R10X62Plane444Unorm3Pack16 => Format.G10X6B10X6R10X62Plane444Unorm3Pack16Ext,
            ImageFormat.G12X4B12X4R12X42Plane444Unorm3Pack16 => Format.G12X4B12X4R12X42Plane444Unorm3Pack16Ext,
            ImageFormat.G16B16R162Plane444Unorm => Format.G16B16R162Plane444UnormExt,
            ImageFormat.A4R4G4B4UnormPack16 => Format.A4R4G4B4UnormPack16Ext,
            ImageFormat.A4B4G4R4UnormPack16 => Format.A4B4G4R4UnormPack16Ext,
            _ => throw new ArgumentOutOfRangeException(nameof(@this), @this, null)
        };
    }
}