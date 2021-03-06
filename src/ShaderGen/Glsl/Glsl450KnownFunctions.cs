﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ShaderGen.Hlsl;

namespace ShaderGen.Glsl
{
    public static class Glsl450KnownFunctions
    {
        private static Dictionary<string, TypeInvocationTranslator> s_mappings = GetMappings();

        private static Dictionary<string, TypeInvocationTranslator> GetMappings()
        {
            Dictionary<string, TypeInvocationTranslator> ret = new Dictionary<string, TypeInvocationTranslator>();

            Dictionary<string, InvocationTranslator> builtinMappings = new Dictionary<string, InvocationTranslator>()
            {
                { "Abs", SimpleNameTranslator("abs") },
                { "Pow", SimpleNameTranslator("pow") },
                { "Acos", SimpleNameTranslator("acos") },
                { "Cos", SimpleNameTranslator("cos") },
                { "Ddx", SimpleNameTranslator("dFdx") },
                { "DdxFine", SimpleNameTranslator("dFdxFine") },
                { "Ddy", SimpleNameTranslator("dFdy") },
                { "DdyFine", SimpleNameTranslator("dFdyFine") },
                { "Floor", SimpleNameTranslator("floor") },
                { "Frac", SimpleNameTranslator("fract") },
                { "Lerp", SimpleNameTranslator("mix") },
                { "Sin", SimpleNameTranslator("sin") },
                { "SmoothStep", SimpleNameTranslator("smoothstep") },
                { "Tan", SimpleNameTranslator("tan") },
                { "Clamp", SimpleNameTranslator("clamp") },
                { "Mod", SimpleNameTranslator("mod") },
                { "Mul", MatrixMul },
                { "Sample", Sample },
                { "SampleGrad", SampleGrad },
                { "SampleComparisonLevelZero", SampleComparisonLevelZero },
                { "Load", Load },
                { "Store", Store },
                { "Discard", Discard },
                { "Saturate", Saturate },
                { nameof(ShaderBuiltins.ClipToTextureCoordinates), ClipToTextureCoordinates },
                { "VertexID", VertexID },
                { "InstanceID", InstanceID },
                { "DispatchThreadID", DispatchThreadID },
                { "GroupThreadID", GroupThreadID },
                { "IsFrontFace", IsFrontFace },
            };
            ret.Add("ShaderGen.ShaderBuiltins", new DictionaryTypeInvocationTranslator(builtinMappings));

            Dictionary<string, InvocationTranslator> v2Mappings = new Dictionary<string, InvocationTranslator>()
            {
                { "Abs", SimpleNameTranslator("abs") },
                { "Add", BinaryOpTranslator("+") },
                { "Clamp", SimpleNameTranslator("clamp") },
                { "Cos", SimpleNameTranslator("cos") },
                { "Distance", SimpleNameTranslator("distance") },
                { "DistanceSquared", DistanceSquared },
                { "Divide", BinaryOpTranslator("/") },
                { "Dot", SimpleNameTranslator("dot") },
                { "Lerp", SimpleNameTranslator("mix") },
                { "Max", SimpleNameTranslator("max") },
                { "Min", SimpleNameTranslator("min") },
                { "Multiply", BinaryOpTranslator("*") },
                { "Negate", Negate },
                { "Normalize", SimpleNameTranslator("normalize") },
                { "Reflect", SimpleNameTranslator("reflect") },
                { "Sin", SimpleNameTranslator("sin") },
                { "SquareRoot", SimpleNameTranslator("sqrt") },
                { "Subtract", BinaryOpTranslator("-") },
                { "Length", SimpleNameTranslator("length") },
                { "LengthSquared", LengthSquared },
                { "ctor", VectorCtor },
                { "Zero", VectorStaticAccessor },
                { "One", VectorStaticAccessor },
                { "UnitX", VectorStaticAccessor },
                { "UnitY", VectorStaticAccessor },
                { "Transform", Vector2Transform },
            };
            ret.Add("System.Numerics.Vector2", new DictionaryTypeInvocationTranslator(v2Mappings));

            Dictionary<string, InvocationTranslator> v3Mappings = new Dictionary<string, InvocationTranslator>()
            {
                { "Abs", SimpleNameTranslator("abs") },
                { "Add", BinaryOpTranslator("+") },
                { "Clamp", SimpleNameTranslator("clamp") },
                { "Cos", SimpleNameTranslator("cos") },
                { "Cross", SimpleNameTranslator("cross") },
                { "Distance", SimpleNameTranslator("distance") },
                { "DistanceSquared", DistanceSquared },
                { "Divide", BinaryOpTranslator("/") },
                { "Dot", SimpleNameTranslator("dot") },
                { "Lerp", SimpleNameTranslator("mix") },
                { "Max", SimpleNameTranslator("max") },
                { "Min", SimpleNameTranslator("min") },
                { "Multiply", BinaryOpTranslator("*") },
                { "Negate", Negate },
                { "Normalize", SimpleNameTranslator("normalize") },
                { "Reflect", SimpleNameTranslator("reflect") },
                { "Sin", SimpleNameTranslator("sin") },
                { "SquareRoot", SimpleNameTranslator("sqrt") },
                { "Subtract", BinaryOpTranslator("-") },
                { "Length", SimpleNameTranslator("length") },
                { "LengthSquared", LengthSquared },
                { "ctor", VectorCtor },
                { "Zero", VectorStaticAccessor },
                { "One", VectorStaticAccessor },
                { "UnitX", VectorStaticAccessor },
                { "UnitY", VectorStaticAccessor },
                { "UnitZ", VectorStaticAccessor },
                { "Transform", Vector3Transform },
            };
            ret.Add("System.Numerics.Vector3", new DictionaryTypeInvocationTranslator(v3Mappings));

            Dictionary<string, InvocationTranslator> v4Mappings = new Dictionary<string, InvocationTranslator>()
            {
                { "Abs", SimpleNameTranslator("abs") },
                { "Add", BinaryOpTranslator("+") },
                { "Clamp", SimpleNameTranslator("clamp") },
                { "Cos", SimpleNameTranslator("cos") },
                { "Distance", SimpleNameTranslator("distance") },
                { "DistanceSquared", DistanceSquared },
                { "Divide", BinaryOpTranslator("/") },
                { "Dot", SimpleNameTranslator("dot") },
                { "Lerp", SimpleNameTranslator("mix") },
                { "Max", SimpleNameTranslator("max") },
                { "Min", SimpleNameTranslator("min") },
                { "Multiply", BinaryOpTranslator("*") },
                { "Negate", Negate },
                { "Normalize", SimpleNameTranslator("normalize") },
                { "Reflect", SimpleNameTranslator("reflect") },
                { "Sin", SimpleNameTranslator("sin") },
                { "SquareRoot", SimpleNameTranslator("sqrt") },
                { "Subtract", BinaryOpTranslator("-") },
                { "Length", SimpleNameTranslator("length") },
                { "LengthSquared", LengthSquared },
                { "ctor", VectorCtor },
                { "Zero", VectorStaticAccessor },
                { "One", VectorStaticAccessor },
                { "UnitX", VectorStaticAccessor },
                { "UnitY", VectorStaticAccessor },
                { "UnitZ", VectorStaticAccessor },
                { "UnitW", VectorStaticAccessor },
                { "Transform", Vector4Transform },
            };
            ret.Add("System.Numerics.Vector4", new DictionaryTypeInvocationTranslator(v4Mappings));

            Dictionary<string, InvocationTranslator> u2Mappings = new Dictionary<string, InvocationTranslator>()
            {
                { "ctor", VectorCtor },
            };
            ret.Add("ShaderGen.UInt2", new DictionaryTypeInvocationTranslator(u2Mappings));
            ret.Add("ShaderGen.Int2", new DictionaryTypeInvocationTranslator(u2Mappings));

            Dictionary<string, InvocationTranslator> m4x4Mappings = new Dictionary<string, InvocationTranslator>()
            {
                { "ctor", MatrixCtor }
            };
            ret.Add("System.Numerics.Matrix4x4", new DictionaryTypeInvocationTranslator(m4x4Mappings));

            Dictionary<string, InvocationTranslator> mathfMappings = new Dictionary<string, InvocationTranslator>()
            {
                { "Cos", SimpleNameTranslator("cos") },
                { "Max", SimpleNameTranslator("max") },
                { "Min", SimpleNameTranslator("min") },
                { "Pow", SimpleNameTranslator("pow") },
                { "Sin", SimpleNameTranslator("sin") },
                { "Sqrt", SimpleNameTranslator("sqrt") },
            };
            ret.Add("System.MathF", new DictionaryTypeInvocationTranslator(mathfMappings));

            ret.Add("ShaderGen.ShaderSwizzle", new SwizzleTranslator());

            Dictionary<string, InvocationTranslator> vectorExtensionMappings = new Dictionary<string, InvocationTranslator>()
            {
                { nameof(VectorExtensions.GetComponent), VectorGetComponent },
                { nameof(VectorExtensions.SetComponent), VectorSetComponent },
            };
            ret.Add("ShaderGen.VectorExtensions", new DictionaryTypeInvocationTranslator(vectorExtensionMappings));

            return ret;
        }

