using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tmac.Frameworks.Translation.Converter;
using Tmac.Frameworks.Translation.Translate;

namespace DLog.Common.Helper
{
    public static class TranslateHelper
    {
        /// <summary>
        /// 将输入内容字对字转换为简体中文，不进行术语替换，也不进行分词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string BasicToSimplified(string input)
        {
            if (null == input)
            {
                throw new ArgumentNullException("input");
            }
            return BasicChineseConverter.Convert(input, TranslationDirection.TraditionalToSimplified);
        }

        /// <summary>
        /// 将输入内容字对字转换为繁体中文，不进行术语替换，也不进行分词
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string BasicToTraditional(string input)
        {
            if (null == input)
            {
                throw new ArgumentNullException("input");
            }
            return BasicChineseConverter.Convert(input, TranslationDirection.SimplifiedToTraditional);
        }
    }
}
