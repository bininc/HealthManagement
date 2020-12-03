using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Services.Description;
using System.Xml.Serialization;

namespace TmoCommon
{
    public static class WebServiceHelper
    {
        private static string GetClassName(string url)
        {
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }

        /// <summary>
        /// 根据WebService的URL，生成一个本地的dll,放在C盘下面，例如：C:|DBMS_WebService.dll
        /// 创建人：程媛媛 创建时间：2010-6-21
        /// </summary>
        /// <param name="url">WebService的UR</param>
        /// <returns></returns>
        public static string CreateWebServiceDLL(string url)
        {
            string @namespace = "WebServiceDLL";
            string classname = GetClassName(url);
            // 1. 使用 WebClient 下载 WSDL 信息。
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(url + "?WSDL");
            // 2. 创建和格式化 WSDL 文档。
            ServiceDescription description = ServiceDescription.Read(stream);
            // 3. 创建客户端代理代理类。
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.ProtocolName = "Soap"; // 指定访问协议。
            importer.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;
            importer.AddServiceDescription(description, null, null); // 添加 WSDL 文档。
            // 4. 使用 CodeDom 编译客户端代理类。
            CodeNamespace nmspace = new CodeNamespace(@namespace); // 为代理类添加命名空间，缺省为全局空间。
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);
            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.OutputAssembly = string.Format("WebService_{0}.dll", classname);  // 可以指定你所需的任何文件名。
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");
            CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
            if (result.Errors.HasErrors)
            {
                // 显示编译错误信息
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in result.Errors)
                {
                    sb.Append(ce);
                    sb.Append(Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }
            return parameter.OutputAssembly;
        }
    }
}