        private static string MatrixCtor(string typeName, string methodName, InvocationParameterInfo[] p)
        {
            string paramList = string.Join(", ",
                p[0].Identifier, p[4].Identifier, p[8].Identifier, p[12].Identifier,
                p[1].Identifier, p[5].Identifier, p[9].Identifier, p[13].Identifier,
                p[2].Identifier, p[6].Identifier, p[10].Identifier, p[14].Identifier,
                p[3].Identifier, p[7].Identifier, p[11].Identifier, p[15].Identifier);

            return $"mat4({paramList})";
        }

        private static string VectorGetComponent(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return $"{parameters[0].Identifier}[{parameters[1].Identifier}]";
        }

        private static string VectorSetComponent(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return $"{parameters[0].Identifier}[{parameters[1].Identifier}] = {parameters[2].Identifier}";
        }

        public static string TranslateInvocation(string type, string method, InvocationParameterInfo[] parameters)
        {
            if (s_mappings.TryGetValue(type, out var dict))
            {
                if (dict.GetTranslator(method, parameters, out var mappedValue))
                {
                    return mappedValue(type, method, parameters);
                }
            }

            throw new ShaderGenerationException($"Reference to unknown function: {type}.{method}");
        }

        private static InvocationTranslator SimpleNameTranslator(string nameTarget)
        {
            return (type, method, parameters) =>
            {
                return $"{nameTarget}({InvocationParameterInfo.GetInvocationParameterList(parameters)})";
            };
        }

        private static string LengthSquared(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return $"dot({parameters[0].Identifier}, {parameters[0].Identifier})";
        }

        private static string Negate(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return $"-{parameters[0].Identifier}";
        }

        private static string DistanceSquared(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return $"dot({parameters[0].Identifier} - {parameters[1].Identifier}, {parameters[0].Identifier} - {parameters[1].Identifier})";
        }

        private static InvocationTranslator BinaryOpTranslator(string op)
        {
            return (type, method, parameters) =>
            {
                return $"{parameters[0].Identifier} {op} {parameters[1].Identifier}";
            };
        }

        private static string MatrixMul(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return $"{parameters[0].Identifier} * {parameters[1].Identifier}";
        }

        private static string Sample(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            if (parameters[0].FullTypeName == "ShaderGen.Texture2DResource")
            {
                return $"texture(sampler2D({parameters[0].Identifier}, {parameters[1].Identifier}), {parameters[2].Identifier})";
            }
            else if (parameters[0].FullTypeName == "ShaderGen.Texture2DArrayResource")
            {
                return $"texture(sampler2DArray({parameters[0].Identifier}, {parameters[1].Identifier}), vec3({parameters[2].Identifier}, {parameters[3].Identifier}))";
            }
            else if (parameters[0].FullTypeName == "ShaderGen.TextureCubeResource")
            {
                return $"texture(samplerCube({parameters[0].Identifier}, {parameters[1].Identifier}), {parameters[2].Identifier})";
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static string SampleGrad(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            if (parameters[0].FullTypeName == "ShaderGen.Texture2DResource")
            {
                return $"textureGrad(sampler2D({parameters[0].Identifier}, {parameters[1].Identifier}), {parameters[2].Identifier}, {parameters[3].Identifier}, {parameters[4].Identifier})";
            }
            else if (parameters[0].FullTypeName == "ShaderGen.Texture2DArrayResource")
            {
                return $"textureGrad(sampler2DArray({parameters[0].Identifier}, {parameters[1].Identifier}), vec3({parameters[2].Identifier}, {parameters[3].Identifier}), {parameters[4].Identifier}, {parameters[5].Identifier})";
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static string SampleComparisonLevelZero(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            if (parameters[0].FullTypeName == "ShaderGen.DepthTexture2DResource")
            {
                return $"textureLod(sampler2DShadow({parameters[0].Identifier}, {parameters[1].Identifier}), vec3({parameters[2].Identifier}, {parameters[3].Identifier}), 0.0)";
            }
            else if (parameters[0].FullTypeName == "ShaderGen.DepthTexture2DArrayResource")
            {
                // See https://github.com/KhronosGroup/SPIRV-Cross/issues/207 for why we need to use textureGrad here instead of textureLod.
                return $"textureGrad(sampler2DArrayShadow({parameters[0].Identifier}, {parameters[1].Identifier}), vec4({parameters[2].Identifier}, {parameters[3].Identifier}, {parameters[4].Identifier}), vec2(0.0), vec2(0.0))";
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static string Load(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            if (parameters[0].FullTypeName.Contains("RWTexture2D"))
            {
                return $"imageLoad({parameters[0].Identifier}, ivec2({parameters[1].Identifier}))";
            }
            else if (parameters[0].FullTypeName == "ShaderGen.Texture2DResource")
            {
                return $"texelFetch(sampler2D({parameters[0].Identifier}, {parameters[1].Identifier}), ivec2({parameters[2].Identifier}), {parameters[3].Identifier})";
            }
            else
            {
                return $"texelFetch(sampler2DMS({parameters[0].Identifier}, {parameters[1].Identifier}), ivec2({parameters[2].Identifier}), {parameters[3].Identifier})";
            }
        }

        private static string Store(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return $"imageStore({parameters[0].Identifier}, ivec2({parameters[1].Identifier}), {parameters[2].Identifier})";
        }

        private static string Discard(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return $"discard;";
        }

        private static string Saturate(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            if (parameters.Length == 1)
            {
                return $"clamp({parameters[0].Identifier}, 0, 1)";
            }
            else
            {
                throw new ShaderGenerationException("Unhandled number of arguments to ShaderBuiltins.Discard.");
            }
        }

        private static string ClipToTextureCoordinates(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            string target = parameters[0].Identifier;
            return $"vec2(({target}.x / {target}.w) / 2 + 0.5, ({target}.y / {target}.w) / -2 + 0.5)";
        }

        private static string VertexID(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return "gl_VertexIndex";
        }

        private static string InstanceID(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return "gl_InstanceIndex";
        }

        private static string DispatchThreadID(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return "gl_GlobalInvocationID";
        }

        private static string GroupThreadID(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return "gl_LocalInvocationID";
        }

        private static string IsFrontFace(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return "gl_FrontFacing";
        }

        private static string VectorCtor(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            GetVectorTypeInfo(typeName, out string shaderType, out int elementCount);
            string paramList;
            if (parameters.Length == 0)
            {
                paramList = string.Join(", ", Enumerable.Repeat("0", elementCount));
            }
            else if (parameters.Length == 1)
            {
                paramList = string.Join(", ", Enumerable.Repeat(parameters[0].Identifier, elementCount));
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < parameters.Length; i++)
                {
                    InvocationParameterInfo ipi = parameters[i];
                    sb.Append(ipi.Identifier);

                    if (i != parameters.Length - 1)
                    {
                        sb.Append(", ");
                    }
                }

                paramList = sb.ToString();
            }

            return $"{shaderType}({paramList})";
        }

        private static string VectorStaticAccessor(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            Debug.Assert(parameters.Length == 0);
            GetVectorTypeInfo(typeName, out string shaderType, out int elementCount);
            if (methodName == "Zero")
            {
                return $"{shaderType}({string.Join(", ", Enumerable.Repeat("0", elementCount))})";
            }
            else if (methodName == "One")
            {
                return $"{shaderType}({string.Join(", ", Enumerable.Repeat("1", elementCount))})";
            }
            else if (methodName == "UnitX")
            {
                string paramList;
                if (elementCount == 2) { paramList = "1, 0"; }
                else if (elementCount == 3) { paramList = "1, 0, 0"; }
                else { paramList = "1, 0, 0, 0"; }
                return $"{shaderType}({paramList})";
            }
            else if (methodName == "UnitY")
            {
                string paramList;
                if (elementCount == 2) { paramList = "0, 1"; }
                else if (elementCount == 3) { paramList = "0, 1, 0"; }
                else { paramList = "0, 1, 0, 0"; }
                return $"{shaderType}({paramList})";
            }
            else if (methodName == "UnitZ")
            {
                string paramList;
                if (elementCount == 3) { paramList = "0, 0, 1"; }
                else { paramList = "0, 0, 1, 0"; }
                return $"{shaderType}({paramList})";
            }
            else if (methodName == "UnitW")
            {
                return $"{shaderType}(0, 0, 0, 1)";
            }
            else
            {
                Debug.Fail("Invalid static vector accessor: " + methodName);
                return null;
            }
        }

        private static string Vector2Transform(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return $"({parameters[1].Identifier} * vec4({parameters[0].Identifier}, 0, 1)).xy";
        }

        private static string Vector3Transform(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            return $"({parameters[1].Identifier} * vec4({parameters[0].Identifier}, 1)).xyz";
        }

        private static string Vector4Transform(string typeName, string methodName, InvocationParameterInfo[] parameters)
        {
            string vecParam;
            if (parameters[0].FullTypeName == "System.Numerics.Vector2")
            {
                vecParam = $"vec4({parameters[0].Identifier}, 0, 1)";
            }
            else if (parameters[0].FullTypeName == "System.Numerics.Vector3")
            {
                vecParam = $"vec4({parameters[0].Identifier}, 1)";
            }
            else
            {
                vecParam = parameters[0].Identifier;
            }

            return $"{parameters[1].Identifier} * {vecParam}";
        }

        private static void GetVectorTypeInfo(string name, out string shaderType, out int elementCount)
        {
            if (name == "System.Numerics.Vector2") { shaderType = "vec2"; elementCount = 2; }
            else if (name == "System.Numerics.Vector3") { shaderType = "vec3"; elementCount = 3; }
            else if (name == "System.Numerics.Vector4") { shaderType = "vec4"; elementCount = 4; }
            else if (name == "ShaderGen.Int2") { shaderType = "ivec2"; elementCount = 2; }
            else if (name == "ShaderGen.UInt2") { shaderType = "uvec2"; elementCount = 2; }
            else { throw new ShaderGenerationException("VectorCtor translator was called on an invalid type: " + name); }
        }
    }
}
